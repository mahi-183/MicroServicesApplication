// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSMQ.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserRepositoryManager.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;
    using UserRepositoryManager.Context;
    using UserRepositoryManager.Interface;
    using System.Linq;

    public class AdminRepositoryManagerService : IAdminRepositoryManager
    {
        private UserManager<ApplicationUser> userManager;
        private AuthenticationContext authenticationContext;
        private ApplicationSetting applicationSetting;

        public AdminRepositoryManagerService(AuthenticationContext authenticationContext, UserManager<ApplicationUser> userManager, IOptions<ApplicationSetting> appSetting)
        {
            this.applicationSetting = appSetting.Value;
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        public async Task<string> AdminRegistration(RegistrationModel registrationModel)
        {
            try
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = registrationModel.UserName,
                    Email = registrationModel.EmailId,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    UserType = "Admin",
                    ServiceId = registrationModel.ServiceId,
                };

                var result = await this.userManager.CreateAsync(applicationUser, registrationModel.Password);

                if (result.Succeeded)
                {
                    return result.ToString();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<string> AdminLogin(LoginModel loginModel)
        {
            try
            {
                var user = await this.userManager.FindByNameAsync(loginModel.UserName);
                if (user != null && await this.userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    if (user.UserType.Equals("Admin"))
                    {
                        ////Authentication successful so generate jwt token 
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim("UserId", user.Id.ToString())
                            }),

                            ////Token Expiry time 
                            Expires = DateTime.UtcNow.AddDays(5),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);

                        ////return token in string formate
                        return token;
                    }
                    else
                    {
                        var result = "Please Login With Admin";
                        return result;
                    }
                }
                else
                {
                    var result = "UserName and password incorrecrt.!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> UserStatistics()
        {
            try
            {
                UserStatastics userStatistics = new UserStatastics();
                var result = (from user in this.authenticationContext.ApplicationUser
                             where (user.ServiceId == 1)
                             select user).Count();
                var result1 = (from user1 in this.authenticationContext.ApplicationUser
                               where (user1.ServiceId == 2)
                               select user1).Count();

                userStatistics.Advanced = result;
                userStatistics.Basic = result1;
                
                if (!userStatistics.Equals(null))
                {
                    return userStatistics;
                }
                else
                {
                    throw new Exception();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
