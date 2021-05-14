using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ArhivUtility {
	public static class Work {
		public static EventHandler<ProgressChangedEventArgs> ProgressChanged { get; set; }
		public static Dictionary<string, string> ShortTermenePastrare { get; set; }
		public static Dictionary<string, string> LongTermenePastrare { get; set; }
		public static string[] TermenePastrare => _centralizator.RetrieveTermenePastrare();
		public static string[] Subfonduri => _centralizator.RetrieveSubfonduri();
		public static DateFirma DateFirma { get => _centralizator.DateFirma ?? null; }

		private static Excel.Application _application;
		private static CentralizatorData _centralizator;
		private static string _currentAction;

		public static void StartApplication() {
			if (_application == null)
				_application = new Excel.Application();
			_application.Visible = false;
			_centralizator = null;
		}

		public static void StopApplication(object sender = null, EventArgs e = null) {
			try {
				if (_application != null)
					_application.Quit();
				_application = null;
			} catch (Exception) {

			}
		}

		private static void ReportProgress(int progress = -1, string message = null) {
			ProgressChanged.Invoke(null, new ProgressChangedEventArgs(progress, message));
		}
		private static void ReportMessage(string message) => ReportProgress(message: message);
		private static void ReportWarning(string message) {
			ReportWarning(new GenericWarning(message));
		}
		private static void ReportWarning(Warning warning, int progress = -1) {
			ProgressChanged.Invoke(null, new ProgressChangedEventArgs(progress, warning));
		}

		public static void DoReadWork(string filename) {
			// open the workbook
			Excel._Workbook workbook;
			Excel._Worksheet worksheet;
			_centralizator = new CentralizatorData();
			int row;
			try {
				_currentAction = "Deschidere fisier";
				workbook = _application.Workbooks.Open(filename, ReadOnly: true);
				_currentAction = "Deschidere foaie de lucru Arhivatorul_template din centralizator";
				worksheet = workbook.Sheets["Arhivatorul_template"];
			} catch (Exception ex) {
				throw new DataFormatException("Eroare la deschiderea documentului!", _currentAction, ex);
			}

			ReportProgress(10, "Se parcurg datele din centralizator...");

			ReportProgress(10, "- Se obtin datele firmei...");
			try {
				// find the "Date Arvutil" row in 'date identificare'
				_currentAction = "Deschidere foaie de lucru \"Date identificare\" din centralizator";
				Excel._Worksheet dateWorksheet = workbook.Sheets["Date identificare"];
				_currentAction = "Cautare meta date: Date Arvutil, si Denumiri anterioare";
				row = 1;
				int maxRows = 200; bool foundDate = false; bool foundDenumiri = false;
				while (row < maxRows && (!foundDate || !foundDenumiri)) {
					if ((dateWorksheet.Cells[row, "A"].Value ?? "").Trim() == "Date Arvutil") {
						_currentAction = "Parcurgere meta date: Date Arvutil";
						foundDate = true;
						// retrieve data
						var dateFirma = _centralizator.DateFirma;
						dateFirma.Nume = dateWorksheet.Cells[row + 0, "B"].Value.ToString();
						dateFirma.Judet = dateWorksheet.Cells[row + 1, "B"].Value.ToString();
						dateFirma.Localitate = dateWorksheet.Cells[row + 2, "B"].Value.ToString();
						dateFirma.Adresa = dateWorksheet.Cells[row + 3, "B"].Value.ToString();
						dateFirma.CUI = dateWorksheet.Cells[row + 4, "B"].Value.ToString();
						dateFirma.NrInmatriculare = dateWorksheet.Cells[row + 5, "B"].Value.ToString();
						dateFirma.CodCAEN = dateWorksheet.Cells[row + 6, "B"].Value.ToString();
						_centralizator.DateFirma = dateFirma;
					} else if ((dateWorksheet.Cells[row, "A"].Value ?? "").Trim() == "Denumiri anterioare") {
						_currentAction = "Parcurgere meta date: Denumiri anterioare";
						foundDenumiri = true;
						var dateFirma = _centralizator.DateFirma;
						while (true) {
							if (worksheet.Cells[row, "B"].Value2 == null)
								break;
							dateFirma.DenumiriAnterioare.Add(
								worksheet.Cells[row, "B"].Value2.ToString()
							);
							row++;
						}
						_centralizator.DateFirma = dateFirma;
					}
					row++;
				}
				_currentAction = "Cautare meta date: Date Arvutil, si Denumiri anterioare";
				if (!foundDate) {
					//throw new InvalidOperationException("Nu s-au gasit datele Arvutil");
					ReportWarning(new GenericWarning("Nu s-au gasit datele Arvutil! Se completeaza cu valori prestabilite."));
					_centralizator.DateFirma = DateFirma.GetDefault();
				}
				if (!foundDenumiri)
					ReportWarning(new GenericWarning("Nu s-au gasit denumirile anterioare. Nu se completeaza."));
			} catch (Exception) {
				//throw new DataFormatException("Datele meta ale firmei nu sunt formatate corect!", _currentAction, ex);
				ReportWarning(new GenericWarning("Eroare la citirea Datelor Arvutil sau a Denumirilor anterioare. " +
					"Se completeaza cu valori prestabilite. Ati lasat spatii goale?"));
				_centralizator.DateFirma = DateFirma.GetDefault();
			}

			ReportProgress(10, "- Se numara randurile...");
			// estimate the row count
			_currentAction = "Estimarea numarului de randuri in centralizator";
			row = 2;
			int rowCount = 0;
			while (true) {
				if (worksheet.Cells[row, "O"].Value == null)
					break;
				row++;
				rowCount++;
			}

			ReportProgress(10, $"- Se parcurg {rowCount} randuri...");
			int inexistentUACount = 0;
			// percentage range: 10 - 100
			try {
				// detect whether it's the new format or the old format
				_currentAction = "Determinarea formatului centralizatorului";
				//bool oldFormat = true;
				/*if (new string[] {"Serviciile", "Servicii"}.ToList().Contains(
						worksheet.Cells[1, "H"].Value.ToString().Trim()))
					oldFormat = false;*/
				_currentAction = "Parcurgerea randurilor centralizatorului";
				for (int i = 0; i < rowCount; i++) {
					row = i + 2;
					_currentAction = $"Parcurgerea randului {row} din centralizator";
					var data = new CentralizatorItemData();
					string uavalue;
					data.RowNumber = row;
					//data.Compartiment = worksheet.Cells[row, (oldFormat) ? "G" : "H"].Value.ToString().Trim().ToUpper();
					// string join the Directie and Compartiment into the CentralizatorItemData
					//string[] compartParts = new string[] {
					//	worksheet.Cells[row, "G"].Value != null ?
					//	worksheet.Cells[row, "G"].Value.ToString().ToUpper() :
					//	null,
					//	worksheet.Cells[row, "H"].Value != null ?
					//	worksheet.Cells[row, "H"].Value.ToString().ToUpper() :
					//	null
					//};
					//data.Compartiment = String.Join(" ", compartParts).Trim();
					string directia = worksheet.Cells[row, "G"].Value != null ?
						worksheet.Cells[row, "G"].Value.ToString().ToUpper() :
						null;
					string compartiment = worksheet.Cells[row, "H"].Value != null ?
						worksheet.Cells[row, "H"].Value.ToString().ToUpper() :
						null;
					data.Directia = directia;
					data.Compartiment = compartiment;
					uavalue = (worksheet.Cells[row, "L"].Value ?? 0).ToString();
					if (uavalue == null || uavalue.Trim() == "")
						uavalue = "0";
					data.NrUA = uavalue;
					data.ErrorOnDateExtreme = false;
					data.ErrorOnNrUA = data.NrUA == "0";
					data.ErrorOnIndicativ = false;
					data.Indicativ = worksheet.Cells[row, "N"].Value != null ?
						worksheet.Cells[row, "N"].Value.ToString() : // this fuckery might not be needed but i ain't risking shit
						"";
					data.Continut = worksheet.Cells[row, "O"].Value.ToString();
					data.DateExtreme = worksheet.Cells[row, "P"].Value.ToString();
					data.NrFile = (int)(Convert.ToDouble(worksheet.Cells[row, "Q"].Value) ?? 0.0);
					data.Observatii = worksheet.Cells[row, "R"].Value != null ?
						worksheet.Cells[row, "R"].Value.ToString() :
						"";
					data.AnInceput = Convert.ToInt32(worksheet.Cells[row, "U"].Value.ToString());
					data.AnSfarsit = Convert.ToInt32(worksheet.Cells[row, "V"].Value.ToString());
					data.TermenPastrare = worksheet.Cells[row, "W"].Value.ToString().Trim().ToUpper();
					// subfond
					if (worksheet.Cells[row, "E"].Value != null) {
						data.Subfond = worksheet.Cells[row, "E"].Value.ToString().Trim();
					} else {
						data.Subfond = "";
					}
					_centralizator.Dosare.Add(data);
					// report progress
					int progress = 10 + (int)((float)row / rowCount * 90);
					ReportProgress(progress);
					// if nrUA is 0, send a warning
					if (data.NrUA == "0") {
						ReportWarning(new UAInexistentWarning(
							new Range<int>(row),
							data.AnInceput,
							data.Compartiment,
							data.TermenPastrare));
						inexistentUACount++;
					}
				}
				_currentAction = "Inchiderea centralizatorului";
				workbook.Close(SaveChanges: false);
			} catch (Exception ex) {
				throw new DataFormatException("Eroare la parcurgerea centralizatorului!", _currentAction, ex);
			}
			// display summary
			if (inexistentUACount == 0) {
				ReportMessage("Nu s-au gasit UA necompletate!");
			} else {
				ReportMessage($"S-au gasit un numar de {inexistentUACount} UA necompletate!");
			}
			ShortTermenePastrare = _centralizator.GenerateShortTermenePastrare();
			LongTermenePastrare = _centralizator.GenerateLongTermenePastrare();
		}

		public static void DoCheckWork(bool checkUA, bool checkDates, bool checkIndicative) {
			DoCheckWork(checkUA, checkDates, checkIndicative, doInternally: false);
		}
		private static void DoCheckWork(bool checkUA, bool checkDates, bool checkIndicative, bool doInternally = false) {
			int UAErrorCount = 0, datesErrorCount = 0, indicativeErrorCount = 0;
			string[] subfonduri = _centralizator.RetrieveSubfonduri();
			bool hasSubfonduri = subfonduri.Count() >= 2;
			if (checkUA) {
				string[] compartimente = _centralizator.RetrieveCompartimente();
				string[] termenePastrare = _centralizator.RetrieveTermenePastrare();
				int[] aniInceput = _centralizator.RetrieveAniInceput();
				// do a check on the UA's
				foreach (string subfond in subfonduri) {
					foreach (string compartiment in compartimente) {
						foreach (string termenPastrare in termenePastrare) {
							foreach (int anInceput in aniInceput) {
								var dosare = _centralizator.RetrieveFiltered(
									compartiment,
									termenPastrare,
									anInceput,
									subfond);
								if (dosare.Dosare.Count == 0)
									continue;
								int currentUA = 1;
								List<int> repeatUAs = new List<int>();
								List<int> notFoundUAs = new List<int>();
								List<CentralizatorItemData> fillerUAs = new List<CentralizatorItemData>();
								foreach (var dosar in dosare.Dosare) {
									var found = dosare.Dosare
										.Where(data => data.NrUAInt == currentUA && !data.IsFiller);
									if (found.Count() > 1) {
										// there might be the case of UA's like 123 and 123A
										bool isDistinct = found.Distinct().Count() == found.Count();
										int repeatCount = found.Count();
										if (!isDistinct) {
											ReportWarning(new UARepeatingWarning(
												currentUA.ToString(), anInceput, compartiment, termenPastrare, subfond));
											UAErrorCount++;
											// put the error tag on each one
											found.ToList().ForEach(dat =>
												dat.ErrorOnNrUA = true);
										}
									}
									if (found.Count() < 1) {
										// not ok
										ReportWarning(new UANotFoundWarning(
											new Range<string>(currentUA.ToString()), anInceput, compartiment, termenPastrare, subfond));
										UAErrorCount++;
									}
									currentUA++;
								}
							}
						}
					}
				}
			}
			if (checkDates) {
				// do a check on the extreme dates
				foreach (var dosar in _centralizator.Dosare) {
					bool verifSuccess = true;
					try {
						bool success;
						string[] parts = dosar.DateExtreme.Split('-');
						// should be formatted as either
						//  2010-2012
						// or
						//  month1-month2
						// or
						//  month
						success = int.TryParse(parts[0], out int anInceputStr);
						// if count is 1 then there is only one month, handle it the same way
						if (!success || parts.Count() == 1) {
							// maybe it is the month1-month2 variant
							// check to see if the start and end year match
							if (dosar.AnInceput != dosar.AnSfarsit) {
								if (!doInternally)
									ReportWarning(new DateExtremeMismatchWarning(dosar, hasSubfonduri));
								verifSuccess = false;
								datesErrorCount++;
							}
							continue;
						}
						if (parts.Count() != 2)
							throw new FormatException();
						// ok the second one should be an int by now
						int anSfarsitStr = int.Parse(parts[1]);
						// check if they match
						if (!(dosar.AnInceput == anInceputStr && dosar.AnSfarsit == anSfarsitStr)) {
							if (!doInternally)
								ReportWarning(new DateExtremeMismatchWarning(dosar, hasSubfonduri));
							datesErrorCount++;
							verifSuccess = false;
						}
					} catch (FormatException) {
						if (!doInternally)
							ReportWarning(new DateExtremeFormatWarning(dosar, hasSubfonduri));
						datesErrorCount++;
						verifSuccess = false;
					}
					dosar.ErrorOnDateExtreme = !verifSuccess;
				}
			}
			if (checkIndicative) {
				foreach (var subfond in subfonduri) {
					// do a check on the indicative
					// get all the indicative
					var indicative = _centralizator.Dosare
						.Select(dat => dat.Indicativ)
						.Distinct();
					foreach (var indicativ in indicative) {
						if (indicativ == string.Empty)
							continue;
						// get all the distinct TP's for given indicativ
						var termenePastrare = _centralizator.Dosare
							.Where(dat => dat.Indicativ == indicativ && dat.Subfond == subfond)
							.Select(dat => dat.TermenPastrare)
							.Distinct();
						if (termenePastrare.Count() > 1) {
							ReportWarning(new IndicativTPMismatchWarning(indicativ, subfond));
							indicativeErrorCount++;
						}
					}
				}
			}
			if (checkUA) {
				if (UAErrorCount == 0) {
					ReportProgress(message: "Nu s-au gasit probleme in numerele UA!");
				} else {
					ReportProgress(message: $"S-au gasit un numar de {UAErrorCount} probleme in numerele UA!");
				}
			} else {
				ReportProgress(message: "Numerele UA nu au fost verificate, din actiunea utilizatorului.");
			}
			if (checkDates) {
				if (datesErrorCount == 0) {
					ReportProgress(message: "Nu s-au gasit probleme in datele extreme!");
				} else {
					ReportProgress(message: $"S-au gasit un numar de {datesErrorCount} probleme in datele extreme!");
				}
			} else {
				ReportProgress(message: "Datele extreme nu au fost verificate, din actiunea utilizatorului.");
			}
			if (checkIndicative) {
				if (indicativeErrorCount == 0) {
					ReportProgress(message: "Nu s-au gasit probleme in indicative!");
				} else {
					ReportProgress(message: $"S-au gasit un numar de {indicativeErrorCount} probleme in indicative!");
				}
			} else {
				ReportProgress(message: "Indicativele nu au fost verificate, din actiunea utilizatorului.");
			}
			// and this is kinda it
		}

		public static void DoWriteWork(string path, bool generateRegistre, bool writeErrors, DateTime? regDate) {
			Excel._Workbook workbook = null;
			Excel._Worksheet worksheet;
			//var registru = new List<RegistruItemData>();
			var registre = new Dictionary<string, List<RegistruItemData>>();
			int registruCurrent = 1;
			int row;
			int inventareGenerate = 0;
			int totalDosareCount = _centralizator.Dosare.Count;
			int dosarCurent = 0;
			string[] subfonduri = Subfonduri;
			// indicates whether subfond separation needs to be done
			bool separateInventare = subfonduri.Count() >= 2;
			ReportProgress(0, "Se genereaza inventarele...");
			// percentage range 0 - 100
			try {
				foreach (var subfond in subfonduri) {
					registruCurrent = 1;
					registre.Add(subfond, new List<RegistruItemData>());
					if (separateInventare)
						ReportProgress(message: $"Se genereaza subfondul {subfond}");
					_currentAction = $"Identificarea compartimentelor si termenelor de pastrare" +
						(separateInventare ? $" pentru subfond {subfond}" : "");
					string[] compartimente = _centralizator.RetrieveCompartimente(subfond);
					string[] termenePastrare = _centralizator.RetrieveTermenePastrare(subfond);
					_currentAction = $"Sortarea initiala a compartimentelor si termenelor de pastrare" +
						(separateInventare ? $" pentru subfond {subfond}" : "");
					termenePastrare = termenePastrare
						.OrderByDescending(tp => {
							if (ShortTermenePastrare[tp] == "P") return 999;
							if (ShortTermenePastrare[tp].Contains("CS"))
								return int.Parse(new string(ShortTermenePastrare[tp].Where(chr => char.IsDigit(chr)).ToArray()));
							else return int.Parse(ShortTermenePastrare[tp]);
						})
						.ToArray();
					compartimente = compartimente
						.OrderBy(comp => comp)
						.ToArray();
					_currentAction = $"Crearea folderului tinta";
					//string directory = Path.GetDirectoryName(path) + "\\" + _centralizator.FolderName;
					string directory = Path.GetDirectoryName(path) + "\\" +
						(separateInventare ? $"{subfond}\\" : "") +
						_centralizator.FolderName;
					if (Directory.Exists(directory))
						Directory.Delete(directory, recursive: true);
					Directory.CreateDirectory(directory);
					foreach (var termenPastrare in termenePastrare) {
						foreach (var compartiment in compartimente) {
							_currentAction = $"Generarea compartimentului {compartiment} si termenului {termenPastrare}" +
								(separateInventare ? $" pentru subfond {subfond}" : "");
							// calculate progress
							int progress = (int)((float)dosarCurent / totalDosareCount * 100);
							// get year list
							int[] aniInceput = _centralizator
								.RetrieveAniInceput(compartiment, termenPastrare)
								.OrderBy(integer => integer)
								.ToArray();
							// get data for each year
							var dateAni = new Dictionary<int, CentralizatorData>();
							foreach (var anInceput in aniInceput) {
								var dateAn = _centralizator.RetrieveFiltered(
									compartiment,
									termenPastrare,
									anInceput,
									subfond);
								if (dateAn.Dosare.Count == 0)
									continue;
								dateAni[anInceput] = dateAn;
								dateAni[anInceput].Dosare.Reverse();
								// fill the registry
								var item = new RegistruItemData {
									NrCrt = registruCurrent++,
									TermenPastrare = termenPastrare,
									Compartiment = compartiment,
									DataIntrarii = regDate,
									AnInceput = anInceput,
									AnSfarsit = dateAni[anInceput].Dosare
										.Select(data => data.AnSfarsit)
										.Max(),
									NumarDosare = dateAni[anInceput].Dosare.Count
								};
								registre[subfond].Add(item);
							}
							// calculate the number of dosare
							int dosareCount = dateAni.Sum(data => data.Value.Dosare.Count);
							// decide whether to create this file or not
							if (aniInceput.Length == 0) {
								//ReportProgress(progress, $"- {compartiment} {termenPastrare} - Nu exista ani");
								continue;
							}
							inventareGenerate++;
							ReportProgress(message: $"- {compartiment.ToUpper()} {termenPastrare.ToUpper()} are {aniInceput.Length} ani cu {dosareCount} dosare");
							string filename = directory + string.Format("\\{0} TP {1}.xls",
								compartiment,
								ShortTermenePastrare[termenPastrare]);
							// create file
							_currentAction = $"Crearea fisierului {filename}";
							File.WriteAllBytes(filename, Properties.Resources.INVENTAR_MODEL);
							// open file in Application
							workbook = _application.Workbooks.Open(filename);
							workbook.DoNotPromptForConvert = true;
							workbook.CheckCompatibility = false;
							// populate info part
							_currentAction = $"Scrierea informatiilor firmei in prima pagina a inventarului {compartiment} TP {termenPastrare}";
							worksheet = workbook.Worksheets["Info"];
							var dateFirma = _centralizator.DateFirma;
							worksheet.Cells[13, "A"] = (separateInventare ? subfond : dateFirma.Nume);
							if (!separateInventare) {
								worksheet.Cells[16, "E"] = dateFirma.Judet;
								worksheet.Cells[17, "E"] = dateFirma.Localitate;
								worksheet.Cells[18, "E"] = dateFirma.Adresa;
								worksheet.Cells[20, "F"] = dateFirma.CUI;
								worksheet.Cells[21, "F"] = dateFirma.NrInmatriculare;
								worksheet.Cells[22, "F"] = dateFirma.CodCAEN;
							}
							// use the first an to get accurate compartiment information not supplied by the 'compartiment' variable
							string accurateDirectie = dateAni.First().Value.Dosare[0].Directia;
							string accurateCompartiment = dateAni.First().Value.Dosare[0].Compartiment;
							worksheet.Cells[24, "F"] = accurateDirectie ?? accurateCompartiment;
							worksheet.Cells[25, "D"] = accurateDirectie != null ? accurateCompartiment : "";
							worksheet.Cells[26, "F"] = LongTermenePastrare[termenPastrare];
							worksheet.Cells[28, "F"] = regDate.HasValue ? regDate.Value.ToString("dd.MM.yyyy") : " ";
							row = 31;
							foreach (var denAnt in dateFirma.DenumiriAnterioare) {
								worksheet.Cells[row++, "D"] = denAnt;
							}
							// make the yearly worksheets
							foreach (int anInceput in aniInceput) {
								// determine if it is actually needed
								if (!dateAni.ContainsKey(anInceput))
									continue;
								var anData = dateAni[anInceput];
								if (anData.Dosare.Count == 0)
									continue;
								// get the registry number
								int regNum = registre[subfond]
									.FindLast(data => data.AnInceput == anInceput)
									.NrCrt;
								_currentAction = $"Scrierea informatiilor pe anul {anInceput} pentru {compartiment} TP {termenPastrare}";
								// copy worksheet
								worksheet = workbook.Worksheets["AN"];
								worksheet.Copy(Type.Missing, workbook.Worksheets[workbook.Worksheets.Count]);
								workbook.Worksheets[workbook.Worksheets.Count].Name = anInceput.ToString();
								worksheet = workbook.Worksheets[anInceput.ToString()];
								// populate worksheet
								worksheet.Cells[6, "D"] = anInceput;
								worksheet.Cells[7, "D"] = LongTermenePastrare[termenPastrare];
								if (generateRegistre)
									worksheet.Cells[3, "E"] = regNum;
								int headerRow = 10;
								// write the data on each row
								foreach (var data in anData.Dosare) {
									Excel.Range rowRange = worksheet.Rows[headerRow];
									rowRange.Columns["A"] = data.NrUA;
									rowRange.Columns["B"] = data.Indicativ;
									rowRange.Columns["C"] = data.Continut;
									rowRange.Columns["D"] = data.DateExtreme;
									rowRange.Columns["E"] = data.NrFile == 0 ? "" : data.NrFile.ToString();
									rowRange.Columns["F"] = data.Observatii;
									rowRange.Insert();
									// make cells red
									if (writeErrors) {
										bool hasErrors = false;
										if (data.NrUA == "0") {
											rowRange.Columns["A:F"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
											hasErrors = true;
										}
										if (data.IsFiller && data.ErrorOnNrUA) {
											rowRange.Columns["B:F"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
											hasErrors = true;
										}
										if (!data.IsFiller && data.ErrorOnNrUA) {
											rowRange.Columns["A"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
											hasErrors = true;
										}
										if (data.ErrorOnDateExtreme) {
											rowRange.Columns["D"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
											hasErrors = true;
										}
										if (data.ErrorOnIndicativ) {
											rowRange.Columns["B"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
											hasErrors = true;
										}
										if (hasErrors) {
											// color a cell at the top red
											worksheet.Cells[6, "E"].Interior.Color = ColorTranslator.ToOle(Color.OrangeRed);
										}
									}
									// write the progress data
									progress = (int)((float)dosarCurent / totalDosareCount * 100);
									ReportProgress(progress);
									dosarCurent++;
								}
								// delete that additional row that gets left behind
								worksheet.Rows[headerRow].Delete();
								int dataRows = anData.Dosare.Count;
								// write the metadata
								int metaRow = headerRow - 1 + dataRows + 2;
								// get the number of pages required to print this
								int pageCount = worksheet.PageSetup.Pages.Count;
								string metaFormat = worksheet.Cells[metaRow, "A"].Value.ToString();
								worksheet.Cells[metaRow, "A"] = string.Format(metaFormat,
									pageCount,
									anData.Dosare.Count);
							}
							// delete the template "AN" worksheet
							_application.DisplayAlerts = false;
							workbook.Worksheets["AN"].Delete();
							_application.DisplayAlerts = true;
							// close workbook
							workbook.Close(SaveChanges: true);
						}
					}
				}
			} catch (Exception ex) {
				try {
					workbook.Close(SaveChanges: true);
				} catch (Exception) { }
				_application.Quit();
				throw new DataFormatException("Eroare la scrierea inventarelor!", _currentAction, ex);
			}
			ReportProgress(100, $"S-au generat {inventareGenerate} inventare cu total {totalDosareCount} dosare" +
				(separateInventare ? $" separate pe {subfonduri.Count()} subfonduri!" : "!"));

			if (generateRegistre) {
				ReportProgress(0, "Se completeaza registrele...");
				int currentItem = 0;
				int totalItems = registre.Sum(pair => pair.Value.Count);
				foreach (var subfond in subfonduri) {
					ReportProgress(message: $"- Exista {registre[subfond].Count} intrari" +
								(separateInventare ? $" pentru subfond {subfond}" : ""));
					// before writing, reverse the whole thing, because it is reversed again when written in Excel
					registre[subfond].Reverse();
					try {
						_currentAction = "Crearea registrului" +
								(separateInventare ? $" pentru subfond {subfond}" : "");
						string folderpath = Path.GetDirectoryName(path);
						//if (!Directory.Exists(folderpath))
						//	Directory.CreateDirectory(folderpath);
						string filepath = folderpath +
							(separateInventare ? $"\\{subfond}" : "") +
							"\\REGISTRU EVIDENTA CURENTA.xlsx";
						File.WriteAllBytes(filepath, Properties.Resources.REGISTRU_MODEL);
						_currentAction = "Initializarea popularii registrului" +
								(separateInventare ? $" pentru subfond {subfond}" : "");
						workbook = _application.Workbooks.Open(filepath);
						worksheet = workbook.Worksheets["Registru"];
						// fill the name row
						worksheet.Cells[2, "A"] = (separateInventare ? subfond : _centralizator.DateFirma.Nume);
						// fill rows
						int headerRow = 6;
						foreach (var regItem in registre[subfond]) {
							_currentAction = $"Scrierea randului {regItem.NrCrt}" +
								(separateInventare ? $" pentru subfond {subfond}" : "");
							Excel.Range rowRange = worksheet.Range[$"A{headerRow}", $"M{headerRow}"];
							rowRange.Columns["A"] = regItem.NrCrt;
							if (regItem.DataIntrarii.HasValue)
								rowRange.Columns["B"] = regItem.DataIntrarii.Value.ToString("dd.MM.yyyy");
							else rowRange.Columns["B"] = " ";
							rowRange.Columns["C"] = regItem.Compartiment;
							rowRange.Columns["D"] = (regItem.AnInceput == regItem.AnSfarsit) ?
								string.Format("{0}", regItem.AnInceput) :
								string.Format("{0} - {1}", regItem.AnInceput, regItem.AnSfarsit);
							rowRange.Columns["E"] = ShortTermenePastrare[regItem.TermenPastrare];
							rowRange.Columns["F"] = regItem.NumarDosare;
							rowRange.Columns["G"] = regItem.NumarDosare;
							rowRange.Columns["H"] = "-";
							rowRange.Insert();
							// write the progress data
							int progress = (int)((float)currentItem++ / totalItems * 100);
							ReportProgress(progress);
						}
						worksheet.Rows[headerRow].Delete();
						workbook.Close(SaveChanges: true);
					} catch (Exception ex) {
						try {
							workbook.Close(SaveChanges: true);
						} catch (Exception) { }
						throw new DataFormatException("Eroare la scrierea registrului!", _currentAction, ex);
					}
				}
			}
		}
	}
}