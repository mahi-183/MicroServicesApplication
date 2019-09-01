// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserMicroservice
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using UserBusinessManager.Interface;
    using UserBusinessManager.Service;
    using UserModel;
    using UserRepositoryManager;
    using UserRepositoryManager.Context;
    using UserRepositoryManager.Interface;
    using UserRepositoryManager.Service;
    using Operation = Swashbuckle.AspNetCore.Swagger.Operation;

    /// <summary>
    /// The start up class contains the all startup code like Database connection.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services this method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //// Inject AppSettings
            services.Configure<ApplicationSetting>(this.Configuration.GetSection("ApplicationSettings"));

            ////Allow origin backend to run on different port
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:4200"));
            });

            //// Inside this we have provide database connection string
            services.AddDbContext<UserRepositoryManager.Context.AuthenticationContext>(options =>
              options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            //// validation for Password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            services.AddTransient<IUserBusinessManager, UserBusinessManagerService>();
            services.AddTransient<IUserRepositoryManager, UserRepositoryManagerService>();
            services.AddTransient<IAdminBusinessManager, AdminBusinessManagerService>();
            services.AddTransient<IAdminRepositoryManager, AdminRepositoryManagerService>();

            //// Add Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFandooApp", Version = "v1", Description = "Fandoo App" });
                c.OperationFilter<FileUploadedOperation>();
            });
            
            //// Jwt Authentication
            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// Configures the specified application. This method gets called by the runtime. Use this method to configure the HTTP request pipeline..
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UserCors(builder =>
            // builder.WithOrigins(Configuration["ApplicationSettings: Client_Url"].ToString())
            // .AlloAnyHeader()
            // .AllowAnyMethod()
            // );
            app.UseCors("AllowOrigin");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFandooApp");
            });

            //// configure the pipeline to use authentication
            //// make the authentication service is available to the application
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    /// <summary>
    /// The file for Athorization header
    /// </summary>
    public class FileUploadedOperation : IOperationFilter
    {
        /// <summary>
        /// Apply function
        /// </summary>
        /// <param name="swaggerDocument">swaggerDocument parameter</param>
        /// <param name="documentFilter">documentFilter parameter </param>
        public void Apply(Operation swaggerDocument, OperationFilterContext documentFilter)
        {
            if (swaggerDocument.Parameters == null)
            {
                swaggerDocument.Parameters = new List<IParameter>();
            }

            swaggerDocument.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}
