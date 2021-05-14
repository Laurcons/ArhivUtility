using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ArhivUtility {
	public partial class CloseExcelForm : Form {
		public CloseExcelForm() {
			InitializeComponent();
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e) {
			// close the app's process
			Work.StopApplication();
			// close all excels
			Process[] processes = Process.GetProcessesByName("EXCEL");
			foreach (var proc in processes) {
				proc.Kill();
			}
			// open the application's process
			Work.StartApplication();
			// wait
			System.Threading.Thread.Sleep(1000);
		}

		private void CloseExcelForm_Load(object sender, EventArgs e) {
			worker.RunWorkerAsync();
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Close();
		}
	}
}
