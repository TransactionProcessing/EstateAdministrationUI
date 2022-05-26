namespace EstateAdministrationUI.Common
{
    using System;
    using Shared.General;

    public class ConfigurationService : IConfigurationService
    {
        #region Properties

        public String BoldBIAuthorisationUri {
            get {
                return ConfigurationReader.GetValue("AppSettings", "BoldBIAuthorisationUri");
            }
        }

        public String BoldBIEmbedSecret {
            get {
                return ConfigurationReader.GetValue("AppSettings", "BoldBIEmbedSecret");
            }
        }

        public String BoldBIRootUri {
            get {
                return ConfigurationReader.GetValue("AppSettings", "BoldBIRootUri");
            }
        }

        public String BoldBISiteIdentifier {
            get {
                return ConfigurationReader.GetValue("AppSettings", "BoldBISiteIdentifier");
            }
        }

        public String BoldBIUserEmail {
            get {
                return ConfigurationReader.GetValue("AppSettings", "BoldBIUserEmail");
            }
        }

        #endregion
    }
}