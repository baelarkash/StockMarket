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

namespace StockMarket.LoadEngine
{
	public static class Configuration
	{
		public static void SaveItem<T>(List<T> items,string filePath) where T : IConfigurable
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Converters.Add(new JavaScriptDateTimeConverter());
			serializer.NullValueHandling = NullValueHandling.Ignore;

			using (StreamWriter sw = new StreamWriter(filePath))

			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, items);
			}
		}
		public static List<T> LoadItem<T>(string filePath) where T : IConfigurable
		{
			List<T> item = null;
			// deserialize JSON directly from a file
			using (StreamReader file = File.OpenText(filePath))
			{
				JsonSerializer serializer = new JsonSerializer();
				item = (List<T>)serializer.Deserialize(file, typeof(List<Building>));
			}
			return item;
		}		
	}
}
