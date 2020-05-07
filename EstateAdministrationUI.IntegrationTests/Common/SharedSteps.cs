using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.IntegrationTests.Common
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Coypu;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using OpenQA.Selenium;
    using SecurityService.DataTransferObjects;
    using SecurityService.DataTransferObjects.Requests;
    using SecurityService.DataTransferObjects.Responses;
    using Shared.IntegrationTesting;
    using Shouldly;
    using TechTalk.SpecFlow;

    [Binding]
    [Scope(Tag = "shared")]
    public class SharedSteps
    {
        private readonly TestingContext TestingContext;

        private readonly IWebDriver WebDriver;

        public SharedSteps(TestingContext testingContext, IWebDriver webDriver)
        {
            this.TestingContext = testingContext;
            this.WebDriver = webDriver;
        }

        [Given(@"I create the following roles")]
        public async Task GivenICreateTheFollowingRoles(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                CreateRoleRequest createRoleRequest = new CreateRoleRequest
                                                      {
                                                          RoleName = SpecflowTableHelper.GetStringRowValue(tableRow, "Role Name").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"))
                                                      };
                CreateRoleResponse createRoleResponse = await this.CreateRole(createRoleRequest, CancellationToken.None).ConfigureAwait(false);

                createRoleResponse.ShouldNotBeNull();
                createRoleResponse.RoleId.ShouldNotBe(Guid.Empty);

                this.TestingContext.Roles.Add(createRoleRequest.RoleName, createRoleResponse.RoleId);
            }
        }

        [Given(@"I have a token to access the estate management resource")]
        public async Task GivenIHaveATokenToAccessTheEstateManagementResource(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                String clientId = SpecflowTableHelper.GetStringRowValue(tableRow, "ClientId").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));

                ClientDetails clientDetails = this.TestingContext.GetClientDetails(clientId);

                if (clientDetails.GrantTypes.Contains("client_credentials"))
                {
                    TokenResponse tokenResponse = await this.TestingContext.DockerHelper.SecurityServiceClient.GetToken(clientId, clientDetails.ClientSecret, CancellationToken.None).ConfigureAwait(false);

                    this.TestingContext.AccessToken = tokenResponse.AccessToken;
                }
            }
        }

        [Given(@"I have created the following estates")]
        public async Task GivenIHaveCreatedTheFollowingEstates(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                String estateName = SpecflowTableHelper.GetStringRowValue(tableRow, "EstateName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));

                CreateEstateRequest createEstateRequest = new CreateEstateRequest
                                                          {
                                                              EstateId = Guid.NewGuid(),
                                                              EstateName = estateName
                                                          };

                CreateEstateResponse response = await this.TestingContext.DockerHelper.EstateClient.CreateEstate(this.TestingContext.AccessToken, createEstateRequest, CancellationToken.None).ConfigureAwait(false);

                response.ShouldNotBeNull();
                response.EstateId.ShouldNotBe(Guid.Empty);

                // Cache the estate id
                this.TestingContext.AddEstateDetails(response.EstateId, estateName);

                this.TestingContext.Logger.LogInformation($"Estate {estateName} created with Id {response.EstateId}");
            }

            foreach (TableRow tableRow in table.Rows)
            {
                EstateDetails estateDetails = this.TestingContext.GetEstateDetails(tableRow, this.TestingContext.DockerHelper.TestId);

                EstateResponse estate = null;
                await Retry.For(async () =>
                                {
                                    estate = await this.TestingContext.DockerHelper.EstateClient
                                                       .GetEstate(this.TestingContext.AccessToken, estateDetails.EstateId, CancellationToken.None).ConfigureAwait(false);
                                    estate.ShouldNotBeNull();
                                }).ConfigureAwait(false);


                estate.EstateName.ShouldBe(estateDetails.EstateName);
            }
        }

        [Given(@"I have created the following operators")]
        public async Task GivenIHaveCreatedTheFollowingOperators(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                String operatorName = SpecflowTableHelper.GetStringRowValue(tableRow, "OperatorName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
                Boolean requireCustomMerchantNumber = SpecflowTableHelper.GetBooleanValue(tableRow, "RequireCustomMerchantNumber");
                Boolean requireCustomTerminalNumber = SpecflowTableHelper.GetBooleanValue(tableRow, "RequireCustomTerminalNumber");

                CreateOperatorRequest createOperatorRequest = new CreateOperatorRequest
                                                              {
                                                                  Name = operatorName,
                                                                  RequireCustomMerchantNumber = requireCustomMerchantNumber,
                                                                  RequireCustomTerminalNumber = requireCustomTerminalNumber
                                                              };

                // lookup the estate id based on the name in the table
                EstateDetails estateDetails = this.TestingContext.GetEstateDetails(tableRow, this.TestingContext.DockerHelper.TestId);

                CreateOperatorResponse response = await this.TestingContext.DockerHelper.EstateClient.CreateOperator(this.TestingContext.AccessToken, estateDetails.EstateId, createOperatorRequest, CancellationToken.None).ConfigureAwait(false);

                response.ShouldNotBeNull();
                response.EstateId.ShouldNotBe(Guid.Empty);
                response.OperatorId.ShouldNotBe(Guid.Empty);

                // Cache the estate id
                estateDetails.AddOperator(response.OperatorId, operatorName);

                this.TestingContext.Logger.LogInformation($"Operator {operatorName} created with Id {response.OperatorId} for Estate {estateDetails.EstateName}");
            }
        }

        [Given(@"I have created the following security users")]
        public async Task GivenIHaveCreatedTheFollowingSecurityUsers(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                // lookup the estate id based on the name in the table
                EstateDetails estateDetails = this.TestingContext.GetEstateDetails(tableRow, this.TestingContext.DockerHelper.TestId);

                if (tableRow.ContainsKey("EstateName") && tableRow.ContainsKey("MerchantName") == false)
                {
                    // Creating an Estate User
                    CreateEstateUserRequest createEstateUserRequest = new CreateEstateUserRequest
                    {
                        EmailAddress = SpecflowTableHelper.GetStringRowValue(tableRow, "EmailAddress").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")),
                        FamilyName = SpecflowTableHelper.GetStringRowValue(tableRow, "FamilyName"),
                        GivenName = SpecflowTableHelper.GetStringRowValue(tableRow, "GivenName"),
                        MiddleName = SpecflowTableHelper.GetStringRowValue(tableRow, "MiddleName"),
                        Password = SpecflowTableHelper.GetStringRowValue(tableRow, "Password")
                    };

                    CreateEstateUserResponse createEstateUserResponse =
                        await this.TestingContext.DockerHelper.EstateClient.CreateEstateUser(this.TestingContext.AccessToken, estateDetails.EstateId, createEstateUserRequest, CancellationToken.None);

                    createEstateUserResponse.EstateId.ShouldBe(estateDetails.EstateId);
                    createEstateUserResponse.UserId.ShouldNotBe(Guid.Empty);

                    estateDetails.SetEstateUser(createEstateUserRequest.EmailAddress, createEstateUserRequest.Password);

                    this.TestingContext.Logger.LogInformation($"Security user {createEstateUserRequest.EmailAddress} assigned to Estate {estateDetails.EstateName}");
                }
                else if (tableRow.ContainsKey("MerchantName"))
                {
                    // Creating a merchant user
                    String token = this.TestingContext.AccessToken;
                    if (String.IsNullOrEmpty(estateDetails.AccessToken) == false)
                    {
                        token = estateDetails.AccessToken;
                    }
                    // lookup the merchant id based on the name in the table
                    String merchantName = SpecflowTableHelper.GetStringRowValue(tableRow, "MerchantName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
                    Guid merchantId = estateDetails.GetMerchantId(merchantName);

                    CreateMerchantUserRequest createMerchantUserRequest = new CreateMerchantUserRequest
                    {
                        EmailAddress = SpecflowTableHelper.GetStringRowValue(tableRow, "EmailAddress").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")),
                        FamilyName = SpecflowTableHelper.GetStringRowValue(tableRow, "FamilyName"),
                        GivenName = SpecflowTableHelper.GetStringRowValue(tableRow, "GivenName"),
                        MiddleName = SpecflowTableHelper.GetStringRowValue(tableRow, "MiddleName"),
                        Password = SpecflowTableHelper.GetStringRowValue(tableRow, "Password")
                    };

                    CreateMerchantUserResponse createMerchantUserResponse =
                        await this.TestingContext.DockerHelper.EstateClient.CreateMerchantUser(token, estateDetails.EstateId, merchantId, createMerchantUserRequest, CancellationToken.None);

                    createMerchantUserResponse.EstateId.ShouldBe(estateDetails.EstateId);
                    createMerchantUserResponse.MerchantId.ShouldBe(merchantId);
                    createMerchantUserResponse.UserId.ShouldNotBe(Guid.Empty);

                    estateDetails.AddMerchantUser(merchantName, createMerchantUserRequest.EmailAddress, createMerchantUserRequest.Password);

                    this.TestingContext.Logger.LogInformation($"Security user {createMerchantUserRequest.EmailAddress} assigned to Merchant {merchantName}");
                }
            }
        }

        [Given(@"I click on the My Estate sidebar option")]
        public void GivenIClickOnTheMyEstateSidebarOption()
        {
            this.WebDriver.ClickButtonById("estateDetailsLink");
        }

        [Then(@"I am presented with the Estate Details Screen")]
        public void ThenIAmPresentedWithTheEstateDetailsScreen()
        {
            this.WebDriver.Title.ShouldBe("Edit Golf Club");
        }

        [Then(@"My Estate Details will be shown")]
        public void ThenMyEstateDetailsWillBeShown(Table table)
        {
            TableRow tableRow = table.Rows.Single();

            IWebElement element = this.WebDriver.FindElement(By.Id("EstateName"));
            element.ShouldNotBeNull();
            String elementValue = element.GetProperty("value");
            elementValue.ShouldBe(SpecflowTableHelper.GetStringRowValue(tableRow, "EstateName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")));
        }


        private async Task<CreateRoleResponse> CreateRole(CreateRoleRequest createRoleRequest,
                                                          CancellationToken cancellationToken)
        {
            CreateRoleResponse createRoleResponse = await this.TestingContext.DockerHelper.SecurityServiceClient.CreateRole(createRoleRequest, cancellationToken).ConfigureAwait(false);
            return createRoleResponse;
        }


        private async Task<CreateApiResourceResponse> CreateApiResource(CreateApiResourceRequest createApiResourceRequest,
                                                                        CancellationToken cancellationToken)
        {
            CreateApiResourceResponse createApiResourceResponse = await this.TestingContext.DockerHelper.SecurityServiceClient.CreateApiResource(createApiResourceRequest, cancellationToken).ConfigureAwait(false);
            return createApiResourceResponse;
        }

        private async Task<CreateClientResponse> CreateClient(CreateClientRequest createClientRequest,
                                                              CancellationToken cancellationToken)
        {
            CreateClientResponse createClientResponse = await this.TestingContext.DockerHelper.SecurityServiceClient.CreateClient(createClientRequest, cancellationToken).ConfigureAwait(false);
            return createClientResponse;
        }

        [Given(@"I create the following identity resources")]
        public async Task GivenICreateTheFollowingIdentityResources(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                // Get the scopes
                String userClaims = SpecflowTableHelper.GetStringRowValue(tableRow, "UserClaims");

                CreateIdentityResourceRequest createIdentityResourceRequest = new CreateIdentityResourceRequest
                {
                    Name = SpecflowTableHelper.GetStringRowValue(tableRow, "Name"),
                    Claims = string.IsNullOrEmpty(userClaims) ? null : userClaims.Split(",").ToList(),
                    Description = SpecflowTableHelper.GetStringRowValue(tableRow, "Description"),
                    DisplayName = SpecflowTableHelper.GetStringRowValue(tableRow, "DisplayName")
                };

                await this.CreateIdentityResource(createIdentityResourceRequest, CancellationToken.None).ConfigureAwait(false);
            }
        }

        private async Task CreateIdentityResource(CreateIdentityResourceRequest createIdentityResourceRequest,
                                                                             CancellationToken cancellationToken)
        {
            CreateIdentityResourceResponse createIdentityResourceResponse = null;

            List<IdentityResourceDetails> identityResourceList = await this.TestingContext.DockerHelper.SecurityServiceClient.GetIdentityResources(cancellationToken);

            if (identityResourceList == null || identityResourceList.Any() == false)
            {
                createIdentityResourceResponse = await this
                                                                                 .TestingContext.DockerHelper.SecurityServiceClient
                                                                                 .CreateIdentityResource(createIdentityResourceRequest, cancellationToken)
                                                                                 .ConfigureAwait(false);
                createIdentityResourceResponse.ShouldNotBeNull();
                createIdentityResourceResponse.IdentityResourceName.ShouldNotBeNullOrEmpty();

                this.TestingContext.IdentityResources.Add(createIdentityResourceResponse.IdentityResourceName);
            }
            else
            {
                if (identityResourceList.Where(i => i.Name == createIdentityResourceRequest.Name).Any())
                {
                    return;
                }

                createIdentityResourceResponse = await this
                                                       .TestingContext.DockerHelper.SecurityServiceClient
                                                       .CreateIdentityResource(createIdentityResourceRequest, cancellationToken)
                                                       .ConfigureAwait(false);
                createIdentityResourceResponse.ShouldNotBeNull();
                createIdentityResourceResponse.IdentityResourceName.ShouldNotBeNullOrEmpty();

                this.TestingContext.IdentityResources.Add(createIdentityResourceResponse.IdentityResourceName);
            }
        }

        [Given(@"I create the following api resources")]
        public async Task GivenICreateTheFollowingApiResources(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                // Get the scopes
                String scopes = SpecflowTableHelper.GetStringRowValue(tableRow, "Scopes");
                String userClaims = SpecflowTableHelper.GetStringRowValue(tableRow, "UserClaims");
                scopes = scopes.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));

                CreateApiResourceRequest createApiResourceRequest = new CreateApiResourceRequest
                {
                    Secret = SpecflowTableHelper.GetStringRowValue(tableRow, "Secret"),
                    Name = SpecflowTableHelper.GetStringRowValue(tableRow, "Name").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")),
                    Scopes = string.IsNullOrEmpty(scopes) ? null : scopes.Split(",").ToList(),
                    UserClaims = string.IsNullOrEmpty(userClaims) ? null : userClaims.Split(",").ToList(),
                    Description = SpecflowTableHelper.GetStringRowValue(tableRow, "Description"),
                    DisplayName = SpecflowTableHelper.GetStringRowValue(tableRow, "DisplayName")
                };
                CreateApiResourceResponse createApiResourceResponse =
                    await this.CreateApiResource(createApiResourceRequest, CancellationToken.None).ConfigureAwait(false);

                createApiResourceResponse.ShouldNotBeNull();
                createApiResourceResponse.ApiResourceName.ShouldNotBeNullOrEmpty();

                this.TestingContext.ApiResources.Add(createApiResourceResponse.ApiResourceName);
            }
        }

        [Given(@"I create the following users")]
        public async Task GivenICreateTheFollowingUsers(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                // Get the claims
                Dictionary<String, String> userClaims = null;
                String claims = SpecflowTableHelper.GetStringRowValue(tableRow, "Claims");
                if (string.IsNullOrEmpty(claims) == false)
                {
                    userClaims = new Dictionary<String, String>();
                    String[] claimList = claims.Split(",");
                    foreach (String claim in claimList)
                    {
                        // Split into claim name and value
                        String[] c = claim.Split(":");
                        userClaims.Add(c[0], c[1]);
                    }
                }

                String roles = SpecflowTableHelper.GetStringRowValue(tableRow, "Roles");
                roles = roles.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));

                CreateUserRequest createUserRequest = new CreateUserRequest
                {
                    EmailAddress = SpecflowTableHelper.GetStringRowValue(tableRow, "Email Address").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")),
                    FamilyName = SpecflowTableHelper.GetStringRowValue(tableRow, "Family Name"),
                    GivenName = SpecflowTableHelper.GetStringRowValue(tableRow, "Given Name"),
                    PhoneNumber = SpecflowTableHelper.GetStringRowValue(tableRow, "Phone Number"),
                    MiddleName = SpecflowTableHelper.GetStringRowValue(tableRow, "Middle name"),
                    Claims = userClaims,
                    Roles = string.IsNullOrEmpty(roles) ? null : roles.Split(",").ToList(),
                    Password = SpecflowTableHelper.GetStringRowValue(tableRow, "Password")
                };
                CreateUserResponse createUserResponse = await this.CreateUser(createUserRequest, CancellationToken.None).ConfigureAwait(false);

                createUserResponse.ShouldNotBeNull();
                createUserResponse.UserId.ShouldNotBe(Guid.Empty);

                this.TestingContext.Users.Add(createUserRequest.EmailAddress, createUserResponse.UserId);
            }
        }

        private async Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest,
                                                          CancellationToken cancellationToken)
        {
            CreateUserResponse createUserResponse = await this.TestingContext.DockerHelper.SecurityServiceClient.CreateUser(createUserRequest, cancellationToken).ConfigureAwait(false);
            return createUserResponse;
        }

        [Given(@"I create the following clients")]
        public async Task GivenICreateTheFollowingClients(Table table)
        {
            foreach (TableRow tableRow in table.Rows)
            {
                // Get the scopes
                String scopes = SpecflowTableHelper.GetStringRowValue(tableRow, "Scopes");
                // Get the grant types
                String grantTypes = SpecflowTableHelper.GetStringRowValue(tableRow, "GrantTypes");
                // Get the redirect uris
                String redirectUris = SpecflowTableHelper.GetStringRowValue(tableRow, "RedirectUris");
                // Get the post logout redirect uris
                String postLogoutRedirectUris = SpecflowTableHelper.GetStringRowValue(tableRow, "PostLogoutRedirectUris");

                scopes = scopes.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
                redirectUris = redirectUris.Replace("[port]", this.TestingContext.DockerHelper.EstateManagementUIPort.ToString());
                postLogoutRedirectUris = postLogoutRedirectUris.Replace("[port]", this.TestingContext.DockerHelper.EstateManagementUIPort.ToString());

                CreateClientRequest createClientRequest = new CreateClientRequest
                {
                    ClientId = SpecflowTableHelper.GetStringRowValue(tableRow, "ClientId").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")),
                    Secret = SpecflowTableHelper.GetStringRowValue(tableRow, "Secret"),
                    ClientName = SpecflowTableHelper.GetStringRowValue(tableRow, "Name"),
                    AllowedScopes = string.IsNullOrEmpty(scopes) ? null : scopes.Split(",").ToList(),
                    AllowedGrantTypes = string.IsNullOrEmpty(grantTypes) ? null : grantTypes.Split(",").ToList(),
                    ClientRedirectUris = string.IsNullOrEmpty(redirectUris) ? null : redirectUris.Split(",").ToList(),
                    ClientPostLogoutRedirectUris = string.IsNullOrEmpty(postLogoutRedirectUris) ? null : postLogoutRedirectUris.Split(",").ToList(),
                    ClientDescription = SpecflowTableHelper.GetStringRowValue(tableRow, "Description"),
                    RequireConsent = SpecflowTableHelper.GetBooleanValue(tableRow, "RequireConsent"),
                    AllowOfflineAccess = SpecflowTableHelper.GetBooleanValue(tableRow, "AllowOfflineAccess")

                };

                // Do the replacement on the Uris
                //if (createClientRequest.ClientRedirectUris != null && createClientRequest.ClientRedirectUris.Any())
                //{
                //    foreach (String clientRedirectUri in createClientRequest.ClientRedirectUris)
                //    {
                //        clientRedirectUri
                //    }
                //    createClientRequest.ClientRedirectUris.ForEach(c => c = c.Replace("[port]", this.TestingContext.DockerHelper.SecurityServiceTestUIPort.ToString()));
                //}

                //if (createClientRequest.ClientPostLogoutRedirectUris != null && createClientRequest.ClientPostLogoutRedirectUris.Any())
                //{
                //    createClientRequest.ClientPostLogoutRedirectUris.ForEach(c => c = c.Replace("[port]", this.TestingContext.DockerHelper.SecurityServiceTestUIPort.ToString()));
                //}

                CreateClientResponse createClientResponse = await this.CreateClient(createClientRequest, CancellationToken.None).ConfigureAwait(false);

                createClientResponse.ShouldNotBeNull();
                createClientResponse.ClientId.ShouldNotBeNullOrEmpty();

                this.TestingContext.Clients.Add(ClientDetails.Create(createClientResponse.ClientId, createClientRequest.Secret, createClientRequest.AllowedGrantTypes));
            }
        }

        [Given(@"I am on the application home page")]
        public void GivenIAmOnTheApplicationHomePage()
        {
            this.WebDriver.Navigate().GoToUrl($"http://localhost:{this.TestingContext.DockerHelper.EstateManagementUIPort}");
            this.WebDriver.Title.ShouldBe("Welcome");
        }

        [Given(@"I click on the Sign In Button")]
        public void GivenIClickOnTheSignInButton()
        {
            this.WebDriver.ClickButtonById("loginButton");
        }

        [Then(@"I am presented with a login screen")]
        public void ThenIAmPresentedWithALoginScreen()
        {
            IWebElement loginButton = this.WebDriver.FindButtonByText("Login");
            loginButton.ShouldNotBeNull();
        }

        [When(@"I login with the username '(.*)' and password '(.*)'")]
        public void WhenILoginWithTheUsernameAndPassword(String userName, String password)
        {
            this.WebDriver.FillIn("Username", userName.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")));
            this.WebDriver.FillIn("Password", password);
            this.WebDriver.ClickButtonByText("Login");
        }

        [Then(@"I am presented with the Estate Administrator Dashboard")]
        public void ThenIAmPresentedWithTheEstateAdministratorDashboard()
        {
            this.WebDriver.Title.ShouldBe("Dashboard");
        }

    }
}
