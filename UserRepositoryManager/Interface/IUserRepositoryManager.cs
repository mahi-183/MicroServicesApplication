// --------------------------------------------------------------------------------------------------------------------
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
        /// Registrations the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>return the result.</returns>
        Task<bool> Registration(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>return the token.</returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// facebook login.
        /// </summary>
        /// <param name="email">email address.</param>
        /// <returns>return the success result.</returns>
        Task<string> FacebookLogin(string email);

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
        Task<string> ImageUpload(IFormFile file, string userId);

        /// <summary>
        /// push notification from firebase cloud.
        /// </summary>
        /// <returns>return the success result.</returns>
        //Task<string> SendNotificationFromFirebaseCloud();

        /// <summary>
        /// get user Details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<ApplicationUser> GetUser(string id);

        /// <summary>
        /// get user details.
        /// </summary>
        /// <param name="loginModel">login model</param>
        /// <returns>return user details</returns>
        IList<ApplicationUser> GetUserDetails(LoginModel loginModel);
    }
}
