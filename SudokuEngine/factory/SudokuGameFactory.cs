using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SudokuEngine.common;
using System.IO;

namespace SudokuEngine.factory
{
	public class SudokuGameFactory
	{
		string[] csvSep = { "," };
		string csvEmptyValue = "-";

		public SudokuGame getGameByCSV(string path)
		{
			SudokuGame sGame = new SudokuGame();

			string[] csv = File.ReadAllLines(path);
			foreach (string row in csv)
			{
				List<SudokuCell> sudokuRow = new List<SudokuCell>();
				string[] cells = row.Split(csvSep, StringSplitOptions.None);
				foreach (string cell in cells)
				{
					if (string.IsNullOrWhiteSpace(cell) || cell.Equals(csvEmptyValue))
					{
						sudokuRow.Add(new SudokuCell());
					}
					else
					{
						sudokuRow.Add(new SudokuCell(int.Parse(cell)));
					}
				}

				sGame.GameMatrix.Add(sudokuRow);
			}

			return sGame;
		}

		public SudokuGame getGameBySerializedClass(string path)
		{
			throw new MissingMethodException("TODO: implementare la deserializzazione del Game");
			//SudokuGame sGame = new UnsolvedGame();

			//return sGame;
		}

		public void saveGameToCSV(SudokuGame game, string path)
		{
			StreamWriter sw = new System.IO.StreamWriter(path);
			foreach (List<SudokuCell> row in game.GameMatrix)
			{
				StringBuilder sRowCSV = new StringBuilder(20);
				foreach (SudokuCell cell in row)
				{
					if (cell.Value != 0)
					{
						sRowCSV.Append(cell.Value.ToString());
						sRowCSV.Append(csvSep[0]);
					}
					else
					{
						sRowCSV.Append(csvEmptyValue);
						sRowCSV.Append(csvSep[0]);
					}
				}
				sRowCSV.Remove(sRowCSV.Length - 1, 1);

				sw.WriteLine(sRowCSV.ToString());
			}
			sw.Close();
		}

		public void saveGameSerialized(SudokuGame game, string path)
		{
			var serializer = new XmlSerializer(game.GetType());
			StreamWriter sw = new System.IO.StreamWriter(path);
			serializer.Serialize(sw, game);
			sw.Close();
		}

	}
}
