using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public AccountUserController(IUserBusinessManager businessManager)
        {

            this.businessManager = businessManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public Task<ApplicationUser> Registration(RegistrationModel registartionModel)
        {
            return this.businessManager.Registration(registartionModel);
        }
    }
}