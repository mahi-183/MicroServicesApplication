// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// LoginModel class is set properties of username and password
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(15, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 15 characters")]
        [DataType(DataType.Text)]
        public string UserName
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }

    }
}
