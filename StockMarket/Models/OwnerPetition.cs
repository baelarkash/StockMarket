using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
	public class OwnerPetition
	{
		public Owner Owner { get; set; }
		public List<ResourceQuantityMarket> Resources { get; set; }

	}
}
