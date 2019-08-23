using System;
using System.Collections.Generic;
namespace UserMicroservice.Controller
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UserBusinessManager.Interface;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBusinessManager adminBusinessManager;

        public AdminController(IAdminBusinessManager adminBusinessManager)
        {
            this.adminBusinessManager = adminBusinessManager;
        }
    }
}
