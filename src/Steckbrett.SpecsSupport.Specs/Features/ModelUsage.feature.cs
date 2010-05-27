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
    [NUnit.Framework.DescriptionAttribute("Model usage")]
    [NUnit.Framework.CategoryAttribute("model_mapping")]
    public partial class ModelUsageFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en"), "Model usage", "", new string[] {
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
        [NUnit.Framework.DescriptionAttribute("Getting not created instance by Id")]
        public virtual void GettingNotCreatedInstanceById()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Getting not created instance by Id", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Then("an instance of Customer with Id 123 should not exist");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Getting created instance by Id")]
        public virtual void GettingCreatedInstanceById()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Getting created instance by Id", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("an instance of Customer with Id 123");
            testRunner.Then("an instance of Customer with Id 123 should exist");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a number of instances")]
        public virtual void GeneratingANumberOfInstances()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a number of instances", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("I created 3 instances of Customer");
            testRunner.Then("3 instances of Customer should exist");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a number of instances and removing them by type")]
        public virtual void GeneratingANumberOfInstancesAndRemovingThemByType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a number of instances and removing them by type", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("I created 3 instances of Customer");
            testRunner.And("I created 2 instances of Order");
            testRunner.When("I remove the instances of Customer");
            testRunner.Then("0 instances of Customer should exist");
            testRunner.Then("2 instances of Order should exist");
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generating a number of instances and removing them all")]
        public virtual void GeneratingANumberOfInstancesAndRemovingThemAll()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generating a number of instances and removing them all", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            testRunner.Given("I created 3 instances of Customer");
            testRunner.And("I created 2 instances of Order");
            testRunner.When("I remove all the instances");
            testRunner.Then("0 instances of Customer should exist");
            testRunner.Then("0 instances of Order should exist");
            testRunner.CollectScenarioErrors();
        }
    }
}
