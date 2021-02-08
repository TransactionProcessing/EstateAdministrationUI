namespace EstateAdministrationUI.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ErrorViewModel
    {
        #region Properties

        public String RequestId { get; set; }

        public Boolean ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        #endregion
    }
}