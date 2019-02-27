using StockMarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class Job:IConfigurable
	{
		public string Name { get; set; }
		public int Tier { get; set; }
	}
}
