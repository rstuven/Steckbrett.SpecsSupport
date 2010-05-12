// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.2.0.0
//      Runtime Version:2.0.50727.4927
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace Steckbrett.SpecsSupport.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("AutoPoco generation")]
    [NUnit.Framework.CategoryAttribute("model_mapping")]
    public partial class AutoPocoGenerationFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en"), "AutoPoco generation", "In order to be the greatest lazy developer\r\nAs a lazy developer using SpecFlow\r\nI" +
                    " want to automagically generate random object instances", new string[] {
                        "model_mapping"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a random instance")]
        public virtual void GeneratingARandomInstance()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a random instance", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("a random instance of Customer");
            testRunner.Then("I should have 1 random instance of Customer");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a number of random instances")]
        public virtual void GeneratingANumberOfRandomInstances()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a number of random instances", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("3 random instances of Customer");
            testRunner.Then("I should have 3 random instances of Customer");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a consecutive number of random instances")]
        public virtual void GeneratingAConsecutiveNumberOfRandomInstances()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a consecutive number of random instances", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("3 random instances of Customer");
            testRunner.Given("2 random instances of Customer");
            testRunner.Then("I should have 5 random instances of Customer");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating random instances related by IList property with explicit typing")]
        public virtual void GeneratingRandomInstancesRelatedByIListPropertyWithExplicitTyping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating random instances related by IList property with explicit typing", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("a random instance of Order");
            testRunner.And("5 random instances of Detail added to Details of Order 1");
            testRunner.Then("I should have a random instance of Order with 5 random Details");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating random instances related by IList property with implicit typing")]
        public virtual void GeneratingRandomInstancesRelatedByIListPropertyWithImplicitTyping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating random instances related by IList property with implicit typing", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("a random instance of Order");
            testRunner.And("5 random instances added to Details of Order 1");
            testRunner.Then("I should have a random instance of Order with 5 random Details");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating random instances related by method with explicit typing")]
        public virtual void GeneratingRandomInstancesRelatedByMethodWithExplicitTyping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating random instances related by method with explicit typing", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("a random instance of Order");
            testRunner.And("5 random instances of Detail passed to AddDetail of Order 1");
            testRunner.Then("I should have a random instance of Order with 5 random Details");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating random instances related by method with implicit typing")]
        public virtual void GeneratingRandomInstancesRelatedByMethodWithImplicitTyping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating random instances related by method with implicit typing", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("a random instance of Order");
            testRunner.And("5 random instances passed to AddDetail of Order 1");
            testRunner.Then("I should have a random instance of Order with 5 random Details");
            testRunner.CollectScenarioErrors();
        }
    }
}
