
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserBusinessManager.Interface;
using UserBusinessManager.Service;
using UserModel;
using UserRepositoryManager;
using UserRepositoryManager.Context;

namespace UserMicroservice
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //inside this we have provide database connection string
           services.AddDbContext<UserRepositoryManager.Context.AuthenticationContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            
            services.AddTransient<IUserBusinessManager, UserBusinessManagerService>();
            services.AddTransient<IUserRepositoryManager, UserRepositoryManagerService>();

            /*  services.AddDefaultIdentity<UserModel>()
                  .AddEntityFrameworkStores<AuthenticationContext>(); */

            ////Jwt Authentication

            //var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings: JWT_Secret"].ToString());

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>                    //AddJwtBearer is for allow the onlly HTTPs request from client requet
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = false;

            //    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSignKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ClockSkew = TimeSpan.Zero,
            //    };
            //});
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //this for JWtToken generation
            //app.UserCors(builder =>
            //builder.WithOrigins(Configuration["ApplicationSettings: Client_Url"].ToString())
            //.AlloAnyHeader()
            //.AllowAnyMethod()
            //);

            //call use authentication from aap variable
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
