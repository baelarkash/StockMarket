using StockMarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
    public class Resource: IConfigurable
	{
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public override bool Equals(object obj)
        {
			//Resource aux = (Resource)obj;
			//return this.Name == aux.Name && this.Type == aux.Type;
			return this.Name == ((Resource)obj).Name;
		}
    }
}
