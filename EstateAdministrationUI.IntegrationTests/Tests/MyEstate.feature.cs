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
    public partial class MyEstateFeature : object, Xunit.IClassFixture<MyEstateFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "base",
                "shared"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "MyEstate.feature"
#line hidden
        
        public MyEstateFeature(MyEstateFeature.FixtureData fixtureData, EstateAdministrationUI_IntegrationTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "MyEstate", null, ProgrammingLanguage.CSharp, new string[] {
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
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Role Name"});
            table1.AddRow(new string[] {
                        "Estate[id]"});
#line 6
 testRunner.Given("I create the following roles", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Secret",
                        "Scopes",
                        "UserClaims"});
            table2.AddRow(new string[] {
                        "estateManagement[id]",
                        "Estate Managememt REST",
                        "Secret1",
                        "estateManagement[id]",
                        "MerchantId,EstateId,role"});
#line 10
 testRunner.Given("I create the following api resources", ((string)(null)), table2, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "DisplayName",
                        "Description",
                        "UserClaims"});
            table3.AddRow(new string[] {
                        "openid",
                        "Your user identifier",
                        "",
                        "sub"});
            table3.AddRow(new string[] {
                        "profile",
                        "User profile",
                        "Your user profile information (first name, last name, etc.)",
                        "name,role,email,given_name,middle_name,family_name,EstateId,MerchantId"});
            table3.AddRow(new string[] {
                        "email",
                        "Email",
                        "Email and Email Verified Flags",
                        "email_verified,email"});
#line 14
 testRunner.Given("I create the following identity resources", ((string)(null)), table3, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId",
                        "Name",
                        "Secret",
                        "Scopes",
                        "GrantTypes",
                        "RedirectUris",
                        "PostLogoutRedirectUris",
                        "RequireConsent",
                        "AllowOfflineAccess"});
            table4.AddRow(new string[] {
                        "serviceClient[id]",
                        "Service Client",
                        "Secret1",
                        "estateManagement[id]",
                        "client_credentials",
                        "",
                        "",
                        "",
                        ""});
            table4.AddRow(new string[] {
                        "estateUIClient[id]",
                        "Merchant Client",
                        "Secret1",
                        "estateManagement[id],openid,email,profile",
                        "hybrid",
                        "http://localhost:[port]/signin-oidc",
                        "http://localhost:[port]/signout-oidc",
                        "false",
                        "true"});
#line 20
 testRunner.Given("I create the following clients", ((string)(null)), table4, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "ClientId"});
            table5.AddRow(new string[] {
                        "serviceClient[id]"});
#line 25
 testRunner.Given("I have a token to access the estate management resource", ((string)(null)), table5, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName"});
            table6.AddRow(new string[] {
                        "Test Estate [id]"});
#line 29
 testRunner.Given("I have created the following estates", ((string)(null)), table6, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "EstateName",
                        "OperatorName",
                        "RequireCustomMerchantNumber",
                        "RequireCustomTerminalNumber"});
            table7.AddRow(new string[] {
                        "Test Estate [id]",
                        "Test Operator [id]",
                        "True",
                        "True"});
#line 33
 testRunner.And("I have created the following operators", ((string)(null)), table7, "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "EmailAddress",
                        "Password",
                        "GivenName",
                        "FamilyName",
                        "EstateName"});
            table8.AddRow(new string[] {
                        "estateuser[id]@testestate1.co.uk",
                        "123456",
                        "TestEstate",
                        "User1",
                        "Test Estate [id]"});
#line 37
 testRunner.And("I have created the following security users", ((string)(null)), table8, "And ");
#line hidden
#line 41
 testRunner.Given("I am on the application home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 43
 testRunner.And("I click on the Sign In Button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 45
 testRunner.Then("I am presented with a login screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 47
 testRunner.When("I login with the username \'estateuser[id]@testestate1.co.uk\' and password \'123456" +
                    "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 49
 testRunner.Then("I am presented with the Estate Administrator Dashboard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="View Estate")]
        [Xunit.TraitAttribute("FeatureTitle", "MyEstate")]
        [Xunit.TraitAttribute("Description", "View Estate")]
        [Xunit.TraitAttribute("Category", "PRTest")]
        public virtual void ViewEstate()
        {
            string[] tagsOfScenario = new string[] {
                    "PRTest"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View Estate", null, new string[] {
                        "PRTest"});
#line 52
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
#line 53
 testRunner.Given("I click on the My Estate sidebar option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 54
 testRunner.Then("I am presented with the Estate Details Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "EstateName"});
                table9.AddRow(new string[] {
                            "Test Estate [id]"});
#line 55
 testRunner.And("My Estate Details will be shown", ((string)(null)), table9, "And ");
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
                MyEstateFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                MyEstateFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
