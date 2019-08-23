// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloudinaryImage.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel.Helper
{
    using System;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The class is for to upload image to cloud
    /// </summary>
    public class CloudinaryImage
    {
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>return the image url.</returns>
        /// <exception cref="Exception">Throw Exception.</exception>
        public string UploadImageCloudinary(IFormFile file)
        {
            try
            {
                ////Image file name
                var name = file.FileName;

                ////open the file and read in stream
                var stream = file.OpenReadStream();

                ////Account details of cloudinary which cloudName, Api, secreteKey etc
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("bridgelabz-com", "921945993926817", "UcknrXKjOuFQAFZHwzoPxMFKELY");

                ////Given all details to upload the image to cloud
                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

                ////uploadParams for uploading image 
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };

                ////uploaded the image to cloud with the stream and name of the image file
                var uploadResult = cloudinary.Upload(uploadParams);

                ////get url of uploded image on cloudinary 
                var uploadUrl = uploadResult.Uri.ToString();

                ////return the cloudinary image url
                return uploadUrl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
