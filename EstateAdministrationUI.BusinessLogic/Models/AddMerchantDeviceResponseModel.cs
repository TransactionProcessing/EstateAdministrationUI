namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddMerchantDeviceResponseModel
{
    public Guid EstateId { get; set; }

    public Guid MerchantId { get; set; }

    public Guid DeviceId { get; set; }
}