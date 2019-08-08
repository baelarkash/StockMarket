using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Machinery: Upgrade, IResearchableItem, IRequired, IConfigurable
	{
		public string Name { get; set; }
		Requirements Requirements { get; set; }
		public int Tier { get; set; }
		public Machinery()
		{
			
		}
		public void Unlocked() { }
		public static bool operator ==(Machinery item, Machinery comparison)
		{
			return item.Name == (comparison).Name;
		}
		public static bool operator !=(Machinery item, Machinery comparison)
		{
			return item.Name != (comparison).Name;
		}
	}
}
