namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SecurityService.DataTransferObjects.Responses;
    using Shared.IntegrationTesting;
    using Shared.Logger;
    using Shouldly;
    using TechTalk.SpecFlow;

    public class TestingContext
    {
        public EstateDetails GetEstateDetails(TableRow tableRow, Guid? testId = null)
        {
            String estateName = SpecflowTableHelper.GetStringRowValue(tableRow, "EstateName").Replace("[id]", testId.Value.ToString("N"));

            EstateDetails estateDetails = this.Estates.SingleOrDefault(e => e.EstateName == estateName);

            estateDetails.ShouldNotBeNull();

            return estateDetails;
        }

        public List<Guid> GetAllEstateIds()
        {
            return this.Estates.Select(e => e.EstateId).ToList();
        }

        public EstateDetails GetEstateDetails(String estateName)
        {
            EstateDetails estateDetails = this.Estates.SingleOrDefault(e => e.EstateName == estateName);

            estateDetails.ShouldNotBeNull();

            return estateDetails;
        }

        private List<EstateDetails> Estates;
        public void AddEstateDetails(Guid estateId, String estateName)
        {
            this.Estates.Add(EstateDetails.Create(estateId, estateName));
        }

        public String AccessToken { get; set; }

        public DockerHelper DockerHelper { get; set; }

        public NlogLogger Logger { get; set; }

        public Dictionary<String, Guid> Users;
        public Dictionary<String, Guid> Roles;

        public List<ClientDetails> Clients;

        public List<String> ApiResources;
        public List<String> IdentityResources;

        public TokenResponse TokenResponse;

        public TestingContext()
        {
            this.Users = new Dictionary<String, Guid>();
            this.Roles = new Dictionary<String, Guid>();
            this.Clients = new List<ClientDetails>();
            this.ApiResources = new List<String>();
            this.Estates = new List<EstateDetails>();
            this.IdentityResources = new List<String>();
        }

        public ClientDetails GetClientDetails(String clientId)
        {
            ClientDetails clientDetails = this.Clients.SingleOrDefault(c => c.ClientId == clientId);

            clientDetails.ShouldNotBeNull();

            return clientDetails;
        }

        public void AddClientDetails(String clientId,
                                     String clientSecret,
                                     List<String> grantTypes)
        {
            this.Clients.Add(ClientDetails.Create(clientId, clientSecret, grantTypes));
        }
    }
}