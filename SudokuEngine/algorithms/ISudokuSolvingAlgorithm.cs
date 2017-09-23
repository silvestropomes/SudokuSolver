using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	public interface ISudokuSolvingAlgorithm
	{
		SudokuGame applyAlgorithm(SudokuGame game);
	}
}
