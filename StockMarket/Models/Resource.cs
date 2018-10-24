using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Resource
    {
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public override bool Equals(object obj)
        {
            return this.Name == ((Resource)obj).Name;
        }
    }
}
