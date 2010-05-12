﻿namespace Steckbrett.SpecsSupport.Specs.Model
{
	public class Customer : ModelBase
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Telephone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State{ get; set; }
		public Customer Parent { get; set; }
	}
}
