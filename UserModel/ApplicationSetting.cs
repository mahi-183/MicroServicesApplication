// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationSetting.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// ApplicationSetting is for ser the properties 
    public class ApplicationSetting
    {
       public string JWT_Secret { get; set; }

        public string Client_Url { get; set; }
    }
}
