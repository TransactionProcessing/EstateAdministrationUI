namespace EstateAdministrationUI.Common;

using System;
using System.Collections.Specialized;
using System.Web;

public static class QueryStringHelper
{
    public static String GetValueFromQueryString(String queryString, String fieldName)
    {
        if (String.IsNullOrEmpty(queryString)){
           return String.Empty;
        }

        NameValueCollection parsedQueryString = HttpUtility.ParseQueryString(queryString);

        String result = parsedQueryString[fieldName];

        if (String.Compare(result, "undefined", StringComparison.InvariantCultureIgnoreCase) == 0) {
            return String.Empty;
        }

        return result;
    }

    public static DateTime GetDateTimeValueFromQueryString(String queryString, String fieldName, String dateFormat="yyyy-MM-dd")
    {
        String fieldValue = QueryStringHelper.GetValueFromQueryString(queryString, fieldName);

        if (String.IsNullOrEmpty(fieldValue)) {
            return DateTime.MinValue;
        }

        DateTime result = DateTime.ParseExact(fieldValue, dateFormat, null);

        return result;
    }


    public static Guid? GetGuidValueFromQueryString(String queryString, String fieldName)
    {
        String fieldValue = QueryStringHelper.GetValueFromQueryString(queryString, fieldName);

        if (String.IsNullOrWhiteSpace(fieldValue) || fieldValue.Equals("undefined", StringComparison.OrdinalIgnoreCase) || fieldValue.Equals("00000000-0000-0000-0000-000000000000", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (Guid.TryParse(fieldValue, out Guid result))
        {
            return result;
        }

        // Handle invalid Guid format here if needed
        // You can throw an exception, log an error, or take any other appropriate action

        return null;
    }

    public static Int32? GetIntegerValueFromQueryString(String queryString, String fieldName)
    {
        String fieldValue = QueryStringHelper.GetValueFromQueryString(queryString, fieldName);

        if (String.IsNullOrWhiteSpace(fieldValue) || fieldValue.Equals("undefined", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (Int32.TryParse(fieldValue, out Int32 result))
        {
            return result;
        }

        // Handle invalid Int32 format here if needed
        // You can throw an exception, log an error, or take any other appropriate action

        return null;
    }
}