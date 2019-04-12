using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Interfaces;

namespace StockMarket.Models.Common
{
	public class Requirement
	{
		public int Amount { get; set; }
		public IRequired Object { get; set; }
	}
	public class Requirements
	{
		public List<Requirement> PreRequisites{ get; set; }
		public List<T> getRequirements<T>() where T : class, IRequired
		{
			List<T> resultado = new List<T>();
			foreach (var requirement in this.PreRequisites)
			{
				T aux = requirement.Object as T;
				if (aux != null)
				{
					resultado.Add(aux);
				}
			}
			return resultado;
		}

	}
}
