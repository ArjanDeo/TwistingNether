using LazyCache;
using Microsoft.EntityFrameworkCore;
using Pathoschild.Http.Client;
using TwistingNether.Core;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess;
using TwistingNether.DataAccess.Configuration;

namespace TwistingNether.API
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAppCache, CachingService>();
            builder.Services.AddScoped<Common>();
            builder.Services.AddScoped<IGeneralService, GeneralService>();
            builder.Services.AddScoped<IKeystoneService, KeystoneService>();
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            builder.Services.AddSingleton<FluentClient>();
            Settings.ClientId = builder.Configuration.GetSection("BattleNet").GetSection("ClientId").Value;
            Settings.ClientSecret = builder.Configuration.GetSection("BattleNet").GetSection("ClientSecret").Value;

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://127.0.0.1", "http://localhost:5173", "http://localhost")
                        .AllowAnyHeader());
                options.AddPolicy("ProdPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://whatchores.furyshiftz.com","https://twistingnether.furyshiftz.com")
                    .AllowAnyHeader());
            });
            builder.Services.AddOptions();
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                

                app.UseCors("DevPolicy");
            }
            else
            {
                app.UseCors("ProdPolicy");
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
