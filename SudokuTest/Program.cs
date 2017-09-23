using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.factory;
using System.Xml.Serialization;
using SudokuEngine.common;
using SudokuEngine.algorithms;


namespace SudokuTest
{
	class Program
	{
		static void Main(string[] args)
		{
			SudokuEngine.factory.SudokuGameFactory factory = new SudokuGameFactory();
			SudokuGame game = factory.getGameByCSV(@"C:\sudoku\CSVGame.csv");
			//if (!game.isSolved())
			//{
				SudokuGame gameToSolve = game.DeepCloneWithSerialization();
				SudokuGame solvedGame = SudokuEngine.SudokuSolver.solve(gameToSolve, SudokuAlgorithmFactory.LogicalBasedAlgo);
				solvedGame = SudokuEngine.SudokuSolver.solve(solvedGame, SudokuAlgorithmFactory.BruteForceAlgo);
				//solvedGame.isValid();
			//}
				//factory.saveGameSerialized(solvedGame, @"C:\sudoku\propvaSolvedGame.xml");
				factory.saveGameToCSV(solvedGame, @"C:\sudoku\CSVResolvedGame.csv");
				Console.WriteLine("solved:{0}", solvedGame.isSolved() && solvedGame.isValid());
				Console.ReadKey();
			
		}

	}
}
