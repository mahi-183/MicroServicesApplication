// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserRepositoryManager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UserModel;

    /// <summary>
    /// IUserRepositoryManager interface is declaration of methods
    /// </summary>
    public interface IUserRepositoryManager
    {
        Task<string> Login(LoginModel loginModel);

        Task<bool> Registration(RegistrationModel registrationModel);
    }
}
