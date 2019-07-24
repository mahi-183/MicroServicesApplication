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
    using UserModel;

    /// <summary>
    /// IUserBusinessManager interface is for declaration of methods 
    /// </summary>
    public interface IUserBusinessManager
    {

        string Registration(RegistrationModel registrationModel);

        string Login(int id);
    }
}
