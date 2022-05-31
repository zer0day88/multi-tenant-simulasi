using System.Globalization;
using DapperPostgreSQL;
using FluentValidation.AspNetCore;
using Humanizer;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using multi_tenant.ServiceCollections;
using Swashbuckle.AspNetCore.SwaggerUI;
using Utility.Authorize.Helper;
using Utility.Validation.Filter;

namespace multi_tenant
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
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddCors();

            services.AddControllers(config => { config.Filters.Add(new ErrorValidationFilter()); })
                .AddNewtonsoftJson()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                    options.ValidatorOptions.LanguageManager.Culture = new CultureInfo("Id");
                    options.ValidatorOptions.DisplayNameResolver = (type, member, expression) =>
                    {
                        if (member != null)
                        {
                            return member.Name.Humanize();
                        }

                        return null;
                    };
                });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "multi_tenant", Version = "v1"});
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddFluentValidationRulesToSwagger();

            //global postgresql connection dependency injection , don't remove
            services.AddScoped(
                c => new SQLConn(
                    Configuration.GetValue<string>("DatabaseSettings:ConnectionString")
                )
            );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "multi_tenant v1");
                        c.DocExpansion(DocExpansion.None);
                    });
            }

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}