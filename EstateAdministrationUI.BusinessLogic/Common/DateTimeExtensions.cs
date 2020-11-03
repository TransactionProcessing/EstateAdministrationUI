namespace EstateAdministrationUI.BusinessLogic.Common
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Methods

        /// <summary>
        /// Starts the of week.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt,
                                           DayOfWeek startOfWeek)
        {
            Int32 diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        #endregion
    }
}