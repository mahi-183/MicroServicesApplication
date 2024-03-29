﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgetPassword.cs" company="Bridgelabz">
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

    /// <summary>
    /// The ForgetPassword.
    /// </summary>
    public class ForgetPassword
    {
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        [EmailAddress(ErrorMessage = "Email Address not valid")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
