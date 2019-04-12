using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Models.Common;

namespace StockMarket.Models.Map
{
	public class Tile
	{
		#region "Properties"
		public int xPosition { get; set; }
		public int yPosition { get; set; }
		public TileType MainTileType { get; set; }
		public TileType SecondaryTileType { get; set; }
		public MapResource MapResource { get; set; }
		public List<Worker> Workers { get; set; }
		public Job ActiveJob { get; set; }
		public Building Building { get; set; }

		#endregion
		#region "Contructor"
		public Tile(int x, int y)
		{
			this.xPosition = x;
			this.yPosition= y;
		}
		#endregion
		#region "Methods"
		public override string ToString()
		{
			return "(" + xPosition + "," + yPosition + ")";
		}
		public bool changeActiveJob(Job newJob)
		{
			bool resultado = true;
			
			foreach(var worker in Workers)
			{
				if (!worker.changeJob(newJob)) {
					resultado = false;
					break;
				}
			}
			if(resultado)
				ActiveJob = newJob;
			return resultado;
		}
		#endregion
	}
}
