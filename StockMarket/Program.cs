using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models;
using StockMarket.Utils;
using System.Diagnostics;

namespace StockMarket
{
    class Program
    {
        static void Main(string[] args)
        {
			List<Parameter> variables = new List<Parameter>();
			variables.Add(new Parameter() { name = "q", parameter = (object)4d });
			variables.Add(new Parameter() { name = "Q", parameter = (object)10d });


			//var demandCurve = new DemandCurve(string.Empty, "((a+b)-(a+b)*(a+b))/-2b");			
			var demandCurve = new DemandCurve(string.Empty, "2q^2-q+2","2q^2/3+2q-q^2/2","q","q^2/2");
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

    }
}
