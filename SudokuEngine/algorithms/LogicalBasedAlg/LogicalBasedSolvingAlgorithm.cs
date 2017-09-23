using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	/// <summary>
	/// <para>Algoritmo basato sulla logica</para>
	/// <para>Elimina i valori non ammissibile controllando i constraint del Sudoku</para>
	/// <para>La logica da per scontato matrici 9x9</para>
	/// <para>Questo algoritmo risolve solo i sudoku facili e medi</para>
	/// </summary>
	public class LogicalBasedSolvingAlgorithm : ISudokuSolvingAlgorithm
	{
		internal LogicalBasedSolvingAlgorithm() { ;}

		public SudokuGame applyAlgorithm(SudokuGame game)
		{
			SudokuGame LogicalBasedGame = LogicalBasedAlgoMapper.BaseGameToLogicalBasedGame(game);
			// per ogni cella, escludo i valori già presenti:
			// - nella riga
			// - nella colonna
			// - nel suo outerBox

			// Algoritmo incrementale. Una singola passata può non essere sufficiente a convergere verso la soluzione.
			// L'algoritmo si ferma appena una passata non riesce a migliorare la precisione della soluzione.
			while (onePassAlgorithm(LogicalBasedGame)) ;
			return LogicalBasedGame;
		}

		protected bool onePassAlgorithm(SudokuGame game)
		{
			bool atLeastOneOperation = false;
			atLeastOneOperation |= useRowConstraint(game);
			atLeastOneOperation |= useColumnConstraint(game);
			atLeastOneOperation |= useBoxConstraint(game);

			return atLeastOneOperation;
		}

		/// <summary>
		/// per ogni cella, rimuove tra gli AllowableValues i valori già presenti nella riga
		/// </summary>
		/// <param name="game"></param>
		/// <returns>'true' se ha operato almeno un'azione</returns>
		protected bool useRowConstraint(SudokuGame game)
		{
			bool atLeastOneOperation = false;
			for (int rowIndex = 0; rowIndex < game.GameMatrix.Count; ++rowIndex)
			{
				List<SudokuCell> row = SudokuUtility.getMatrixSubSpace(game, new MatrixCoords { TopologyType = SpaceType.ROW, ROW = rowIndex });
				bool atLeastOneOperationInRow = false;
				atLeastOneOperationInRow = useConstraintOnGenericSpace(row);

				atLeastOneOperation |= atLeastOneOperationInRow;
			}

			return atLeastOneOperation;
		}

		/// <summary>
		/// per ogni cella, rimuove tra gli AllowableValues i valori già presenti nella colonna
		/// </summary>
		/// <param name="game"></param>
		/// <returns>'true' se ha operato almeno un'azione</returns>
		protected bool useColumnConstraint(SudokuGame game)
		{
			bool atLeastOneOperation = false;
			for (int colIndex = 0; colIndex < game.GameMatrix.Count; ++colIndex)
			{
				List<SudokuCell> column = SudokuUtility.getMatrixSubSpace(game, new MatrixCoords { TopologyType = SpaceType.COLUMN, COLUMN = colIndex });
				bool atLeastOneOperationInColumn = false;
				atLeastOneOperationInColumn = useConstraintOnGenericSpace(column);

				atLeastOneOperation |= atLeastOneOperationInColumn;
			}

			return atLeastOneOperation;
		}

		/// <summary>
		/// per ogni cella, rimuove tra gli AllowableValues i valori già presenti nel rispettivo box
		/// </summary>
		/// <param name="game"></param>
		/// <returns>'true' se ha operato almeno un'azione</returns>
		protected bool useBoxConstraint(SudokuGame game)
		{
			bool atLeastOneOperation = false;
			for (int boxRowIndex = 0; boxRowIndex < SudokuUtility.DEFAULT_OUTERBOX_SIZE; ++boxRowIndex)
			{
				for (int boxColIndex = 0; boxColIndex < SudokuUtility.DEFAULT_OUTERBOX_SIZE; ++boxColIndex)
				{
					List<SudokuCell> box = SudokuUtility.getMatrixSubSpace(game, new MatrixCoords
					{
						TopologyType = SpaceType.BOX,
						ROW = boxRowIndex * 3,
						COLUMN = boxColIndex * 3
					});
					bool atLeastOneOperationInBox = false;
					atLeastOneOperationInBox = useConstraintOnGenericSpace(box);

					atLeastOneOperation |= atLeastOneOperationInBox;
				}
			}

			return atLeastOneOperation;

		}

		protected virtual bool useConstraintOnGenericSpace(List<SudokuCell> space)
		{
			bool atLeastOneOperation = false;
			atLeastOneOperation |= deleteImpossibleValues(space);
			//atLeastOneOperation |= useMoreLogic(space);
			return atLeastOneOperation;
		}

		protected bool deleteImpossibleValues(List<SudokuCell> space)
		{
			bool atLeastOneOperation = false;
			// cerco tutti i valori definitivi
			List<int> rowFinalValues = new List<int>();
			foreach (LogicalBasedAlgoCell cell in space)
			{
				if (cell.Value != 0)
				{
					rowFinalValues.Add(cell.Value);
				}
			}

			foreach (LogicalBasedAlgoCell cell in space)
			{
				int removedValues = cell.AllowableValues.RemoveAll(x => rowFinalValues.Contains(x));
				atLeastOneOperation |= (removedValues > 0);

			}
			// finalizzo i valori appena scoperti
			if (atLeastOneOperation)
			{

				finalizeValues(space);
			}
			return atLeastOneOperation;
		}

		/// <summary>
		/// Vincolo: 
		/// Premessa: in uno spazio ci sono celle con valori non definiti.
		///	          Effettuando l'unione dei possibili valori, almeno uno di questi occorre una ed una sola volta.
		///	Dunque: Il possibile valore, data la sua unicità, è anche l'unico valore possibile per quella cella.
		/// </summary>
		/// <param name="space"></param>
		/// <returns></returns>
		protected bool useMoreLogic(List<SudokuCell> space)
		{
			bool atLeastOneOperation = false;
			Dictionary<int, int> counterTable = new Dictionary<int, int>();
			for (int i = 1; i < 10; ++i)
			{
				counterTable.Add(i, 0);
			}

			List<int> finalValuesList = new List<int>();

			foreach (LogicalBasedAlgoCell cell in space)
			{
				if (cell.Value != 0)
				{
					finalValuesList.Add(cell.Value);
				}
				else
				{
					foreach (int v in cell.AllowableValues)
					{
						++counterTable[v];
					}
				}
			}

			// controllo la cardinalità dei valori
			List<int> valuesToFinalize = new List<int>();

			foreach (var item in counterTable)
			{
				// elemento che occorre una sola volta nello spazio di ricerca
				// e non è già un valore definitivo
				if ( item.Value == 1 && !finalValuesList.Contains(item.Key) )
				{
					valuesToFinalize.Add(item.Key);
				}
			}

			if (valuesToFinalize.Count > 0)
			{
				foreach (LogicalBasedAlgoCell cell in space)
				{
					foreach (int v in cell.AllowableValues)
					{
						if (valuesToFinalize.Contains(v))
						{
							cell.setDefinitiveValue(v);
							atLeastOneOperation = true;
							break;
						}
					}
				}
			}


			return atLeastOneOperation;
		}

		protected void finalizeValues(List<SudokuCell> cellList)
		{
			foreach (LogicalBasedAlgoCell cell in cellList)
			{
				if (cell.AllowableValues.Count == 1)
				{
					cell.setDefinitiveValue(cell.AllowableValues[0]);
				}
			}
		}


	}

}
