using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	/// <summary>
	/// Contiene i metodi necessari a convertire le strutture in un formato adatto all'algoritmo.
	/// TODO: pensare ad un'interfaccia per questi Mapper
	/// </summary>
	class LogicalBasedAlgoMapper
	{
		public static SudokuGame BaseGameToLogicalBasedGame(SudokuGame game)
		{
			SudokuGame retGame = game.DeepCloneWithSerialization();
			foreach (List<SudokuCell> row in retGame.GameMatrix)
			{
				for (int cellIndex = 0; cellIndex < row.Count; ++cellIndex)
				{
					row[cellIndex] = BaseCellToLogicalBasedCell(row[cellIndex]);
				}
			}

			return retGame;
		}

		public static LogicalBasedAlgoCell BaseCellToLogicalBasedCell(SudokuCell cell)
		{
			if (cell == null) return null;
			if (cell is LogicalBasedAlgoCell) return (LogicalBasedAlgoCell)cell;

			LogicalBasedAlgoCell retCell = null;
			if (cell.Value != 0)
			{
				retCell = new LogicalBasedAlgoCell(cell.Value);
			}
			else
			{
				retCell = new LogicalBasedAlgoCell();
			}

			return retCell;
		}
	}
}
