﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.6.0.0
//      SpecFlow Generator Version:3.6.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace EstateAdministrationUI.IntegrationTests.Tests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.6.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "base")]
    [Xunit.TraitAttribute("Category", "shared")]
    public partial class MyMerchantsFeature : object, Xunit.IClassFixture<MyMerchantsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "base",
                "shared"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "MyMerchants.feature"
#line hidden
        
        public MyMerchantsFeature(MyMerchantsFeature.FixtureData fixtureData, EstateAdministrationUI_IntegrationTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Tests", "MyMerchants", null, ProgrammingLanguage.CSharp, new string[] {
                        "base",
                        "shared"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "Role Name"});
            table21.AddRow(new string[] {
                        "Estate[id]"});
            table21.AddRow(new string[] {
                        "Merchant[id]"});
#line 6
 testRunner.Given("I create the following roles", ((string)(null)), table21, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description"});
            table22.AddRow(new string[] {
                        "estateManagement[id]",
                        "Estate Managememt REST Scope",
                        "A scope for Estate Managememt REST"});
#line 11
 testRunner.Given("I create the following api scopes", ((string)(null)), table22, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Secret",
                        "Scopes",
                        "UserClaims"});
            table23.AddRow(new string[] {
                        "estateManagement[id]",
                        "Estate Managememt REST",
                        "Secret1",
                        "estateManagement[id]",
                        "merchantId,estateId,role"});
#line 15
 testRunner.Given("I create the following api resources", ((string)(null)), table23, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description",
                        "UserClaims"});
            table24.AddRow(new string[] {
                        "openid",
                        "Your user identifier",
                        "",
                        "sub"});
            table24.AddRow(new string[] {
                        "profile",
                        "User profile",
                        "Your user profile information (first name, last name, etc.)",
                        "name,role,email,given_name,middle_name,family_name,estateId,merchantId"});
            table24.AddRow(new string[] {
                        "email",
                        "Email",
                        "Email and Email Verified Flags",
                        "email_verified,email"});
#line 19
 testRunner.Given("I create the following identity resources", ((string)(null)), table24, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId",
                        "Name",
                        "Secret",
                        "Scopes",
                        "GrantTypes",
                        "RedirectUris",
                        "PostLogoutRedirectUris",
                        "RequireConsent",
                        "AllowOfflineAccess"});
            table25.AddRow(new string[] {
                        "serviceClient[id]",
                        "Service Client",
                        "Secret1",
                        "estateManagement[id]",
                        "client_credentials",
                        "",
                        "",
                        "",
                        ""});
            table25.AddRow(new string[] {
                        "estateUIClient[id]",
                        "Merchant Client",
                        "Secret1",
                        "estateManagement[id],openid,email,profile",
                        "hybrid",
                        "http://localhost:[port]/signin-oidc",
                        "http://localhost:[port]/signout-oidc",
                        "false",
                        "true"});
#line 25
 testRunner.Given("I create the following clients", ((string)(null)), table25, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId"});
            table26.AddRow(new string[] {
                        "serviceClient[id]"});
#line 30
 testRunner.Given("I have a token to access the estate management resource", ((string)(null)), table26, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table27 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName"});
            table27.AddRow(new string[] {
                        "Test Estate [id]"});
#line 34
 testRunner.Given("I have created the following estates", ((string)(null)), table27, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table28 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName",
                        "OperatorName",
                        "RequireCustomMerchantNumber",
                        "RequireCustomTerminalNumber"});
            table28.AddRow(new string[] {
                        "Test Estate [id]",
                        "Test Operator [id]",
                        "True",
                        "True"});
#line 38
 testRunner.And("I have created the following operators", ((string)(null)), table28, "And ");
#line hidden
            TechTalk.SpecFlow.Table table29 = new TechTalk.SpecFlow.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "EstateName"});
            table29.AddRow(new string[] {
                        "estateuser[id]@testestate1.co.uk",
                        "123456",
                        "TestEstate",
                        "User1",
                        "Test Estate [id]"});
#line 42
 testRunner.And("I have created the following security users", ((string)(null)), table29, "And ");
#line hidden
            TechTalk.SpecFlow.Table table30 = new TechTalk.SpecFlow.Table(new string[] {
                        "MerchantName",
                        "AddressLine1",
                        "Town",
                        "Region",
                        "Country",
                        "ContactName",
                        "EmailAddress",
                        "EstateName"});
            table30.AddRow(new string[] {
                        "Test Merchant 1",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant1.co.uk",
                        "Test Estate [id]"});
            table30.AddRow(new string[] {
                        "Test Merchant 2",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant2.co.uk",
                        "Test Estate [id]"});
            table30.AddRow(new string[] {
                        "Test Merchant 3",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant3.co.uk",
                        "Test Estate [id]"});
#line 46
 testRunner.Given("I create the following merchants", ((string)(null)), table30, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table31 = new TechTalk.SpecFlow.Table(new string[] {
                        "OperatorName",
                        "MerchantName",
                        "MerchantNumber",
                        "TerminalNumber",
                        "EstateName"});
            table31.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 1",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
            table31.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 2",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
            table31.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 3",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
#line 52
 testRunner.When("I assign the following  operator to the merchants", ((string)(null)), table31, "When ");
#line hidden
            TechTalk.SpecFlow.Table table32 = new TechTalk.SpecFlow.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "MerchantName",
                        "EstateName"});
            table32.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant1.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 1",
                        "Test Estate [id]"});
            table32.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant2.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 2",
                        "Test Estate [id]"});
            table32.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant3.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 3",
                        "Test Estate [id]"});
