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
		public bool changeJob(Job newJob)
		{
			bool resultado = true;
			var requirements = newJob.Requirements.getRequirements<WorkerJob>();
			foreach(var requirement in requirements)
			{
				var aux = this.Jobs.FirstOrDefault(x => x.Job == requirement.Job);
				if (aux == null|| aux.Level >= requirement.Level) {
					resultado = false;
					break;
				}
			}
			var jobToAdd = this.Jobs.FirstOrDefault(x => x.Job == newJob);
			if (jobToAdd == null)
			{
				this.Jobs.Add(new WorkerJob(newJob,0));
			}
			return resultado;
		}
	}
}
