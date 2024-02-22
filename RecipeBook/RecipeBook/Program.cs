using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.Context;
using log4net;
using RecipeBook.Providers.Interfaces;
using RecipeBook.Providers.Providers;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Repositories;
using RecipeBook.Mapper;
using AutoMapper;

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
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("recipedb")));

            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IRecipeProvider, RecipeProvider>();
            builder.Services.AddScoped<ILogger<RecipeProvider>, Logger<RecipeProvider>>();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddScoped<ICtx, Ctx>();

            builder.Host.ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddLog4Net("log4net.config");
            });
            //builder.Logging.AddLog4Net();

            var app = builder.Build();

            /*var mapper = app.Services.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();*/

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