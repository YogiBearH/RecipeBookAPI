using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.Context;
using log4net;
using RecipeBook.Providers.Interfaces;

namespace RecipeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<Ctx>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("RecipeDb")));

            builder.Services.AddScoped<IRecipeProvider, IRecipeProvider>();

            builder.Host.ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddLog4Net("log4net.config");
            });
            builder.Logging.AddLog4Net();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}