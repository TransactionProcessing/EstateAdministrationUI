namespace EstateAdministrationUI.Common
{
    using System;
    using System.Collections.Generic;
    using Shared.Exceptions;
    using Shared.General;

    public class ConfigurationService : IConfigurationService
    {
        #region Properties

        public String BoldBIAuthorisationUri {
            get {
                return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "AuthorisationUri");
            }
        }

        public String BoldBIEmbedSecret {
            get {
                return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "EmbedSecret");
            }
        }

        public String BoldBIRootUri {
            get {
                return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "RootUri");
            }
        }

        public String BoldBISiteIdentifier {
            get {
                return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "SiteIdentifier");
            }
        }

        public String BoldBIUserEmail {
            get {
                return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "UserEmail");
            }
        }

        #endregion

        #region Methods

        public String BoldBIDashboardDataSourceId(String dashboardName) {
            return this.BoldBIDashboardDetails(dashboardName).dataSourceId;
        }

        public (String dashboardId, String dataSourceId) BoldBIDashboardDetails(String dashboardName) {
            Dictionary<String, Dictionary<String, String>> dashboardList =
                ConfigurationReader.GetValueFromSection<Dictionary<String, Dictionary<String, String>>>("AppSettings:BoldBiConfiguration:Dashboards", null);

            Dictionary<String, String> dashboard = dashboardList.GetValueOrDefault(dashboardName);

            if (dashboard == null) {
                throw new NotFoundException($"Dashboard with name [{dashboardName}] not found");
            }

            return (dashboard["Id"], dashboard["DataSourceId"]);
            ;
        }

        public String BoldBIDashboardId(String dashboardName) {
            return this.BoldBIDashboardDetails(dashboardName).dashboardId;
        }

        #endregion
    }
}