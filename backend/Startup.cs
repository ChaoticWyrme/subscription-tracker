using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using SubWatchApi.Models;

namespace SubWatchApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddEntityFrameworkSqlite().AddDbContext<SubWatchContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("SubWatchConnection"))
            //);
            services.AddDbContext<SubWatchContext>(opt => opt.UseInMemoryDatabase("SubWatch"));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
                //context.Response.Headers.Add("Report-To", "{\"group\":\"default\",\"endpoints\":[{\"url\":\"https://wyrme.report-uri.com/a/d/g\"}],\"include_subdomains\":true}");
                //context.Response.Headers.Add("Content-Security-Policy", "default-src *; report-uri https://wyrme.report-uri.com/r/d/csp/enforce");
                await next.Invoke();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubWatchApi");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseHttpsRedirection();
            var rewrite = new RewriteOptions();
            rewrite.AddRedirect("^$", "swagger");
            app.UseRewriter(rewrite);

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
