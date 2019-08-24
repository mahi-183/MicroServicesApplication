// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAdminRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;

    public interface IAdminRepositoryManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        Task<string> AdminRegistration(RegistrationModel registrationModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<string> AdminLogin(LoginModel loginModel);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<dynamic> UserStatistics();
    }


}
