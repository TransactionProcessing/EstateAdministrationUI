namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class TodaysSalesValueByHourModel
{
    public int Hour { get; set; }

    public Decimal TodaysSalesValue { get; set; }

    public Decimal ComparisonSalesValue { get; set; }
}