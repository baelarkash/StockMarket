using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Enumerables;

namespace StockMarket.Models.Common
{
	public class UpgradeItem
	{
		public Resource Resource { get; set; }
		public double Value { get; set; }
		public UtilsEnumerables.benefitType BenefitType { get; set; }
		public UtilsEnumerables.upgradeType UpgradeType { get; set; }
		public void addUpgradeValue(ref double flat,ref double percentage)
		{
			if(BenefitType == UtilsEnumerables.benefitType.FLAT)
			{
				flat += Value;
			}
			else
			{
				percentage += percentage;
			}
		}
	}
}
