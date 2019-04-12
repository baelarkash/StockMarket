using StockMarket.Models.Common;
using StockMarket.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Logic
{
	public class PlayerController
	{
		private Player Player;
		public void changeActiveJob(Tile tile ,Job newJob)
		{
			if (Player.avaliableJobs.Contains(newJob))
			{
				Player.Map.changeActiveJob(tile, newJob);
			}
		}
	}
}
