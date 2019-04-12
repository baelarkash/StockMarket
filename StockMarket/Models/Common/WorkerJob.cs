using StockMarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Common
{
	public class WorkerJob:IRequired
	{
		public Job Job { get; set; }
		public int Level { get; set; }
		public double Experience { get; set; }
		public WorkerJob(Job job, double experience)
		{
			Job = job;
			Experience = experience;
			Level = 1;
		}
	}
}
