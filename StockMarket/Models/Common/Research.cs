using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Research : IResearchableItem, IRequired, IConfigurable
	{
		public string Name { get; set; }
		public Requirements Requirements { get; set; }
		List<IResearchableItem> UnlockedItems { get; set; }
		public int Tier { get; set; }
		public Research()
		{
			
		}
		public void Unlocked()
		{
			
		}
		public static bool operator ==(Research item, Research comparison)
		{
			return item.Name == (comparison).Name;
		}
		public static bool operator !=(Research item, Research comparison)
		{
			return item.Name != (comparison).Name;
		}
	}
}
