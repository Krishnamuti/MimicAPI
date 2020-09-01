using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MimicAPI.Database;
using MimicAPI.Repository;
using MimicAPI.Repository.Contracts;
using AutoMapper;
using MimicAPI.Helpers;

namespace MimicAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MimicContext>(opt => {
                opt.UseSqlite("Data Source=Database\\Mimic.db");
            });

            services.AddScoped<IPalavraRepository, PalavraRepository>();

            #region AutoMapper-Config            
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new DTOMapperProfile()); });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddMvc();
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
