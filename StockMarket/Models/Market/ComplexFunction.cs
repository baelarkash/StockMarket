using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
    public class ComplexFunction
    {
        public string ExpressionLiteral { get; set; }
        public List<InternalFunction> Function { get; set; }
        public string IntegralLiteral { get; set; }
        public List<InternalFunction> IntegralFunction { get; set; }
        //public List<string> Parameters { get; set; }
        //public List<string> IntegralParameters { get; set; }
        public ComplexFunction(string expression, string integral)
        {
            ExpressionLiteral = expression;
            IntegralLiteral = integral;
            if (!string.IsNullOrEmpty(expression))
            {
                Function = Utils.ExpressionReader.CreateExpression(expression);
            }
            if (!string.IsNullOrEmpty(integral))
            {
				IntegralFunction = Utils.ExpressionReader.CreateExpression(integral);
            }
        }
		public double EvalFunction(List<Parameter> parameters)
		{
			return EvalSimpleFunction(parameters, Function);
		}

		private double EvalSimpleFunction(List<Parameter> parameters,List<InternalFunction> functionToEvaluate)
        {
            List<Parameter> intermediateValues = new List<Parameter>();
			if (checkParameters(parameters, functionToEvaluate))
			{
				foreach (var lambda in functionToEvaluate)
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
			var aux = new List<Parameter>();
			var firstValue = EvalSimpleFunction(parameters, IntegralFunction);
			
			aux.AddRange(parameters);
			//var parameter = (double)aux.First(x => x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity)).parameter;
			//parameter += units;
			aux.RemoveAll(x => x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity));
			var parameter = new Parameter();
			parameter.name = Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity);
			parameter.parameter = (double)parameters.First(x => x.name == parameter.name).parameter + units;
			aux.Add(parameter);
			var secondValue = EvalSimpleFunction(aux, IntegralFunction);
			return (double)(secondValue - firstValue)/Math.Abs(units);           
        }
		private bool checkParameters(List<Parameter> parameters, List<InternalFunction> functions)
		{
			List<string> variables = new List<string>();
			variables.AddRange(functions.SelectMany(x => x.parameters.Where(y=>!y.Contains("internal_var"))).ToList());
			variables = variables.Distinct().ToList();
			List<string> items = parameters.Select(x => x.name).ToList();
			return variables.All(x => items.Contains(x));

		}
    }
}
