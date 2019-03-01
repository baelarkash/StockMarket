using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StockMarket.Models.Common;
using StockMarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Configuration
{
	public static class Configuration
	{
		private static List<Building> buildings;
		private static List<Job> jobs;
		private static List<Machinery> machineries;
		private static List<Research> researches;
		private static List<Resource> resources;
		private static List<ResourceType> resourceTypes;
		private static List<Tool> tools;


		public static void SaveItem<T>(List<T> items,string filePath) where T : IConfigurable
		{
			JsonSerializer serializer = new JsonSerializer() {
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameHandling = TypeNameHandling.Auto				
			};
			serializer.Converters.Add(new JavaScriptDateTimeConverter());
			
			using (StreamWriter sw = new StreamWriter(filePath))

			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, items);
			}
		}
		public static List<T> LoadItem<T>(string filePath) where T : IConfigurable
		{
			List<T> item = null;
			using (StreamReader file = File.OpenText(filePath))
			{
				JsonSerializer serializer = new JsonSerializer() { TypeNameHandling = TypeNameHandling.Auto };				
				item = (List<T>)serializer.Deserialize(file, typeof(List<Building>));
			}
			return item;
		}		
	}
}
