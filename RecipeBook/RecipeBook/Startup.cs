using RecipeBook.Data.Context;
using RecipeBook.Data.Interfaces;
using RecipeBook.Data.Repositories;
using RecipeBook.Providers.Interfaces;
using RecipeBook.Providers.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using log4net;
using AutoMapper;
using RecipeBook.Mapper;

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
            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<Ctx>(
                options => options.UseNpgsql(Configuration.GetConnectionString("recipedb")));

            services.AddScoped<IRecipeRepository, RecipeRepository>();
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
                app.UseDeveloperExceptionPage();
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

            app.UseCors("AllowUI");

            // Resets data on API startup
            db.Database.ExecuteSqlRaw("DROP SCHEMA public CASCADE; CREATE SCHEMA public;");
            db.Database.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
