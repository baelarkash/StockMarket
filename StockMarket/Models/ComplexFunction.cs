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
				IntegralFunction = Utils.ExpressionReader.CreateExpression(integral, IntegralParameters);
            }
        }
        public double EvalFunction(List<Parameter> parameters)
        {

            List<Parameter> intermediateValues = new List<Parameter>();
			if (checkParameters(parameters, Function))
			{
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
			return -1;
        }
        public double EvalFunctionRange(List<Parameter> parameters,double units)
        {
            ///TODO
            
			if (checkParameters(parameters, IntegralFunction))
			{
				List<Parameter> intermediateValues = new List<Parameter>();
				foreach (var lambda in IntegralFunction)
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
				var parameter =(double) parameters.First(x => x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity)).parameter;
				parameter += units;
				parameters.First(x => x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity)).parameter = (object)parameter;
				List<Parameter> intermediateValues2 = new List<Parameter>();
				foreach (var lambda in IntegralFunction)
				{
					List<object> items = new List<object>();

					foreach (var param in lambda.parameters)
					{
						if (param.Contains("internal_var"))
						{
							items.Add(intermediateValues2.First(x => x.name == param).parameter);
						}
						else
						{
							items.Add(parameters.First(x => x.name == param).parameter);
						}
					}

					var result = lambda.function.DynamicInvoke(items.ToArray());
					intermediateValues2.Add(new Parameter() { name = lambda.variableName, parameter = result });
				}

				return (double)((double)intermediateValues2.Last().parameter - (double)intermediateValues.Last().parameter)/units;
			}
			return -1;

            
        }
		private bool checkParameters(List<Parameter> parameters, List<InternalFunction> functions)
		{
			List<string> variables = new List<string>();
			variables.AddRange(functions.SelectMany(x => x.parameters.Where(y=>!y.Contains("internal_var"))).ToList());
			variables = variables.Distinct().ToList();
			List<string> items = parameters.Select(x => x.name).ToList();
			return items.All(x => variables.Contains(x));

		}
    }
}
