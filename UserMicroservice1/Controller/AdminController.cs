using System;
using System.Collections.Generic;
namespace UserMicroservice.Controller
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UserBusinessManager.Interface;
    using UserModel;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// create reference of admin business manager
        /// </summary>
        private IAdminBusinessManager adminBusinessManager;

        /// <summary>
        /// create reference of user business manager
        /// </summary>
        private IUserBusinessManager userBusinessManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminBusinessManager"></param>
        public AdminController(IAdminBusinessManager adminBusinessManager, IUserBusinessManager userBusinessManager)
        {
            this.adminBusinessManager = adminBusinessManager;
            this.userBusinessManager = userBusinessManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AdminRegistration")]
        public async Task<IActionResult> AdminRegistration(RegistrationModel registrationModel)
        {
            try
            {
                if (!registrationModel.Equals(null))
                {
                    ////AdminBusinessManager method called
                    var result = await this.adminBusinessManager.AdminRegistration(registrationModel);

                    if (!result.Equals(null))
                    {
                        return this.Ok(new { result });
                    }
                    else
                    {
                        return this.BadRequest();
                    }
                }
                else
                {
                    return BadRequest(new { Message = "model is invalid" });
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
        [HttpPost]
        [Route("AdminLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        {
            try
            {
                if (!loginModel.Equals(null))
                {
                    var token = await this.adminBusinessManager.AdminLogin(loginModel);
                    
                    ///User deatails
                    var adminDetails = this.userBusinessManager.GetUserDetails(loginModel);

                    if (!token.Equals(null) && !adminDetails.Equals(null))
                    {
                        return this.Ok(new { token , adminDetails});
                    }
                    else
                    {
                        return this.BadRequest();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserStatistics")]
        public async Task<IActionResult> UserStatistics()
        {
            try
            {
                var result = await this.adminBusinessManager.UserStatistics();
                if (!result.Equals(null))
                {
                    return this.Ok(new { result });
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
        [Route("getUsers")]
        [AllowAnonymous]
        public IActionResult GetAllUSer()
        {
            try
            {
                var userDetails = this.adminBusinessManager.GetAllUSer();
                if (!userDetails.Equals(null))
                {
                    return this.Ok(new { userDetails });
                }
                else
                {
                    throw new Exception("user details not fetched");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
