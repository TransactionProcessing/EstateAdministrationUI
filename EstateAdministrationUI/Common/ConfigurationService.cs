namespace EstateAdministrationUI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Shared.Exceptions;
    using Shared.General;

    [ExcludeFromCodeCoverage]
    public class ConfigurationService : IConfigurationService
    {
        #region Properties

        public String BoldBIAuthorisationUri {
            get {
                //return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "AuthorisationUri");
                return String.Empty;
            }
        }

        public String BoldBIEmbedSecret {
            get {
                //return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "EmbedSecret");
                return String.Empty;
            }
        }

        public String BoldBIRootUri {
            get {
                //return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "RootUri");
                return String.Empty;
            }
        }

        public String BoldBISiteIdentifier {
            get {
                //return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "SiteIdentifier");
                return String.Empty;
            }
        }

        public String BoldBIUserEmail {
            get {
                //return ConfigurationReader.GetValue("AppSettings:BoldBiConfiguration", "UserEmail");
                return String.Empty;
            }
        }

        #endregion

        #region Methods

        public String BoldBIDashboardDataSourceId(String dashboardName) {
            return String.Empty;
            //return this.BoldBIDashboardDetails(dashboardName).dataSourceId;
        }

        public (String dashboardId, String dataSourceId) BoldBIDashboardDetails(String dashboardName) {
            //Dictionary<String, Dictionary<String, String>> dashboardList =
            //    ConfigurationReader.GetValueFromSection<Dictionary<String, Dictionary<String, String>>>("AppSettings:BoldBiConfiguration:Dashboards", null);

            //Dictionary<String, String> dashboard = dashboardList.GetValueOrDefault(dashboardName);

            //if (dashboard == null) {
            //    throw new NotFoundException($"Dashboard with name [{dashboardName}] not found");
            //}

            //return (dashboard["Id"], dashboard["DataSourceId"]);
            //;
            return (String.Empty, String.Empty);
        }

        public String BoldBIDashboardId(String dashboardName) {
            //return this.BoldBIDashboardDetails(dashboardName).dashboardId;
            return String.Empty;
        }

        #endregion
    }
}