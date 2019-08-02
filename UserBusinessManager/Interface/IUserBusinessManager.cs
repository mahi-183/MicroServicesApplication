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

        Task<bool> Registration(RegistrationModel registrationModel);

        Task<string> Login(LoginModel loginModel);

        Task<string> ForgetPassword(ForgetPassword forgetPassword);

        //Task<string> ResetPasssword(ResetPassword resetPassword);
    }
}
