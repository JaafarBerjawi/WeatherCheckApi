using Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Security.Business;
using Security.Data;
using WeatherCheck.Business;
using WeatherCheck.Data;
namespace WeatherCheck.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<UserSchemaFilter>();
            });

            //Service Registration
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<AuthenticationService>();
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<IWeatherAPIService, WeatherAPIService>();
            builder.Services.AddTransient<CurrentWeatherService>();

            builder.Services.AddTransient<IAuthenticationDataManager, Security.Data.EFCore.AuthenticationDataManager>();
            builder.Services.AddTransient<IWeatherConditionDataManager, WeatherCheck.Data.EfCore.WeatherConditionDataManager>();

            builder.Services.AddSingleton<EncryptionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
