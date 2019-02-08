using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class ResourceType
    {
        public string Name { get; set; }
		public override bool Equals(object obj)
		{
			return this.Name == ((ResourceType)obj).Name;
		}
	}
}
