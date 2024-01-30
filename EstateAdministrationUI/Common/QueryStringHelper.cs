namespace EstateAdministrationUI.Common;

using System;
using System.Collections.Specialized;
using System.Web;

public static class QueryStringHelper
{
    public static String GetValueFromQueryString(String queryString, String fieldName)
    {
        if (String.IsNullOrEmpty(queryString)){
            throw new ArgumentNullException(nameof(queryString));
        }

        NameValueCollection parsedQueryString = HttpUtility.ParseQueryString(queryString);

        String result = parsedQueryString[fieldName];

        return result;
    }

    public static DateTime GetDateTimeValueFromQueryString(String queryString, String fieldName, String dateFormat="yyyy-MM-dd")
    {
        String fieldValue = QueryStringHelper.GetValueFromQueryString(queryString, fieldName);

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
}