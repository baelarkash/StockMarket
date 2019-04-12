using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Map
{
	public class MapResource:IConfigurable
	{
		#region "Properties"
		public int MapResourceId { get; set; }
		public string Name { get; set; }
		public List<Resource> Resources { get; set; }
		#endregion
	}
}
