using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class WorkerJob
	{
		public Job Job { get; set; }
		public int Level { get; set; }
		public double Experience { get; set; }
	}
}
