using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
    public class ResourceQuantity
    {
        public Resource Resource { get; set; }
        public double Quantity { get; set; }
    }
	public class ResourceQuantityMarket:ResourceQuantity
	{
		public Enumerables.ExpressionEnumerables.petitionType petitionType { get; set; }
		
	}
}
