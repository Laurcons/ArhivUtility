namespace ArhivUtility {
	partial class WarningsForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.filtreazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterGenericToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterFormatDateExtremeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterMismatchDateExtremeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterInexistentUAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterNotFoundUAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterRepeatingUAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterIncorrectIndicativToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.filterSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterDeselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dgv = new System.Windows.Forms.DataGridView();
			this.nrcrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.copyWorker = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtreazaToolStripMenuItem,
            this.generateReportToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(805, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// filtreazaToolStripMenuItem
			// 
			this.filtreazaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterGenericToolStripMenuItem,
            this.filterFormatDateExtremeToolStripMenuItem,
            this.filterMismatchDateExtremeToolStripMenuItem,
            this.filterInexistentUAToolStripMenuItem,
            this.filterNotFoundUAToolStripMenuItem,
            this.filterRepeatingUAToolStripMenuItem,
            this.filterIncorrectIndicativToolStripMenuItem,
            this.toolStripSeparator1,
            this.filterSelectAllToolStripMenuItem,
            this.filterDeselectAllToolStripMenuItem});
			this.filtreazaToolStripMenuItem.Name = "filtreazaToolStripMenuItem";
			this.filtreazaToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.filtreazaToolStripMenuItem.Text = "Filtrează";
			// 
			// filterGenericToolStripMenuItem
			// 
			this.filterGenericToolStripMenuItem.Checked = true;
			this.filterGenericToolStripMenuItem.CheckOnClick = true;
			this.filterGenericToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterGenericToolStripMenuItem.Name = "filterGenericToolStripMenuItem";
			this.filterGenericToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterGenericToolStripMenuItem.Text = "Avertizări generice";
			this.filterGenericToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterGenericToolStripMenuItem_CheckedChanged);
			// 
			// filterFormatDateExtremeToolStripMenuItem
			// 
			this.filterFormatDateExtremeToolStripMenuItem.Checked = true;
			this.filterFormatDateExtremeToolStripMenuItem.CheckOnClick = true;
			this.filterFormatDateExtremeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterFormatDateExtremeToolStripMenuItem.Name = "filterFormatDateExtremeToolStripMenuItem";
			this.filterFormatDateExtremeToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterFormatDateExtremeToolStripMenuItem.Text = "Date extreme ciudate";
			this.filterFormatDateExtremeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterFormatDateExtremeToolStripMenuItem_CheckedChanged);
			// 
			// filterMismatchDateExtremeToolStripMenuItem
			// 
			this.filterMismatchDateExtremeToolStripMenuItem.Checked = true;
			this.filterMismatchDateExtremeToolStripMenuItem.CheckOnClick = true;
			this.filterMismatchDateExtremeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterMismatchDateExtremeToolStripMenuItem.Name = "filterMismatchDateExtremeToolStripMenuItem";
			this.filterMismatchDateExtremeToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterMismatchDateExtremeToolStripMenuItem.Text = "Date extreme incorecte";
			this.filterMismatchDateExtremeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterMismatchDateExtremeToolStripMenuItem_CheckedChanged);
			// 
			// filterInexistentUAToolStripMenuItem
			// 
			this.filterInexistentUAToolStripMenuItem.Checked = true;
			this.filterInexistentUAToolStripMenuItem.CheckOnClick = true;
			this.filterInexistentUAToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterInexistentUAToolStripMenuItem.Name = "filterInexistentUAToolStripMenuItem";
			this.filterInexistentUAToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterInexistentUAToolStripMenuItem.Text = "UA necompletate";
			this.filterInexistentUAToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterInexistentUAToolStripMenuItem_CheckedChanged);
			// 
			// filterNotFoundUAToolStripMenuItem
			// 
			this.filterNotFoundUAToolStripMenuItem.Checked = true;
			this.filterNotFoundUAToolStripMenuItem.CheckOnClick = true;
			this.filterNotFoundUAToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterNotFoundUAToolStripMenuItem.Name = "filterNotFoundUAToolStripMenuItem";
			this.filterNotFoundUAToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterNotFoundUAToolStripMenuItem.Text = "UA inexistente";
			this.filterNotFoundUAToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterNotFoundUAToolStripMenuItem_CheckedChanged);
			// 
			// filterRepeatingUAToolStripMenuItem
			// 
			this.filterRepeatingUAToolStripMenuItem.Checked = true;
			this.filterRepeatingUAToolStripMenuItem.CheckOnClick = true;
			this.filterRepeatingUAToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterRepeatingUAToolStripMenuItem.Name = "filterRepeatingUAToolStripMenuItem";
			this.filterRepeatingUAToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterRepeatingUAToolStripMenuItem.Text = "UA repetate";
			this.filterRepeatingUAToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterRepeatingUAToolStripMenuItem_CheckedChanged);
			// 
			// filterIncorrectIndicativToolStripMenuItem
			// 
			this.filterIncorrectIndicativToolStripMenuItem.Checked = true;
			this.filterIncorrectIndicativToolStripMenuItem.CheckOnClick = true;
			this.filterIncorrectIndicativToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.filterIncorrectIndicativToolStripMenuItem.Name = "filterIncorrectIndicativToolStripMenuItem";
			this.filterIncorrectIndicativToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterIncorrectIndicativToolStripMenuItem.Text = "Indicative vs TP incorecte";
			this.filterIncorrectIndicativToolStripMenuItem.CheckedChanged += new System.EventHandler(this.filterIncorrectIndicativToolStripMenuItem_CheckedChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
			// 
			// filterSelectAllToolStripMenuItem
			// 
			this.filterSelectAllToolStripMenuItem.Name = "filterSelectAllToolStripMenuItem";
			this.filterSelectAllToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterSelectAllToolStripMenuItem.Text = "Selectează tot";
			this.filterSelectAllToolStripMenuItem.Click += new System.EventHandler(this.filterSelectAllToolStripMenuItem_Click);
			// 
			// filterDeselectAllToolStripMenuItem
			// 
			this.filterDeselectAllToolStripMenuItem.Name = "filterDeselectAllToolStripMenuItem";
			this.filterDeselectAllToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.filterDeselectAllToolStripMenuItem.Text = "Deselectează tot";
			this.filterDeselectAllToolStripMenuItem.Click += new System.EventHandler(this.filterDeselectAllToolStripMenuItem_Click);
			// 
			// generateReportToolStripMenuItem
			// 
			this.generateReportToolStripMenuItem.Name = "generateReportToolStripMenuItem";
			this.generateReportToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
			this.generateReportToolStripMenuItem.Text = "Generează raport";
			this.generateReportToolStripMenuItem.Click += new System.EventHandler(this.generateReportToolStripMenuItem_Click);
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nrcrt,
            this.text});
			this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv.Location = new System.Drawing.Point(0, 24);
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgv.Size = new System.Drawing.Size(805, 337);
			this.dgv.TabIndex = 1;
			// 
			// nrcrt
			// 
			this.nrcrt.HeaderText = "Nr. crt.";
			this.nrcrt.Name = "nrcrt";
			this.nrcrt.ReadOnly = true;
			this.nrcrt.Width = 64;
			// 
			// text
			// 
			this.text.HeaderText = "Text avertizare";
			this.text.Name = "text";
			this.text.ReadOnly = true;
			this.text.Width = 102;
			// 
			// copyWorker
			// 
			this.copyWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.copyWorker_DoWork);
			this.copyWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.copyWorker_RunWorkerCompleted);
			// 
			// WarningsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(805, 361);
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "WarningsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Listă avertizări";
			this.Load += new System.EventHandler(this.WarningsForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.ToolStripMenuItem filtreazaToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterGenericToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterFormatDateExtremeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterMismatchDateExtremeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterInexistentUAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterNotFoundUAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterRepeatingUAToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem filterSelectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterDeselectAllToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn nrcrt;
		private System.Windows.Forms.DataGridViewTextBoxColumn text;
		private System.Windows.Forms.ToolStripMenuItem generateReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterIncorrectIndicativToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker copyWorker;
	}
}