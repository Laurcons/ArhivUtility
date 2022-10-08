using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public class CentralizatorItemData {
		public int RowNumber { get; set; }
		public bool IsFiller => RowNumber == -1;
		public string Subfond { get; set; }
		public string Directia { get; set; }
		public string Compartiment { get; set; }
		public string DirectiaCompartiment => ((Directia ?? "") + " - " + (Compartiment ?? "")).Trim();
		public int AnInceput { get; set; }
		public int AnSfarsit { get; set; }
		public string TermenPastrare { get; set; }

		public string NrUA { get; set; }
		public int NrUAInt => CentralizatorData.ConvertUAToInt(NrUA);
		public string Indicativ { get; set; }
		public string Continut { get; set; }
		public string DateExtreme { get; set; }
		public int NrFile { get; set; }
		public string Observatii { get; set; }

		public bool ErrorOnDateExtreme { get; set; }
		public bool ErrorOnNrUA { get; set; }
		public bool ErrorOnIndicativ { get; set; }
	}

	public class CentralizatorData {
		public List<CentralizatorItemData> Dosare { get; set; }
		public DateFirma DateFirma { get; set; }
		public string FolderName { get {
				return string.Format("ANEXA 2");
			} }

		public CentralizatorData() {
			Dosare = new List<CentralizatorItemData>();
			DateFirma = new DateFirma();
		}

		public string[] RetrieveCompartimente(string subfond = null) {
			return Dosare
				.Where(dat => (subfond == null ? true : dat.Subfond == subfond))
				.Select((data) => data.DirectiaCompartiment)
				.Distinct()
				.ToArray();
		}

		public string[] RetrieveTermenePastrare(string subfond = null) {
			return Dosare
				.Where(dat => (subfond == null ? true : dat.Subfond == subfond))
				.Select(data => data.TermenPastrare)
				.Distinct()
				.ToArray();
		}

		public int[] RetrieveAniInceput(string directiaCompartiment = null, string termenPastrare = null) {
			return Dosare
				.Where(data => ((directiaCompartiment != null) ? data.DirectiaCompartiment == directiaCompartiment : true) &&
				               ((termenPastrare != null) ? data.TermenPastrare == termenPastrare : true))
				.Select(data => data.AnInceput)
				.Distinct()
				.OrderBy(integer => integer)
				.ToArray();
		}

		public string[] RetrieveSubfonduri() {
			return Dosare
				.Select(data => data.Subfond)
				.Distinct()
				.ToArray();
		}

		public CentralizatorData RetrieveFiltered(string directiaCompartiment, string termenPastrare, int an, string subfond = null) {
			return new CentralizatorData() {
				Dosare = Dosare
					.Where(data => data.DirectiaCompartiment == directiaCompartiment &&
								   data.TermenPastrare == termenPastrare &&
								   data.AnInceput == an &&
								   (subfond != null ? data.Subfond == subfond : true))
					.OrderBy(data => ConvertUAToInt(data.NrUA))
					.ToList(),
				DateFirma = DateFirma
			};
		}

		public static int ConvertUAToInt(string ua) {
			// some weird shit because there is stuff such as 123A
			int ret; bool res;
			string ua_copy = string.Copy(ua);
			if (ua_copy == "0")
				return Int32.MaxValue;
			do {
				res = int.TryParse(ua_copy, out ret);
				if (ua_copy.Length == 0)
					throw new FormatException("Nu s-a putut converti numar UA pentru sortare!");
				ua_copy = ua_copy.Remove(ua_copy.Length - 1);
			} while (!res);
			return ret;
		}

		public Dictionary<string, string> GenerateShortTermenePastrare() {
			string[] termenePastrare = RetrieveTermenePastrare();
			var shortTermenePastrare = new Dictionary<string, string>();
			foreach (var termenPastrare in termenePastrare) {
				string term = termenPastrare;
				if (termenPastrare.Contains("P"))
					term = "P";
				else term = new string(term.Where(chr => char.IsDigit(chr)).ToArray());
				if (termenPastrare.Contains("CS") ||
					termenPastrare.Contains("CE"))
					term += " CS";
				shortTermenePastrare[termenPastrare] = term.Trim();
			}
			return shortTermenePastrare;
		}
		
		public Dictionary<string, string> GenerateLongTermenePastrare() {
			string[] termenePastrare = RetrieveTermenePastrare();
			var shortTermenePastrare = GenerateShortTermenePastrare();
			var longTermenePastrare = new Dictionary<string, string>();
			foreach (var termenPastrare in termenePastrare) {
				var shrt = shortTermenePastrare[termenPastrare];
				string lng;
				if (shrt == "P") {
					lng = "PERMANENT";
				} else if (shrt.Contains("CS")) {
					int index = shrt.IndexOf(" CS");
					lng = shrt.Insert(index, " ANI");
				} else {
					int nb = int.Parse(shrt);
					if (nb == 1)
						lng = $"{nb} AN";
					else if (nb < 20)
						lng = $"{nb} ANI";
					else if (nb < 100)
						lng = $"{nb} DE ANI";
					else lng = $"{nb} ANI";
				}
				longTermenePastrare[termenPastrare] = lng;
			}
			return longTermenePastrare;
		}
	}

	public class DateFirma {
		public string Nume;
		public string Judet;
		public string Localitate;
		public string Adresa;
		public string CUI;
		public string NrInmatriculare;
		public string CodCAEN;
		public List<string> DenumiriAnterioare;

		public DateFirma() {
			DenumiriAnterioare = new List<string>();
		}

		public static DateFirma GetDefault() {
			return new DateFirma {
				Nume = "NEDENUMIT",
				Judet = "CLUJ",
				Localitate = "LUNCANI",
				Adresa = "DJ107F 505",
				CUI = "34381953",
				NrInmatriculare = "J12/1144/2015",
				CodCAEN = "91"
			};
		}
	}

	public class RegistruItemData {
		public int NrCrt;
		public DateTime? DataIntrarii;
		public string Compartiment;
		public int AnInceput;
		public int AnSfarsit;
		public string TermenPastrare;
		public int NumarDosare;
	}

	public enum WorkType {
		Read,
		Check,
		Write
	}

	[Obsolete("The WarningData system has been overhauled, use the new types")]
	public class WarningData {
		public WarningType Type { get; set; }
		private string _text;
		private int _rowNumberStart;
		public int RowNumberStart { get => _rowNumberStart;
			set {
				_rowNumberStart = value;
				RowNumberEnd = value;
			} }
		public int RowNumberEnd { get; set; }
		public string NrUAStart { get; set; }
		public string NrUAEnd { get; set; }
		public int AnInceput { get; set; }
		public string Compartiment { get; set; }
		public string TermenPastrare { get; set; }
		public string Indicativ { get; set; }

		public string Message {
			set => _text = value;
			get {
				switch (Type) {
					case WarningType.Generic:
						return _text;
					case WarningType.DateExtremeFormat:
						return "Date extreme indescifrabile";
					case WarningType.DateExtremeMismatch:
						return "Date extreme nu coincid cu anii inceput/sfarsit";
					case WarningType.InexistentUA:
						return "Numar UA necompletat in centralizator";
					case WarningType.NotFoundUA:
						return "Numar UA nu a fost gasit";
					case WarningType.RepeatingUA:
						return "Numar UA apare de mai multe ori";
					default:
						throw new NotImplementedException();
				}
			}
		}

		public override string ToString() {
			switch (Type) {
				case WarningType.Generic:
					return Message;
				case WarningType.InexistentUA:
					if (RowNumberEnd == RowNumberStart) {
						return $"Numar UA necompletat pentru randul {RowNumberStart} din centralizator, adica an {AnInceput} {Compartiment} TP {TermenPastrare}";
					} else {
						return $"Numere UA necompletate pentru randurile de la {RowNumberStart} pana la {RowNumberEnd} din centralizator, adica an {AnInceput} {Compartiment} TP {TermenPastrare}";
					}
				case WarningType.NotFoundUA:
					if (RowNumberStart == RowNumberEnd) {
						return $"Numarul UA {RowNumberStart} nu a fost gasit in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
					} else {
						return $"Numerele UA de la {RowNumberStart} pana la {RowNumberEnd} nu au fost gasite in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
					}
				case WarningType.RepeatingUA:
					if (RowNumberStart == RowNumberEnd) {
						return $"Numarul UA {RowNumberStart} se repeta de mai multe ori in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
					} else {
						return $"Numerele UA de la {RowNumberStart} pana la {RowNumberEnd} se repeta de mai multe ori in cadrul anului {AnInceput} {Compartiment} {TermenPastrare}";
					}
				case WarningType.DateExtremeMismatch:
					if (RowNumberStart == RowNumberEnd) {
						return $"Datele extreme nu coincid cu anii de inceput/sfarsit pentru randul {RowNumberStart}, adica nr UA {NrUAStart} anul {AnInceput} {Compartiment} {TermenPastrare}";
					} else {
						throw new NotImplementedException();
					}
				case WarningType.DateExtremeFormat:
					if (RowNumberStart == RowNumberEnd) {
						return $"Format indescifrabil pentru datele extreme pentru randul {RowNumberStart}, adica nr UA {NrUAStart} anul {AnInceput} {Compartiment} {TermenPastrare}";
					} else {
						throw new NotImplementedException();
					}
				case WarningType.IndicativTPMismatch:
					return $"Indicativul {Indicativ} corespunde in mod eronat mai multor termene de pastrare";
				default:
					throw new Exception("Impossible exception");
			}
		}
	}

	[Obsolete("The WarningData system has been overhauled, use the new types")]
	public enum WarningType {
		Generic,
		InexistentUA,
		NotFoundUA,
		RepeatingUA,
		DateExtremeMismatch,
		DateExtremeFormat,
		IndicativTPMismatch
	}
}
