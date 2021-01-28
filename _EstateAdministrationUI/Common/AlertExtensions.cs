namespace EstateAdministrationUI.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AlertExtensions
    {
        #region Methods

        /// <summary>
        /// Withes the danger.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static IActionResult WithDanger(this IActionResult result,
                                               String title,
                                               String body)
        {
            return AlertExtensions.Alert(result, "danger", title, body);
        }

        /// <summary>
        /// Withes the information.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static IActionResult WithInfo(this IActionResult result,
                                             String title,
                                             String body)
        {
            return AlertExtensions.Alert(result, "info", title, body);
        }

        /// <summary>
        /// Withes the success.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static IActionResult WithSuccess(this IActionResult result,
                                                String title,
                                                String body)
        {
            return AlertExtensions.Alert(result, "success", title, body);
        }

        /// <summary>
        /// Withes the warning.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static IActionResult WithWarning(this IActionResult result,
                                                String title,
                                                String body)
        {
            return AlertExtensions.Alert(result, "warning", title, body);
        }

        /// <summary>
        /// Alerts the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        private static IActionResult Alert(IActionResult result,
                                           String type,
                                           String title,
                                           String body)
        {
            return new AlertDecoratorResult(result, type, title, body);
        }

        #endregion
    }
}