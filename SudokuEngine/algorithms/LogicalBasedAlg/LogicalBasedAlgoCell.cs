using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	[XmlInclude(typeof(LogicalBasedAlgoCell))]
	[Serializable]
	public class LogicalBasedAlgoCell : SudokuCell
	{
		public List<int> AllowableValues { get; set; }

		public LogicalBasedAlgoCell() : base()
		{
			AllowableValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		}

		public LogicalBasedAlgoCell(int v)
			: base(v)
		{
			AllowableValues = new List<int>();
		}

		/// <summary>
		/// Set the final value and clear AllowableValues
		/// </summary>
		/// <param name="v">value to set</param>
		public virtual void setDefinitiveValue(int v)
		{
			if (v != 0)
			{
				this.Value = v;
				this.AllowableValues = new List<int>();
			}
		}

	}
}
