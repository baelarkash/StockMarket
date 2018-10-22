using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Market
    {

        public Stock CurrentStock { get; set; }
        private List<DemandCurveResources> demandCurves;
        private double currentMoney;
        private DemandCurve GetDemandCurve(string resource,bool type) {
            DemandCurve curve;
            if (type) {
                curve = demandCurves.FirstOrDefault(x => x.resourceTypes.Any(y => y.Name == resource)).demandCurve;
            }
            else
            {
                curve = demandCurves.FirstOrDefault(x => x.resources.Any(y => y.Name == resource)).demandCurve;
            }
            return curve;
        }



    }
}
