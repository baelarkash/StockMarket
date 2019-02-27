using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models;
using StockMarket.Utils;
using System.Diagnostics;
using StockMarket.Models.Market;
using StockMarket.Models.Common;
using StockMarket.LoadEngine;
using StockMarket.Models.Map;

namespace StockMarket
{
    class Program
    {
        static void Main(string[] args)
        {
			var buildings = new List<Building>();
			buildings.Add(new Building() { Name = "Aserradero" });
			//public static string filePath = @"C:\Users\Alberto\Desktop\";
			Configuration.SaveItem<Building>(buildings, @"C:\Users\Alberto\Desktop\Buildings.txt");
			var buildings2 = Configuration.LoadItem<Building>(@"C:\Users\Alberto\Desktop\Buildings.txt");
			string a = "";

			//var cosa = -1 % 20;
			//HexMap Map = new HexMap(20, 20);
			//var tiles = Map.getSurroundedTiles(1, 2, 3);
			//foreach(var item in tiles.GroupBy(x => x.xPosition).OrderBy(x=>x.Key))
			//{
			//	Console.WriteLine("");
			//	foreach(var tile in item.OrderBy(x=>x.yPosition))
			//	{
			//		Console.Write(tile.ToString());
			//	}
			//}

			//string a = "";
			//pruebaMarket();
			///TODO Crear funcion para la integral de la funcion original dentro de demandCurve
			///TODO Expresiones del modulo Math (log,max,min...)  

			///TODO controlar la venta de recursos para que no sea menor que 0
			///TODO Gestionar peticiones de compra y venta simultaneas para distintas peticiones
			///TODO Problema con 2q^2, eleva todo al cuadrado

		}
		public static void timeTest()
		{
			Stopwatch sw1 = new Stopwatch();
			List<Parameter> variables = new List<Parameter>();
			variables.Add(new Parameter() { name = "q", parameter = (object)4d });
			variables.Add(new Parameter() { name = "Q", parameter = (object)10d });
			sw1.Start();

			//var demandCurve = new DemandCurve(string.Empty, "((a+b)-(a+b)*(a+b))/-2b");			
			var demandCurve = new DemandCurve(string.Empty, "2q^2-q+2", "2q^2/3+2q-q^2/2", "q", "q^2/2");
			sw1.Stop();
			//var demandCurve = new DemandCurve(string.Empty, "((q+2)(q^2))^2", "q^2/2+2q", "q", "q^2/2");
			//var demandCurve = new DemandCurve(string.Empty, "Q(q+1)", "q^2/2+2q", "q", "q^2/2");
			double result1 = 0;
			double result12 = 0;
			//variables.Add(new Parameter() { name = "c", parameter = (object)3m });
			//var result = demandCurve.EvalBuyDemandCurve(variables,3);
			for (int i = 0; i < 10000; i++)
			{
				result1 = demandCurve.EvalBuyDemandCurve(variables);
				result12 = demandCurve.EvalBuyDemandCurve(variables, 2);
			}
			//sw1.Stop();
			Console.WriteLine("Elapsed Tree={0}", sw1.Elapsed);
			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			double result2 = 0;
			double result22 = 0;
			for (int i = 0; i < 10000; i++)
			{
				double q = 0;
				q = (double)variables.First(x => x.name == "q").parameter;
				result2 = 2 * (q * q) - q + 2;
				//var result = q+ 2;
				double intermediateValue = 0;
				for (int j = 0; j < 3; j++)
				{

					intermediateValue += 2 * (q * q) - q + 2;
					q++;
				}
				result22 = intermediateValue / 3;
			}

			sw2.Stop();
			Console.WriteLine("Elapsed normal={0}", sw2.Elapsed);
			string a = string.Empty;
			string b = string.Empty;
		}
		public static void prueba()
		{
			List<Parameter> variables = new List<Parameter>();
			variables.Add(new Parameter() { name = "q", parameter = (object)4d });
			variables.Add(new Parameter() { name = "Q", parameter = (object)10d });


			//var demandCurve = new DemandCurve(string.Empty, "((a+b)-(a+b)*(a+b))/-2b");			
			var demandCurve = new DemandCurve(string.Empty, "2q^2-q+2", "2q^2/3+2q-q^2/2", "q", "q^2/2");
			//var demandCurve = new DemandCurve(string.Empty, "((q+2)(q^2))^2", "q^2/2+2q", "q", "q^2/2");
			//var demandCurve = new DemandCurve(string.Empty, "Q(q+1)", "q^2/2+2q", "q", "q^2/2");
			double result1 = 0;
			double result12 = 0;
			//variables.Add(new Parameter() { name = "c", parameter = (object)3m });
			//var result = demandCurve.EvalBuyDemandCurve(variables,3);
			result1 = demandCurve.EvalBuyDemandCurve(variables);
			result12 = demandCurve.EvalBuyDemandCurve(variables, 2);

			string a = string.Empty;
			string b = string.Empty;
		}
		public static void pruebaMarket()
		{
			Market m = new Market();
			m.CurrentStock = new Stock();
			var resourceTypes = new List<ResourceType>();
			var resources = generateBasicResources(resourceTypes);
			var resourceQ = new List<ResourceQuantity>();
			foreach(var resource in resources)
			{
				resourceQ.Add(new ResourceQuantity() { Quantity = 1, Resource = resource });
			}
			m.CurrentStock.addResources(resourceQ);
			ConcurrentExchangePetition petition = new ConcurrentExchangePetition();
			Player owner1 = new Player() { name = "owner1" };
			Player owner2 = new Player() { name = "owner2" };
			var resourcesOwner1 = new List<ResourceQuantityMarket>();
			resourcesOwner1.Add(new ResourceQuantityMarket() { Quantity = 3, Resource = resources[1],petitionType = Enumerables.ExpressionEnumerables.petitionType.BUY });
			resourcesOwner1.Add(new ResourceQuantityMarket() { Quantity = 3, Resource = resources[0], petitionType = Enumerables.ExpressionEnumerables.petitionType.SELL });
			petition.Petitions.Add(new PlayerPetition() { Owner = owner1, Resources = resourcesOwner1 });
			var resourcesOwner2 = new List<ResourceQuantityMarket>();
			resourcesOwner2.Add(new ResourceQuantityMarket() { Quantity = 2, Resource = resources[2], petitionType = Enumerables.ExpressionEnumerables.petitionType.BUY });
			resourcesOwner2.Add(new ResourceQuantityMarket() { Quantity = 5, Resource = resources[1], petitionType = Enumerables.ExpressionEnumerables.petitionType.SELL });
			petition.Petitions.Add(new PlayerPetition() { Owner = owner2, Resources = resourcesOwner2 });
			List<DemandCurveResources> curves = new List<DemandCurveResources>();
			var curve = new DemandCurveResources();
			curve.resources = resources;
			curve.demandCurve = new DemandCurve("normal","q+Q","q^2/2 +Q*q","q+Q-1", "q^2/2 +Q*q-q");
			curves.Add(curve);
			m.addDemandCurve(curves);
			var result = m.concurrentExchangeResponse(petition);
			string a = "";
		}
		public static List<Resource> generateBasicResources(List<ResourceType> resourceTypes)
		{
			var resources = new List<Resource>();
			ResourceType type = new ResourceType() { Name = "Madera" };
			ResourceType type2 = new ResourceType() { Name = "Comida" };
			ResourceType type3 = new ResourceType() { Name = "Piedra" };
			resources.Add(new Resource() { Name = "Madera", Type = type });
			resources.Add(new Resource() { Name = "Fruta", Type = type2 });
			resources.Add(new Resource() { Name = "Peces", Type = type2 });
			resources.Add(new Resource() { Name = "Piedra", Type = type3 });
			return resources;
		}
    }
}
