using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SudokuEngine.common;

namespace SudokuEngine.algorithms
{
	[XmlInclude(typeof(BruteForceAlgoCell))]
	[Serializable]
	public class BruteForceAlgoCell : LogicalBasedAlgoCell
	{
		// ritorna il valore definitivo o il "tentativo". Utile per il check di coerenza.
		public override int Value
		{
			get
			{
				if (base.Value != 0)
				{
					return base.Value;
				}
				else
				{
					return TempValue;
				}
			}
			set
			{
				base.Value = value;
			}
		}

		public int TempValue { get; set; }

		public override void setDefinitiveValue(int v)
		{
			base.setDefinitiveValue(v);
			TempValue = 0;
		}

		public BruteForceAlgoCell()
			: base()
		{
		}

		public BruteForceAlgoCell(int value)
			: base(value)
		{
		}

		public bool isValueReallyFinal()
		{
			return (this.AllowableValues.Count == 0) && (this.Value != 0);
		}

	}
}
