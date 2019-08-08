using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
    public class ResourceType: IConfigurable
	{
        public string Name { get; set; }
		public override bool Equals(object obj)
		{
			return this.Name == ((ResourceType)obj).Name;
		}
		public override int GetHashCode()
		{
			return Name.Length;
		}
		public static bool operator == (ResourceType resourceType,ResourceType comparison)
		{
			return resourceType.Name == ((ResourceType)comparison).Name;
		}
		public static bool operator !=(ResourceType resourceType, ResourceType comparison)
		{
			return resourceType.Name != ((ResourceType)comparison).Name;
		}
	}

}
