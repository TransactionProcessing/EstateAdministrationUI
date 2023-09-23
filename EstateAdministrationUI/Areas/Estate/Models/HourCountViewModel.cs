namespace EstateAdministrationUI.Areas.Estate.Controllers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class HourCountViewModel
{
    public Int32 Hour { get; set; }
    public Decimal TodaysCount { get; set; }
    public Decimal ComparisonCount { get; set; }
}