using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Requirement
	{
		public int Amount { get; set; }
		public IRequired Object { get; set; }
	}
}
