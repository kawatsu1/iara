using AutoMapper;
using Iara.Domain.Entities;
using Iara.Infra.Context;
using Iara.Infra.Repositories;
using Iara.Infra.Repositories.Interfaces;
using Iara.Services.DTOS;
using Iara.Services.Services;
using Iara.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Iara.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(cfg => Configuration);

            services.AddControllers();

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cotacao, CotacaoDto>().ReverseMap();
                cfg.CreateMap<CotacaoItem, CotacaoItemDto>().ReverseMap();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            services.AddDbContext<IaraContext>(options => options
                .UseSqlServer(Configuration["ConnectionStrings:IaraConnectionString"])
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())),
            ServiceLifetime.Transient);

            services.AddScoped<ICotacaoRepository, CotacaoRepository>();
            services.AddScoped<ICotacaoService, CotacaoService>();
            services.AddScoped<ICotacaoItemRepository, CotacaoItemRepository>();
            services.AddScoped<ICotacaoItemService, CotacaoItemService>();


            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Iara Teste API",
                    Version = "v1",
                    Description = "Iara Teste - Cotacao",
                    Contact = new OpenApiContact
                    {
                        Name = "Iara Tech",
                        Email = "suporte@iara.tech",
                        Url = new Uri("https://www.iara.tech/quem-somos")
                    },
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Iara.Teste.Api v1"));

            app.UseHttpsRedirection();

            app.UseCors("*");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