#line 58
 testRunner.When("I create the following security users", ((string)(null)), table32, "When ");
#line hidden
            TechTalk.SpecFlow.Table table33 = new TechTalk.SpecFlow.Table(new string[] {
                        "DeviceIdentifier",
                        "MerchantName",
                        "EstateName"});
            table33.AddRow(new string[] {
                        "TestDevice1",
                        "Test Merchant 1",
                        "Test Estate [id]"});
            table33.AddRow(new string[] {
                        "TestDevice2",
                        "Test Merchant 2",
                        "Test Estate [id]"});
            table33.AddRow(new string[] {
                        "TestDevice3",
                        "Test Merchant 3",
                        "Test Estate [id]"});
#line 64
 testRunner.When("I add the following devices to the merchant", ((string)(null)), table33, "When ");
#line hidden
#line 70
 testRunner.Given("I am on the application home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 72
 testRunner.And("I click on the Sign In Button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 74
 testRunner.Then("I am presented with a login screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 76
 testRunner.When("I login with the username \'estateuser[id]@testestate1.co.uk\' and password \'123456" +
                    "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 78
 testRunner.Then("I am presented with the Estate Administrator Dashboard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="View My Merchants")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "View My Merchants")]
        public virtual void ViewMyMerchants()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View My Merchants", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 80
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 81
 testRunner.Given("I click on the My Merchants sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 82
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table34 = new TechTalk.SpecFlow.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table34.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table34.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table34.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 83
 testRunner.And("the following merchants details are in the list", ((string)(null)), table34, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="View Single Merchant")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "View Single Merchant")]
        public virtual void ViewSingleMerchant()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View Single Merchant", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 89
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 90
 testRunner.Given("I click on the My Merchants sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 91
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table35 = new TechTalk.SpecFlow.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table35.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table35.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table35.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 92
 testRunner.And("the following merchants details are in the list", ((string)(null)), table35, "And ");
#line hidden
#line 97
 testRunner.When("I select \'Test Merchant 1\' from the merchant list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 98
 testRunner.Then("I am presented the merchant details screen for \'Test Merchant 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Make Merchant Deposit")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "Make Merchant Deposit")]
        [Xunit.TraitAttribute("Category", "PRTest")]
        public virtual void MakeMerchantDeposit()
        {
            string[] tagsOfScenario = new string[] {
                    "PRTest"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Make Merchant Deposit", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 101
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 102
 testRunner.Given("I click on the My Merchants sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 103
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table36 = new TechTalk.SpecFlow.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table36.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table36.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table36.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 104
 testRunner.And("the following merchants details are in the list", ((string)(null)), table36, "And ");
#line hidden
#line 109
 testRunner.When("I click the Make Deposit button for \'Test Merchant 1\' from the merchant list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 110
 testRunner.Then("I am presented the make merchant deposit screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table37 = new TechTalk.SpecFlow.Table(new string[] {
                            "DepositAmount",
                            "DepositDate",
                            "DepositReference"});
                table37.AddRow(new string[] {
                            "1000",
                            "Today",
                            "Test Deposit 1"});
#line 111
 testRunner.When("I make the following deposit", ((string)(null)), table37, "When ");
#line hidden
#line 114
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 115
 testRunner.When("I select \'Test Merchant 1\' from the merchant list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 116
 testRunner.Then("I am presented the merchant details screen for \'Test Merchant 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 117
 testRunner.And("the available balance for the merchant should be 1000.00", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Create New Merchant")]
        [Xunit.TraitAttribute("FeatureTitle", "MyMerchants")]
        [Xunit.TraitAttribute("Description", "Create New Merchant")]
        public virtual void CreateNewMerchant()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create New Merchant", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 119
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 120
 testRunner.Given("I click on the My Merchants sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 121
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table38 = new TechTalk.SpecFlow.Table(new string[] {
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
#line 122
 testRunner.And("the following merchants details are in the list", ((string)(null)), table38, "And ");
#line hidden
#line 127
 testRunner.When("I click the Add New Merchant button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 128
 testRunner.Then("I am presented the new merchant screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table39 = new TechTalk.SpecFlow.Table(new string[] {
                            "MerchantName",
                            "AddressLine1",
                            "Town",
                            "Region",
                            "PostCode",
                            "Country",
                            "ContactName",
                            "ContactEmail",
                            "ContactPhoneNumber"});
                table39.AddRow(new string[] {
                            "Test Merchant 4",
                            "Address Line 1",
                            "TestTown",
                            "TestRegion",
                            "TE57 1NG",
                            "United Kingdom",
                            "Test Contact 4",
                            "testcontact@testmerchant4.co.uk",
                            "0123456789"});
#line 129
 testRunner.When("I enter the following new merchant details", ((string)(null)), table39, "When ");
#line hidden
#line 132
 testRunner.When("I click the Create Merchant button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 133
 testRunner.Then("I am presented the merchant details screen for \'Test Merchant 4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.6.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                MyMerchantsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                MyMerchantsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
