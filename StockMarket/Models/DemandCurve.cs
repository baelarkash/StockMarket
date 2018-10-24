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
        public ComplexFunction Function { get; set; }
        public DemandCurve(string name, string expression,string integral)
        {

            Name = name;
            Function = new ComplexFunction(expression, integral);
        }
        public double EvalDemandCurve(List<Parameter> parameters)
        {
            return Function.EvalFunction(parameters);
        }
        public double EvalDemandCurve(double currentResourceQuantity,double endResourceQuantity,double currentTypeQuantity)
        {
            ///TODO
            return 1;
        }
    }
}
