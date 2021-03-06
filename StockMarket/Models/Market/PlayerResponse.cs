﻿using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
	public class PlayerResponse
	{
		public Player Owner { get; set; }
		public int Value { get; set; }
		public List<ResourceQuantity> Resources { get; set; }
	}
}
