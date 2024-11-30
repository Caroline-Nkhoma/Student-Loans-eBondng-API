using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentLoanseBonderAPI.APIBehavior;
using StudentLoanseBonderAPI.Filters;
using StudentLoanseBonderAPI.Services;
using Supabase;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace StudentLoanseBonderAPI;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var configuration = builder.Configuration;
		// Add services to the container.

		builder.Services.AddAutoMapper(typeof(Program));
		builder.Services.AddScoped<Supabase.Client>(_ =>
			new Supabase.Client(
				supabaseUrl: configuration.GetValue<string>("SupabaseURL"),
				supabaseKey: configuration.GetValue<string>("SupabaseKey"),
				new SupabaseOptions
				{
					AutoConnectRealtime = true,
					AutoRefreshToken = true,
				}
			)
		);
		builder.Services.AddScoped<SmtpClient>(_ => new SmtpClient(configuration.GetValue<string>("EmailService:Host"), configuration.GetValue<int>("EmailService:Port"))
		{
			EnableSsl = true,
			UseDefaultCredentials = false,
			Credentials = new NetworkCredential(configuration.GetValue<string>("EmailService:SenderEmailAddress"), configuration.GetValue<string>("EmailService:SenderEmailPassword")),
		});

		builder.Services.AddScoped<IFileStorageService, SupabaseStorageService>();
		builder.Services.AddScoped<IEmailService, SMTPServerEmailService>();

		builder.Services.AddHttpContextAccessor();

		builder.Services.AddScoped<AccountService>();
		builder.Services.AddScoped<UserService>();
		builder.Services.AddScoped<StudentService>();
		builder.Services.AddScoped<InstitutionService>();
		builder.Services.AddScoped<BondingFormService>();
		builder.Services.AddScoped<CommentService>();
		builder.Services.AddScoped<BondingPeriodService>();
		builder.Services.AddScoped<BondingStatusService>();

		builder.Services.AddControllers(options =>
		{
			options.Filters.Add(typeof(ParseBadRequest));
		}).ConfigureApiBehaviorOptions(BadRequestsBehavior.Parse);

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey"])),
				ClockSkew = TimeSpan.Zero,
			};
			options.MapInboundClaims = false;
		});

		builder.Services.AddAuthorization();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v0", new OpenApiInfo { Title = "StudentLoanseBonderAPI", Version = "v0" });
		});

		builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
		{
			options.User.RequireUniqueEmail = true;
		}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

		builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("StudentLoanseBonderAPIDatabase")).UseSnakeCaseNamingConvention());

		builder.Services.AddCors(options =>
		{
			var frontendURL = configuration.GetValue<string>("FrontendURL")!;
			options.AddDefaultPolicy(builder =>
			{
				builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(["total_amount_of_records"]); ;
			});
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v0/swagger.json", "StudentLoanseBonderAPI"));
		}

		app.UseHttpsRedirection();

		app.UseRouting();

		app.UseCors();

		app.UseAuthentication();

		app.UseAuthorization();


		app.MapControllers();

		using (var scope = app.Services.CreateScope())
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

			logger.LogDebug("Getting Role Manager Service");
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roleNames = new []{ "User", "Student", "LoansBoardOfficial", "InstitutionAdmin", "SystemAdmin" };

            foreach (var roleName in roleNames)
            {
				logger.LogInformation($"Checking if role {roleName} already exists");
				var roleExists = await roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
				{
					logger.LogWarning($"Role {roleName} not found, creating it...");
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
				else
				{
					logger.LogInformation($"Role {roleName} already exists");
				}
            }
        }

		app.Run();
	}
}
