using StockMarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class Job:IConfigurable,IRequired,IResearchableItem
	{
		public string Name { get; set; }
		public int Tier { get; set; }
		public List<ResourceQuantity> BaseProduction { get; set; }
		public Requirements Requirements { get; set; }
		public override bool Equals(object obj)
		{
			return this.Name == ((Job)obj).Name;
		}

		public void Unlocked() { }
	}
}
