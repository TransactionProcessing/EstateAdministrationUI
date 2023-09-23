namespace EstateAdministrationUI.BusinessLogic.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class CalendarDateModel
{
    public DateTime Date { get; set; }

    public string DayOfWeek { get; set; }

    public int DayOfWeekNumber { get; set; }

    public string DayOfWeekShort { get; set; }

    public string MonthName { get; set; }

    public string MonthNameShort { get; set; }

    public int MonthNumber { get; set; }

    public int WeekNumber { get; set; }

    public string WeekNumberString { get; set; }

    public int Year { get; set; }

    public string YearWeekNumber { get; set; }
}