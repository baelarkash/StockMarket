﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Enumerables;

namespace StockMarket.Utils
{
	public static class ExpressionVariables
	{
		private static Dictionary<ExpressionEnumerables.variables, string> variables;
		public static string getVariable(ExpressionEnumerables.variables key)
		{
			string output;
			variables.TryGetValue(key, out output);
			return output;
		}
		static ExpressionVariables()
		{
			variables = new Dictionary<ExpressionEnumerables.variables, string>();
			variables[ExpressionEnumerables.variables.resourceQuantity] = "q";
			variables[ExpressionEnumerables.variables.typeQuantity] = "Q";
			variables[ExpressionEnumerables.variables.currentMoney] = "M";
		}

	}
}
