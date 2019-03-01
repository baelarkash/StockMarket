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
		public List<Tool> Tools { get; set; }
		public Worker()
		{
			Jobs = new List<WorkerJob>();
			Tools = new List<Tool>();
		}
		public void addExperience(Job job,double experience)
		{
			var aux = Jobs.FirstOrDefault(x => x.Job == job);
			if (aux != null)
			{
				aux.Experience += experience;
			}
			else
			{
				Jobs.Add(new WorkerJob(job, experience));
			}
		}
	}
}
