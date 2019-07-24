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

    using UserModel;

    /// <summary>
    /// IUserRepositoryManager interface is declaration of methods
    /// </summary>
    public interface IUserRepositoryManager
    {
        string Login(int id);

        string Registration(RegistrationModel registrationModel);
    }
}
