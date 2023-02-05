namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AssignOperatorToMerchantResponseModel
{
    public Guid EstateId { get; set; }

    public Guid MerchantId { get; set; }

    public Guid OperatorId { get; set; }
}