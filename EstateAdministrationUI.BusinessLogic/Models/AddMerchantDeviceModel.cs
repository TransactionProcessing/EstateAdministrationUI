﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAdministrationUI.BusinessLogic.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddMerchantDeviceModel
    {
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public String DeviceIdentifier { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class AddMerchantDeviceResponseModel
    {
        public Guid EstateId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid DeviceId { get; set; }
    }
}
