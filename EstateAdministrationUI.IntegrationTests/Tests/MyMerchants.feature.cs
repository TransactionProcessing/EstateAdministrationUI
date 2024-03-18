﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:1.0.0.0
//      Reqnroll Generator Version:1.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace EstateAdministrationUI.IntegrationTests.Tests
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "1.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "base")]
    [Xunit.TraitAttribute("Category", "shared")]
    public partial class MyMerchantsFeature : object, Xunit.IClassFixture<MyMerchantsFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "base",
                "shared"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "MyMerchants.feature"
#line hidden
        
        public MyMerchantsFeature(MyMerchantsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(null, Reqnroll.xUnit.ReqnrollPlugin.XUnitParallelWorkerTracker.Instance.GetWorkerId());
            Reqnroll.FeatureInfo featureInfo = new Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Tests", "MyMerchants", null, ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            string testWorkerId = testRunner.TestWorkerId;
            await testRunner.OnFeatureEndAsync();
            testRunner = null;
            Reqnroll.xUnit.ReqnrollPlugin.XUnitParallelWorkerTracker.Instance.ReleaseWorker(testWorkerId);
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        public virtual async System.Threading.Tasks.Task FeatureBackgroundAsync()
        {
#line 4
#line hidden
            Reqnroll.Table table25 = new Reqnroll.Table(new string[] {
                        "Role Name"});
            table25.AddRow(new string[] {
                        "Estate"});
            table25.AddRow(new string[] {
                        "Merchant"});
#line 6
 await testRunner.GivenAsync("I create the following roles", ((string)(null)), table25, "Given ");
#line hidden
            Reqnroll.Table table26 = new Reqnroll.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description"});
            table26.AddRow(new string[] {
                        "estateManagement",
                        "Estate Managememt REST Scope",
                        "A scope for Estate Managememt REST"});
            table26.AddRow(new string[] {
                        "transactionProcessor",
                        "Transaction Processor REST Scope",
                        "Scope for Transaction Processor REST"});
            table26.AddRow(new string[] {
                        "fileProcessor",
                        "File Processor REST Scope",
                        "Scope for File Processor REST"});
#line 11
 await testRunner.GivenAsync("I create the following api scopes", ((string)(null)), table26, "Given ");
#line hidden
            Reqnroll.Table table27 = new Reqnroll.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Secret",
                        "Scopes",
                        "UserClaims"});
            table27.AddRow(new string[] {
                        "estateManagement",
                        "Estate Managememt REST",
                        "Secret1",
                        "estateManagement",
                        "merchantId,estateId,role"});
            table27.AddRow(new string[] {
                        "transactionProcessor",
                        "Transaction Processor REST",
                        "Secret1",
                        "transactionProcessor",
                        "merchantId,estateId,role"});
            table27.AddRow(new string[] {
                        "fileProcessor",
                        "File Processor REST",
                        "Secret1",
                        "fileProcessor",
                        "merchantId,estateId,role"});
#line 17
 await testRunner.GivenAsync("I create the following api resources", ((string)(null)), table27, "Given ");
#line hidden
            Reqnroll.Table table28 = new Reqnroll.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description",
                        "UserClaims"});
            table28.AddRow(new string[] {
                        "openid",
                        "Your user identifier",
                        "",
                        "sub"});
            table28.AddRow(new string[] {
                        "profile",
                        "User profile",
                        "Your user profile information (first name, last name, etc.)",
                        "name,role,email,given_name,middle_name,family_name,estateId,merchantId"});
            table28.AddRow(new string[] {
                        "email",
                        "Email",
                        "Email and Email Verified Flags",
                        "email_verified,email"});
#line 23
 await testRunner.GivenAsync("I create the following identity resources", ((string)(null)), table28, "Given ");
#line hidden
            Reqnroll.Table table29 = new Reqnroll.Table(new string[] {
                        "ClientId",
                        "Name",
                        "Secret",
                        "Scopes",
                        "GrantTypes",
                        "RedirectUris",
                        "PostLogoutRedirectUris",
                        "RequireConsent",
                        "AllowOfflineAccess",
                        "ClientUri"});
            table29.AddRow(new string[] {
                        "serviceClient",
                        "Service Client",
                        "Secret1",
                        "estateManagement,transactionProcessor",
                        "client_credentials",
                        "",
                        "",
                        "",
                        "",
                        ""});
            table29.AddRow(new string[] {
                        "estateUIClient",
                        "Merchant Client",
                        "Secret1",
                        "estateManagement,fileProcessor,transactionProcessor,openid,email,profile",
                        "hybrid",
                        "https://localhost:[port]/signin-oidc",
                        "https://localhost:[port]/signout-oidc",
                        "false",
                        "true",
                        "https://[url]:[port]"});
