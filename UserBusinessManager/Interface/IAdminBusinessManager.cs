// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAdminBusinessManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;

    public interface IAdminBusinessManager
    {
        /// <summary>
        /// The Admin registration.
        /// </summary>
        /// <param name="registrationModel">registration model</param>
        /// <returns>return string.</returns>
        Task<string> AdminRegistration(RegistrationModel registrationModel);

        /// <summary>
        /// Login the Admin.
        /// </summary>
        /// <param name="loginModel">Login Model.</param>
        /// <returns>return string.</returns>
        Task<string> AdminLogin(LoginModel loginModel);

        /// <summary>
        /// UserStatistics
        /// </summary>
        /// <returns>return the UserStatistics</returns>
        Task<dynamic> UserStatistics();
    }
}
