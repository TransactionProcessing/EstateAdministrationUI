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

        #region Methods

        String BoldBIDashboardDataSourceId(String dashboardName);

        (String dashboardId, String dataSourceId) BoldBIDashboardDetails(String dashboardName);

        public String BoldBIDashboardId(String dashboardName);

        #endregion
    }
}