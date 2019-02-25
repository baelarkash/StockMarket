using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
    public class DemandCurveResources
    {
        public DemandCurve demandCurve { get; set; }
        public List<Resource> resources { get; set; }
        public List<ResourceType> resourceTypes { get; set; }
    }
}
