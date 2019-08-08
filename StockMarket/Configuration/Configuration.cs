using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Common;
namespace StockMarket.Configuration
{
	public static class Configuration
	{
		private static List<Building> AllBuildings;					 
		private static List<Job> AllJobs;							
		private static List<Machinery> AllMachineries;				
		private static List<Research> AllResearches;
		private static List<Resource> AllResources;
		private static List<ResourceType> AllResourceTypes;
		private static List<Tool> AllTools;

		public static bool loadConfiguration()
		{
			bool result = true;
			AllBuildings = ConfigurationUtils.LoadItemList<Building>(@"C:\Users\Alberto\Desktop\Buildings.txt");
			AllJobs = ConfigurationUtils.LoadItemList<Job>("");
			AllMachineries = ConfigurationUtils.LoadItemList<Machinery>("");
			AllResearches = ConfigurationUtils.LoadItemList<Research>("");
			AllResources = ConfigurationUtils.LoadItemList<Resource>("");
			AllResourceTypes = ConfigurationUtils.LoadItemList<ResourceType>("");
			AllTools = ConfigurationUtils.LoadItemList<Tool>("");
			return result;
		}
	}
}
