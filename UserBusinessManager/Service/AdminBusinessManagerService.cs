// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminRepositoryManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserBusinessManager.Interface;
    using UserModel;
    using UserRepositoryManager.Interface;

    /// <summary>
    /// Admin business manager service class.
    /// </summary>
    public class AdminBusinessManagerService : IAdminBusinessManager
    {
        /// <summary>
        /// the IAdminRepositoryManager reference.
        /// </summary>
        private IAdminRepositoryManager adminRepositoryManager;

        /// <summary>
        /// initializes the objects.
        /// </summary>
        /// <param name="adminRepositoryManager">initializes the IAdminRepositoryManager.</param>
        public AdminBusinessManagerService(IAdminRepositoryManager adminRepositoryManager)
        {
            this.adminRepositoryManager = adminRepositoryManager;
        }

        /// <summary>
        /// Admin registration 
        /// </summary>
        /// <param name="registrationModel">registration model.</param>
        /// <returns>return the result after successful registration.</returns>
        public async Task<string> AdminRegistration(RegistrationModel registrationModel)
        {
            try
            {
                var result = await this.adminRepositoryManager.AdminRegistration(registrationModel);
                ////check the result is not null.
                if (!result.Equals(null))
                {
                    ////return the result after successful registration.
                    return result;
                }
                else
                {
                    ////throw the exception
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ////throw the exception
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Admin login
        /// </summary>
        /// <param name="loginModel">login model</param>
        /// <returns>return the result.</returns>
        public async Task<string> AdminLogin(LoginModel loginModel)
        {
            try
            {
                ////check the login model is not null
                if (!loginModel.Equals(null))
                {
                    var token = await this.adminRepositoryManager.AdminLogin(loginModel);
                    ////check the token is not null
                    if (!token.Equals(null))
                    {
                        ////return the token
                        return token;
                    }
                    else
                    {
                        ////throw the exception
                        throw new Exception();
                    }
                }
                else
                {
                    ////throw the exception
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ////throw the exception
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// user statics for advance and basic.
        /// </summary>
        /// <returns>return the result.</returns>
        public async Task<dynamic> UserStatistics()
        {
            try
            {
                ////admin repository manager method called
                var result = await this.adminRepositoryManager.UserStatistics();
                ////check the result is not null
                if(!result.Equals(null))
                {
                    ////return the result.
                    return result;
                }
                else
                {
                    ////throw exception
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>return the user list</returns>
        public IList<ApplicationUser> GetAllUSer()
        {
            try
            {
                var result = this.adminRepositoryManager.GetAllUSer();
                if (!result.Equals(null))
                {
                    return result;
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
