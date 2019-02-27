using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
	public class PlayerPetition
	{
		public Player Owner { get; set; }
		public List<ResourceQuantityMarket> Resources { get; set; }

	}
}
