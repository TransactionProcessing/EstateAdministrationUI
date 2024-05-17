namespace EstateAdministrationUI.IntegrationTests.Common{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BoDi;
    using EstateManagement.Database.Contexts;
    using EstateManagement.Database.Entities;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Requests.Estate;
    using EstateManagement.DataTransferObjects.Requests.Merchant;
    using EstateManagement.DataTransferObjects.Requests.Operator;
    using EstateManagement.DataTransferObjects.Responses;
    using EstateManagement.DataTransferObjects.Responses.Estate;
    using EstateManagement.IntegrationTesting.Helpers;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;
    using Reqnroll;
    using SecurityService.DataTransferObjects;
    using SecurityService.DataTransferObjects.Requests;
    using SecurityService.DataTransferObjects.Responses;
    using SecurityService.IntegrationTesting.Helpers;
    using Shared.IntegrationTesting;
    using Shouldly;
    using TransactionProcessor.DataTransferObjects;
    using AssignOperatorRequest = EstateManagement.DataTransferObjects.Requests.Estate.AssignOperatorRequest;
    using MerchantOperatorResponse = EstateManagement.DataTransferObjects.Responses.Merchant.MerchantOperatorResponse;
    using MerchantResponse = EstateManagement.DataTransferObjects.Responses.Merchant.MerchantResponse;

    [Binding]
    [Scope(Tag = "shared")]
    public class SharedSteps{
        #region Fields

        private readonly EstateManagementSteps EstateManagementSteps;

        private readonly SecurityServiceSteps SecurityServiceSteps;

        private readonly TestingContext TestingContext;

        private readonly IWebDriver WebDriver;

        private readonly EstateAdministrationUISteps EstateAdministrationUiSteps;

        #endregion

        #region Constructors

        public SharedSteps(ScenarioContext scenarioContext, TestingContext testingContext, IObjectContainer container){

            var webDriver = container.Resolve<IWebDriver>(scenarioContext.ScenarioInfo.Title.Replace(" ", ""));
            
            this.TestingContext = testingContext;
            //this.WebDriver = webDriver;
            this.SecurityServiceSteps = new SecurityServiceSteps(this.TestingContext.DockerHelper.SecurityServiceClient);
            this.EstateManagementSteps = new EstateManagementSteps(this.TestingContext.DockerHelper.EstateClient, this.TestingContext.DockerHelper.HttpClient);
            this.EstateAdministrationUiSteps = new EstateAdministrationUISteps(webDriver, this.TestingContext.DockerHelper.EstateManagementUiPort);
        }

        #endregion

        #region Methods

        [Given(@"I am on the application home page")]
        public void GivenIAmOnTheApplicationHomePage(){
            this.EstateAdministrationUiSteps.NavigateToHomePage();
        }

        [Given(@"I click on the My Contracts sidebar option")]
        public async Task GivenIClickOnTheMyContractsSidebarOption(){
            await this.EstateAdministrationUiSteps.ClickContractsSidebarOption();
        }

        [Given(@"I click on the My Estate sidebar option")]
        public async Task GivenIClickOnTheMyEstateSidebarOption(){
            await this.EstateAdministrationUiSteps.ClickMyEstateSidebarOption();
        }

        [Given(@"I click on the My Merchants sidebar option")]
        public async Task GivenIClickOnTheMyMerchantsSidebarOption(){
            await this.EstateAdministrationUiSteps.ClickMyMerchantsSidebarOption();
        }

        [Given(@"I click on the My Operators sidebar option")]
        public async Task GivenIClickOnTheMyOperatorsSidebarOption(){
            await this.EstateAdministrationUiSteps.ClickMyOperatorsSidebarOption();
        }

        [Given(@"I click on the Sign In Button")]
        public async Task GivenIClickOnTheSignInButton(){
            await this.EstateAdministrationUiSteps.ClickOnTheSignInButton();
        }

        [Given(@"I create the following api resources")]
        public async Task GivenICreateTheFollowingApiResources(DataTable table){
            List<CreateApiResourceRequest> requests = table.Rows.ToCreateApiResourceRequests();
            await this.SecurityServiceSteps.GivenTheFollowingApiResourcesExist(requests);

            foreach (CreateApiResourceRequest createApiResourceRequest in requests){
                this.TestingContext.ApiResources.Add(createApiResourceRequest.Name);
            }
        }

        [Given(@"I create the following api scopes")]
        public async Task GivenICreateTheFollowingApiScopes(DataTable table){
            List<CreateApiScopeRequest> requests = table.Rows.ToCreateApiScopeRequests();
            await this.SecurityServiceSteps.GivenICreateTheFollowingApiScopes(requests);
        }

        [Given(@"I create the following clients")]
        public async Task GivenICreateTheFollowingClients(DataTable table){
            List<CreateClientRequest> requests = table.Rows.ToCreateClientRequests(this.TestingContext.DockerHelper.TestId, this.TestingContext.DockerHelper.EstateManagementUiPort);
            List<(String clientId, String secret, List<String> allowedGrantTypes)> clients = await this.SecurityServiceSteps.GivenTheFollowingClientsExist(requests);
            foreach ((String clientId, String secret, List<String> allowedGrantTypes) client in clients){
                this.TestingContext.AddClientDetails(client.clientId, client.secret, client.allowedGrantTypes);
            }
        }

        [Given(@"I create the following identity resources")]
        public async Task GivenICreateTheFollowingIdentityResources(DataTable table){
            foreach (DataTableRow tableRow in table.Rows)
            {
                // Get the scopes
                String userClaims = ReqnrollTableHelper.GetStringRowValue(tableRow, "UserClaims");

                CreateIdentityResourceRequest createIdentityResourceRequest = new CreateIdentityResourceRequest
                {
                    Name = ReqnrollTableHelper.GetStringRowValue(tableRow, "Name"),
                    Claims = String.IsNullOrEmpty(userClaims) ? null : userClaims.Split(",").ToList(),
                    Description = ReqnrollTableHelper.GetStringRowValue(tableRow, "Description"),
                    DisplayName = ReqnrollTableHelper.GetStringRowValue(tableRow, "DisplayName")
                };

                await this.CreateIdentityResource(createIdentityResourceRequest, CancellationToken.None).ConfigureAwait(false);
            }
        }

        [Given(@"I create the following roles")]
        public async Task GivenICreateTheFollowingRoles(DataTable table){
            List<CreateRoleRequest> requests = table.Rows.ToCreateRoleRequests();
            List<(String, Guid)> responses = await this.SecurityServiceSteps.GivenICreateTheFollowingRoles(requests, CancellationToken.None);

            foreach ((String, Guid) response in responses){
                this.TestingContext.Roles.Add(response.Item1, response.Item2);
            }
        }

        [Given(@"I create the following users")]
        public async Task GivenICreateTheFollowingUsers(DataTable table){
            List<CreateUserRequest> requests = table.Rows.ToCreateUserRequests();
            foreach (CreateUserRequest createUserRequest in requests){
                createUserRequest.EmailAddress = createUserRequest.EmailAddress.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
                //createUserRequest.Roles.ForEach(r => r.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")));
                List<String> newRoles = new List<String>();
                foreach (String role in createUserRequest.Roles){
                    newRoles.Add(role.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N")));
                }
                createUserRequest.Roles = newRoles;
            }

            List<(String, Guid)> results = await this.SecurityServiceSteps.GivenICreateTheFollowingUsers(requests, CancellationToken.None);

            foreach ((String, Guid) response in results)
            {
                this.TestingContext.Users.Add(response.Item1, response.Item2);
            }
        }

        [Given(@"I have a token to access the estate management resource")]
        public async Task GivenIHaveATokenToAccessTheEstateManagementResource(DataTable table){
            DataTableRow firstRow = table.Rows.First();
            String clientId = ReqnrollTableHelper.GetStringRowValue(firstRow, "ClientId").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
            ClientDetails clientDetails = this.TestingContext.GetClientDetails(clientId);

            this.TestingContext.AccessToken = await this.SecurityServiceSteps.GetClientToken(clientDetails.ClientId, clientDetails.ClientSecret, CancellationToken.None);
        }

        [Given(@"I have created the following estates")]
        public async Task GivenIHaveCreatedTheFollowingEstates(DataTable table){
            List<CreateEstateRequest> requests = table.Rows.ToCreateEstateRequests();
            
            List<EstateResponse> verifiedEstates = await this.EstateManagementSteps.WhenICreateTheFollowingEstates(this.TestingContext.AccessToken, requests);

            foreach (EstateResponse verifiedEstate in verifiedEstates){

                await Retry.For(async () => {
                                    String databaseName = $"EstateReportingReadModel{verifiedEstate.EstateId}";
                                    var connString = Setup.GetLocalConnectionString(databaseName);
                                    connString = $"{connString};Encrypt=false";
                                    var ctx = new EstateManagementSqlServerContext(connString);

                                    var estates = ctx.Estates.ToList();
                                    estates.Count.ShouldBe(1);

                                    this.TestingContext.AddEstateDetails(verifiedEstate.EstateId, verifiedEstate.EstateName, verifiedEstate.EstateReference);
                                    this.TestingContext.Logger.LogInformation($"Estate {verifiedEstate.EstateName} created with Id {verifiedEstate.EstateId}");
                                });
            }
        }

        [Given(@"I have created the following operators")]
        public async Task GivenIHaveCreatedTheFollowingOperators(DataTable table){
            List<(EstateDetails estate, CreateOperatorRequest request)> requests = table.Rows.ToCreateOperatorRequests(this.TestingContext.Estates);

            List<(Guid, EstateOperatorResponse)> results = await this.EstateManagementSteps.WhenICreateTheFollowingOperators(this.TestingContext.AccessToken, requests);

            foreach ((Guid, EstateOperatorResponse) result in results){
                this.TestingContext.Logger.LogInformation($"Operator {result.Item2.Name} created with Id {result.Item2.OperatorId} for Estate {result.Item1}");
            }
        }

        [Given("I have assigned the following operators to the estates")]
        public async Task GivenIHaveAssignedTheFollowingOperatorsToTheEstates(DataTable dataTable)
        {
            List<(EstateDetails estate, AssignOperatorRequest request)> requests = dataTable.Rows.ToAssignOperatorToEstateRequests(this.TestingContext.Estates);

            await this.EstateManagementSteps.GivenIHaveAssignedTheFollowingOperatorsToTheEstates(this.TestingContext.AccessToken, requests);

            // TODO Verify
        }

        [Then(@"I am presented the make merchant deposit screen")]
        public async Task ThenIAmPresentedTheMakeMerchantDepositScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheMakeMerchantDepositScreen();
        }

        [Then(@"I am presented the merchant details screen for '(.*)'")]
        public async Task ThenIAmPresentedTheMerchantDetailsScreenFor(String merchantName){
            await this.EstateAdministrationUiSteps.VerifyOnTheTheMerchantDetailsScreen(merchantName);
        }

        [Then(@"I am presented the new contract screen")]
        public async Task ThenIAmPresentedTheNewContractScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheNewContractScreen();
        }

        [Then(@"I am presented the new merchant screen")]
        public async Task ThenIAmPresentedTheNewMerchantScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheNewMerchantScreen();
        }

        [Then(@"I am presented the new operator screen")]
        public async Task ThenIAmPresentedTheNewOperatorScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheNewOperatorScreen();
        }

        [Then(@"I am presented the new product screen")]
        public async Task ThenIAmPresentedTheNewProductScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheNewProductScreen();
        }

        [Then(@"I am presented the new transaction fee screen")]
        public async Task ThenIAmPresentedTheNewTransactionFeeScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheNewTransactionFeeScreen();
        }

        [Then(@"I am presented with a login screen")]
        public async Task ThenIAmPresentedWithALoginScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheLoginScreen();
        }

        [Then(@"I am presented with the Contracts List Screen")]
        public async Task ThenIAmPresentedWithTheContractsListScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheContractsListScreen();
        }

        [Then(@"I am presented with the Estate Administrator Dashboard")]
        public async Task ThenIAmPresentedWithTheEstateAdministratorDashboard(){
            await this.EstateAdministrationUiSteps.VerifyOnTheDashboard();
        }

        [Then(@"I am presented with the Estate Details Screen")]
        public async Task ThenIAmPresentedWithTheEstateDetailsScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheEstateDetailsScreen();
        }

        [Then(@"I am presented with the Merchants List Screen")]
        public async Task ThenIAmPresentedWithTheMerchantsListScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheMerchantsListScreen();
        }

        [Then(@"I am presented with the Operators List Screen")]
        public async Task ThenIAmPresentedWithTheOperatorsListScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheOperatorsListScreen();
        }

        [Then(@"I am presented with the Products List Screen")]
        public async Task ThenIAmPresentedWithTheProductsListScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheProductsListScreen();
        }

        [Then(@"I am presented with the Transaction Fee List Screen")]
        public async Task ThenIAmPresentedWithTheTransactionFeeListScreen(){
            await this.EstateAdministrationUiSteps.VerifyOnTheTransactionFeeListScreen();
        }

        [Then(@"My Estate Details will be shown")]
        public async Task ThenMyEstateDetailsWillBeShown(DataTable table){
            DataTableRow tableRow = table.Rows.Single();
            String estateName  = ReqnrollTableHelper.GetStringRowValue(tableRow, "EstateName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
            await this.EstateAdministrationUiSteps.VerifyTheCorrectEstateDetailsAreDisplayed(estateName);
        }

        [Then(@"the available balance for the merchant should be (.*)")]
        public async Task ThenTheAvailableBalanceForTheMerchantShouldBe(Decimal availableBalance){
            await this.EstateAdministrationUiSteps.VerifyTheAvailableBalanceIsDisplayed(availableBalance);
        }

        [Then(@"the following contract details are in the list")]
        public async Task ThenTheFollowingContractDetailsAreInTheList(DataTable table){
            List<String> contractDescriptions = new List<String>();
            foreach (DataTableRow tableRow in table.Rows){
                contractDescriptions.Add(ReqnrollTableHelper.GetStringRowValue(tableRow, "ContractDescription"));
            }

            await this.EstateAdministrationUiSteps.VerifyTheContractDetailsAreInTheList(contractDescriptions);
        }

        [Then(@"the following fee details are in the list")]
        public async Task ThenTheFollowingFeeDetailsAreInTheList(DataTable table){
            List<String> feeDescriptions = new List<String>();
            foreach (DataTableRow tableRow in table.Rows){
                feeDescriptions.Add(ReqnrollTableHelper.GetStringRowValue(tableRow, "Description"));
            }

            await this.EstateAdministrationUiSteps.VerifyTheFeeDetailsAreInTheList(feeDescriptions);
        }

        [Then(@"the following merchants details are in the list")]
        public async Task ThenTheFollowingMerchantsDetailsAreInTheList(DataTable table){
            List<EstateAdministrationUISteps.MerchantDetails> merchantDetailsList = new List<EstateAdministrationUISteps.MerchantDetails>();
            foreach (DataTableRow tableRow in table.Rows){
                EstateAdministrationUISteps.MerchantDetails m = new EstateAdministrationUISteps.MerchantDetails(tableRow["MerchantName"],
                                                                                                                tableRow["ContactName"],
                                                                                                                tableRow["AddressLine1"],
                                                                                                                tableRow["Town"],
                                                                                                                tableRow["NumberOfUsers"],
                                                                                                                tableRow["NumberOfDevices"],
                                                                                                                tableRow["NumberOfOperators"]);
                merchantDetailsList.Add(m);
            }

            await this.EstateAdministrationUiSteps.VerifyMerchantDetailsAreInTheList(merchantDetailsList);
        }

        [Then(@"the following operator details are in the list")]
        public async Task ThenTheFollowingOperatorDetailsAreInTheList(DataTable table){
            List<String> operatorsList = new List<String>();
            foreach (DataTableRow tableRow in table.Rows)
            {
                operatorsList.Add(ReqnrollTableHelper.GetStringRowValue(tableRow, "OperatorName"));
            }

            await this.EstateAdministrationUiSteps.VerifyOperatorDetailsAreInTheList(operatorsList);
        }

        [Then(@"the following product details are in the list")]
        public async Task ThenTheFollowingProductDetailsAreInTheList(DataTable table){
            List<String> productsList = new List<String>();
            foreach (DataTableRow tableRow in table.Rows)
            {
                productsList.Add(ReqnrollTableHelper.GetStringRowValue(tableRow, "ProductName"));
            }

            await this.EstateAdministrationUiSteps.VerifyProductDetailsAreInTheList(productsList);
        }

        [Then(@"the merchants settlement schedule is '([^']*)'")]
        public async Task ThenTheMerchantsSettlementScheduleIs(String settlementSchedule){
            await this.EstateAdministrationUiSteps.VerifyMerchantsSettlementSchedule(settlementSchedule);
        }

        [When(@"I add the following devices to the merchant")]
        public async Task WhenIAddTheFollowingDevicesToTheMerchant(DataTable table){
            List<(EstateDetails, Guid, AddMerchantDeviceRequest)> requests = table.Rows.ToAddMerchantDeviceRequests(this.TestingContext.Estates);

            List<(EstateDetails, EstateManagement.DataTransferObjects.Responses.Merchant.MerchantResponse, String)> results = await this.EstateManagementSteps.GivenIHaveAssignedTheFollowingDevicesToTheMerchants(this.TestingContext.AccessToken, requests);
            foreach ((EstateDetails, EstateManagement.DataTransferObjects.Responses.Merchant.MerchantResponse, String) result in results){
                this.TestingContext.Logger.LogInformation($"Device {result.Item3} assigned to Merchant {result.Item2.MerchantName} Estate {result.Item1.EstateName}");
            }
        }

        [When(@"I assign the following  operator to the merchants")]
        public async Task WhenIAssignTheFollowingOperatorToTheMerchants(DataTable table){
            List<(EstateDetails, Guid, EstateManagement.DataTransferObjects.Requests.Merchant.AssignOperatorRequest)> requests = table.Rows.ToAssignOperatorRequests(this.TestingContext.Estates);

            List<(EstateDetails, EstateManagement.DataTransferObjects.Responses.Merchant.MerchantOperatorResponse)> results = await this.EstateManagementSteps.WhenIAssignTheFollowingOperatorToTheMerchants(this.TestingContext.AccessToken, requests);

            foreach ((EstateDetails, MerchantOperatorResponse) result in results){
                this.TestingContext.Logger.LogInformation($"Operator {result.Item2.Name} assigned to Estate {result.Item1.EstateName}");
            }
        }

        [When(@"I click the Add New Contract button")]
        public async Task WhenIClickTheAddNewContractButton(){
            await this.EstateAdministrationUiSteps.ClickAddNewContractButton();
        }

        [When(@"I click the Add New Merchant button")]
        public async Task WhenIClickTheAddNewMerchantButton(){
            await this.EstateAdministrationUiSteps.ClickAddNewMerchantButton();
        }

        [When(@"I click the Add New Operator button")]
        public async Task WhenIClickTheAddNewOperatorButton(){
            await this.EstateAdministrationUiSteps.ClickAddNewOperatorButton();
        }

        [When(@"I click the Add New Product button")]
        public async Task WhenIClickTheAddNewProductButton(){
            await this.EstateAdministrationUiSteps.ClickAddNewProductButton();
        }

        [When(@"I click the Add New Transaction Fee button")]
        public async Task WhenIClickTheAddNewTransactionFeeButton(){
            await this.EstateAdministrationUiSteps.ClickAddNewTransactionFeeButton();
        }

        [When(@"I click the Create Contract button")]
        public async Task WhenIClickTheCreateContractButton(){
            await this.EstateAdministrationUiSteps.ClickTheCreateContractButton();
        }

        [When(@"I click the Create Merchant button")]
        public async Task WhenIClickTheCreateMerchantButton(){
            await this.EstateAdministrationUiSteps.ClickTheCreateMerchantButton();
        }

        [When(@"I click the Create Operator button")]
        public async Task WhenIClickTheCreateOperatorButton(){
            await this.EstateAdministrationUiSteps.ClickTheCreateOperatorButton();
        }

        [When(@"I click the Create Product button")]
        public async Task WhenIClickTheCreateProductButton(){
            await this.EstateAdministrationUiSteps.ClickTheCreateProductButton();
        }

        [When(@"I click the Create Transaction Fee button")]
        public async Task WhenIClickTheCreateTransactionFeeButton(){
            await this.EstateAdministrationUiSteps.ClickTheCreateTransactionFeeButton();
        }

        [When(@"I click the Make Deposit button for '(.*)' from the merchant list")]
        public async Task WhenIClickTheMakeDepositButtonForFromTheMerchantList(String merchantName){
            await this.EstateAdministrationUiSteps.ClickTheMakeDepositButtonForTheMerchant(merchantName);
        }

        [When(@"I click the Products Link for '(.*)'")]
        public async Task WhenIClickTheProductsLinkFor(String contractDescription){
            await this.EstateAdministrationUiSteps.ClickTheProductsLinkForContract(contractDescription);
        }

        [When(@"I click the Transaction Fees Link for '(.*)'")]
        public async Task WhenIClickTheTransactionFeesLinkFor(String productName){
            await this.EstateAdministrationUiSteps.ClickTheTransactionFeesLinkForTheProduct(productName);
        }

        [Given("I create the following merchants")]
        [When(@"I create the following merchants")]
        public async Task WhenICreateTheFollowingMerchants(DataTable table){
            List<(EstateDetails estate, CreateMerchantRequest)> requests = table.Rows.ToCreateMerchantRequests(this.TestingContext.Estates);

            List<EstateManagement.DataTransferObjects.Responses.Merchant.MerchantResponse> verifiedMerchants = await this.EstateManagementSteps.WhenICreateTheFollowingMerchants(this.TestingContext.AccessToken, requests);

            foreach (MerchantResponse verifiedMerchant in verifiedMerchants){
                EstateDetails estateDetails = this.TestingContext.GetEstateDetails(verifiedMerchant.EstateId);
                estateDetails.AddMerchant(verifiedMerchant);
                this.TestingContext.Logger.LogInformation($"Merchant {verifiedMerchant.MerchantName} created with Id {verifiedMerchant.MerchantId} for Estate {estateDetails.EstateName}");
            }
        }

        [When(@"I create the following security users")]
        [Given("I have created the following security users")]
        public async Task WhenICreateTheFollowingSecurityUsers(DataTable table){
            List<CreateNewUserRequest> createUserRequests = table.Rows.ToCreateNewUserRequests(this.TestingContext.Estates);
            await this.EstateManagementSteps.WhenICreateTheFollowingSecurityUsers(this.TestingContext.AccessToken, createUserRequests, this.TestingContext.Estates);
        }

        [When(@"I enter the following new contract details")]
        public async Task WhenIEnterTheFollowingNewContractDetails(DataTable table){
            DataTableRow tableRow = table.Rows.Single();

            String contractDescription = ReqnrollTableHelper.GetStringRowValue(tableRow, "ContractDescription");
            String operatorName = ReqnrollTableHelper.GetStringRowValue(tableRow, "OperatorName").Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));

            await this.EstateAdministrationUiSteps.EnterContractDetails(contractDescription,
                                                                        operatorName);
        }

        [When(@"I enter the following new merchant details")]
        public async Task WhenIEnterTheFollowingNewMerchantDetails(DataTable table){
            DataTableRow tableRow = table.Rows.Single();

            String merchantName = ReqnrollTableHelper.GetStringRowValue(tableRow, "MerchantName");
            String addressLine1 = ReqnrollTableHelper.GetStringRowValue(tableRow, "AddressLine1");
            String town = ReqnrollTableHelper.GetStringRowValue(tableRow, "Town");
            String region = ReqnrollTableHelper.GetStringRowValue(tableRow, "Region");
            String postCode = ReqnrollTableHelper.GetStringRowValue(tableRow, "PostCode");
            String country = ReqnrollTableHelper.GetStringRowValue(tableRow, "Country");
            String contactName = ReqnrollTableHelper.GetStringRowValue(tableRow, "ContactName");
            String contactEmail = ReqnrollTableHelper.GetStringRowValue(tableRow, "ContactEmail");
            String contactPhoneNumber = ReqnrollTableHelper.GetStringRowValue(tableRow, "ContactPhoneNumber");
            String settlementSchedule = ReqnrollTableHelper.GetStringRowValue(tableRow, "SettlementSchedule");

            await this.EstateAdministrationUiSteps.EnterMerchantDetails(merchantName,
                                                                        addressLine1,
                                                                        town,
                                                                        region,
                                                                        postCode,
                                                                        country,
                                                                        contactName,
                                                                        contactEmail,
                                                                        contactPhoneNumber,
                                                                        settlementSchedule);
        }

        [When(@"I enter the following new operator details")]
        public async Task WhenIEnterTheFollowingNewOperatorDetails(DataTable table){
            DataTableRow tableRow = table.Rows.Single();

            String operatorName = ReqnrollTableHelper.GetStringRowValue(tableRow, "OperatorName");
            await this.EstateAdministrationUiSteps.EnterOperatorDetails(operatorName);
        }

        [When(@"I enter the following new product details")]
        public async Task WhenIEnterTheFollowingNewProductDetails(DataTable table){
            DataTableRow productDetails = table.Rows.Single();
            String productName = ReqnrollTableHelper.GetStringRowValue(productDetails, "ProductName");
            String displayText = ReqnrollTableHelper.GetStringRowValue(productDetails, "DisplayText");
            String productValue = ReqnrollTableHelper.GetStringRowValue(productDetails, "Value");
            String productType = ReqnrollTableHelper.GetStringRowValue(productDetails, "ProductType");

            await this.EstateAdministrationUiSteps.EnterProductDetails(productName, displayText, productValue, productType);
        }

        [When(@"I enter the following new transaction fee details")]
        public async Task WhenIEnterTheFollowingNewTransactionFeeDetails(DataTable table){
            DataTableRow productDetails = table.Rows.Single();
            String description = ReqnrollTableHelper.GetStringRowValue(productDetails, "Description");
            String calculationType = ReqnrollTableHelper.GetStringRowValue(productDetails, "CalculationType");
            String feeType = ReqnrollTableHelper.GetStringRowValue(productDetails, "FeeType");
            String feeValue = ReqnrollTableHelper.GetStringRowValue(productDetails, "Value");

            await this.EstateAdministrationUiSteps.EnterTransactionFeeDetails(description, calculationType, feeType, feeValue);
        }

        [When(@"I login with the username '(.*)' and password '(.*)'")]
        public async Task WhenILoginWithTheUsernameAndPassword(String userName, String password){

            String username = userName.Replace("[id]", this.TestingContext.DockerHelper.TestId.ToString("N"));
            await this.EstateAdministrationUiSteps.Login(username, password);
        }

        [When(@"I make the following deposit")]
        public async Task WhenIMakeTheFollowingDeposit(DataTable table){
            DataTableRow depositDetails = table.Rows.Single();
            Decimal depositAmount = ReqnrollTableHelper.GetDecimalValue(depositDetails, "DepositAmount");
            String depositDateString = ReqnrollTableHelper.GetStringRowValue(depositDetails, "DepositDate");
            DateTime depositDate = ReqnrollTableHelper.GetDateForDateString(depositDateString, DateTime.Now);
            String depositReference = ReqnrollTableHelper.GetStringRowValue(depositDetails, "DepositReference");

            await this.EstateAdministrationUiSteps.MakeMerchantDeposit(depositDate, depositAmount, depositReference);
        }

        [When(@"I select '(.*)' from the merchant list")]
        public async Task WhenISelectFromTheMerchantList(String merchantName){
            await this.EstateAdministrationUiSteps.ClickTheMerchantLinkForMerchant(merchantName);
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
                if (identityResourceList.Any(i => i.Name == createIdentityResourceRequest.Name))
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

        #endregion
    }
}