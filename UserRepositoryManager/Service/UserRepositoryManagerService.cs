﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepositoryManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;

    /// <summary>
    /// UserRepositoryService class implents the interface IUserRepository
    /// </summary>
    public class UserRepositoryManagerService : IUserRepositoryManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserRepositoryManagerService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<bool> Login(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegistrationAsync(RegistrationModel registrationModel)
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
                return result.Succeeded;
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
 