#line 29
 await testRunner.GivenAsync("I create the following clients", ((string)(null)), table29, "Given ");
#line hidden
            Reqnroll.Table table30 = new Reqnroll.Table(new string[] {
                        "ClientId"});
            table30.AddRow(new string[] {
                        "serviceClient"});
#line 34
 await testRunner.GivenAsync("I have a token to access the estate management resource", ((string)(null)), table30, "Given ");
#line hidden
            Reqnroll.Table table31 = new Reqnroll.Table(new string[] {
                        "EstateName"});
            table31.AddRow(new string[] {
                        "Test Estate"});
#line 38
 await testRunner.GivenAsync("I have created the following estates", ((string)(null)), table31, "Given ");
#line hidden
            Reqnroll.Table table32 = new Reqnroll.Table(new string[] {
                        "EstateName",
                        "OperatorName",
                        "RequireCustomMerchantNumber",
                        "RequireCustomTerminalNumber"});
            table32.AddRow(new string[] {
                        "Test Estate",
                        "Test Operator",
                        "True",
                        "True"});
#line 42
 await testRunner.AndAsync("I have created the following operators", ((string)(null)), table32, "And ");
#line hidden
            Reqnroll.Table table33 = new Reqnroll.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "EstateName"});
            table33.AddRow(new string[] {
                        "estateuser@testestate1.co.uk",
                        "123456",
                        "TestEstate",
                        "User1",
                        "Test Estate"});
#line 46
 await testRunner.AndAsync("I have created the following security users", ((string)(null)), table33, "And ");
#line hidden
            Reqnroll.Table table34 = new Reqnroll.Table(new string[] {
                        "MerchantName",
                        "SettlementSchedule",
                        "AddressLine1",
                        "Town",
                        "Region",
                        "Country",
                        "ContactName",
                        "EmailAddress",
                        "EstateName"});
            table34.AddRow(new string[] {
                        "Test Merchant 1",
                        "Immediate",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant1.co.uk",
                        "Test Estate"});
            table34.AddRow(new string[] {
                        "Test Merchant 2",
                        "Weekly",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant2.co.uk",
                        "Test Estate"});
            table34.AddRow(new string[] {
                        "Test Merchant 3",
                        "Monthly",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant3.co.uk",
                        "Test Estate"});
#line 50
 await testRunner.GivenAsync("I create the following merchants", ((string)(null)), table34, "Given ");
#line hidden
            Reqnroll.Table table35 = new Reqnroll.Table(new string[] {
                        "OperatorName",
                        "MerchantName",
                        "MerchantNumber",
                        "TerminalNumber",
                        "EstateName"});
            table35.AddRow(new string[] {
                        "Test Operator",
                        "Test Merchant 1",
                        "00000001",
                        "10000001",
                        "Test Estate"});
            table35.AddRow(new string[] {
                        "Test Operator",
                        "Test Merchant 2",
                        "00000001",
                        "10000001",
                        "Test Estate"});
            table35.AddRow(new string[] {
                        "Test Operator",
                        "Test Merchant 3",
                        "00000001",
                        "10000001",
                        "Test Estate"});
#line 56
 await testRunner.WhenAsync("I assign the following  operator to the merchants", ((string)(null)), table35, "When ");
#line hidden
            Reqnroll.Table table36 = new Reqnroll.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "MerchantName",
                        "EstateName"});
            table36.AddRow(new string[] {
                        "merchantuser1@testmerchant1.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 1",
                        "Test Estate"});
            table36.AddRow(new string[] {
                        "merchantuser1@testmerchant2.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 2",
                        "Test Estate"});
            table36.AddRow(new string[] {
                        "merchantuser1@testmerchant3.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 3",
                        "Test Estate"});
#line 62
 await testRunner.WhenAsync("I create the following security users", ((string)(null)), table36, "When ");
#line hidden
            Reqnroll.Table table37 = new Reqnroll.Table(new string[] {
                        "DeviceIdentifier",
                        "MerchantName",
                        "EstateName"});
            table37.AddRow(new string[] {
                        "TestDevice1",
                        "Test Merchant 1",
                        "Test Estate"});
            table37.AddRow(new string[] {
                        "TestDevice2",
                        "Test Merchant 2",
                        "Test Estate"});
            table37.AddRow(new string[] {
                        "TestDevice3",
                        "Test Merchant 3",
                        "Test Estate"});
#line 68
 await testRunner.WhenAsync("I add the following devices to the merchant", ((string)(null)), table37, "When ");
#line hidden
#line 74
 await testRunner.GivenAsync("I am on the application home page", ((string)(null)), ((Reqnroll.Table)(null)), "Given ");
