using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecipeBook.Data.Context;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Repositories;
using RecipeBook.Mapper;
using RecipeBook.Providers.Interfaces;
using RecipeBook.Providers.Providers;
using RecipeBook.GlobalExceptionHandler;

namespace RecipeBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adds services to the container
            services.AddSingleton<ILog>(provider =>
            {
                return LogManager.GetLogger(typeof(ExceptionHandlerMiddleware));
            });
            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<Ctx>(
                options => options.UseNpgsql(Configuration.GetConnectionString("recipedb")));
            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeProvider, RecipeProvider>();
            services.AddScoped<ILogger<RecipeProvider>, Logger<RecipeProvider>>();
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddScoped<ICtx, Ctx>();

            //Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowUI",
                        options =>
                        {
                            options.WithOrigins("http://localhost:5173")
                                   .AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .AllowCredentials();
                        });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Ctx db)
        {
            if (env.IsDevelopment()) 
            {
                app.UseSwaggerUI();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipe.Book.API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseExceptionHandler();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowUI");

            // Resets data on API startup
            try
            { 
                db.Database.ExecuteSqlRaw("DROP SCHEMA public CASCADE; CREATE SCHEMA public;");
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB RESET ERROR: " + ex.Message);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
