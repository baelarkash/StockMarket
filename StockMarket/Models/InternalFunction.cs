using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class InternalFunction
    {
        public Delegate function { get; set; }
        public string variableName { get; set; }
        public List<string> parameters { get; set; }
        public string expressionLiteral { get; set; }
    }
}
