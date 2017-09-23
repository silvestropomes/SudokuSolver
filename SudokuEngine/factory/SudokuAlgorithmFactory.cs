using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.algorithms;

namespace SudokuEngine.factory
{
	public class SudokuAlgorithmFactory
	{
		private static ISudokuSolvingAlgorithm _logicalBasedInstance = null;
		private static ISudokuSolvingAlgorithm _bruteForceAlgoInstance = null;

		/// <summary>
		/// <para>Get instance of LogicalBasedSolvingAlgorithm</para>
		/// <para>Singleton</para>
		/// </summary>
		/// <returns></returns>
		public static ISudokuSolvingAlgorithm LogicalBasedAlgo
		{
			get
			{
				if (_logicalBasedInstance == null)
				{
					_logicalBasedInstance = new LogicalBasedSolvingAlgorithm();
				}

				return _logicalBasedInstance;
			}
		}

		public static ISudokuSolvingAlgorithm BruteForceAlgo
		{
			get
			{
				if (_bruteForceAlgoInstance == null)
				{
					_bruteForceAlgoInstance = new BruteForceSolvingAlgorithm();
				}

				return _bruteForceAlgoInstance;
			}
		}

	}
}
