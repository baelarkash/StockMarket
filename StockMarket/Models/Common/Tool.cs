using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Tool : Upgrade, IResearchableItem,IRequired, IConfigurable
	{
		public string Name { get; set; }
		public Requirements Requirements { get; set; }
		public Tool()
		{
			
		}
		public void Unlocked()
		{
		}
		public static bool operator ==(Tool item, Tool comparison)
		{
			return item.Name == (comparison).Name;
		}
		public static bool operator !=(Tool item, Tool comparison)
		{
			return item.Name != (comparison).Name;
		}
	}
}
