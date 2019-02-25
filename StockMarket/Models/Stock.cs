using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models
{
    public class Stock
    {
        public List<ResourceQuantity> Products { get; set; }
		public Stock()
		{
			Products = new List<ResourceQuantity>();
		}

		public bool addResources(List<ResourceQuantity> resources)
		{
			bool result = true;
			foreach (var resource in resources)
			{
				var item = Products.FirstOrDefault(x => x.Resource == resource.Resource);
				if (item != null)
				{
					item.Quantity += resource.Quantity;

				}
				else
				{
					Products.Add(resource);
				}
			}
			return result;
		}
		public bool removeResources(List<ResourceQuantity> resources)
		{
			bool result = true;
			foreach (var resource in resources)
			{
				var item = Products.FirstOrDefault(x => x.Resource == resource.Resource);
				if (item != null)
				{
					if (item.Quantity - resource.Quantity < 0)
					{
						return false;
					}
					item.Quantity -= resource.Quantity;
				}
				else
				{
					return false;
				}
			}
			return result;
		}
	}
}
