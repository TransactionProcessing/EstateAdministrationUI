namespace EstateAdministrationUI.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.IActionResult" />
    [ExcludeFromCodeCoverage]
    public class AlertDecoratorResult : IActionResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertDecoratorResult" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        public AlertDecoratorResult(IActionResult result,
                                    String type,
                                    String title,
                                    String body)
        {
            this.Result = result;
            this.Type = type;
            this.Title = title;
            this.Body = body;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public String Body { get; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public IActionResult Result { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public String Title { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public String Type { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the result operation of the action method asynchronously. This method is called by MVC to process
        /// the result of an action method.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes
        /// information about the action that was executed and request information.</param>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            //NOTE: Be sure you add a using statement for Microsoft.Extensions.DependencyInjection, otherwise
            //      this overload of GetService won't be available!
            ITempDataDictionaryFactory factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();

            ITempDataDictionary tempData = factory.GetTempData(context.HttpContext);
            tempData["_alert.type"] = this.Type;
            tempData["_alert.title"] = this.Title;
            tempData["_alert.body"] = this.Body;

            await this.Result.ExecuteResultAsync(context);
        }

        #endregion
    }
}