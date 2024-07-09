using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Tests.General
{
    using Common;
    using Shouldly;
    using Xunit;

    public class QueryStringHelperTests
    {
        [Fact]
        public void QueryStringHelper_GetValueFromQueryString_ValueReturned(){
            String queryString = "?value1=1&value2=2";

            var value1 = QueryStringHelper.GetValueFromQueryString(queryString, "value1");
            value1.ShouldBe("1");

            var value2 = QueryStringHelper.GetValueFromQueryString(queryString, "value2");
            value2.ShouldBe("2");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("?value1=undefined")]
        public void QueryStringHelper_GetValueFromQueryString_NullOrEmpty_ValueReturned(String queryString) {
            var result = QueryStringHelper.GetValueFromQueryString(queryString, "value1");
            result.ShouldBeEmpty();
        }

        [Fact]
        public void QueryStringHelper_GetDateTimeValueFromQueryString_ValueReturned()
        {
            String queryString = "?value1=2023-09-23";

            var value1 = QueryStringHelper.GetDateTimeValueFromQueryString(queryString, "value1");
            value1.Date.ShouldBe(new DateTime(2023,9,23));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abcdef")]
        [InlineData("undefined")]
        public void QueryStringHelper_GetDateTimeValueFromQueryString_NullOrEmpty_ValueReturned(String queryString)
        {
            var result = QueryStringHelper.GetDateTimeValueFromQueryString(queryString, "value1");
            result.ShouldBe(DateTime.MinValue);
        }

        [Fact]
        public void QueryStringHelper_GetIntegerValueFromQueryString_ValueReturned()
        {
            String queryString = "?value1=2";

            var value1 = QueryStringHelper.GetIntegerValueFromQueryString(queryString, "value1");
            value1.ShouldBe(2);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("?value1=abcdef")]
        [InlineData("?value1=undefined")]
        public void QueryStringHelper_GetIntegerValueFromQueryString_InvalidValues_ValueReturned(String queryString)
        {
            var result = QueryStringHelper.GetIntegerValueFromQueryString(queryString, "value1");
            result.ShouldBeNull();
        }

        [Fact]
        public void QueryStringHelper_GetGuidValueFromQueryString_ValueReturned()
        {
            String queryString = "?value1=B3294DF6-3731-4A61-8103-4F8569AB1055";

            var value1 = QueryStringHelper.GetGuidValueFromQueryString(queryString, "value1");
            value1.ShouldBe(Guid.Parse("B3294DF6-3731-4A61-8103-4F8569AB1055"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("?value1=abcdef")]
        [InlineData("?value1=undefined")]
        [InlineData("?value1=00000000-0000-0000-0000-000000000000")]
        public void QueryStringHelper_GetGuidValueFromQueryString_InvalidValues_ValueReturned(String queryString)
        {
            var result = QueryStringHelper.GetGuidValueFromQueryString(queryString, "value1");
            result.ShouldBeNull();
        }
    }
}
