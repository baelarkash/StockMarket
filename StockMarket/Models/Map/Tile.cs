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
		public int xPosition { get; set; }
		public int yPosition { get; set; }
		public TileType MainTileType { get; set; }
		public TileType SecondaryTileType { get; set; }
		public MapResource MapResource { get; set; }
		public List<Worker> Workers { get; set; }

		public Tile(int x, int y)
		{
			this.xPosition = x;
			this.yPosition= y;
		}
		public override string ToString()
		{
			return "(" + xPosition + "," + yPosition + ")";
		}
	}
}
