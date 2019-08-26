// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel
{
    using System;
    using UserModel.Enumerable;

    /// <summary>
    /// Notes Model
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string Color { get; set; }
        
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        //// [Required(ErrorMessage = "Please select file.")]
        //// [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NotesModel"/> is reminder.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reminder; otherwise, <c>false</c>.
        /// </value>
        public DateTime? Reminder { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pin; otherwise, <c>false</c>.
        /// </value>
        public bool IsPin { get; set; }

        /// <summary>
        /// Gets or sets the type of the note.
        /// </summary>
        /// <value>
        /// The type of the note.
        /// </value>
        public NoteTypeEnum noteType { get; set; }
    }
}
