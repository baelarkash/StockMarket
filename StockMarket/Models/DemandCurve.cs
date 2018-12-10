using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace StockMarket.Models
{
    public class DemandCurve
    {
        public string Name { get; set; }
        public ComplexFunction BuyFunction { get; set; }
		public ComplexFunction SellFunction { get; set; }
		public List<string> parameters { get; set; }
		public DemandCurve(string name, string buyExpression,string buyIntegral, string sellExpression, string sellIntegral)
        {
            Name = name;
			BuyFunction = new ComplexFunction(buyExpression, buyIntegral);
			parameters = BuyFunction.Function.SelectMany(x => x.parameters.Where(y=>!y.Contains("internal_var"))).Distinct().ToList();
			SellFunction = new ComplexFunction(sellExpression , sellIntegral);
		}
        public double EvalBuyDemandCurve(List<Parameter> parameters)
        {
            return BuyFunction.EvalFunction(parameters);
        }
        public double EvalBuyDemandCurve(List<Parameter> parameters, double units)
        {            
            return BuyFunction.EvalFunctionRange(parameters,units);
        }
		public double EvalSellDemandCurve(List<Parameter> parameters)
		{
			return SellFunction.EvalFunction(parameters);
		}
		public double EvalSellDemandCurve(List<Parameter> parameters, double units)
		{
			return SellFunction.EvalFunctionRange(parameters, units);
		}
	}
}

