﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "MyMerchants", null, ProgrammingLanguage.CSharp, new string[] {
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
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Role Name"});
            table14.AddRow(new string[] {
                        "Estate[id]"});
            table14.AddRow(new string[] {
                        "Merchant[id]"});
#line 6
 testRunner.Given("I create the following roles", ((string)(null)), table14, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Secret",
                        "Scopes",
                        "UserClaims"});
            table15.AddRow(new string[] {
                        "estateManagement[id]",
                        "Estate Managememt REST",
                        "Secret1",
                        "estateManagement[id]",
                        "MerchantId,EstateId,role"});
#line 11
 testRunner.Given("I create the following api resources", ((string)(null)), table15, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description",
                        "UserClaims"});
            table16.AddRow(new string[] {
                        "openid",
                        "Your user identifier",
                        "",
                        "sub"});
            table16.AddRow(new string[] {
                        "profile",
                        "User profile",
                        "Your user profile information (first name, last name, etc.)",
                        "name,role,email,given_name,middle_name,family_name,EstateId,MerchantId"});
            table16.AddRow(new string[] {
                        "email",
                        "Email",
                        "Email and Email Verified Flags",
                        "email_verified,email"});
#line 15
 testRunner.Given("I create the following identity resources", ((string)(null)), table16, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId",
                        "Name",
                        "Secret",
                        "Scopes",
                        "GrantTypes",
                        "RedirectUris",
                        "PostLogoutRedirectUris",
                        "RequireConsent",
                        "AllowOfflineAccess"});
            table17.AddRow(new string[] {
                        "serviceClient[id]",
                        "Service Client",
                        "Secret1",
                        "estateManagement[id]",
                        "client_credentials",
                        "",
                        "",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "estateUIClient[id]",
                        "Merchant Client",
                        "Secret1",
                        "estateManagement[id],openid,email,profile",
                        "hybrid",
                        "http://localhost:[port]/signin-oidc",
                        "http://localhost:[port]/signout-oidc",
                        "false",
                        "true"});
#line 21
 testRunner.Given("I create the following clients", ((string)(null)), table17, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId"});
            table18.AddRow(new string[] {
                        "serviceClient[id]"});
#line 26
 testRunner.Given("I have a token to access the estate management resource", ((string)(null)), table18, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName"});
            table19.AddRow(new string[] {
                        "Test Estate [id]"});
#line 30
 testRunner.Given("I have created the following estates", ((string)(null)), table19, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName",
                        "OperatorName",
                        "RequireCustomMerchantNumber",
                        "RequireCustomTerminalNumber"});
            table20.AddRow(new string[] {
                        "Test Estate [id]",
                        "Test Operator [id]",
                        "True",
                        "True"});
#line 34
 testRunner.And("I have created the following operators", ((string)(null)), table20, "And ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "EstateName"});
            table21.AddRow(new string[] {
                        "estateuser[id]@testestate1.co.uk",
                        "123456",
                        "TestEstate",
                        "User1",
                        "Test Estate [id]"});
#line 38
 testRunner.And("I have created the following security users", ((string)(null)), table21, "And ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "MerchantName",
                        "AddressLine1",
                        "Town",
                        "Region",
                        "Country",
                        "ContactName",
                        "EmailAddress",
                        "EstateName"});
            table22.AddRow(new string[] {
                        "Test Merchant 1",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant1.co.uk",
                        "Test Estate [id]"});
            table22.AddRow(new string[] {
                        "Test Merchant 2",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant2.co.uk",
                        "Test Estate [id]"});
            table22.AddRow(new string[] {
                        "Test Merchant 3",
                        "Address Line 1",
                        "TestTown",
                        "Test Region",
                        "United Kingdom",
                        "Test Contact 1",
                        "testcontact1@merchant3.co.uk",
                        "Test Estate [id]"});
#line 42
 testRunner.Given("I create the following merchants", ((string)(null)), table22, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "OperatorName",
                        "MerchantName",
                        "MerchantNumber",
                        "TerminalNumber",
                        "EstateName"});
            table23.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 1",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
            table23.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 2",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
            table23.AddRow(new string[] {
                        "Test Operator [id]",
                        "Test Merchant 3",
                        "00000001",
                        "10000001",
                        "Test Estate [id]"});
#line 48
 testRunner.When("I assign the following  operator to the merchants", ((string)(null)), table23, "When ");
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "MerchantName",
                        "EstateName"});
            table24.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant1.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 1",
                        "Test Estate [id]"});
            table24.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant2.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 2",
                        "Test Estate [id]"});
            table24.AddRow(new string[] {
                        "merchantuser1[id]@testmerchant3.co.uk",
                        "123456",
                        "TestMerchant",
                        "User1",
                        "Test Merchant 3",
                        "Test Estate [id]"});
#line 54
 testRunner.When("I create the following security users", ((string)(null)), table24, "When ");
#line hidden
            TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                        "DeviceIdentifier",
                        "MerchantName",
                        "EstateName"});
            table25.AddRow(new string[] {
                        "TestDevice1",
                        "Test Merchant 1",
                        "Test Estate [id]"});
            table25.AddRow(new string[] {
                        "TestDevice2",
                        "Test Merchant 2",
                        "Test Estate [id]"});
            table25.AddRow(new string[] {
                        "TestDevice3",
                        "Test Merchant 3",
                        "Test Estate [id]"});
#line 60
 testRunner.When("I add the following devices to the merchant", ((string)(null)), table25, "When ");
#line hidden
#line 66
 testRunner.Given("I am on the application home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 68
 testRunner.And("I click on the Sign In Button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 70
 testRunner.Then("I am presented with a login screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 72
 testRunner.When("I login with the username \'estateuser[id]@testestate1.co.uk\' and password \'123456" +
                    "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 74
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
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View My Merchants", null, ((string[])(null)));
#line 76
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
#line 77
 testRunner.Given("I click on the My Merchants sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 78
 testRunner.Then("I am presented with the Merchants List Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
                            "MerchantName",
                            "ContactName",
                            "AddressLine1",
                            "Town",
                            "NumberOfUsers",
                            "NumberOfDevices",
                            "NumberOfOperators"});
                table26.AddRow(new string[] {
                            "Test Merchant 1",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table26.AddRow(new string[] {
                            "Test Merchant 2",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
                table26.AddRow(new string[] {
                            "Test Merchant 3",
                            "Test Contact 1",
                            "Address Line 1",
                            "TestTown",
                            "0",
                            "1",
                            "1"});
#line 79
 testRunner.And("the following merchants details are in the list", ((string)(null)), table26, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
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
