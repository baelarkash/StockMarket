﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
	public class ConcurrentExchangePetition
	{
		public List<OwnerPetition> Petitions { get; set; }
		public ConcurrentExchangePetition()
		{
			Petitions = new List<OwnerPetition>();
		}
	}
}