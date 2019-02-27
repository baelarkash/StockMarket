using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class Worker
	{
		public string Name { get; set; }
		public List<WorkerJob> Jobs { get; set; }
	}
}
