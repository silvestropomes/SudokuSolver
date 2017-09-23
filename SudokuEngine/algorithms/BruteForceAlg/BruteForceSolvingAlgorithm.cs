using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	public class BruteForceSolvingAlgorithm :ISudokuSolvingAlgorithm
	{
		protected const int DEFAULT_OUTERBOX_SIZE = 3;

		internal BruteForceSolvingAlgorithm() { ;}
	
		
		public SudokuGame applyAlgorithm(SudokuGame game)
		{
			SudokuGame bruteForceGame = BruteForceAlgoMapper.BaseGameToBruteForceGame(game.DeepCloneWithSerialization());

			// inizializzo la prima scelta ed appiattisco le celle rimanenti
			List<BruteForceAlgoCell> bfCellList = new List<BruteForceAlgoCell>();
			foreach (List<SudokuCell> row in bruteForceGame.GameMatrix)
			{
				foreach (BruteForceAlgoCell cell in row)
				{
					if (!cell.isValueReallyFinal())
					{
						bfCellList.Add(cell);
					}
				}
			}

			if (bfCellList.Count > 0)
			{
				solveGameRecursively(bruteForceGame, bfCellList, 0);
			}

			return bruteForceGame;
			// nessuna soluzione trovata
			//throw new SudokuSolutionNotFound("soluzione non trovata");
			
		}

		private bool solveGameRecursively(SudokuGame game, List<BruteForceAlgoCell> bfCellList, int listIndex)
		{
			if (bfCellList != null && bfCellList.Count > listIndex)
			{
				BruteForceAlgoCell currentCell = bfCellList[listIndex];
				foreach (int tempValue in currentCell.AllowableValues)
				{
					currentCell.TempValue = tempValue;

					// se il valore scelto non è ammissibile, provo con il successivo
					if (!game.isValid()) continue;

					bool solvedGame = true;
					if (bfCellList.Count > (listIndex + 1))
					{
						solvedGame = solveGameRecursively(game, bfCellList, listIndex+1);
					}
					// se il gioco è valido, la soluzione è stata raggiunta
					if (game.isValid() && solvedGame) return true;
					
				}
				// se nessun valore ha dato esito positivo, ripristino il valore nullo
				currentCell.TempValue = 0;
			}

			// spazzate tutte le combinazioni, nessuna soluzione trovata
			return false;
		}


		private SudokuGame finalizeGame(SudokuGame game)
		{
			foreach (List<SudokuCell> row in game.GameMatrix)
			{
				foreach (BruteForceAlgoCell cell in row)
				{
					cell.Value = cell.TempValue;
				}
			}

			return game;
		}
	}
}
