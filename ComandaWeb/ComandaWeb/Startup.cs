using ComandaWeb.DAL.Comanda;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ComandaWeb
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
            services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();
            services.AddDbContext<ComandaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Comanda")));

            //Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "APIComandaWeb",
                    Description = "Serviço de comanda web",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe",
                        Email = "felipe.teste@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Uso Livre"
                    }
                });

                var arquivoXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var caminhoXml = Path.Combine(AppContext.BaseDirectory, arquivoXml);
                c.IncludeXmlComments(arquivoXml);

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Copie 'Bearer ' + token'." 
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
                        new string[] {}
                    }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "JwtBearer";
                option.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ComandaWeb-Autenticacao-Valida")),
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidIssuer = "ComandaWeb.WebApp",
                    ValidAudience = "Postman",
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory fabricaLog)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            fabricaLog.AddProvider(new ProvedorLog(new ConfiguracaoProvedorLog
            {
                LevelLog = LogLevel.Information
            },Configuration)); 

            app.UseHttpsRedirection();
            app.UseAuthentication();
                
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Comanda Web");
                    c.RoutePrefix = string.Empty;
                }
            );
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
