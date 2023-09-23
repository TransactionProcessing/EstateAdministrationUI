namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TodaysSettlementModel
{
    public Decimal TodaysSettlementValue { get; set; }

    public int TodaysSettlementCount { get; set; }

    public Decimal ComparisonSettlementValue { get; set; }

    public int ComparisonSettlementCount { get; set; }
}