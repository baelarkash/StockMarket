using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Building : Upgrade, IResearchableItem,IRequired,IConfigurable
	{
		public string Name { get; set; }
		public Requirements Requirements { get; set; }
		public List<Machinery> Machineries { get; set; }
		public int Tier { get; set; }
		public int Size { get; set; }

		public Building()
		{
			
		}
		public void Unlocked() { }
		public static bool operator ==(Building item, Building comparison)
		{
			return item.Name == (comparison).Name;
		}
		public static bool operator !=(Building item, Building comparison)
		{
			return item.Name != (comparison).Name;
		}
	}
}
