using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models;

namespace StockMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var demandCurve = new DemandCurve(string.Empty, "(a+b)+(a+b)");

            List<Parameter> variables = new List<Parameter>();
            variables.Add(new Parameter() { name = "a", parameter = (object)1m });
            variables.Add(new Parameter() { name = "b", parameter = (object)2m });
            variables.Add(new Parameter() { name = "c", parameter = (object)3m });
            var result = demandCurve.EvalDemandCurve(variables);

        }
    }
}
