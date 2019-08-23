// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserModel
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// LoginModel class is set properties of username and password
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3,
               ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        [Required]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        public string Password
        {
            get;
            set;
        }
    }
}
