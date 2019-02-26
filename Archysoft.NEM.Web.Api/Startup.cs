using Archysoft.NEM.Data.Repositories.Abstract;
using Archysoft.NEM.Data.Repositories.Concrete;
using Archysoft.NEM.Domain.Model.Services.Abstract;
using Archysoft.NEM.Domain.Model.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Archysoft.NEM.Web.Api
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
            services.AddMvc();

            //services
            services.AddTransient<ITransactionService, TransactionService>();

            //repositories
            services.AddTransient<ITransactionInfoRepository, TransactionInfoRepository>();

        }

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

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
