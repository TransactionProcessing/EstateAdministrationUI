namespace EstateAdministrationUI.BusinessLogic.Models{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddProductToContractModel{
        #region Properties

        public String DisplayText{ get; set; }

        public String ProductName{ get; set; }

        public Int32 ProductType{ get; set; }

        public Decimal? Value{ get; set; }

        #endregion
    }
}