using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class ComplexFunction
    {
        public string ExpressionLiteral { get; set; }
        public List<InternalFunction> Function { get; set; }
        public string IntegralLiteral { get; set; }
        public List<InternalFunction> IntegralFunction { get; set; }
        public List<string> Parameters { get; set; }
        public List<string> IntegralParameters { get; set; }
        public ComplexFunction(string expression, string integral)
        {
            ExpressionLiteral = expression;
            IntegralLiteral = integral;
            if (!string.IsNullOrEmpty(expression))
            {
                Function = Utils.ExpressionReader.CreateExpression(expression, Parameters);
            }
            if (!string.IsNullOrEmpty(integral))
            {
                Function = Utils.ExpressionReader.CreateExpression(integral, IntegralParameters);
            }
        }
        public double EvalFunction(List<Parameter> parameters)
        {

            List<Parameter> intermediateValues = new List<Parameter>();

            foreach (var lambda in Function)
            {
                List<object> items = new List<object>();

                foreach (var param in lambda.parameters)
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
                intermediateValues.Add(new Parameter() { name = lambda.variableName, parameter = result });
            }


            return (double)intermediateValues.Last().parameter;
        }
        public double EvalFunctionRange(List<Parameter> parameters)
        {
            ///TODO
            List<Parameter> intermediateValues = new List<Parameter>();

            foreach (var lambda in Function)
            {
                List<object> items = new List<object>();

                foreach (var param in lambda.parameters)
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
                intermediateValues.Add(new Parameter() { name = lambda.variableName, parameter = result });
            }


            return (double)intermediateValues.Last().parameter;
        }
    }
}
