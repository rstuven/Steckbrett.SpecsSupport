using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	[Binding]
	public class ModelUsageSteps
	{
		private const string IRemoveTheInstancesOf =
			@"^I remove the instances? of (\w+)$";
		[Given(IRemoveTheInstancesOf)]
		[When(IRemoveTheInstancesOf)]
		public static void SoIRemoveTheInstancesOf(string typeName)
		{
			ScenarioContext.Current.InstancesOf(typeName).Clear();
		}

		private const string IRemoveAllTheInstances =
			@"^I remove all the instances$";
		[Given(IRemoveAllTheInstances)]
		[When(IRemoveAllTheInstances)]
		public static void SoIRemoveAllTheInstances()
		{
			foreach (var type in ScenarioContext.Current.InstanceTypes())
			{
				ScenarioContext.Current.InstancesOf(type).Clear();
			}
		}

	}
}
