using System.Linq;
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

		[When(@"^I get an instance of Customer with Id (\d+)$")]
		public void WhenIGetAnInstanceOfCustomerWithId(int customerId)
		{
			ctx.Customer = ScenarioContext.Current.InstanceById<Customer>(customerId);
		}

		[Then(@"^I should get null$")]
		public void ThenIShouldGetNull()
		{
			Assert.That(ctx.Customer, Is.Null);
		}

		[Then(@"^I should get an instance of Customer with Id (\d+)$")]
		public void ThenIShouldGetAnInstanceOfCustomerWithId(int customerId)
		{
			Assert.That(ctx.Customer, Is.Not.Null);
			Assert.That(ctx.Customer.Id, Is.EqualTo(customerId));
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

		[Then(@"^I should have (\d+) instances of (.*)$")]
		public void ThenIShouldHaveExpectedInstancesOfCustomer(int expectedCount, string typeName)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);
			var instances = ScenarioContext.Current.InstancesOf(type);
			var count = instances.Count();

			Assert.That(count, Is.EqualTo(expectedCount));
		}

	}
}