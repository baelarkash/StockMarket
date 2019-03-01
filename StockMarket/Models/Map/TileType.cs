using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Map
{
	public class TileType:IConfigurable
	{
		public int TileTypeId { get; set; }
		public string Name { get; set; }
	}
}
