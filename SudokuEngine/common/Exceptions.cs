using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuEngine.common
{
	class SudokuSolutionNotFound : Exception
	{
		public SudokuSolutionNotFound()
			: base()
		{
		}

		public SudokuSolutionNotFound(string message)
			: base(message)
		{
		}
	}

	class SudokuGameInvalid : Exception
	{
		public SudokuGameInvalid()
			: base()
		{
		}

		public SudokuGameInvalid(string message) 
			: base(message)
		{
		}
	}
}
