// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// RegistrationModel class is for set all user data
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        [Required]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Should be minimum 3 characters and a maximum of 20 characters")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "{0} is mandatory field")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Should be minimum 3 characters and a maximum of 20 characters")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        [Required]
        [EmailAddress]
        public string EmailId
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
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Length should be minimum 3 charecter and maximum 15")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        //[Required(ErrorMessage = "Please select file.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string Image { get; set; }


        /// <summary>
        /// Gets or sets the UserType.
        /// </summary>
        /// <value>
        /// The UeserType.
        /// </value>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or sets the ServiceId.
        /// </summary>
        /// <value>
        /// The ServiceId.
        /// </value>
        [ForeignKey("ServiceModel")]
        public int ServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NotificationToken { get; set; }
    }
}