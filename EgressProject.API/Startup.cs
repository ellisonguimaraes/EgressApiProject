using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgressProject.API.Data;
using EgressProject.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using EgressProject.API.Repositories;
using EgressProject.API.Models;
using System.Reflection;
using System.IO;
using EgressProject.API.Models.Utils;
using Microsoft.Extensions.Options;
using EgressProject.API.Services.Auth;
using EgressProject.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Business;
using FluentValidation;
using EgressProject.API.Validators;
using FluentValidation.AspNetCore;
using EgressProject.API.Models.InputModel;

namespace EgressProject.API
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
            // Configure ORM EntityFramework
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("EgressDbPostgreSQL"));
            });

            // GET Token Configuration in appsettings.json, insert in TokenConfiguration object and injection dependency
            TokenConfiguration configuration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfigurations"))
                .Configure(configuration);
            services.AddSingleton(configuration);

            // Authentication Configure
            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Secret))
                };
            });

            services.AddControllers()
                // Ignore Loop Handling .Include()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddFluentValidation(option => {
                    option.DisableDataAnnotationsValidation = true;
                    option.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            // AutoMapper Configure
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Egress API", 
                    Version = "v1",
                    Description = "Egress API from Universidade Estadual de Santa Cruz (UESC)",
                    Contact = new OpenApiContact {
                        Name = "Ellison W. M. Guimarães",
                        Email = "ellison.guimaraes@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/ellisonguimaraes/")
                    }
                });

                // Configure Authentication Support in Swagger Page
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                                Enter 'Bearer' [space] and then your token in the text input below.
                                                Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Configure XML Comments to Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IJwTUtils, JwTUtils>();

            // Dependency Injection Validators
            services.AddScoped<IValidator<Login>, LoginValidator>();
            services.AddScoped<IValidator<RegisterInputModel>, RegisterInputModelValidator>();
            services.AddScoped<IValidator<TokenInputModel>, TokenInputModelValidator>();

            // Dependency Injection Business Class
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IAuthenticateBusiness, AuthenticateBusiness>();
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IEmploymentBusiness, EmploymentBusiness>();

            // Dependency Injection Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<IPersonCourseRepository, PersonCourseRepository>();
            services.AddScoped<IEntityRepository<Course>, CourseRepository>();
            services.AddScoped<IEntityRepository<Employment>, EmploymentRepository>();
            services.AddScoped<IEntityRepository<Especialization>, EspecializationRepository>();
            services.AddScoped<IEntityRepository<Highlights>, HighlightsRepository>();
            services.AddScoped<IEntityRepository<JobAdvertisement>, JobAdvertisementRepository>();
            services.AddScoped<IEntityRepository<News>, NewsRepository>();
            services.AddScoped<IEntityRepository<Person>, PersonRepository>();
            services.AddScoped<IEntityRepository<Testimony>, TestimonyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseMiddleware<JwTMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Egress API v1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
