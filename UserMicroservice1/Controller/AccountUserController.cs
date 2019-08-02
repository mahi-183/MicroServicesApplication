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
        IUserBusinessManager businessManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountUserController(IUserBusinessManager businessManager,UserManager<ApplicationUser> userManager)
        {
            this.businessManager = businessManager;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public Task<bool> Registration(RegistrationModel registartionModel)
         {
            return this.businessManager.Registration(registartionModel);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var Token = await this.businessManager.Login(loginModel);
            if ( Token == null )
            {
              return  this.BadRequest();
            }
            else
            {
                return this.Ok(new { Token });
            }
           
            
        }

        [HttpGet]
        [Authorize]
        //Get: api/UserProfile
        public async Task<Object> UserProfile()
        {
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
        public async Task<IActionResult> ForgetPassword(ForgetPassword forgetPassword)
        {
            var result = await this.businessManager.ForgetPassword(forgetPassword);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return BadRequest();
            }   
        }

    }
}