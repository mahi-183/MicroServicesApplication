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
    using System.Threading.Tasks;

    /// <summary>
    /// RegistrationModel class is for set all user data
    /// </summary>
    public class RegistrationModel
    {
        [DataType(DataType.Text)]
        [StringLength(15,MinimumLength =3,
            ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        [Required]
        public string UserName
        {
            get;
            set;
        }
        //FirstName is public property for set and get with proper validation
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 20 characters")]
        [DataType(DataType.Text)]
        public string FirstName
        {
            get;
            set;
        }
        
        //LastName is public property for set and get with proper validation
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Name Should be minimum 3 characters and a maximum of 20 characters")]
        [DataType(DataType.Text)]
        public string LastName
        {
            get;
            set;
        }
        
        //EmailId is public property for set and get with proper validation
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string EmailId
        {
            get;
            set;
        }

        //Password is public property for set and get with proper validation
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength =6,
            ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        public string Password
        {
            get;
            set;
        }

       
    }
}