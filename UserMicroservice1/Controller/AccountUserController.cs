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

namespace UserMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {
        //create the reference of the IUserBusinessManager
        IUserBusinessManager businessManager;
        //reference of the UserManager 
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// intantiate the constructor
        /// </summary>
        /// <param name="businessManager"></param>
        /// <param name="userManager"></param>
        public AccountUserController(IUserBusinessManager businessManager, UserManager<ApplicationUser> userManager)
        {
            this.businessManager = businessManager;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public Task<bool> Registration(RegistrationModel registartionModel)
        {
            //business layer method called
            return this.businessManager.Registration(registartionModel);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //token comes inside the Token string variable
            var Token = await this.businessManager.Login(loginModel);
            if (Token == null)
            {
                //token is null return bad request
                return this.BadRequest();
            }
            else
            {
                //return token
                return this.Ok(new { Token });
            }
        }

        [HttpGet]
        [Route("Profile")]
        //Get: api/UserProfile
        public async Task<Object> UserProfile()
        {
            //To verified the userId with payload
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
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

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            //businessManger method called
            var result = await this.businessManager.ForgetPassword(email);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return BadRequest();
            }   
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            //BusinessManager layer method callled 
            var result = await this.businessManager.ResetPassword(resetPassword);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> ImageUplpoad(IFormFile file, string email)
        {
            try
            {
                if (!file.Equals(null) && !email.Equals(null))
                {
                    var ImageUrl = await this.businessManager.ImageUpload(file, email);
                    if (!ImageUrl.Equals(null))
                    {
                        return this.Ok(new { ImageUrl });
                    }
                    else
                    {
                        return this.BadRequest(new { Message = "Image not uploaded on Cloudinary" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Please select valid Image file or valid email"});
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}