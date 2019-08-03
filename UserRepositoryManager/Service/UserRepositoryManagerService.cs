// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepositoryManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager
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
    //using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// UserRepositoryService class implents the interface IUserRepository
    /// </summary>
    public class UserRepositoryManagerService : IUserRepositoryManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSetting _applicationSetting;

        public UserRepositoryManagerService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IOptions<ApplicationSetting> appSetting)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._applicationSetting = appSetting.Value;
        }

        //Registration 
        public async Task<bool> Registration(RegistrationModel registrationModel)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.EmailId,
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationModel.Password);
                if (result.Succeeded)
                {
                    string token =await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    await _userManager.ConfirmEmailAsync(applicationUser, token);
                    return result.Succeeded;
                }
                else
                {
                    return false;
                }
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Login with UserName and Password
        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return (token);
            }
            else
            {
                //return BadRequest(new { Message = "UserName and Passward is incorrect" });
                var message = "UserName and Password is incorrest";
                return message;
            }
        }


        public async Task<string> ForgetPassword(string email)
        {
            var Email = await _userManager.FindByEmailAsync(email);
            if (Email != null)
            {
                MSMQ msmq = new MSMQ();
                //string token = await _userManager.GenerateEmailConfirmationTokenAsync(Email);
                //await _userManager.ConfirmEmailAsync(Email, token);
                msmq.sendEmailToQueue(email);
                return true.ToString();
            }
            else
            {
                var message = "EmailId Is Invalid";
                return message;
            }
        }


        //public async Task<string> ResetPassword(ResetPassword resetPassword,string token)
        //{
        //    var user = await _userManager.FindByEmailAsync(resetPassword.Password);
        //    if (user == null)
        //    {
        //        var message = "EmailId Is Invalid";
        //        return message;
        //    }
        //    else
        //    {
        //        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        await _userManager.ConfirmEmailAsync(user, token);
        //        return token;
        //    }
        //}
    }
}
 