// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPassword.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    //ResetPassword is for 
    public class ResetPassword
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "Passord must be minimum 3 charecter and maximum 15 charecter")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        public string Email { get; set; } 

    }
}
