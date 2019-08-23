// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    
    /// <summary>
    /// ApplicationUser class Derived the IdentityUser
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

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
        /// The UserType.
        /// </value>
        public string UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NotificationToken { get; set; }
    }
}
