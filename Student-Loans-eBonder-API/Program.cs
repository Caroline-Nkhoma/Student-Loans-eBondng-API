using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentLoanseBonderAPI.Services;
using System.Text;

namespace StudentLoanseBonderAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		builder.Services.AddScoped<AccountService>();

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"])),
				ClockSkew = TimeSpan.Zero,
			};
			options.MapInboundClaims = false;
		});

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v0", new OpenApiInfo { Title = "StudentLoanseBonderAPI", Version = "v0" });
		});

		builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

		builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("StudentLoanseBonderAPIDatabase")).UseSnakeCaseNamingConvention());

		builder.Services.AddCors(options =>
		{
			var frontendURL = builder.Configuration.GetValue<string>("FrontendURL")!;
			options.AddDefaultPolicy(builder =>
			{
				builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
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

		app.Run();
	}
}
