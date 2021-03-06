﻿using System.Linq;
using NUnit.Framework;
using Steckbrett.SpecsSupport.Specs.Model;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Specs.Steps
{
	[Binding]
	class AutoMapperSteps
	{
		[Then(@"a Customer called John Doe should exist")]
		public void ThenIShouldHaveACustomerCalledJohnDoe()
		{
			var customer = ScenarioContext.Current.InstancesOf<Customer>()
				.Where(c => c.FirstName == "John")
				.Where(c => c.LastName == "Doe")
				.FirstOrDefault();

			Assert.That(customer, Is.Not.Null);
		}

		[Then(@"I should have the Order (\d+) related to Customer (\d+)")]
		public void ThenIShouldHaveTheOrderRelatedToCustomer(int orderId, int customerId)
		{
			var order = ScenarioContext.Current.InstanceById<Order>(orderId);
			var customer = ScenarioContext.Current.InstanceById<Customer>(customerId);

			Assert.That(order, Is.Not.Null);
			Assert.That(customer, Is.Not.Null);
			Assert.That(order.Customer, Is.Not.Null);
			Assert.That(order.Customer.Id, Is.EqualTo(customer.Id));
		}

		[When(@"I calculate Order (\d+) total")]
		public void WhenICalculateOrderTotal(int orderId)
		{
			var order = ScenarioContext.Current.InstanceById<Order>(orderId);

			order.Total = order.Details.Sum(d => d.Price * d.Quantity);
		}

		[Then(@"I should get total (.+) in Order (\d+)")]
		public void ThenIShouldGetTotalInOrder(decimal total, int orderId)
		{
			var order = ScenarioContext.Current.InstanceById<Order>(orderId);

			Assert.That(order.Total, Is.EqualTo(total));
		}

		[Then(@"the first instance of Customer should have Id (\d+)")]
		public void ThenTheFirstInstanceOfCustomerShouldHaveId(int id)
		{
			var customer = ScenarioContext.Current.InstancesOf<Customer>().FirstOrDefault();

			Assert.That(customer, Is.Not.Null);
			Assert.That(customer.Id, Is.EqualTo(id));
		}

		[Then(@"I should have Customer (\d+) referencing to no parent customer")]
		public void ThenIShouldHaveCustomerReferencingToNoParentCustomer(int customerId)
		{
			var customer = ScenarioContext.Current.InstanceById<Customer>(customerId);
			Assert.That(customer, Is.Not.Null);
			Assert.That(customer.Parent, Is.Null);
		}

		[Then(@"I should have Customer (\d+) referencing to parent customer (\d+)")]
		public void ThenIShouldHaveCustomerReferencingToParentCustomer(int customerId, int parentId)
		{
			var customer = ScenarioContext.Current.InstanceById<Customer>(customerId);
			Assert.That(customer, Is.Not.Null);
			Assert.That(customer.Parent, Is.Not.Null);
			Assert.That(customer.Parent.Id, Is.EqualTo(parentId));
		}


		[Then(@"the Group (\d+) should have (\d+) Customers")]
		public void ThenTheGroupShouldHaveCustomers(int groupId, int countCustomers)
		{
			var group = ScenarioContext.Current.InstanceById<Group>(groupId);
			Assert.That(group.Customers.Count, Is.EqualTo(countCustomers));
		}


	}

}