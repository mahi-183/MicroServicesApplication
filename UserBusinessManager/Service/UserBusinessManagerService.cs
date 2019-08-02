﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBusinessManagerService.cs" company="Bridgelabz">
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
    using UserRepositoryManager;

    /// <summary>
    /// UserBusinessManagerService class implements the IUserBusinessManager interface
    /// </summary>
    /// <seealso cref="UserBusinessManager.Interface.IUserBusinessManager" />
    public class UserBusinessManagerService : IUserBusinessManager
    {
        IUserRepositoryManager userRepositoryManager;

        public UserBusinessManagerService(IUserRepositoryManager userRepositoryManager)
        {
            this.userRepositoryManager = userRepositoryManager;
        }

        public Task<bool> Registration(RegistrationModel registrationModel)
        {
            try
            {
                return this.userRepositoryManager.Registration(registrationModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Login with username and password   
        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                var result =  await this.userRepositoryManager.Login(loginModel);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ForgetPassword(ForgetPassword forgetPassword)
        {
            try
            {
                var result = await this.userRepositoryManager.ForgetPassword(forgetPassword);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<string> ResetPassword(ResetPassword resetPassword)
        //{
        //    try
        //    {
        //        var result = await this.userRepositoryManager.ForgetPassword(resetPassword.ConfirmPassword);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
