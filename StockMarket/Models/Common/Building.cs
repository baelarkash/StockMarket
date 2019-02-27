﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Building : IResearchableItem,IRequired,IConfigurable
	{
		public string Name { get; set; }
		List<Requirement> Requirements { get; set; }
		public Building()
		{
			Requirements = new List<Requirement>();
		}
		public void Unlocked() { }
	}
}
