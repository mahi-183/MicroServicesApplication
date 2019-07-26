// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepositoryManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;

    /// <summary>
    /// UserRepositoryService class implents the interface IUserRepository
    /// </summary>
    class UserRepositoryManagerService : IUserRepositoryManager
    {
        public Task<ApplicationUser> Login(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Registration(RegistrationModel registrationModel)
        {
            Task<ApplicationUser> result;
            try
            {
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
 