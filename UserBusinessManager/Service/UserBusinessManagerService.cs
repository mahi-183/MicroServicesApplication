using System;
using System.Collections.Generic;
using System.Text;
using UserBusinessManager.Interface;
using UserModel;
using UserRepositoryManager;

namespace UserBusinessManager.Service
{
    class UserBusinessManagerService : IUserBusinessManager
    {
        IUserRepositoryManager userRepositoryManager;

        public UserBusinessManagerService(IUserRepositoryManager userRepositoryManager)
        {
            this.userRepositoryManager = userRepositoryManager;
        }

        public string Login(int id)
        {
            throw new NotImplementedException();
        }

        public string Registration(RegistrationModel registrationModel)
        {
            throw new NotImplementedException();
        }
    }
}
