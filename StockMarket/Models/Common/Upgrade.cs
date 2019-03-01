using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class Upgrade
	{
		public List<UpgradeItem> Improvement { get; set; }
		public Upgrade()
		{
			Improvement = new List<UpgradeItem>();
		}
		public UpgradeItem getUpgrade(Resource resource)
		{
			return Improvement.FirstOrDefault(x => x.Resource == resource);
		}
	}
}
