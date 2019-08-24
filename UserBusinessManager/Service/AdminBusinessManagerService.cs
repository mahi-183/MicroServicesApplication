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
    using System.Text;
    using System.Threading.Tasks;
    using UserBusinessManager.Interface;
    using UserModel;
    using UserRepositoryManager.Interface;

    /// <summary>
    /// 
    /// </summary>
    public class AdminBusinessManagerService : IAdminBusinessManager
    {
        /// <summary>
        /// 
        /// </summary>
        private IAdminRepositoryManager adminRepositoryManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminRepositoryManager"></param>
        public AdminBusinessManagerService(IAdminRepositoryManager adminRepositoryManager)
        {
            this.adminRepositoryManager = adminRepositoryManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns></returns>
        public async Task<string> AdminRegistration(RegistrationModel registrationModel)
        {
            try
            {
                var result = await this.adminRepositoryManager.AdminRegistration(registrationModel);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<string> AdminLogin(LoginModel loginModel)
        {
            try
            {
                if (!loginModel.Equals(null))
                {
                    var token = await this.adminRepositoryManager.AdminLogin(loginModel);
                    if (!token.Equals(null))
                    {
                        return token;
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

        public async Task<dynamic> UserStatistics()
        {
            try
            {
                var result = await this.adminRepositoryManager.UserStatistics();
                if(!result.Equals(null))
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
