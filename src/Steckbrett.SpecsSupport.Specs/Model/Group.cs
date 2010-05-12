using System.Collections.Generic;

namespace Steckbrett.SpecsSupport.Specs.Model
{
	public class Group : ModelBase
	{
		public IList<Customer> Customers { get; set; }

		public Group()
		{
			Customers = new List<Customer>();
		}

		public void AddCustomer(Customer customer)
		{
			Customers.Add(customer);
		}
	}
}
