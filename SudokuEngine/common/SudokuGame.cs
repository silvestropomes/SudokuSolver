using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using SudokuEngine.common;

namespace SudokuEngine.common
{
	[Serializable]
	public class SudokuGame
	{
		public List<List<SudokuCell>> GameMatrix { get; set; }

		public SudokuGame()
		{
			GameMatrix = new List<List<SudokuCell>>();
		}

		public SudokuGame DeepCloneWithSerialization()
		{
			System.IO.MemoryStream mStream = new System.IO.MemoryStream();

			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(mStream, this);
			mStream.Seek(0, SeekOrigin.Begin);
			return (SudokuGame)formatter.Deserialize(mStream);

		}

		// controllo di coerenza del gioco
		public bool isValid()
		{
			return
				checkConsistencyRow() &&
				checkConsistencyColumn() &&
				checkConsistencyBox();
			
		}

		private bool checkConsistencyGenericSpace(List<SudokuCell> space)
		{
			List<int> cellValues = new List<int>();
			foreach (SudokuCell cell in space)
			{
				if (cell.Value == 0)
				{
					// skip
				}
				else if (cellValues.Contains(cell.Value))
				{
					// trovato un duplicato
					return false;
				}
				else
				{
					cellValues.Add(cell.Value);
				}
			}

			return true;
		}

		private bool checkConsistencyRow()
		{
			for (int rowIndex = 0; rowIndex < GameMatrix.Count; ++rowIndex)
			{
				List<SudokuCell> row = SudokuUtility.getMatrixSubSpace(this, new MatrixCoords { TopologyType = SpaceType.ROW, ROW = rowIndex });
				if(!checkConsistencyGenericSpace(row)) return false;
			}
			
			return true;
		}

		private bool checkConsistencyColumn()
		{
			for (int colIndex = 0; colIndex < GameMatrix.Count; ++colIndex)
			{
				List<SudokuCell> column = SudokuUtility.getMatrixSubSpace(this, new MatrixCoords { TopologyType = SpaceType.COLUMN, COLUMN = colIndex });
				if(!checkConsistencyGenericSpace(column)) return false;
			}
			
			return true;
		}

		private bool checkConsistencyBox()
		{
			

			for (int boxRowIndex = 0; boxRowIndex < SudokuUtility.DEFAULT_OUTERBOX_SIZE; ++boxRowIndex)
			{
				for (int boxColIndex = 0; boxColIndex < SudokuUtility.DEFAULT_OUTERBOX_SIZE; ++boxColIndex)
				{
					List<SudokuCell> box = SudokuUtility.getMatrixSubSpace(this, new MatrixCoords
					{
						TopologyType = SpaceType.BOX,
						ROW = boxRowIndex * 3,
						COLUMN = boxColIndex * 3
					});
					if(!checkConsistencyGenericSpace(box)) return false;

				}
			}
			
			return true;
		}

		// "true" se il game ha tutti i valori impostati ed è valido
		public bool isSolved()
		{
			foreach(List<SudokuCell> row in this.GameMatrix) {
				foreach (SudokuCell cell in row)
				{
					if (cell.Value == 0) return false;
				}
			}

			return true;
		}

	}

	[Serializable]
	public class SudokuCell
	{
		public virtual int Value { get; set; }
		

		public SudokuCell() {
			Value = 0;
		}

		public SudokuCell(int v)
		{
			this.Value = v;
		}

	}
}
