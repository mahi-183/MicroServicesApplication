// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserBusinessManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
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
        /// <returns>Return the success result</returns>
        Task<bool> Registration(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>return token.</returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// facebook login
        /// </summary>
        /// <param name="email">email address.</param>
        /// <returns>return the success result.</returns>
        Task<string> FacebookLogin(string email);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Result the result.</returns>
        Task<string> ForgetPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>Return the result.</returns>
        Task<string> ResetPassword(ResetPassword resetPassword);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="email">The email.</param>
        /// <returns>Return the Image url.</returns>
        Task<string> ImageUpload(IFormFile file, string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ////Task<string> SendNotificationFromFirebaseCloud();

        /// <summary>
        /// get the user details.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns>return the user details.</returns>
        IList<ApplicationUser> GetUser(string id);

        /// <summary>
        /// Get user details
        /// </summary>
        /// <param name="loginModel">login model</param>
        /// <returns>returns user details.</returns>
        IList<ApplicationUser> GetUserDetails(LoginModel loginModel);
    }
}
