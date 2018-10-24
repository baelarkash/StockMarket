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
            //var demandCurve = new DemandCurve(string.Empty, "((a+b)-(a+b)*(a+b))/-2b");
            var demandCurve = new DemandCurve(string.Empty, "a_1 ^ a_1","");
            //var demandCurve = new DemandCurve(string.Empty, "a+b+c");

            List<Parameter> variables = new List<Parameter>();
            variables.Add(new Parameter() { name = "a_1", parameter = (object)4 });
            //variables.Add(new Parameter() { name = "b", parameter = (object)2m });
            //variables.Add(new Parameter() { name = "c", parameter = (object)3m });
            var result = demandCurve.EvalDemandCurve(variables);
            string a = string.Empty;
            string b = string.Empty;
            ///TODO Crear funcion para la integral de la funcion original dentro de demandCurve
            ///TODO Expresiones del modulo Math (log,max,min...)  
            ///TODO Expresiones del tipo (a+b)(a+b)
            ///TODO controlar la venta de recursos para que no sea menor que 0
            ///TODO Gestionar peticiones de compra y venta simultaneas para distintas peticiones

        }
    }
}
