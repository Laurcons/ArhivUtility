using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public abstract class Warning {
		public abstract string Message { get; }
		public abstract string Subfond { get; }
		public abstract override string ToString();
	}

	public class GenericWarning : Warning {
		public override string Message { get; }
		public override string Subfond { get; }

		public GenericWarning(string message, string subfond = null) {
			Message = message;
			Subfond = subfond;
		}
		public override string ToString() => Message;
	}

	public class UAInexistentWarning : Warning {
		public override string Message =>
			(Rows.IsSingle) ?
				$"Numar UA necompletat pentru randul {Rows.Begin} din centralizator, adica an {AnInceput} {Compartiment} TP {TermenPastrare}" :
				$"Numere UA necompletate pentru randurile de la {Rows.Begin} pana la {Rows.End} din centralizator, adica an {AnInceput} {Compartiment} TP {TermenPastrare}";
		public override string Subfond { get; }

		public Range<int> Rows { get; }
		public int AnInceput { get; }
		public string Compartiment { get; }
		public string TermenPastrare { get; }

		public UAInexistentWarning(Range<int> rowRange, int anInceput, string compartiment, string termenPastrare, string subfond = null) {
			Rows = rowRange;
			AnInceput = anInceput;
			Compartiment = compartiment;
			TermenPastrare = termenPastrare;
			Subfond = subfond;
		}
		public override string ToString() => "UA necompletat";
	}

	public class UANotFoundWarning : Warning {
		public override string Message =>
			(UAs.IsSingle) ?
				$"Numarul UA {UAs.Begin} nu a fost gasit in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}" :
				$"Numerele UA de la {UAs.Begin} pana la {UAs.End} nu au fost gasite in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
		public override string Subfond { get; }
		public Range<string> UAs { get; }
		public int AnInceput { get; }
		public string Compartiment { get; }
		public string TermenPastrare { get; }
		public UANotFoundWarning(Range<string> uaRange, int anInceput, string compartiment, string termenPastrare, string subfond = null) {
			UAs = uaRange;
			AnInceput = anInceput;
			Compartiment = compartiment;
			TermenPastrare = termenPastrare;
			Subfond = subfond;
		}
		public override string ToString() => "UA negasit";
	}
	public class UARepeatingWarning : Warning {
		public override string Message =>
			$"Numarul UA {UA} se repeta de mai multe ori in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
		public override string Subfond { get; }

		public string UA { get; }
		public int AnInceput { get; }
		public string Compartiment { get; }
		public string TermenPastrare { get; }
		public UARepeatingWarning(string ua, int anInceput, string compartiment, string termenPastrare, string subfond = null) {
			UA = ua;
			AnInceput = anInceput;
			Compartiment = compartiment;
			TermenPastrare = termenPastrare;
			Subfond = subfond;
		}
		public override string ToString() => "UA repetat";
	}

	public abstract class DateExtremeWarning : Warning {
		public abstract int Row { get; }
		public abstract string UA { get; }
		public abstract int AnInceput { get; }
		public abstract string Compartiment { get; }
		public abstract string TermenPastrare { get; }

	}
	public class DateExtremeMismatchWarning : DateExtremeWarning {
		public override string Message =>
			$"Datele extreme nu coincid cu anii de inceput/sfarsit pentru randul {Row}, adica nr UA {UA} anul {AnInceput} {Compartiment} {TermenPastrare}";
		public override int Row { get; }
		public override string UA { get; }
		public override int AnInceput { get; }
		public override string Compartiment { get; }
		public override string TermenPastrare { get; }
		public override string Subfond { get; }

		public DateExtremeMismatchWarning(int row, string ua, int anInceput, string compartiment, string termenPastrare, string subfond = null) {
			Row = row;
			UA = ua;
			AnInceput = anInceput;
			Compartiment = compartiment;
			TermenPastrare = termenPastrare;
		}
		public DateExtremeMismatchWarning(CentralizatorItemData data, bool hasSubfond = false) {
			Row = data.RowNumber;
			UA = data.NrUA;
			AnInceput = data.AnInceput;
			Compartiment = data.Compartiment;
			TermenPastrare = data.TermenPastrare;
			Subfond = data.Subfond;
		}
		public override string ToString() => "Date extreme incorecte";
	}

	public class DateExtremeFormatWarning : DateExtremeWarning {
		public override string Message =>
			$"Format indescifrabil pentru datele extreme pentru randul {Row}, adica nr UA {UA} anul {AnInceput} {Compartiment} {TermenPastrare}";
		public override int Row { get; }
		public override string UA { get; }
		public override int AnInceput { get; }
		public override string Compartiment { get; }
		public override string TermenPastrare { get; }
		public override string Subfond { get; }

		public DateExtremeFormatWarning(int row, string ua, int anInceput, string compartiment, string termenPastrare, string subfond = null) {
			Row = row;
			UA = ua;
			AnInceput = anInceput;
			Compartiment = compartiment;
			TermenPastrare = termenPastrare;
			Subfond = subfond;
		}
		public DateExtremeFormatWarning(CentralizatorItemData data, bool hasSubfond = false) {
			Row = data.RowNumber;
			UA = data.NrUA;
			AnInceput = data.AnInceput;
			Compartiment = data.Compartiment;
			TermenPastrare = data.TermenPastrare;
			Subfond = data.Subfond;
		}
		public override string ToString() => "Date extreme ciudate";
	}

	public class IndicativTPMismatchWarning : Warning {
		public override string Message =>
			$"Indicativul {Indicativ} corespunde in mod eronat mai multor termene de pastrare";
		public override string Subfond { get; }
		public string Indicativ { get; set; }
		public IndicativTPMismatchWarning(string indicativ, string subfond = null) {
			Indicativ = indicativ;
			Subfond = subfond;
		}
		public override string ToString() => "TP neunic per indicativ";
	}
}