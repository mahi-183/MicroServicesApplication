// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBusinessManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Service
{
    using Microsoft.AspNetCore.Http;
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
        //create the reference of IUserRepositoryManager Layer
        IUserRepositoryManager userRepositoryManager;

        //Inject the IuserRepositoryManager 
        public UserBusinessManagerService(IUserRepositoryManager userRepositoryManager)
        {
            this.userRepositoryManager = userRepositoryManager;
        }

        public Task<bool> Registration(RegistrationModel registrationModel)
        {
            try
            {
                //RepositoryManager Layer method called
                return this.userRepositoryManager.Registration(registrationModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                //RepositoryManager Layer method called
                var result = await this.userRepositoryManager.Login(loginModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ForgetPassword(string email)
        {
            try
            {
                //RepositoryManager Layer method called
                var result = await this.userRepositoryManager.ForgetPassword(email);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the passsword.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                //RepositoryManager Layer method called
                var result = await userRepositoryManager.ResetPassword(resetPassword);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// </exception>
        public async Task<string> ImageUpload(IFormFile file, string email)
        {
            try
            {
                if (!file.Equals(null) && !email.Equals(null))
                {
                    var ImageUrl = await this.userRepositoryManager.ImageUpload(file, email);
                    if (!ImageUrl.Equals(null))
                    {
                        return ImageUrl;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
