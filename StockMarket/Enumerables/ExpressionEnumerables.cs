using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Enumerables
{
    public static class ExpressionEnumerables
    {
        public enum operations
        {
            SUM,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
            NEGATE,
            POWER
        }
		public enum variables
		{			
			resourceQuantity,
			typeQuantity,
			currentMoney
		}
		public enum specialOperations
		{
			log,
			min,
			max
		}
    }
}
