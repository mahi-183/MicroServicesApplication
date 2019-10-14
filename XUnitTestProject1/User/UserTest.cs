// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTest.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace XUnitTestProject1.User
{
    using Moq;
    using UserBusinessManager.Service;
    using UserModel;
    using UserRepositoryManager;
    using Xunit;

    /// <summary>   
    /// Test all functionality of Users
    /// </summary>
    public class UserTest
    {
        /// <summary>
        /// Registrations the test.
        /// </summary>
        [Fact]
        public void RegistrationTest()
        {
            var userData = new Mock<IUserRepositoryManager>();
            var user = new UserBusinessManagerService(userData.Object);
            
            ////Object of RegistrationModel
            var registrationModelData = new RegistrationModel()
            {
                UserName = "UserName",
                FirstName = "FirstName",
                LastName = "LastName",
                EmailId = "EmailId",
                Password = "Password",
                Image = "Image"
            };

            ////Act
            var data = user.Registration(registrationModelData);

            ////Asert
            Assert.NotNull(data);
        }

        /// <summary>
        /// Users the login.
        /// </summary>
        [Fact]
        public void LoginTest()
        {
            var userData = new Mock<IUserRepositoryManager>();
            var loginData = new UserBusinessManagerService(userData.Object);
            var loginModeldata = new LoginModel()
            {
                UserName = "UserName",
                Password = "Password"
            };

            ////Act
            var data = loginData.Login(loginModeldata);

            ////Assert
            Assert.NotNull(data);
        }
    }
}
