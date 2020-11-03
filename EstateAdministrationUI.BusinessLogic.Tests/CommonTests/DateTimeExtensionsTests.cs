using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Tests.CommonTests
{
    using Common;
    using Shouldly;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DateTimeExtensions_StartOfWeek_FirstDayMonday_CorrectDateReturned()
        {
            DateTime dateTime = new DateTime(2020,10,30);

            DateTime startOfWeekResult = dateTime.StartOfWeek(DayOfWeek.Monday);

            startOfWeekResult.Date.ShouldBe(new DateTime(2020,10,26));
        }

        [Fact]
        public void DateTimeExtensions_StartOfWeek_FirstDaySunday_CorrectDateReturned()
        {
            DateTime dateTime = new DateTime(2020, 10, 30);

            DateTime startOfWeekResult = dateTime.StartOfWeek(DayOfWeek.Sunday);

            startOfWeekResult.Date.ShouldBe(new DateTime(2020, 10, 25));
        }

    }
}
