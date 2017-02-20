using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using FilmFind.Middleware;
using FilmFind.Services;
using FilmFind.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using FilmFind.Services.Import;

namespace FilmFind
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            //services.AddMvc().AddJsonOptions(config => { config.SerializerSettings.ContractResolver = new CamelCasePropertyNameContractResolver()}); // for returning camel cased json results
            services.AddScoped<IMovieData, SqlMovieData>();
            services.AddScoped<IUserMovieDataService, UserMovieDataService>();
            services.AddSingleton(Configuration);//Vad gör detta?
            services.AddDbContext<FilmFindDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FilmFind4")));
            //services.AddIdentity<User, IdentityRole>(config => {
            //    config.User.RequireUniqueEmail = true;
            //    config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents() {
            //        OnRedirectToLogin = async ctx =>
            //        {
            //            /// stuff
            //            await Task.Yield();
            //        }
            //    };
            //}); 

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FilmFindDbContext>();
            //services.AddTransient<seedData> // för att seeda data till databasen
            //services.AddScoped<IMoviesRepository, MoviesRepository> //repository (pattern)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseFileServer();  //use static & default files
            //app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseNodeModules(env.ContentRootPath);

            app.UseIdentity();

            //MVC kollar på inkommande request och försöker mappa upp det mot en metod i en C# klass.              
            app.UseMvc(ConfigureRoute);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Err0r!");
            });
        }

        /// <summary>
        /// Sätter upp route/ mapp - strukturer
        /// </summary>
        /// <param name="routeBuilder"></param>
        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}