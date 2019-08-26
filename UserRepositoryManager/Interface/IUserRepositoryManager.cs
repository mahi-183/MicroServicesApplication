﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using UserModel;

    /// <summary>
    /// IUserRepositoryManager interface is declaration of methods
    /// </summary>
    public interface IUserRepositoryManager
    {
        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>return the token.</returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>return the result.</returns>
        Task<bool> Registration(RegistrationModel registrationModel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>return the result.</returns>
        Task<string> ForgetPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>return the result.</returns>
        Task<string> ResetPassword(ResetPassword resetPassword);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="email">The email.</param>
        /// <returns>return the image url.</returns>
        Task<string> ImageUpload(IFormFile file, string email);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //Task<string> SendNotificationFromFirebaseCloud();

        /// <summary>
        /// get user Details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<ApplicationUser> GetUser(string id);
    }
}
