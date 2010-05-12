using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	[Binding]
	public class ModelUsageSteps : ModelBindingBase
	{
		private const string IRemoveTheInstancesOf =
			@"^I remove the instances? of (\w+)$";
		[Given(IRemoveTheInstancesOf)]
		[When(IRemoveTheInstancesOf)]
		public static void SoIRemoveTheInstancesOf(string typeName)
		{
			var type = GetTypeByName(typeName);
			InstancesOf(type).Clear();
		}

		private const string IRemoveAllTheInstances =
			@"^I remove all the instances$";
		[Given(IRemoveAllTheInstances)]
		[When(IRemoveAllTheInstances)]
		public static void SoIRemoveAllTheInstances()
		{
			foreach (var type in InstanceTypes)
			{
				InstancesOf(type).Clear();
			}
		}

	}
}
