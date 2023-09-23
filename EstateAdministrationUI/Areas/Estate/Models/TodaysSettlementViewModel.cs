namespace EstateAdministrationUI.Areas.Estate.Controllers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TodaysSettlementViewModel
{
    public Decimal TodaysSettlementValue { get; set; }

    public Decimal ComparisonSettlementValue { get; set; }
    public String Label { get; set; }
    public Decimal Variance { get; set; }
}