using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuEngine.common
{
	public class SudokuUtility
	{

		public const int DEFAULT_OUTERBOX_SIZE = 3;

		public static List<SudokuCell> getMatrixSubSpace(SudokuGame game, MatrixCoords coords)
		{
			List<SudokuCell> retList = null;
			switch (coords.TopologyType)
			{
				case SpaceType.ROW:
					retList = game.GameMatrix[coords.ROW];
					break;
				case SpaceType.COLUMN:
					retList = new List<SudokuCell>();
					foreach (List<SudokuCell> row in game.GameMatrix)
					{
						retList.Add(row[coords.COLUMN]);
					}
					break;
				case SpaceType.BOX:
					retList = new List<SudokuCell>();
					for (int row = coords.ROW; row < coords.ROW + DEFAULT_OUTERBOX_SIZE; ++row)
					{
						for (int col = coords.COLUMN; col < coords.COLUMN + DEFAULT_OUTERBOX_SIZE; ++col)
						{
							retList.Add(game.GameMatrix[row][col]);
						}
					}
					break;
				case SpaceType.ALL_MATRIX: break;
				default: throw new Exception("LogicalBasedSolvingAlgorithm.getMatrixSubSpace: SpaceType non previsto");
			}

			return retList;

		}
	}

	public enum SpaceType
	{
		ROW,
		COLUMN,
		BOX,
		ALL_MATRIX
	}

	public class MatrixCoords
	{
		public SpaceType TopologyType { get; set; }
		public int ROW { get; set; }
		public int COLUMN { get; set; }

	}
}
