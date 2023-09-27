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
}