using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Map
{
	public class MapResource
	{
		public int MapResourceId { get; set; }
		public string Name { get; set; }
		public List<Resource> Resources { get; set; }
	}
}
