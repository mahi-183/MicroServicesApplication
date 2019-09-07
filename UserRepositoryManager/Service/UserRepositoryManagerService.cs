// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepositoryManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserRepositoryManager
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;
    using UserModel.Helper;
    using UserRepositoryManager.Context;

    /// <summary>
    /// UserRepositoryService class implements the IUserRepository methods
    /// </summary>
    /// <seealso cref="UserRepositoryManager.IUserRepositoryManager" />
    public class UserRepositoryManagerService : IUserRepositoryManager
    {
        /// <summary>
        /// The application setting reference created.
        /// </summary>
        private readonly ApplicationSetting applicationSetting;

        /// <summary>
        /// The context
        /// </summary>
        private AuthenticationContext context;

        /// <summary>
        /// The user manager.
        /// </summary>
        private UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryManagerService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="appSetting">The application setting.</param>
        /// <param name="context">The context.</param>
        public UserRepositoryManagerService(UserManager<ApplicationUser> userManager, IOptions<ApplicationSetting> appSetting, AuthenticationContext context)
        {
            this.userManager = userManager;
            this.applicationSetting = appSetting.Value;
            this.context = context;
        }

        /// <summary>
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>Return the success result.</returns>
        /// <exception cref="Exception">Throw Exceptions.</exception>
        public async Task<bool> Registration(RegistrationModel registrationModel)
        {
            ////set the registrations feilds in object applicationUser
            var applicationUser = new ApplicationUser()
            {
                UserName = registrationModel.UserName,
                Email = registrationModel.EmailId,
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserType = "User",
                ServiceId = registrationModel.ServiceId
            };
            try
            {
                ////create new user information using the UserManager Type(UserManager.CreateAsync()) 
                var result = await this.userManager.CreateAsync(applicationUser, registrationModel.Password);
                ////check registation succeded or not
                if (result.Succeeded)
                {
                    string token = await this.userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    await this.userManager.ConfirmEmailAsync(applicationUser, token);
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
        /// <returns>return the token.</returns>
        public async Task<string> Login(LoginModel loginModel)
        {
            ////get the deatails of the user by its email address.
            var user = await this.userManager.FindByNameAsync(loginModel.UserName);
            ////check the user is not null
            if (user != null && await this.userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                if (user.UserType.Equals("User"))
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
                    var result = "Invalid user";
                    return result;
                }
                
            }
            else
            {
                var message = "UserName and Password is Incorrest";
                return message;
            }
        }

        /// <summary>
        /// login with facebook
        /// </summary>
        /// <param name="email">email address.</param>
        /// <returns>return the success result.</returns>
        public async Task<string> FacebookLogin(string email)
        {
            ////get the deatails of the user by its email address.
            var user = await this.userManager.FindByNameAsync(email);
            ////check the user is not null
            if (user != null)
            {
                ////check the login user is type user or admin
                if (user.UserType.Equals("User"))
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
                    var result = "Invalid user";
                    return result;
                }

            }
            else
            {
                var message = "UserName and Password is Incorrest";
                return message;
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>return the result</returns>
        public async Task<string> ForgetPassword(string email)
        {
            var emailId = await this.userManager.FindByEmailAsync(email);
            if (emailId != null)
            {
                MSMQ msmq = new MSMQ();
                string token = await this.userManager.GenerateEmailConfirmationTokenAsync(emailId);
                // await this.userManager.ConfirmEmailAsync(emailId, token);

                var encodeToken = System.Text.Encoding.UTF32.GetBytes(token);


                msmq.SendEmailToQueue(email, encodeToken.ToString());
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
        /// <returns>return the result.</returns>
        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            var user = await this.userManager.FindByEmailAsync(resetPassword.Email);
            if (!user.Equals(null))
            {
                ////generate the token
                var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
                var result = await this.userManager.ResetPasswordAsync(user, token, resetPassword.Password);
                ////Email confirmation token generated
                ////var result1 = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                return result.ToString();
            }
            else
            {
                var message = "EmailId Is Invalid";
                return message;
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="email">The email.</param>
        /// <returns>
        /// return the image url.
        /// </returns>
        /// <exception cref="System.Exception">
        /// throw exception
        /// </exception>
        public async Task<string> ImageUpload(IFormFile file, string email)
        {
            try
            {
                ////create object of the cloudinaryImage class
                CloudinaryImage cloudinary = new CloudinaryImage();

                ////Cloudinary UploadImageCloudinary method call
                var uploadUrl = cloudinary.UploadImageCloudinary(file);

                ////Query to get the note data from database 
                var data = this.context.ApplicationUser.Where(applicationUser => applicationUser.Email == email).FirstOrDefault();

                ////update the ImageUrl to database Notes table
                data.Image = uploadUrl;

                ////Update save changes in database table
                int result = await this.context.SaveChangesAsync();

                ////if result is greater than zero then return the update result 
                if (result > 0)
                {
                    return data.Image;
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
        /// <returns></returns>
        //public async Task<string> SendNotificationFromFirebaseCloud()
        //{
        //    try
        //    {
        //        var result = "-1";
        //        var webAddr = "https://fcm.googleapis.com/fcm/send";
        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=Your Key");
        //        httpWebRequest.Method = "POST";

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            string strNJson = @"{
        //            ""to"": ""/topics/ServiceNow"",
        //            ""data"": {
        //                ""ShortDesc"": ""This is sample"",
        //                ""IncidentNo"": ""INC0010438"",
        //                ""Description"": ""This is sample""
        //                },
        //        ""notification"": {
        //                ""title"": ""ServiceNow"",
        //                ""text"": ""Click me to open an Activity!"",
        //                ""sound"":""default""
        //                }
        //        }";
        //            streamWriter.Write(strNJson);
        //            streamWriter.Flush();
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            result = streamReader.ReadToEnd();
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /// <summary>
        /// get user details.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns>return user details.</returns>
        public IList<ApplicationUser> GetUser(string id)
        {
            try
              {
                var list = new List<ApplicationUser>();
                var userData = from user in this.context.ApplicationUser
                               where (user.Id == id)
                               select user;
                foreach(var data in userData)
                {
                    list.Add(data);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get user details.
        /// </summary>
        /// <param name="loginModel">login model data.</param>
        /// <returns>return the user details.</returns>
        public IList<ApplicationUser> GetUserDetails(LoginModel loginModel)
        {
            try
            {
                var list = new List<ApplicationUser>();
                var userData = from user in this.context.ApplicationUser
                               where (user.UserName == loginModel.UserName)
                               select user;
                foreach (var data in userData)
                {
                    list.Add(data);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
} 