using System.Linq;
using NUnit.Framework;
using Steckbrett.SpecsSupport.Specs.Model;
using Steckbrett.SpecsSupport.Steps;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Specs.Steps
{
	[Binding]
	class AutoPocoSteps : ModelBindingBase
	{
		[Then(@"I should have (\d+) random instances? of Customer")]
		public void ThenIShouldHaveRandomInstancesOfCustomer(int count)
		{
			var customers = InstancesOf<Customer>();
	
			Assert.That(customers.Count(), Is.EqualTo(count));

			var id = 0;
			foreach (var customer in customers)
			{
				++id;
				Assert.That(customer.Id, Is.EqualTo(id), "customer.Id");
				Assert.That(customer.FirstName, Is.Not.Null.Or.Empty, "customer.FirstName");
				Assert.That(customer.LastName, Is.Not.Null.Or.Empty, "customer.LastName");
			}
		}

		[Then(@"I should have a random instance of Order with 5 random Details")]
		public void ThenIShouldHaveARandomInstanceOfOrderWith5RandomDetails()
		{
			var order = InstanceById<Order>(1);
			
			Assert.That(order, Is.Not.Null);
			Assert.That(order.Details.Count(), Is.EqualTo(5));

			var id = 0;
			foreach (var detail in order.Details)
			{
				++id;
				Assert.That(detail.Id, Is.EqualTo(id), "detail.Id");
				Assert.That(detail.Product, Is.Not.Null.Or.Empty, "detail.Product");
			}
		}

	}

}
