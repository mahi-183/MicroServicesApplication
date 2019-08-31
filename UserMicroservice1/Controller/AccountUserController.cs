// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUserController.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserMicroservice.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UserBusinessManager.Interface;
    using UserModel;

    /// <summary>
    /// Account controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountUserController : ControllerBase
    {
        /// <summary>
        /// The user business manager
        /// </summary>
        private IUserBusinessManager businessManager;

        /// <summary>
        /// The Admin business manager
        /// </summary>
        private IAdminBusinessManager adminBusinessManager;

        /// <summary>
        /// The user manager
        /// </summary>
        private UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Instantiate the constructor
        /// </summary>
        /// <param name="businessManager">The BusinessManager.</param>
        /// <param name="userManager">The UserManager.</param>
        public AccountUserController(IUserBusinessManager businessManager, UserManager<ApplicationUser> userManager, IAdminBusinessManager adminBusinessManager)
        {
            this.adminBusinessManager = adminBusinessManager;
            this.businessManager = businessManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registartionModel">The registration model.</param>
        /// <returns>Return the success result.</returns>
        [HttpPost]
        [Route("UserRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationModel registartionModel)
        {
            //// business layer method called
            var result = await this.businessManager.Registration(registartionModel);

            if (!result)
            {
                //// Token is null return bad request
                return this.BadRequest();
            }
            else
            {
                return this.Ok(new { result });
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>Return the token.</returns>
        [HttpPost]
        [Route("UserLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ////check the login model data is not null 
            if (!loginModel.Equals(null))
            {
                //// Token comes inside the Token string variable
                var token = await this.businessManager.Login(loginModel);
                ////check the token is null
                if (token.Equals(null))
                {
                    //// Token is null return bad request
                    return this.BadRequest();
                }
                else
                {
                    ////return the token
                    return this.Ok(new { token });
                }
            }
            else
            {
                ////throw the exception if model data is null
                throw new Exception("model data is invalid");
            }
        }

        /// <summary>
        /// Facebook login.
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>return the success result.</returns>
        [HttpPost]
        [Route("FacebookLogin")]
        public async Task<IActionResult> FacebookLogin(string email)
        {
            try
            {
                ////in startup file need to add the secrete key and app-id = 925286397821691 secrete-key = 84fb3aa133f0cad418dd9d405ea878ea
                var result = await this.businessManager.FacebookLogin(email);
                if (!result.Equals(null))
                {
                    return this.Ok(new { result });
                }
                else
                {
                    return this.BadRequest(new { Message = "The Facebook Login not successfuly done!" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Users the profile.
        /// </summary>
        /// <returns>Return the user details</returns>
        [HttpGet]
        [Route("Profile")]
        public async Task<Object> UserProfile()
        {
            //// To verified the userId with payload
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.BadRequest();
            }
            else
            {
                return new
                {
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.Id
                };
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Return response from.</returns>
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (!email.Equals(null))
            {
                //// businessManger method called
                var result = await this.businessManager.ForgetPassword(email);
                ////check the result is not null
                if (!result.Equals(null))
                {
                    ////return the succeed result when the token is send on email.
                    return this.Ok(new { result });
                }
                else
                {
                    ////return the bad result if token is not sent on mail
                    return this.BadRequest();
                }

            }
            else
            {
                ////the email is not valid throw the exception
                throw new Exception("The email address is null");
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>Return the result.</returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
         {
            if (!resetPassword.Equals(null))
            {
                //// BusinessManager layer method callled 
                var result = await this.businessManager.ResetPassword(resetPassword);
                if (result != null)
                {
                    ////return the token when reset the password
                    return this.Ok(new { result });
                }
                else
                {
                    ////return the bad request when the password is not reset successfuly
                    return this.BadRequest();
                }
            }
            else
            {
                ////throw the exception when the model data is invalid
                throw new Exception("Reset Model data is Invalid");
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="email">The email.</param>
        /// <returns>Return the result as a image url.</returns>
        /// <exception cref="System.Exception">Throw exception.</exception>
        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> ImageUplpoad(IFormFile file, string email)
        {
            try
            {
                ////check the image file is valid or not and email address is not null
                if (!file.Equals(null) && !email.Equals(null))
                {
                    ////businessManager layer method called.
                    var imageUrl = await this.businessManager.ImageUpload(file, email);
                    ////check the cloudinary image url is not null
                    if (!imageUrl.Equals(null))
                    {
                        ////return the cloudinary image url
                        return this.Ok(new { imageUrl });
                    }
                    else
                    {
                        ////return the bad result if
                        return this.BadRequest(new { Message = "Image not uploaded on Cloudinary" });
                    }
                }
                else
                {
                    ////return the bad request file or email is null
                    return this.BadRequest(new { Message = "Please select valid Image file or valid email" });
                }
            }
            catch (Exception ex)
            {
                ////throw the exception
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Send Notification From Firebase Cloud.
        /// </summary>
        /// <returns>return result.</returns>
        //[HttpGet]
        //[Route("sendMessage")]
        //public IActionResult SendNotificationFromFirebaseCloud()
        //{
        //    try
        //    {
        //        ////the businessManager Layer method called
        //        var result = this.businessManager.SendNotificationFromFirebaseCloud();
                
        //        if (result.Equals(null))
        //        {
        //            return this.Ok();
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("GetUser")]
        [AllowAnonymous]
        public IList<ApplicationUser> GetUser(string userId)
        {
            try
            {
                if (!userId.Equals(null))
                {
                    IList<ApplicationUser> userDetails = new List<ApplicationUser>();
                    userDetails = this.businessManager.GetUser(userId);
                    if (!userDetails.Equals(null))
                    {
                        return userDetails;
                    }
                    else
                    {
                        throw new Exception("user details not fetched");
                    }
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