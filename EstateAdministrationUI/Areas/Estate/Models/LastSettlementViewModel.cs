namespace EstateAdministrationUI.Areas.Estate.Controllers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class LastSettlementViewModel{
    public DateTime SettlementDate { get; set; }
    public Decimal SalesValue { get; set; }
    public Decimal FeesValue { get; set; }
    public Int32 SalesCount { get; set; }
}