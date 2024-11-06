using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace StudentLoanseBonderAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v0", new OpenApiInfo { Title = "StudentLoanseBonderAPI", Version = "v0" });
		});

		builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("StudentLoanseBonderAPIDatabase")).UseSnakeCaseNamingConvention());

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v0/swagger.json", "StudentLoanseBonderAPI"));
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
