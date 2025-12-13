using LazyCache;
using Microsoft.Extensions.FileProviders;
using Pathoschild.Http.Client;
using System.Reflection;
using System.Web;
using TwistingNether.Core.Services;
using TwistingNether.Core.Services.BattleNet;
using TwistingNether.Core.Services.Character;
using TwistingNether.Core.Services.WarcraftLogs;
using TwistingNether.DataAccess.BattleNet.OAuth;
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
                        .AllowAnyOrigin()
                        .AllowAnyHeader());
                options.AddPolicy("ProdPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
            });
            builder.Services.AddOptions();
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.DefaultModelsExpandDepth(-1));

            var staticPath = Path.Combine(Directory.GetCurrentDirectory(), "static");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticPath),
                RequestPath = "/static"
            });
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

            // Middleman required for Tauri deep linking to work with Battle.net OAuth
            app.MapGet("/old-bnet-tauri-callback", async (HttpRequest req) =>
            {
              
                string code = req.Query.FirstOrDefault(c => c.Key == "code").Value;
                if (code == null)
                {
                    return Results.BadRequest("Missing code parameter");
                }
                 //string state = query.GetValues("state").FirstOrDefault();
                 string clientId = app.Configuration.GetSection("OldBnetTauri-BattleNet").GetSection("ClientId").Value;
                 string clientSecret = app.Configuration.GetSection("OldBnetTauri-BattleNet").GetSection("ClientSecret").Value;
                try
                {
                    IResponse BattleNetTokenExchange = await new FluentClient()
                    .PostAsync("https://oauth.battle.net/token")
                    .WithArgument("redirect_uri", (app.Environment.IsDevelopment() ? "https://localhost:7176" : "https://twistingnetherapi.furyshiftz.com") + "/old-bnet-tauri-callback")
                    .WithArgument("grant_type", "authorization_code")
                    .WithArgument("code", code)
                    .WithBasicAuthentication(clientId, clientSecret);

                    BattleNetAccessTokenModel battleNetAccessToken = await BattleNetTokenExchange.As<BattleNetAccessTokenModel>();
                    var @params = HttpUtility.ParseQueryString(string.Empty);
                    @params["access_token"] = battleNetAccessToken.access_token;
                    @params["token_type"] = battleNetAccessToken.token_type;
                    @params["expires_in"] = battleNetAccessToken.expires_in.ToString();
                    @params["sub"] = battleNetAccessToken.sub;
                    @params["acquired_at"] = DateTime.UtcNow.ToString("o"); // ISO 8601 format

                    var redirectUrl = $"old-bnet-tauri://callback?{@params}";
                    return Results.Redirect(redirectUrl);
                } catch (ApiException ex)
                {
                    var res = ex.Response.AsString();
                    return Results.BadRequest(res);
                }

            });


            app.Run();
        }
    }
}
