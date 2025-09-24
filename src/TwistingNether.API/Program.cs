using LazyCache;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services;
using TwistingNether.Core.Services.BattleNet;
using TwistingNether.Core.Services.Character;
using TwistingNether.Core.Services.WarcraftLogs;
using TwistingNether.DataAccess.Configuration;
using Common = TwistingNether.Core.Common;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
            });

            builder.Services.AddScoped<IAppCache, CachingService>();
            builder.Services.AddScoped<Common>();
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            builder.Services.AddScoped<IBattleNetService, BattleNetService>();
            builder.Services.AddScoped<IWarcraftLogsService, WarcraftLogsService>();
            builder.Services.AddSingleton<FluentClient>();
            Settings.ClientId = builder.Configuration.GetSection("BattleNet").GetSection("ClientId").Value;
            Settings.ClientSecret = builder.Configuration.GetSection("BattleNet").GetSection("ClientSecret").Value;
            Settings.WarcraftLogsClientId = builder.Configuration.GetSection("WarcraftLogs").GetSection("ClientId").Value;
            Settings.WarcraftLogsClientSecret = builder.Configuration.GetSection("WarcraftLogs").GetSection("ClientSecret").Value;

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
                    .WithOrigins("https://twistingnether.furyshiftz.com")
                    .AllowAnyHeader());
            });
            builder.Services.AddOptions();
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.DefaultModelsExpandDepth(-1));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("DevPolicy");
            }
            else
            {
                app.UseCors("ProdPolicy");
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

            app.Run();
        }
    }
}
