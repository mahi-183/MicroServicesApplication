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
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3,
               ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        [Required]
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
