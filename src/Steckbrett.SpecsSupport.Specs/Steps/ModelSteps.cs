using NUnit.Framework;
using Steckbrett.SpecsSupport.Specs.Model;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Specs.Steps
{
	class ModelStepsContext
	{
		public Customer Customer { get; set; }
	}

	[Binding]
	class ModelSteps
	{
		private readonly ModelStepsContext ctx;

		public ModelSteps(ModelStepsContext ctx)
		{
			this.ctx = ctx;
		}

		[Given(@"^an instance of Customer with Id (\d+)$")]
		public void GivenAnInstanceOfCustomerWithId(int customerId)
		{
			var customer = new Customer {Id = customerId};
			ScenarioContext.Current.AddInstance(customer);
		}

		[Given(@"^I created (\d+) instances of Customer$")]
		public void GivenICreatedInstancesOfCustomer(int count)
		{
			for (int i = 0; i < count; i++)
			{
				ScenarioContext.Current.AddInstance(new Customer());
			}
		}

		[Given(@"^I created (\d+) instances of Order$")]
		public void GivenICreatedInstancesOfOrder(int count)
		{
			for (int i = 0; i < count; i++)
			{
				ScenarioContext.Current.AddInstance(new Order());
			}
		}

	}
}