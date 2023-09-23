namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TodaysSalesModel
{
    public Decimal TodaysSalesValue { get; set; }

    public int TodaysSalesCount { get; set; }

    public Decimal ComparisonSalesValue { get; set; }

    public int ComparisonSalesCount { get; set; }
}