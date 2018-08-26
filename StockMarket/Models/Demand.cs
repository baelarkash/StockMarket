using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Demand
    {
        public List<Resource> Resource { get; set; }
        public DemandCurve Curve { get; set; }

    }
}
