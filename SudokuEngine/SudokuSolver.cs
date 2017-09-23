using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;
using SudokuEngine.algorithms;

namespace SudokuEngine
{
	public class SudokuSolver
	{
		public static SudokuGame solve(SudokuGame game, ISudokuSolvingAlgorithm algo)
		{
			SudokuGame processedGame = algo.applyAlgorithm(game);
			return processedGame;
		}
	}
}
