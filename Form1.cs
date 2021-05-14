using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Media;

namespace ArhivUtility {
	public partial class Form1 : Form {
		private SplashForm _splashForm;
		private string _currentFilePath;
		private WorkType _currentWorkType;
		private List<Warning> Warnings;
		private bool _isVerified = false;

		public Form1() {
			InitializeComponent();
			Hide();
			Opacity = 0;
			_splashForm = new SplashForm();
			_splashForm.Show();
			Warnings = new List<Warning>();
			// get version
			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Text = $"ArhivUtility - versiunea {version.Major}.{version.Minor}.{version.Build} b {version.Revision}";
			// put today's date on the date time picker
			registreDatePicker.Value = DateTime.Today;
			panel.Enabled = false;
		} 

		private void label1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) {
			e.Action = DragAction.Continue;
		}

		private void label1_DragDrop(object sender, DragEventArgs e) {
			// pass the filename to the worker
			_currentWorkType = WorkType.Read;
			_currentFilePath = ((string[])e.Data.GetData("FileName"))[0];
			worker.RunWorkerAsync();
			Warnings.Clear();
			label.Text = "Se prelucrează fișierul...";
			label.BackColor = SystemColors.Control;
			rtb.SelectionColor = Color.Green;
			rtb.AppendText("Se parcurge și citește fișierul\n");
			rtb.SelectionColor = Color.Black;
			rtb.ResetText();
		}

