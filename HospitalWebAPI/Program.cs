
using HospitalWebAPI.Authentication;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Data.Authentication;

namespace HospitalWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("HospitalWebApi")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapRazorPages();

            app.UseMiddleware<ApiKeyAuthenticationEndpointFilter>();

            app.Run();
        }
    }

    
}
