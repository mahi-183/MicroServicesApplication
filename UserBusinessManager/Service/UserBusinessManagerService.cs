// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBusinessManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserBusinessManager.Interface;
    using UserModel;
    using UserRepositoryManager;

    /// <summary>
    /// UserBusinessManagerService class implements the IUserBusinessManager interface
    /// </summary>
    /// <seealso cref="UserBusinessManager.Interface.IUserBusinessManager" />
    public class UserBusinessManagerService : IUserBusinessManager
    {
        IUserRepositoryManager userRepositoryManager;

        public UserBusinessManagerService(IUserRepositoryManager userRepositoryManager)
        {
            this.userRepositoryManager = userRepositoryManager;
        }

        //user login from username and password   
        public Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                return this.userRepositoryManager.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Registration(RegistrationModel registrationModel)
        {
            try
            {
                return this.userRepositoryManager.RegistrationAsync(registrationModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
