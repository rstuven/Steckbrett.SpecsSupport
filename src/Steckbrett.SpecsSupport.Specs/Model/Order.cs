using System.Collections.Generic;

namespace Steckbrett.SpecsSupport.Specs.Model
{
	public class Order : ModelBase
	{
		public Customer Customer { get; set; }
		public IList<Detail> Details { get; set; }
		public decimal Total { get; set; }

		public Order()
		{
			Details = new List<Detail>();
		}

		public void AddDetail(Detail detail)
		{
			Details.Add(detail);
		}
	}
}
