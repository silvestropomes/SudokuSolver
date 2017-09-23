using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	public class BruteForceAlgoMapper
	{
		public static SudokuGame BaseGameToBruteForceGame(SudokuGame game)
		{
			SudokuGame retGame = game.DeepCloneWithSerialization();
			foreach (List<SudokuCell> row in retGame.GameMatrix)
			{
				for (int cellIndex = 0; cellIndex < row.Count; ++cellIndex)
				{
					row[cellIndex] = BaseCellToBruteForceCell(row[cellIndex]);
				}
			}

			return retGame;
			
			
		}

		public static SudokuCell BaseCellToBruteForceCell(SudokuCell cell)
		{
			if (cell == null) return null;
			if (cell is BruteForceAlgoCell) return (BruteForceAlgoCell)cell;

			BruteForceAlgoCell retCell = null;
			if (cell.Value != 0)
			{
				retCell = new BruteForceAlgoCell(cell.Value);
				// se il valore è stato definito, non serve propagare gli AllowableValues.
			}
			else
			{
				retCell = new BruteForceAlgoCell();

				// propago eventuali AllowableValues. Questo ottimizza il lavoro dell'algoritmo BruteForce.
				if (cell is LogicalBasedAlgoCell)
				{
					retCell.AllowableValues = new List<int>(((LogicalBasedAlgoCell)cell).AllowableValues);
				}
			}

			return retCell;
		}

	}
}
