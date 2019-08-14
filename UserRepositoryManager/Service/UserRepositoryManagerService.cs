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
    /// UserRepositoryService class implents the IUserRepository methods
    /// </summary>
    /// <seealso cref="UserRepositoryManager.IUserRepositoryManager" />
    public class UserRepositoryManagerService : IUserRepositoryManager
    {
        //create reference of UserManager 
        private UserManager<ApplicationUser> _userManager;
        //create reference of ApplicationSetting for encoding jwt secrete key string
        private readonly ApplicationSetting _applicationSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryManagerService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="appSetting">The application setting.</param>
        public UserRepositoryManagerService(UserManager<ApplicationUser> userManager, IOptions<ApplicationSetting> appSetting)
        {
            this._userManager = userManager;
            this._applicationSetting = appSetting.Value;
        }

        /// <summary>
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Registration(RegistrationModel registrationModel)
        {
            //set the registrations feilds in object applicationUser
            var applicationUser = new ApplicationUser()
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.EmailId,
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
            };
            try
            {
                //create new user information using the UserManager Type(UserManager.CreateAsync()) 
                var result = await _userManager.CreateAsync(applicationUser, registrationModel.Password);
                //check registation succeded or not
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

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                //Authentication successful so generate jwt token 
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    //Token Expiry time 
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                //return token in string formate
                return (token);
            }
            else
            {
                var message = "UserName and Password is incorrest";
                return message;
            }
        }


        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                var message = "EmailId Is Invalid";
                return message;
            }
            else
            {
                //generate the token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.Password);
                return result.ToString();
            }
        }
    }
}
 