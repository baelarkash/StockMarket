using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Configuration;
using StockMarket.Models.Map;
using StockMarket.Utils;

namespace StockMarket.Models.Common
{
	public class Player
	{
		public string name { get; set; }
		public HexMap Map { get; set; }
		public List<Worker> Workers { get; set; }
		public List<Tile> OcuppiedTiles { get; set; }
		public List<ResourceQuantity> GetProduction()
		{
			var result = new List<ResourceQuantity>();
			foreach (var tile in OcuppiedTiles)
			{
				foreach (var worker in Workers)
				{
					//var job = worker.Jobs.FirstOrDefault(x => x.Job == tile.ActiveJob);
					var job = tile.ActiveJob;					
					foreach (var resourceProduction in job.BaseProduction)
					{
						double flatProduction = resourceProduction.Quantity;
						double percentageProduction = 0;
						if (worker.Tools.Count > 0)
						{
							foreach (var tool in worker.Tools)
							{
								var aux = tool.getUpgrade(resourceProduction.Resource);
								if (aux != null) { aux.addUpgradeValue(ref flatProduction, ref percentageProduction); }
							}

						}
						if (tile.Building != null)
						{
							var aux = tile.Building.getUpgrade(resourceProduction.Resource);
							foreach (var machinery in tile.Building.Machineries)
							{
								var aux2 = machinery.getUpgrade(resourceProduction.Resource);
								if (aux2 != null) { aux2.addUpgradeValue(ref flatProduction, ref percentageProduction); }
							}
							if (aux != null) { aux.addUpgradeValue(ref flatProduction, ref percentageProduction); }
						}
						var production = (flatProduction * percentageProduction / 100);
						var auxiliar = worker.Jobs.FirstOrDefault(x => x.Job == job);
						production *= auxiliar != null?(auxiliar.Level - 1) * (Configuration.GlobalParameters.gainPerLevel / 100):1;
						result = ResourceUtils.addResource(new ResourceQuantity() { Quantity = production, Resource = resourceProduction.Resource }, result);
						worker.addExperience(job, production);
					}

				}
			}
			return result;
		}

	}
}
