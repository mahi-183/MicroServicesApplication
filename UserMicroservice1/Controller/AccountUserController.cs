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
            //// Token comes inside the Token string variable
            var token = await this.businessManager.Login(loginModel);
            if (token.Equals(null))
            {
                //// Token is null return bad request
                return this.BadRequest();
            }
            else
            {
                return this.Ok(new { token });
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
            //// businessManger method called
            var result = await this.businessManager.ForgetPassword(email);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return this.BadRequest();
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
            //// BusinessManager layer method callled 
            var result = await this.businessManager.ResetPassword(resetPassword);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return this.BadRequest();
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
                if (!file.Equals(null) && !email.Equals(null))
                {
                    var imageUrl = await this.businessManager.ImageUpload(file, email);
                    if (!imageUrl.Equals(null))
                    {
                        return this.Ok(new { imageUrl });
                    }
                    else
                    {
                        return this.BadRequest(new { Message = "Image not uploaded on Cloudinary" });
                    }
                }
                else
                {
                    return this.BadRequest(new { Message = "Please select valid Image file or valid email" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Send Notification From Firebase Cloud.
        /// </summary>
        /// <returns>return result.</returns>
        [HttpGet]
        [Route("sendMessage")]
        public IActionResult SendNotificationFromFirebaseCloud()
        {
            try
            {
                ////the businessManager Layer method called
                var result = this.businessManager.SendNotificationFromFirebaseCloud();
                
                if (result.Equals(null))
                {
                    return this.Ok();
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

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(string id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    IList<ApplicationUser> userDetails = new List<ApplicationUser>();
                    userDetails = this.businessManager.GetUser(id);
                    if (!userDetails.Equals(null))
                    {
                        return this.Ok(new { userDetails });
                    }
                    else
                    {
                        return this.BadRequest(new { Message = "user not Exist" });
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