using StockMarket.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Utils
{
	public static class ResourceUtils
	{
		public static List<ResourceQuantity> addResource(ResourceQuantity resource,List<ResourceQuantity> list)
		{
			var item = list.FirstOrDefault(x => x.Resource == resource.Resource);
			if(item == null)
			{
				list.Add(resource);
			}
			else
			{
				item.Quantity += resource.Quantity;
			}
			return list;
		}
	}
}
