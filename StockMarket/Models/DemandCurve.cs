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
        public string expressionLiteral { get; set; }
        public List<InternalFunction> Function { get; set; }
        public List<string> Parameters { get; set; }
        public DemandCurve(string name, string expression)
        {
            expressionLiteral = expression;
            Name = name;
            if (!string.IsNullOrEmpty(expression))
            {
                Function = Utils.ExpressionReader.CreateExpression(expression,Parameters);
            }
        }
        public double EvalDemandCurve(List<Parameter> parameters)
        {

            List<Parameter> intermediateValues = new List<Parameter>();
            foreach (var lambda in Function)
            {
                List<object> items = new List<object>();
                               
                foreach(var param in lambda.parameters)
                {
                    if (param.Contains("internal_var"))
                    {
                        items.Add(intermediateValues.First(x => x.name == param).parameter);
                    }
                    else
                    {
                        items.Add(parameters.First(x => x.name == param).parameter);
                    }
                }

                var result = lambda.function.DynamicInvoke(items.ToArray());
                intermediateValues.Add(new Parameter() { name = lambda.variableName, parameter = result});
            }
            
            
            return (double)intermediateValues.Last().parameter;
        }

    }
}
