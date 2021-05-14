using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IO = System.IO;

namespace ArhivUtility {
	public partial class WarningsForm : Form {
		public List<Warning> Warnings;
		public List<Type> Filtering;
		private bool causeDgvReload = true;
		private BackgroundWorkForm _bgForm;
		private bool _hasSubfonduri;
		public WarningsForm(List<Warning> warnings) {
			InitializeComponent();
			Warnings = warnings;
			Filtering = new List<Type>();
			Filtering.AddRange(
				warnings
					.Select(wn => wn.GetType())
					.Distinct()
			);
			_hasSubfonduri = Work.Subfonduri.Count() >= 2;
			Text = $"Lista avertizari pentru {Work.DateFirma.Nume ?? "[necunoscut]"}";
			if (_hasSubfonduri) {
				dgv.Columns.Insert(1, new DataGridViewTextBoxColumn() {
					HeaderText = "Subfond",
					Name = "subfond"
				});
			}
		}

		private void filterGenericToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterGenericToolStripMenuItem.Checked) {
				Filtering.Add(typeof(GenericWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(GenericWarning));
			}
			ReloadDgv();
		}

		private void filterFormatDateExtremeToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterFormatDateExtremeToolStripMenuItem.Checked) {
				Filtering.Add(typeof(DateExtremeFormatWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(DateExtremeFormatWarning));
			}
			ReloadDgv();
		}

		private void filterMismatchDateExtremeToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterMismatchDateExtremeToolStripMenuItem.Checked) {
				Filtering.Add(typeof(DateExtremeMismatchWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(DateExtremeMismatchWarning));
			}
			ReloadDgv();
		}

		private void filterInexistentUAToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterInexistentUAToolStripMenuItem.Checked) {
				Filtering.Add(typeof(UAInexistentWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(UAInexistentWarning));
			}
			ReloadDgv();
		}

		private void filterNotFoundUAToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterNotFoundUAToolStripMenuItem.Checked) {
				Filtering.Add(typeof(UANotFoundWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(UANotFoundWarning));
			}
			ReloadDgv();
		}

		private void filterRepeatingUAToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterRepeatingUAToolStripMenuItem.Checked) {
				Filtering.Add(typeof(UARepeatingWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(UARepeatingWarning));
			}
			ReloadDgv();
		}
		private void filterIncorrectIndicativToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			if (filterIncorrectIndicativToolStripMenuItem.Checked) {
				Filtering.Add(typeof(IndicativTPMismatchWarning));
			} else {
				Filtering.RemoveAll(t => t == typeof(IndicativTPMismatchWarning));
			}
			ReloadDgv();
		}

		private void ReloadDgv() {
			if (!causeDgvReload)
				return;
			dgv.Rows.Clear();
			var filteredWarnings = Warnings
				.Where(warn => Filtering.Contains(warn.GetType()));
			if (_hasSubfonduri) {
				int rownum = 1;
				foreach (var warning in filteredWarnings) {
					dgv.Rows.Add(
						rownum++,
						warning.Subfond,
						warning.Message);
				}
			} else {
				int rownum = 1;
				foreach (var warning in filteredWarnings) {
					dgv.Rows.Add(
						rownum++,
						warning.Message);
				}
			}
		}

		private void filterSelectAllToolStripMenuItem_Click(object sender, EventArgs e) {
			Filtering.Clear();
			Filtering.AddRange(
				Warnings
					.Select(wn => wn.GetType())
					.Distinct()
			);
			causeDgvReload = false;
			filterFormatDateExtremeToolStripMenuItem.Checked = true;
			filterInexistentUAToolStripMenuItem.Checked = true;
			filterGenericToolStripMenuItem.Checked = true;
			filterMismatchDateExtremeToolStripMenuItem.Checked = true;
			filterNotFoundUAToolStripMenuItem.Checked = true;
			filterRepeatingUAToolStripMenuItem.Checked = true;
			filterIncorrectIndicativToolStripMenuItem.Checked = true;
			causeDgvReload = true;
			ReloadDgv();
		}

		private void filterDeselectAllToolStripMenuItem_Click(object sender, EventArgs e) {
			Filtering.Clear();
			causeDgvReload = false;
			filterFormatDateExtremeToolStripMenuItem.Checked = false;
			filterInexistentUAToolStripMenuItem.Checked = false;
			filterGenericToolStripMenuItem.Checked = false;
			filterMismatchDateExtremeToolStripMenuItem.Checked = false;
			filterNotFoundUAToolStripMenuItem.Checked = false;
			filterRepeatingUAToolStripMenuItem.Checked = false;
			filterIncorrectIndicativToolStripMenuItem.Checked = false;
			causeDgvReload = true;
			ReloadDgv();
		}

		private void WarningsForm_Load(object sender, EventArgs e) {
			ReloadDgv();
		}

		private void generateReportToolStripMenuItem_Click(object sender, EventArgs e) {
			if (Warnings.Count == 0) {
				MessageBox.Show("Nu exista nicio avertizare de raportat. Nu se poate construi raportul!", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			_bgForm = new BackgroundWorkForm();
			_bgForm.Show();
			copyWorker.RunWorkerAsync();
		}

		private void deschideWordToolStripMenuItem_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("winword");
		}

		private void copyWorker_DoWork(object sender, DoWorkEventArgs e) {
			var dateFirma = Work.DateFirma;
			/*bool filtered = Filtering.Count ==
				Warnings.Select(wn => wn.GetType()).Distinct().Count();*/
			var filteredWarnings = Warnings
				.Where(warn => 
					Filtering.Contains(warn.GetType()))
				.ToList();
			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			// create a rtf control and use it to write stuff
			var rtb = new RichTextBox();
			// write metadata
			rtb.Font = new Font(rtb.SelectionFont.FontFamily, 11);
			rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, 18);
			rtb.AppendText($"RAPORTUL AVERTIZARILOR\n");
			if (dateFirma != null)
				rtb.AppendText($"Pentru fondul {dateFirma.Nume} avand CUI {dateFirma.CUI} nr.inm. {dateFirma.NrInmatriculare}\n");
			rtb.AppendText($"Generat la {DateTime.Now:dd.MM.yyyy HH:mm} in ArhivUtility v{version.Major}.{version.Minor}.{version.Build} b {version.Revision}\n");
			rtb.AppendText($"Tipuri avertizari: ");
			rtb.AppendText(Filtering.Select(wn => wn.Name).Distinct()
					.Aggregate((str1, str2) => str1 + ", " + str2) + "\n");
			rtb.AppendText($"Total avertizari: {filteredWarnings.Count()}\n");

			rtb.AppendText("\n");
			int row = 1;
			var subfonduri = Work.Subfonduri;
			foreach (var subfond in subfonduri) {
				// select subfonduri
				var selectedWarnings = filteredWarnings
					.Where(wng => wng.Subfond == subfond);
				if (selectedWarnings.Count() == 0)
					continue;
				if (_hasSubfonduri) {
					rtb.AppendText("\n" + subfond.ToUpper() + "\n");
				}
				foreach (var warning in selectedWarnings) {
					rtb.SelectionColor = Color.Red;
					rtb.AppendText($"Avertizarea {row++}: ");
					rtb.SelectionColor = Color.Black;
					rtb.AppendText(warning.Message + '\n');
				}
			}
			// write to file now
			string dirpath = IO.Path.GetTempPath() + "arvutil";
			string filepath = dirpath + @"\raport.rtf";
			if (!IO.Directory.Exists(dirpath)) {
				IO.Directory.CreateDirectory(dirpath);
			}
			rtb.SaveFile(filepath);
			e.Result = filepath;
		}

		private void copyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			// open the file in WordPad
			System.Diagnostics.Process.Start("winword.exe", (string)e.Result);
			System.Media.SystemSounds.Asterisk.Play();
			_bgForm.Close();
		}
	}
}
