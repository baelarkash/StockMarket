using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StockMarket.Models;
using StockMarket.Enumerables;
namespace StockMarket.Utils
{
    public static class ExpressionReader
    {
        public static List<InternalFunction> CreateExpression(string expression,List<string> parameters) {
            List<string> variables = new List<string>();
            List<InternalFunction> delegates = new List<InternalFunction>();
            while (GetExpression(ref expression, variables,delegates)) { }
            parameters = delegates.SelectMany(x => x.parameters.Where(y => !y.Contains("internal_var_"))).Distinct().ToList();
            return delegates;
        }

        public static Delegate GenerateDelegate(string expression,List<ParameterExpression> parameters)
        {
            var internalExpression = new List<ComplexExpression>();            
            expression = GenerateExpression(ExpressionEnumerables.operations.MULTIPLY, expression,internalExpression,parameters);
            expression = GenerateExpression(ExpressionEnumerables.operations.DIVIDE, expression, internalExpression, parameters);
            expression = GenerateExpression(ExpressionEnumerables.operations.SUM, expression, internalExpression, parameters);
            expression = GenerateExpression(ExpressionEnumerables.operations.SUBTRACT, expression, internalExpression, parameters);
            expression = GenerateExpression(ExpressionEnumerables.operations.NEGATE, expression, internalExpression, parameters);
            var lambda =  Expression.Lambda(internalExpression.Last().Expression, parameters.ToArray());
            return lambda.Compile();            
        }

        private static bool GetExpression(ref string expression,List<string> variables,List<InternalFunction> delegates)
        {
            var regex = new Regex("(\\([\\w+*/-]+\\))");
            //var regex = new Regex("((?:\\()[\\w+*/-]+(?:\\)))");
            var matches = regex.Matches(expression);
            if (matches.Count > 0)
            {
                int accumulativeLenghtChange = 0;
                foreach (Match match in matches)
                {
                    string variable = "internal_var_" + variables.Count().ToString();
                    //expression = expression.Replace(match.ToString(), variable);
                    expression= expression.Substring(0, accumulativeLenghtChange + match.Index) + variable + expression.Substring(accumulativeLenghtChange + match.Index + match.Length);
                    accumulativeLenghtChange += variable.Length - match.Length;
                    var parameters = new List<ParameterExpression>();
                    var function = GenerateDelegate(match.ToString().Replace("(", "").Replace(")", ""), parameters);
                    delegates.Add(new InternalFunction { function = function, variableName = variable, parameters = parameters.Select(x => x.Name).ToList() });
                    variables.Add(variable);
                }
            }
            else
            {
                string variable = "end";
                var parameters = new List<ParameterExpression>();
                var function = GenerateDelegate(expression, parameters);
                delegates.Add(new InternalFunction { function = function, variableName = variable, parameters = parameters.Select(x => x.Name).ToList() });
                variables.Add(variable);
            }
            
            return matches.Count > 0;
        }
        private static Expression CheckParameter(string name, List<ParameterExpression> parameters,List<ComplexExpression> expressions)
        {
            //TODO: Mejorar
            if (!string.IsNullOrEmpty(name))
            {
                var reg = new Regex("^[\\d]+");
                var match = reg.Matches(name);
                Expression expression;
                if (match.Count > 0)
                {
                    name = name.Replace(match[0].ToString(), "");
                }
                if (string.IsNullOrEmpty(name))
                {
                    var multiply = decimal.Parse(match[0].ToString());
                    return Expression.Constant(multiply);
                }
                var value = expressions.FirstOrDefault(x => x.VariableName == name);
                if (value != null)
                {
                    expression = value.Expression;
                }
                else if (parameters.FirstOrDefault(x => x.Name == name) == null)
                {
                    var parameter = Expression.Variable(typeof(decimal), name);
                    parameters.Add(parameter);
                    expression = parameter;
                }
                else
                {
                    expression = parameters.FirstOrDefault(x => x.Name == name);
                }
                if (match.Count > 0)
                {
                    var multiply = decimal.Parse(match[0].ToString());
                    return Expression.Multiply(Expression.Constant(multiply), expression);
                }
                else
                {
                    return expression;
                }
            }
            return null;
        }
        private static string GenerateExpression(ExpressionEnumerables.operations operation,string input, List<ComplexExpression> expressionList,List<ParameterExpression> parameters)
        {
            var expReg = string.Empty;
            char splitChar= '+';
            switch (operation)
            {
                case (ExpressionEnumerables.operations.MULTIPLY):
                    expReg = "\\w+[*]\\w+"; 
                    splitChar = '*';break;
                case (ExpressionEnumerables.operations.SUM):
                    expReg = "\\w+[+]\\w+"; 
                    splitChar = '+';break;
                case (ExpressionEnumerables.operations.SUBTRACT):
                    expReg = "\\w+[-]\\w+";
                    splitChar = '-'; break;
                case (ExpressionEnumerables.operations.NEGATE):
                    expReg = "[-]\\w+"; 
                    splitChar = '-';break;
                case (ExpressionEnumerables.operations.DIVIDE):
                    expReg = "\\w+[/]\\w+";
                    splitChar = '/'; break;
            }
            var regex = new Regex(expReg);
            var matches = regex.Matches(input);
            while (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    var values = match.ToString().Split(splitChar);
                    var expression = GenerateOperation(operation, CheckParameter(values[0], parameters, expressionList), CheckParameter(values[1], parameters, expressionList));
                    string varName = "internal_val_" + expressionList.Count().ToString();
                    expressionList.Add(new ComplexExpression() { Expression = expression, VariableName = varName });
                    input = input.Replace(match.ToString(), varName);
                }
                matches = regex.Matches(input);
            }
            return input;
        }
        private static Expression GenerateOperation(ExpressionEnumerables.operations operation, Expression left,Expression right)
        {
            Expression exp = null;
            switch (operation)
            {
                case (ExpressionEnumerables.operations.SUM):
                    exp = Expression.Add(left, right);
                    break;
                case (ExpressionEnumerables.operations.SUBTRACT):
                    exp = Expression.Subtract(left, right);
                    break;
                case (ExpressionEnumerables.operations.MULTIPLY):
                    exp = Expression.Multiply(left, right);
                    break;
                case (ExpressionEnumerables.operations.DIVIDE):
                    exp = Expression.Divide(left, right);
                    break;
                case (ExpressionEnumerables.operations.NEGATE):
                    exp = Expression.Negate(right);
                    break;
                default:
                    break;
            }
            return exp;
        }

    }
}
