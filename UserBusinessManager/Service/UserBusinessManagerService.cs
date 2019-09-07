// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBusinessManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserBusinessManager.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ServiceStack.Redis;
    using UserBusinessManager.Interface;
    using UserModel;
    using UserRepositoryManager;

    /// <summary>
    /// UserBusinessManagerService class implements the IUserBusinessManager interface
    /// </summary>
    /// <seealso cref="UserBusinessManager.Interface.IUserBusinessManager" />
    public class UserBusinessManagerService : IUserBusinessManager
    {
        /// <summary>
        /// create the reference of IUserRepositoryManager Layer
        /// </summary>
        private IUserRepositoryManager userRepositoryManager;

        /// <summary>
        /// The data
        /// </summary>
        private const string data = "";

        /// <summary>
        /// Initialize a new instance.
        /// </summary>
        /// <param name="userRepositoryManager">Initializes a new instance of the UserRepositoryManager Layer Interface.</param>
        public UserBusinessManagerService(IUserRepositoryManager userRepositoryManager)
        {
            this.userRepositoryManager = userRepositoryManager;
        }

        /// <summary>
        /// User Registration method.
        /// </summary>
        /// <param name="registrationModel">Data of registration model.</param>
        /// <returns>Return the result</returns>
        public async Task<bool> Registration(RegistrationModel registrationModel)
        {
            try
            {
                ////RepositoryManager Layer method called
                return await this.userRepositoryManager.Registration(registrationModel);
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
        /// <returns>Return the token.</returns>
        /// <exception cref="Exception">Throw exception</exception>
        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                var userName = loginModel.UserName;
                var cachekey = data + userName;
                ////RepositoryManager Layer method called
                
                using (var redis = new RedisClient())
                {
                    ////clear the redis cache
                    redis.Remove(cachekey);
                    if (redis.Get(cachekey) == null)
                    {
                        ////UserRepository method call
                        var result = await this.userRepositoryManager.Login(loginModel);
                        if (!result.Equals(null))
                        {
                            ////stored token in redis
                            redis.Set(cachekey, result);
                        }

                        return result;
                    }
                    else
                    {
                        var token = redis.Get(loginModel.UserName);

                        return token.ToString();
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> FacebookLogin(string email)
        {
            try
            {
                if (!email.Equals(null))
                {
                    ////repository layer method called.
                    var result = await this.userRepositoryManager.FacebookLogin(email);

                    ////check the result is not null
                    if (!result.Equals(null))
                    {
                        ////return the success result.
                        return result;
                    }
                    else
                    {
                        ////throw the exception message if login not successuly done
                        throw new Exception("FaceBook login not successfuly done!");
                    }
                }
                else
                {
                    throw new Exception("email address is not valid");
                }
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
        /// <returns>Return the token.</returns>
        /// <exception cref="Exception">Throw exception</exception>
        public async Task<string> ForgetPassword(string email)
        {
            try
            {
                ////RepositoryManager Layer method called
                var result = await this.userRepositoryManager.ForgetPassword(email);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>Return the result.</returns>
        /// <exception cref="Exception">Throw exception</exception>
        public async Task<string> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                ////RepositoryManager Layer method called
                var result = await this.userRepositoryManager.ResetPassword(resetPassword);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Image Upload.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <param name="email">User Email.</param>
        /// <returns>Return the Image url.</returns>
        public async Task<string> ImageUpload(IFormFile file, string email)
        {
            try
            {
                if (!file.Equals(null) && !email.Equals(null))
                {
                    var imageUrl = await this.userRepositoryManager.ImageUpload(file, email);
                    if (!imageUrl.Equals(null))
                    {
                        return imageUrl;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // public async Task<string> SendNotificationFromFirebaseCloud()
        // {
        //    try
        //    {
        //        var result = await this.userRepositoryManager.SendNotificationFromFirebaseCloud();
        //        if (!result.Equals(null))
        //        {
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        // }

        /// <summary>
        /// Get User Details from notes microservice.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns>return the user details.</returns>
        public IList<ApplicationUser> GetUser(string id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    var result = this.userRepositoryManager.GetUser(id);
                    if (!result.Equals(null))
                    {
                        return result;
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

        /// <summary>
        /// Get user details 
        /// </summary>
        /// <param name="loginModel">login model</param>
        /// <returns>return the user details</returns>
        public IList<ApplicationUser> GetUserDetails(LoginModel loginModel)
        {
            try
            {
                if (!loginModel.Equals(null))
                {
                    var result = this.userRepositoryManager.GetUserDetails(loginModel);
                    if (!result.Equals(null))
                    {
                        return result;
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
