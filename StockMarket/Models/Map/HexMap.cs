using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Models.Map
{
	public class HexMap
	{
		public Tile[,] Map { get; set; }
		public int ySize { get; set; }
		public int xSize { get; set; }

		public HexMap(int x, int y)
		{
			Map = new Tile[x, y];
			for (int i = 0; i < Map.GetLength(0); i++)
			{
				for (int j = 0; j < Map.GetLength(1); j++)
				{
					Map[i, j] = new Tile(i, j);
				}
			}
			ySize = y;
			xSize = x;
		}

		public IEnumerable<Tile> getSurroundedTiles(int x, int y, int distance)
		{
			List<Tile> tiles = new List<Tile>();

			for (int j = 0; j <= distance; j++)
			{
				var xPosition1 = (x + j) % xSize;
				var xPosition2 = (x - j + xSize) % xSize;
				if (j % 2 == 0)
				{
					for (int i = 0; i <= distance - j / 2; i++)
					{
						if (j != 0 || i != 0)
						{
							var yPosition1 = (y + i) % ySize;							
							var yPosition2 = (y - i + ySize) % ySize;

							tiles.Add(Map[xPosition1, yPosition1]);
							tiles.Add(Map[xPosition1, yPosition2]);
							tiles.Add(Map[xPosition2, yPosition1]);
							tiles.Add(Map[xPosition2, yPosition2]);
						}
					}
				}
				else
				{
					if (x % 2 == 0)
					{
						for (int i = 0; i < distance - j / 2; i++)
						{
							var yPosition1 = (y - (1 + i) + ySize) % ySize;
							var yPosition2 = (y + i) % ySize;

							tiles.Add(Map[xPosition1, yPosition1]);
							tiles.Add(Map[xPosition1, yPosition2]);
							tiles.Add(Map[xPosition2, yPosition1]);
							tiles.Add(Map[xPosition2, yPosition2]);
						}
					}
					else
					{
						for (int i = 0; i < distance - j / 2; i++)
						{
							var yPosition1 = (y - i + ySize) % ySize;
							var yPosition2 = (y + 1 + i) % ySize;

							tiles.Add(Map[xPosition1, yPosition1]);
							tiles.Add(Map[xPosition1, yPosition2]);
							tiles.Add(Map[xPosition2, yPosition1]);
							tiles.Add(Map[xPosition2, yPosition2]);
						}
					}
				}

			}
			return tiles.Distinct();
		}
	}
}
