// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
using Swashbuckle.AspNetCore.Swagger;

namespace UserMicroservice
{
    internal class OpenApiInfo : Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}