		private void label_DragLeave(object sender, EventArgs e) {
			label.BackColor = SystemColors.Control;
		}
		private void copyMenuItem_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(rtb.Text))
				Clipboard.SetText(rtb.Text);
		}

		private void label1_DragOver(object sender, DragEventArgs e) {
			if (worker.IsBusy) {
				e.Effect = DragDropEffects.None;
				label.BackColor = Color.Red;
			} else {
				e.Effect = DragDropEffects.Copy;
				label.BackColor = Color.Green;
			}
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e) {
			this.Invoke((Action)delegate {
				worker_DoUIPreWork();
			});
			if (_currentWorkType == WorkType.Read)
				Work.DoReadWork(_currentFilePath);
			if (_currentWorkType == WorkType.Check) {
				// get the parts to check thing
				var tuple = (Tuple<bool, bool, bool>)e.Argument;
				Work.DoCheckWork(checkUA: tuple.Item1, checkDates: tuple.Item2, checkIndicative: tuple.Item3);
			}
			if (_currentWorkType == WorkType.Write)
				Work.DoWriteWork(_currentFilePath,
					generateRegistruCheckbox.Checked,
					displayWarningsInInventareCheckBox.Checked,
					generateDateCheckbox.Checked ? registreDatePicker.Value : (DateTime?)null);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		private void worker_DoUIPreWork() {
			dgv_tp.Rows.Clear();
			panel.Enabled = false;
			viewWarningsToolStripMenuItem.Enabled = false;
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			// display in rtb
			if (e.UserState != null) {
				// determine whether it's a warning or not
				if (e.UserState is string text) {
					if (text[0] != '!')
						rtb.AppendText((string)e.UserState + '\n');
					else {
						// remove first letter
						text = text.Remove(0, 1);
						rtb.SelectionColor = Color.Red;
						rtb.AppendText("Avertizare! ");
						rtb.SelectionColor = Color.Black;
						rtb.AppendText(text + '\n');
					}
					rtb.ScrollToCaret();
				} else if (e.UserState is Warning warning) {
					if (warning is GenericWarning) {
						rtb.SelectionColor = Color.Red;
						rtb.AppendText("Avertizare! ");
						rtb.SelectionColor = Color.Black;
						rtb.AppendText(warning.Message + '\n');
					}
					Warnings.Add(warning);
				}
			}
			if (e.ProgressPercentage != -1) {
				if (e.ProgressPercentage > 100)
					progressBar.Value = 100;
				else if (e.ProgressPercentage < 0)
					progressBar.Value = 0;
				else progressBar.Value = e.ProgressPercentage;
			}
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			var folderPath = System.IO.Path.GetDirectoryName(_currentFilePath);
			if (e.Error != null) {
				string msg = $"Programul a întâmpinat o eroare! A avut loc următoarea eroare:\n" +
					$"{e.Error.Message}\n";
				if (e.Error is DataFormatException)
					msg += $"Această eroare a avut loc în timpul următoarei acțiuni:\n" +
						   $"{((DataFormatException)e.Error).Action}\n";
				if (e.Error.InnerException != null)
					msg += $"Detalii adiționale:\n" +
						$"{e.Error.InnerException.Message}";
				// open a file stream and try to write data about the exception
				try {
					string fileText = "";
					if (e.Error.InnerException != null) {
						fileText += $"Exception report: {e.Error.GetType().ToString()} with inner {e.Error.InnerException.GetType().ToString()}\n" +
							$"Date: {DateTime.Now.ToString()}\n" +
							$"Main exception: {e.Error.Message}\n" +
						   $"Inner: {e.Error.InnerException.Message}\n" +
						   $"Stack trace for outer:\n" +
						   $"{e.Error.StackTrace}\n\n" +
						   $"Stack trace for inner:\n" +
						   $"{e.Error.InnerException.StackTrace}\n\n" +
						   $"Current action for DataFormatException:\n" +
						   $"{((e.Error is DataFormatException) ? (e.Error as DataFormatException).Action : "None")}";
					} else {
						fileText += $"Exception report: {e.Error.GetType().ToString()}\n" +
							$"Date: {DateTime.Now.ToString()}\n" +
							$"Main exception: {e.Error.Message}\n" +
						   $"Stack trace for outer:\n" +
						   $"{e.Error.StackTrace}\n\n" +
						   $"Current action for DataFormatException:\n" +
						   $"{((e.Error is DataFormatException) ? (e.Error as DataFormatException).Action : "Is not DataFormatException")}";
					}
					string path = folderPath + "\\ArvutilEroare.txt";
					System.IO.File.WriteAllText(path, fileText);
				} catch (System.IO.IOException) {
				}
				//MessageBox.Show(msg, "Atentie!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				// display in rtb
				SystemSounds.Exclamation.Play();
				rtb.SelectionColor = Color.Red;
				rtb.AppendText(msg);
				rtb.SelectionColor = Color.Black;
			} else {
				//MessageBox.Show("Actiunea a fost terminata cu succes!", "ArhivUtil", MessageBoxButtons.OK, MessageBoxIcon.Information);
				SystemSounds.Exclamation.Play();
				rtb.SelectionColor = Color.Green;
				rtb.AppendText("Acțiunea a fost terminată cu succes!\n");
				rtb.SelectionColor = Color.Black;
				// write the entire RTB into a file
				//string path = folderPath + "\\ArvutilSucces.txt";
				// skip that for now
				//System.IO.File.WriteAllText(path, rtb.Text);
				panel.Enabled = true;
				viewWarningsToolStripMenuItem.Enabled = true;
			}
			/*if (Warnings.Count > 0 && e.Error == null) {
				rtb.SelectionColor = Color.Red;
				rtb.AppendText("Exista avertizari!\n");
				rtb.SelectionColor = Color.Black;
			}*/
			if (!Focused)
				this.FlashTaskbar();
			if (e.Error == null) {
				label.Text = Work.DateFirma.Nume;
				if (Work.Subfonduri.Count() >= 2) {
					label.Text += $"\nSubfonduri: {Work.Subfonduri.Count()}";
				}
			}
			progressBar.Value = 0;
			if (e.Error == null && _currentWorkType == WorkType.Check)
				_isVerified = true;
			else if (e.Error == null && _currentWorkType == WorkType.Read)
				_isVerified = false;
			if (!_isVerified) {
				runChecksButton.Text = string.Format("Rulează verificări - [NEVERIFICAT]");
				generateButton.Text = $"Generează - [NEVERIFICAT]";
				displayWarningsInInventareCheckBox.Checked = false;
				displayWarningsInInventareCheckBox.Enabled = false;
			} else {
				runChecksButton.Text = string.Format("Rulează verificări");
				generateButton.Text = $"Generează";
				displayWarningsInInventareCheckBox.Checked = true;
				displayWarningsInInventareCheckBox.Enabled = true;
			}
			FillDgvTP();
		}

		private void Form1_Load(object sender, EventArgs e) {
			Work.ProgressChanged += (obj, ev) => worker.ReportProgress(ev.ProgressPercentage, ev.UserState);
			Application.ApplicationExit += Work.StopApplication;
			//Work.StartApplication();
			loadWorker.RunWorkerAsync();
			// resize event
			Size = new Size(Size.Width + 1, Size.Height + 1); // set to sth just for the event
		}

		private void runChecksButton_Click(object sender, EventArgs e) {
			_currentWorkType = WorkType.Check;
			worker.RunWorkerAsync(new Tuple<bool, bool, bool>(
				checkUACheckbox.Checked,
				checkDatesCheckBox.Checked,
				checkIndicativeCheckBox.Checked));
		}

		private void FillDgvTP() {
			dgv_tp.Rows.Clear();
			try {
				var termene = Work.TermenePastrare;
				foreach (var termen in termene) {
					dgv_tp.Rows.Add(
						termen,
						Work.ShortTermenePastrare[termen],
						Work.LongTermenePastrare[termen]);
				}
			} catch (Exception) {
				dgv_tp.Rows.Add("Eroare!");
			}
		}

		private void generateButton_Click(object sender, EventArgs e) {
			_currentWorkType = WorkType.Write;
			worker.RunWorkerAsync();
		}

		private void generateInventareCheckbox_CheckedChanged(object sender, EventArgs e) {
			generateInventareCheckbox.Checked = true;
		}

		private void generateDateCheckbox_CheckedChanged(object sender, EventArgs e) {
			registreDatePicker.Enabled = generateDateCheckbox.Checked;
		}

		private void dgv_tp_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			// if it is the short column, try parse it, see if it is correct
			string original = (string)dgv_tp.Rows[e.RowIndex].Cells["tp_original"].Value;
			if (dgv_tp.Columns[e.ColumnIndex].Name == "tp_short") {
				var cell = dgv_tp.Rows[e.RowIndex].Cells[e.ColumnIndex];
				string val = (string)cell.Value;
				if (val != "P" && !val.Contains("CS")) {
					bool success = int.TryParse(val, out _);
					if (!success) {
						cell.ErrorText = "Această valoare trebuie să fie un număr sau P!";
						return;
					}
				}
				cell.ErrorText = "";
				Work.ShortTermenePastrare[original] = val;
			}
			if (dgv_tp.Columns[e.ColumnIndex].Name == "tp_long") {
				var cell = dgv_tp.Rows[e.RowIndex].Cells[e.ColumnIndex];
				string val = (string)cell.Value;
				Work.LongTermenePastrare[original] = val;
			}
		}

		private void viewWarningsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!_isVerified) {
				var res = MessageBox.Show("Nu ați verificat incă centralizatorul. Sunteți sigur că doriți să continuați?", "Atenție", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
				if (res == DialogResult.No)
					return;
			}
			new WarningsForm(Warnings).ShowDialog();
		}

		private void copiazaTotToolStripMenuItem_Click(object sender, EventArgs e) {
			//Clipboard.SetText(rtb.Text);
			Clipboard.SetText(rtb.Rtf, TextDataFormat.Rtf);
		}

		private void despreToolStripMenuItem_Click(object sender, EventArgs e) {
			new AboutBox().ShowDialog();
		}

		private void loadWorker_DoWork(object sender, DoWorkEventArgs e) {
//#warning Excel startup is turned off
			Work.StartApplication();
			System.Threading.Thread.Sleep(1000);
		}

		private void loadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Show();
			Focus();
			Opacity = 100;
			_splashForm.Close();
		}

		private void configurareToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!worker.IsBusy)
				new ConfigurationForm().ShowDialog();
			else MessageBox.Show("Asteptați până când acțiunea curentă e finalizată!", "Atenție!", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void stergeTotToolStripMenuItem_Click(object sender, EventArgs e) {
			var res = MessageBox.Show("Sunteți sigur că doriți să ștergeți toate avertizările?", "Atenție", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
			if (res == DialogResult.No)
				return;
			rtb.ResetText();
			Warnings.Clear();
		}

		private void Form1_Resize(object sender, EventArgs e) {
			// do resize calculations
			groupBox3.Height = this.Height - 415;
			groupBox1.Location = new Point(
				groupBox1.Location.X,
				groupBox3.Location.Y +
				groupBox3.Size.Height +
				8
			);
			label.Width = splitContainer1.SplitterDistance - 16;
			progressBar.Width = splitContainer1.SplitterDistance - 16;
			rtb.Width = splitContainer1.SplitterDistance - 16;
			rtb.Height = this.Height - 256;
		}

		private void knownIssuesToolStripMenuItem_Click(object sender, EventArgs e) {
			new KnownIssuesForm().ShowDialog();
		}

		private void generateRegistruCheckbox_CheckedChanged(object sender, EventArgs e) {
			generateDateCheckbox.Enabled = generateRegistruCheckbox.Checked;
			generateDateCheckbox.Checked = false;
		}

		private void closeOpenExcelsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (worker.IsBusy) {
				MessageBox.Show("Te rog așteaptă până când acțiunea curentă e finalizată!", "Atenție!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			new CloseExcelForm().ShowDialog();
		}
	}
}
