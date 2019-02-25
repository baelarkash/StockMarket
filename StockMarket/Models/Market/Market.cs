using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Market
{
    public class Market
    {

        public Stock CurrentStock { get; set; }
        private List<DemandCurveResources> demandCurves;
        private double currentMoney;
		public Market()
		{
			currentMoney = 100;
			demandCurves = new List<DemandCurveResources>();
		}
		public void addDemandCurve(List<DemandCurveResources> curve)
		{
			demandCurves.AddRange(curve);
		}
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
        public ConcurrentExchangeResponse concurrentExchangeResponse(ConcurrentExchangePetition petition)
		{
			var result = new ConcurrentExchangeResponse();
			List<ResourceQuantityMarket> requestedResources = new List<ResourceQuantityMarket>();			
			petition.Petitions.ForEach(x => requestedResources.AddRange(x.Resources));
			foreach (var item in requestedResources.GroupBy(x=>x.Resource))
			{
				var curve = GetDemandCurve(item.Key);
				var resources = CurrentStock.Products.FirstOrDefault(x => x.Resource == item.Key).Quantity;
				var type = CurrentStock.Products.Where(x => x.Resource.Type == item.Key.Type).Sum(x=>x.Quantity) - resources;

				var sellingResources = item.Where(x=>x.petitionType == Enumerables.ExpressionEnumerables.petitionType.SELL).Sum(x => x.Quantity);
				var parameters = new List<Parameter>();				
				parameters.Add(new Parameter() { name = Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity), parameter = resources });
				parameters.Add(new Parameter() { name = Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.typeQuantity), parameter = type});
				double sellingCost = 0;
				double totalSellingCost = 0;
				if (sellingResources > 1)
				{
					sellingCost = curve.EvalSellDemandCurve(parameters, (sellingResources - 1));
					totalSellingCost = sellingCost * sellingResources;
				}
				else if(sellingResources == 1)
				{
					sellingCost = curve.EvalSellDemandCurve(parameters);
					totalSellingCost = sellingCost;
				}

				//resources += sellingResources;
				//type += sellingResources;
				parameters.First(x=> x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.resourceQuantity)).parameter = resources + sellingResources;
				//parameters.First(x=>x.name == Utils.ExpressionVariables.getVariable(Enumerables.ExpressionEnumerables.variables.typeQuantity)).parameter = type+ sellingResources;
				var buyingResources = item.Where(x => x.petitionType == Enumerables.ExpressionEnumerables.petitionType.BUY).Sum(x => x.Quantity);
				double buyingCost = 0;
				double totalBuyingCost = 0;
				if(buyingResources > 1)
				{
					buyingCost = curve.EvalBuyDemandCurve(parameters, -(buyingResources - 1));
					totalBuyingCost = buyingCost * buyingResources;
				}
				else if(buyingResources == 1)
				{
					totalBuyingCost = curve.EvalBuyDemandCurve(parameters);
					buyingCost = totalBuyingCost;
				}
				
				//Actualizamos los valores de mercado
				this.currentMoney += -(totalBuyingCost + totalSellingCost);
				CurrentStock.Products.FirstOrDefault(x => x.Resource == item.Key).Quantity += sellingResources - buyingResources; 
			}
			
			return result;
		}

    }
}