#line hidden
#line 76
 await testRunner.AndAsync("I click on the Sign In Button", ((string)(null)), ((Reqnroll.Table)(null)), "And ");
#line hidden
#line 78
 await testRunner.ThenAsync("I am presented with a login screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
#line 80
 await testRunner.WhenAsync("I login with the username \'estateuser@testestate1.co.uk\' and password \'123456\'", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
#line 82
 await testRunner.ThenAsync("I am presented with the Estate Administrator Dashboard", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Dashboard")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "Dashboard")]
        public async System.Threading.Tasks.Task Dashboard()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            Reqnroll.ScenarioInfo scenarioInfo = new Reqnroll.ScenarioInfo("Dashboard", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 84
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 4
await this.FeatureBackgroundAsync();
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Make Merchant Deposit")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "Make Merchant Deposit")]
        [Xunit.TraitAttribute("Category", "PRTest")]
        public async System.Threading.Tasks.Task MakeMerchantDeposit()
        {
            string[] tagsOfScenario = new string[] {
                    "PRTest"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            Reqnroll.ScenarioInfo scenarioInfo = new Reqnroll.ScenarioInfo("Make Merchant Deposit", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 87
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 4
await this.FeatureBackgroundAsync();
#line hidden
#line 88
 await testRunner.GivenAsync("I click on the My Merchants sidebar option", ((string)(null)), ((Reqnroll.Table)(null)), "Given ");
#line hidden
#line 89
 await testRunner.ThenAsync("I am presented with the Merchants List Screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
                Reqnroll.Table table38 = new Reqnroll.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table38.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table38.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table38.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 90
 await testRunner.AndAsync("the following merchants details are in the list", ((string)(null)), table38, "And ");
#line hidden
#line 95
 await testRunner.WhenAsync("I click the Make Deposit button for \'Test Merchant 1\' from the merchant list", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
#line 96
 await testRunner.ThenAsync("I am presented the make merchant deposit screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
                Reqnroll.Table table39 = new Reqnroll.Table(new string[] {
                            "DepositAmount",
                            "DepositDate",
                            "DepositReference"});
                table39.AddRow(new string[] {
                            "1000",
                            "Today",
                            "Test Deposit 1"});
#line 97
 await testRunner.WhenAsync("I make the following deposit", ((string)(null)), table39, "When ");
#line hidden
#line 100
 await testRunner.ThenAsync("I am presented with the Merchants List Screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
#line 101
 await testRunner.WhenAsync("I select \'Test Merchant 1\' from the merchant list", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
#line 102
 await testRunner.ThenAsync("I am presented the merchant details screen for \'Test Merchant 1\'", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
#line 103
 await testRunner.AndAsync("the available balance for the merchant should be 1000.00", ((string)(null)), ((Reqnroll.Table)(null)), "And ");
#line hidden
#line 104
 await testRunner.GivenAsync("I click on the My Merchants sidebar option", ((string)(null)), ((Reqnroll.Table)(null)), "Given ");
#line hidden
#line 105
 await testRunner.ThenAsync("I am presented with the Merchants List Screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
                Reqnroll.Table table40 = new Reqnroll.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table40.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table40.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table40.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 106
 await testRunner.AndAsync("the following merchants details are in the list", ((string)(null)), table40, "And ");
#line hidden
#line 111
 await testRunner.WhenAsync("I click the Add New Merchant button", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
#line 112
 await testRunner.ThenAsync("I am presented the new merchant screen", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
                Reqnroll.Table table41 = new Reqnroll.Table(new string[] {
                            "MerchantName",
                            "SettlementSchedule",
                            "AddressLine1",
                            "Town",
                            "Region",
                            "PostCode",
                            "Country",
                            "ContactName",
                            "ContactEmail",
                            "ContactPhoneNumber"});
                table41.AddRow(new string[] {
                            "Test Merchant 4",
                            "Monthly",
                            "Address Line 1",
                            "TestTown",
                            "TestRegion",
                            "TE57 1NG",
                            "United Kingdom",
                            "Test Contact 4",
                            "testcontact@testmerchant4.co.uk",
                            "0123456789"});
#line 113
 await testRunner.WhenAsync("I enter the following new merchant details", ((string)(null)), table41, "When ");
#line hidden
#line 116
 await testRunner.WhenAsync("I click the Create Merchant button", ((string)(null)), ((Reqnroll.Table)(null)), "When ");
#line hidden
#line 117
 await testRunner.ThenAsync("I am presented the merchant details screen for \'Test Merchant 4\'", ((string)(null)), ((Reqnroll.Table)(null)), "Then ");
#line hidden
#line 118
 await testRunner.AndAsync("the merchants settlement schedule is \'Monthly\'", ((string)(null)), ((Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "1.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await MyMerchantsFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await MyMerchantsFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
