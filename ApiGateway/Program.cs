
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            //Implementing ocelot with authorization
            var jwtSetting = builder.Configuration.GetSection("Jwt");
            var key = jwtSetting["Key"];
            var issuer = jwtSetting["Issuer"];
            var audience = jwtSetting["Audience"];
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            //Now Adding JWT to ocelot
            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddOcelot();
            builder.Services.AddEndpointsApiExplorer();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.MapControllers();



            // Ensure authentication middleware runs before authorization
            app.UseAuthentication();
            app.UseAuthorization();
            //telling app to use ocelot
            await app.UseOcelot();





            app.Run();
        }
    }
}
