using Chilindo_Banking.Midleware;
using Chilindo_Data.UnitOfWork;
using Chilindo_Database;
using ChilinDo_Service;
using ChilinDo_Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chilindo_Banking
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChilinDoContext>
                (options => options.UseSqlServer(connection));

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICommonService, CommonService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });


            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
