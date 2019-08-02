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
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6,
            ErrorMessage = "Passord must be minimum 3 charecter and maximum 15 charecter")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword should be equal")]
        public string ConfirmPassword { get; set; }
    }
}
