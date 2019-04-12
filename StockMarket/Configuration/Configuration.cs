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
		private static List<Building> AllBuildingsList;
		private static List<Job> AllJobsList;
		private static List<Machinery> AllMachineriesList;
		private static List<Research> AllResearchesList;
		private static List<Resource> AllResourcesList;
		private static List<ResourceType> AllResourceTypesList;
		private static List<Tool> AllToolsList;

		public static void SaveItemList<T>(List<T> items,string filePath) where T : IConfigurable
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
		public static List<T> LoadItemList<T>(string filePath) where T : IConfigurable
		{
			List<T> item = null;
			using (StreamReader file = File.OpenText(filePath))
			{
				JsonSerializer serializer = new JsonSerializer() { TypeNameHandling = TypeNameHandling.Auto };				
				item = (List<T>)serializer.Deserialize(file, typeof(List<T>));
			}
			return item;
		}
		public static void SaveItem<T>(T items, string filePath) 
		{
			JsonSerializer serializer = new JsonSerializer()
			{
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
		public static T LoadItem<T>(string filePath)
		{
			T item = default(T);
			using (StreamReader file = File.OpenText(filePath))
			{
				JsonSerializer serializer = new JsonSerializer() { TypeNameHandling = TypeNameHandling.Auto };
				item = (T)serializer.Deserialize(file, typeof(T));
			}
			return item;
		}
	}
}
