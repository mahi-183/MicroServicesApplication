// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationModel.cs" company="Bridgelabz">
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
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// RegistrationModel class is for set all user data
    /// </summary>
    public class RegistrationModel
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
        //FirstName is public property for set and get with proper validation
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(15, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string FirstName
        {
            get;
            set;
        }

        //LastName is public property for set and get with proper validation
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(15, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string LastName
        {
            get;
            set;
        }

        //City is public property for set and get with proper validation
        [Required(ErrorMessage = "{0} is mandatory field")]
        public string City
        {
            get;
            set;
        }

        //EmailId is public property for set and get with proper validation
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailId
        {
            get;
            set;
        }

        //PhoneNumber is public property for set and get with proper validation
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public int PhoneNumber
        {
            get;
            set;
        }

        //Password is public property for set and get with proper validation
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }

        //ConfirmPassword is public property for set and get with proper validation
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("UserModel.Password", ErrorMessage = "The Password and ConfirmPassword shold be equal")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}