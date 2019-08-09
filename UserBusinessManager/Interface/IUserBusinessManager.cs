// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserBusinessManager.cs" company="Bridgelabz">
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

    /// <summary>
    /// IUserBusinessManager interface is for declaration of methods 
    /// </summary>
    public interface IUserBusinessManager
    {
        /// <summary>
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns></returns>
        Task<bool> Registration(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<string> ForgetPassword(string email);

        /// <summary>
        /// Resets the passsword.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        Task<string> ResetPasssword(ResetPassword resetPassword);
    }
}
