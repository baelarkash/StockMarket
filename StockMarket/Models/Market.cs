﻿using System;
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
        private DemandCurve GetDemandCurve(Resource resource) {
            DemandCurve curve;
            curve = demandCurves.FirstOrDefault(x => x.resources.Any(y => y == resource)).demandCurve;
            if (curve==null) {
                curve = demandCurves.FirstOrDefault(x => x.resourceTypes.Any(y => y.Name == resource.Type.Name)).demandCurve;
            }
            return curve;
        }
        public double sellResources(List<ResourceQuantity> resources)
        {
            double total = 0;
            foreach (var resource in resources)
            {
                var curve = GetDemandCurve(resource.Resource);
                if(curve != null)
                {
                    var item = CurrentStock.Products.FirstOrDefault(x => x.Resource == resource.Resource);
                    var type = CurrentStock.Products.Where(x => x.Resource.Type == resource.Resource.Type);
					List<Parameter> parameters = new List<Parameter>();
					parameters.Add(new Parameter() { name = "q", parameter = item.Quantity });
					total += curve.EvalSellDemandCurve(parameters,resource.Quantity);
                    item.Quantity += resource.Quantity;
                }
            }
            return total;
        }
        public double buyResources(List<ResourceQuantity> resources) 
        {
            double total = 0;
            foreach (var resource in resources)
            {
                var curve = GetDemandCurve(resource.Resource);
                if (curve != null)
                {
                    var item = CurrentStock.Products.FirstOrDefault(x => x.Resource == resource.Resource);
                    var type = CurrentStock.Products.Where(x => x.Resource.Type == resource.Resource.Type);
					List<Parameter> parameters = new List<Parameter>();
					parameters.Add(new Parameter() { name = "q", parameter = item.Quantity });
                    total += curve.EvalBuyDemandCurve(parameters, -resource.Quantity);
                    item.Quantity -= resource.Quantity;
                }
            }
            return total;
        }
        public double exchangeResources(List<ResourceQuantity> buyList, List<ResourceQuantity> sellList)
        {
            double total = 0;
            total += sellResources(sellList);
            total -= buyResources(buyList);
            return total;
        }
        public ConcurrentExchangeResponse exchangeResponse(ConcurrentExchangePetition petition)
		{
			var result = new ConcurrentExchangeResponse();
			List<ResourceQuantity> requestedResources = new List<ResourceQuantity>();			
			petition.Petitions.ForEach(x => requestedResources.AddRange(x.Resources));
			foreach (var item in requestedResources.GroupBy(x=>x.Resource))
			{
				var curve = GetDemandCurve(item.Key);
				var resources = CurrentStock.Products.FirstOrDefault(x => x.Resource == item.Key);
				var type = CurrentStock.Products.Where(x => x.Resource.Type == item.Key.Type);
			}
			
			return result;
		}

    }
}
