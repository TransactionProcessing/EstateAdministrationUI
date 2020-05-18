using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Common
{
    using System.Drawing;
    using System.Linq;
    using System.Reflection;

    public static class Extensions
    {
        #region Methods

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="orderByColumn">The order by column.</param>
        /// <param name="orderByDirection">The order by direction.</param>
        /// <returns></returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable,
                                                String orderByColumn,
                                                String orderByDirection)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(orderByColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (orderByDirection == "asc")
            {
                return enumerable.OrderBy(x => propertyInfo.GetValue(x, null));
            }

            return enumerable.OrderByDescending(x => propertyInfo.GetValue(x, null));
        }

        /// <summary>
        /// Thens the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="orderByColumn">The order by column.</param>
        /// <param name="orderByDirection">The order by direction.</param>
        /// <returns></returns>
        public static IEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> enumerable,
                                               String orderByColumn,
                                               String orderByDirection)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(orderByColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (orderByDirection == "asc")
            {
                return enumerable.ThenBy(x => propertyInfo.GetValue(x, null));
            }

            return enumerable.ThenByDescending(x => propertyInfo.GetValue(x, null));
        }

        /// <summary>
        /// Converts to hex.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static String ToHex(this Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        /// <summary>
        /// Converts to rgb.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static String ToRGB(this Color c)
        {
            return "RGB(" + c.R + "," + c.G + "," + c.B + ")";
        }

        #endregion
    }
}
