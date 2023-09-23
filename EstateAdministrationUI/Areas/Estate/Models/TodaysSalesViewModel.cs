namespace EstateAdministrationUI.Areas.Estate.Controllers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TodaysSalesViewModel{
    public Decimal TodaysValueOfTransactions{ get; set; }
    public Decimal ComparisonValueOfTransactions{ get; set; }
    public String Label{ get; set; }
    public Decimal Variance{ get; set; }
}