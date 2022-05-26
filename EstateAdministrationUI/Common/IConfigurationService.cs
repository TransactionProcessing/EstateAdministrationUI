namespace EstateAdministrationUI.Common
{
    using System;

    public interface IConfigurationService
    {
        #region Properties

        String BoldBIAuthorisationUri { get; }

        String BoldBIEmbedSecret { get; }

        String BoldBIRootUri { get; }

        String BoldBISiteIdentifier { get; }

        String BoldBIUserEmail { get; }

        #endregion
    }
}