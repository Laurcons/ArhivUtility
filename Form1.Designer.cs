namespace ArhivUtility {
	partial class Form1 {
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
			this.components = new System.ComponentModel.Container();
			this.label = new System.Windows.Forms.Label();
			this.rtb = new System.Windows.Forms.RichTextBox();
			this.rtbContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copiazaTotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stergeTotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.worker = new System.ComponentModel.BackgroundWorker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.displayWarningsInInventareCheckBox = new System.Windows.Forms.CheckBox();
			this.registreDatePicker = new System.Windows.Forms.DateTimePicker();
			this.generateDateCheckbox = new System.Windows.Forms.CheckBox();
			this.generateButton = new System.Windows.Forms.Button();
			this.generateInventareCheckbox = new System.Windows.Forms.CheckBox();
			this.generateRegistruCheckbox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkIndicativeCheckBox = new System.Windows.Forms.CheckBox();
			this.runChecksButton = new System.Windows.Forms.Button();
			this.checkDatesCheckBox = new System.Windows.Forms.CheckBox();
			this.checkUACheckbox = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dgv_tp = new System.Windows.Forms.DataGridView();
			this.tp_original = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tp_short = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tp_long = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel = new System.Windows.Forms.Panel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.configurareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.knownIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unelteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeOpenExcelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.despreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadWorker = new System.ComponentModel.BackgroundWorker();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.rtbContext.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv_tp)).BeginInit();
			this.panel.SuspendLayout();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.AllowDrop = true;
			this.label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label.Location = new System.Drawing.Point(12, 3);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(437, 147);
			this.label.TabIndex = 0;
			this.label.Text = "Trage documentul aici";
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label.DragDrop += new System.Windows.Forms.DragEventHandler(this.label1_DragDrop);
			this.label.DragOver += new System.Windows.Forms.DragEventHandler(this.label1_DragOver);
			this.label.DragLeave += new System.EventHandler(this.label_DragLeave);
			this.label.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.label1_QueryContinueDrag);
			// 
			// rtb
			// 
			this.rtb.ContextMenuStrip = this.rtbContext;
			this.rtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtb.Location = new System.Drawing.Point(12, 182);
			this.rtb.Name = "rtb";
			this.rtb.ReadOnly = true;
			this.rtb.Size = new System.Drawing.Size(437, 327);
			this.rtb.TabIndex = 1;
			this.rtb.Text = "";
			// 
			// rtbContext
			// 
			this.rtbContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewWarningsToolStripMenuItem,
            this.copiazaTotToolStripMenuItem,
            this.stergeTotToolStripMenuItem});
			this.rtbContext.Name = "rtbContext";
			this.rtbContext.Size = new System.Drawing.Size(187, 70);
			// 
			// viewWarningsToolStripMenuItem
			// 
			this.viewWarningsToolStripMenuItem.Name = "viewWarningsToolStripMenuItem";
			this.viewWarningsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.viewWarningsToolStripMenuItem.Text = "Vizualizeaza avertizari";
			this.viewWarningsToolStripMenuItem.Click += new System.EventHandler(this.viewWarningsToolStripMenuItem_Click);
			// 
			// copiazaTotToolStripMenuItem
			// 
			this.copiazaTotToolStripMenuItem.Enabled = false;
			this.copiazaTotToolStripMenuItem.Name = "copiazaTotToolStripMenuItem";
			this.copiazaTotToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.copiazaTotToolStripMenuItem.Text = "Copiaza tot";
			this.copiazaTotToolStripMenuItem.Click += new System.EventHandler(this.copiazaTotToolStripMenuItem_Click);
			// 
			// stergeTotToolStripMenuItem
			// 
			this.stergeTotToolStripMenuItem.Name = "stergeTotToolStripMenuItem";
			this.stergeTotToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.stergeTotToolStripMenuItem.Text = "Sterge tot";
			this.stergeTotToolStripMenuItem.Click += new System.EventHandler(this.stergeTotToolStripMenuItem_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 153);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(437, 23);
			this.progressBar.TabIndex = 2;
			// 
			// worker
			// 
			this.worker.WorkerReportsProgress = true;
			this.worker.WorkerSupportsCancellation = true;
			this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
			this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_ProgressChanged);
			this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.displayWarningsInInventareCheckBox);
			this.groupBox1.Controls.Add(this.registreDatePicker);
			this.groupBox1.Controls.Add(this.generateDateCheckbox);
			this.groupBox1.Controls.Add(this.generateButton);
			this.groupBox1.Controls.Add(this.generateInventareCheckbox);
			this.groupBox1.Controls.Add(this.generateRegistruCheckbox);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(4, 417);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(476, 152);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Opțiuni generare";
			// 
			// displayWarningsInInventareCheckBox
			// 
			this.displayWarningsInInventareCheckBox.AutoSize = true;
			this.displayWarningsInInventareCheckBox.Enabled = false;
			this.displayWarningsInInventareCheckBox.Location = new System.Drawing.Point(6, 79);
			this.displayWarningsInInventareCheckBox.Name = "displayWarningsInInventareCheckBox";
			this.displayWarningsInInventareCheckBox.Size = new System.Drawing.Size(297, 22);
			this.displayWarningsInInventareCheckBox.TabIndex = 5;
			this.displayWarningsInInventareCheckBox.Text = "Evidentiază avertizări cu roșu în inventare";
			this.toolTip1.SetToolTip(this.displayWarningsInInventareCheckBox, "Pentru a putea marca cu rosu, trebuie mai intai sa rulezi o verificare!");
			this.displayWarningsInInventareCheckBox.UseVisualStyleBackColor = true;
			// 
			// registreDatePicker
			// 
			this.registreDatePicker.Enabled = false;
			this.registreDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.registreDatePicker.Location = new System.Drawing.Point(358, 21);
			this.registreDatePicker.Name = "registreDatePicker";
			this.registreDatePicker.Size = new System.Drawing.Size(108, 24);
			this.registreDatePicker.TabIndex = 4;
			// 
			// generateDateCheckbox
			// 
			this.generateDateCheckbox.AutoSize = true;
			this.generateDateCheckbox.Location = new System.Drawing.Point(186, 23);
			this.generateDateCheckbox.Name = "generateDateCheckbox";
			this.generateDateCheckbox.Size = new System.Drawing.Size(166, 22);
			this.generateDateCheckbox.TabIndex = 3;
			this.generateDateCheckbox.Text = "Pune data pe registru";
			this.generateDateCheckbox.UseVisualStyleBackColor = true;
			this.generateDateCheckbox.CheckedChanged += new System.EventHandler(this.generateDateCheckbox_CheckedChanged);
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(6, 105);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(464, 41);
			this.generateButton.TabIndex = 2;
			this.generateButton.Text = "Generează";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// generateInventareCheckbox
			// 
			this.generateInventareCheckbox.AutoSize = true;
			this.generateInventareCheckbox.Checked = true;
			this.generateInventareCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.generateInventareCheckbox.Location = new System.Drawing.Point(6, 23);
			this.generateInventareCheckbox.Name = "generateInventareCheckbox";
			this.generateInventareCheckbox.Size = new System.Drawing.Size(174, 22);
			this.generateInventareCheckbox.TabIndex = 1;
			this.generateInventareCheckbox.Text = "Generează inventarele";
			this.generateInventareCheckbox.UseVisualStyleBackColor = true;
			this.generateInventareCheckbox.CheckedChanged += new System.EventHandler(this.generateInventareCheckbox_CheckedChanged);
			// 
			// generateRegistruCheckbox
			// 
			this.generateRegistruCheckbox.AutoSize = true;
			this.generateRegistruCheckbox.Checked = true;
			this.generateRegistruCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.generateRegistruCheckbox.Location = new System.Drawing.Point(6, 51);
			this.generateRegistruCheckbox.Name = "generateRegistruCheckbox";
			this.generateRegistruCheckbox.Size = new System.Drawing.Size(287, 22);
			this.generateRegistruCheckbox.TabIndex = 0;
			this.generateRegistruCheckbox.Text = "Generează registrul de evidență curentă";
			this.generateRegistruCheckbox.UseVisualStyleBackColor = true;
			this.generateRegistruCheckbox.CheckedChanged += new System.EventHandler(this.generateRegistruCheckbox_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkIndicativeCheckBox);
			this.groupBox2.Controls.Add(this.runChecksButton);
			this.groupBox2.Controls.Add(this.checkDatesCheckBox);
			this.groupBox2.Controls.Add(this.checkUACheckbox);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(4, 7);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(476, 166);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Opțiuni verificare";
			// 
			// checkIndicativeCheckBox
			// 
			this.checkIndicativeCheckBox.AutoSize = true;
			this.checkIndicativeCheckBox.Checked = true;
			this.checkIndicativeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkIndicativeCheckBox.Location = new System.Drawing.Point(6, 79);
			this.checkIndicativeCheckBox.Name = "checkIndicativeCheckBox";
			this.checkIndicativeCheckBox.Size = new System.Drawing.Size(245, 22);
			this.checkIndicativeCheckBox.TabIndex = 2;
			this.checkIndicativeCheckBox.Text = "Verifică unicitatea TP per indicativ";
			this.checkIndicativeCheckBox.UseVisualStyleBackColor = true;
			// 
			// runChecksButton
			// 
			this.runChecksButton.Location = new System.Drawing.Point(6, 107);
			this.runChecksButton.Name = "runChecksButton";
			this.runChecksButton.Size = new System.Drawing.Size(464, 48);
			this.runChecksButton.TabIndex = 1;
			this.runChecksButton.Text = "Rulează verificări";
			this.runChecksButton.UseVisualStyleBackColor = true;
			this.runChecksButton.Click += new System.EventHandler(this.runChecksButton_Click);
			// 
			// checkDatesCheckBox
			// 
			this.checkDatesCheckBox.AutoSize = true;
			this.checkDatesCheckBox.Checked = true;
			this.checkDatesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkDatesCheckBox.Location = new System.Drawing.Point(6, 51);
			this.checkDatesCheckBox.Name = "checkDatesCheckBox";
			this.checkDatesCheckBox.Size = new System.Drawing.Size(437, 22);
			this.checkDatesCheckBox.TabIndex = 0;
			this.checkDatesCheckBox.Text = "Verifică concordanța anilor de început/sfârșit cu datele extreme\r\n";
			this.checkDatesCheckBox.UseVisualStyleBackColor = true;
			// 
			// checkUACheckbox
			// 
			this.checkUACheckbox.AutoSize = true;
			this.checkUACheckbox.Checked = true;
			this.checkUACheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkUACheckbox.Location = new System.Drawing.Point(6, 23);
			this.checkUACheckbox.Name = "checkUACheckbox";
			this.checkUACheckbox.Size = new System.Drawing.Size(276, 22);
			this.checkUACheckbox.TabIndex = 0;
			this.checkUACheckbox.Text = "Verifică numerele UA incorect atribuite";
			this.checkUACheckbox.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dgv_tp);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(4, 179);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(476, 232);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Gestionare termene de pastrare";
			// 
			// dgv_tp
			// 
			this.dgv_tp.AllowUserToAddRows = false;
			this.dgv_tp.AllowUserToDeleteRows = false;
			this.dgv_tp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgv_tp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv_tp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tp_original,
            this.tp_short,
            this.tp_long});
			this.dgv_tp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv_tp.Location = new System.Drawing.Point(3, 20);
			this.dgv_tp.Name = "dgv_tp";
			this.dgv_tp.Size = new System.Drawing.Size(470, 209);
			this.dgv_tp.TabIndex = 0;
			this.dgv_tp.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tp_CellEndEdit);
			// 
			// tp_original
			// 
			this.tp_original.HeaderText = "Original";
			this.tp_original.Name = "tp_original";
			this.tp_original.ReadOnly = true;
			this.tp_original.Width = 83;
			// 
			// tp_short
			// 
			this.tp_short.HeaderText = "Nume scurt";
			this.tp_short.Name = "tp_short";
			this.tp_short.Width = 110;
			// 
			// tp_long
			// 
			this.tp_long.HeaderText = "Nume lung";
			this.tp_long.Name = "tp_long";
			this.tp_long.Width = 104;
			// 
			// panel
			// 
			this.panel.Controls.Add(this.groupBox2);
			this.panel.Controls.Add(this.groupBox1);
			this.panel.Controls.Add(this.groupBox3);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(492, 581);
			this.panel.TabIndex = 6;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurareToolStripMenuItem,
            this.knownIssuesToolStripMenuItem,
            this.unelteToolStripMenuItem,
            this.despreToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(959, 24);
			this.menuStrip.TabIndex = 7;
			this.menuStrip.Text = "menuStrip2";
			// 
			// configurareToolStripMenuItem
			// 
			this.configurareToolStripMenuItem.Enabled = false;
			this.configurareToolStripMenuItem.Name = "configurareToolStripMenuItem";
			this.configurareToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
			this.configurareToolStripMenuItem.Text = "Configurare";
			this.configurareToolStripMenuItem.Click += new System.EventHandler(this.configurareToolStripMenuItem_Click);
			// 
			// knownIssuesToolStripMenuItem
			// 
			this.knownIssuesToolStripMenuItem.Name = "knownIssuesToolStripMenuItem";
			this.knownIssuesToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
			this.knownIssuesToolStripMenuItem.Text = "Probleme cunoscute";
			this.knownIssuesToolStripMenuItem.Click += new System.EventHandler(this.knownIssuesToolStripMenuItem_Click);
			// 
			// unelteToolStripMenuItem
			// 
			this.unelteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeOpenExcelsToolStripMenuItem});
			this.unelteToolStripMenuItem.Name = "unelteToolStripMenuItem";
			this.unelteToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
			this.unelteToolStripMenuItem.Text = "Unelte";
			// 
			// closeOpenExcelsToolStripMenuItem
			// 
			this.closeOpenExcelsToolStripMenuItem.Name = "closeOpenExcelsToolStripMenuItem";
			this.closeOpenExcelsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.closeOpenExcelsToolStripMenuItem.Text = "Închide Excel-uri deschise";
			this.closeOpenExcelsToolStripMenuItem.Click += new System.EventHandler(this.closeOpenExcelsToolStripMenuItem_Click);
			// 
			// despreToolStripMenuItem
			// 
			this.despreToolStripMenuItem.Name = "despreToolStripMenuItem";
			this.despreToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
			this.despreToolStripMenuItem.Text = "Despre";
			this.despreToolStripMenuItem.Click += new System.EventHandler(this.despreToolStripMenuItem_Click);
			// 
			// loadWorker
			// 
			this.loadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadWorker_DoWork);
			this.loadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadWorker_RunWorkerCompleted);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label);
			this.splitContainer1.Panel1.Controls.Add(this.rtb);
			this.splitContainer1.Panel1.Controls.Add(this.progressBar);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel);
			this.splitContainer1.Size = new System.Drawing.Size(959, 581);
			this.splitContainer1.SplitterDistance = 463;
			this.splitContainer1.TabIndex = 8;
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.AutoPopDelay = 100000;
			this.toolTip1.InitialDelay = 0;
			this.toolTip1.ReshowDelay = 100;
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(959, 605);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ArhivUtility - versiunea";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.rtbContext.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgv_tp)).EndInit();
			this.panel.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label;
		private System.Windows.Forms.RichTextBox rtb;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.ComponentModel.BackgroundWorker worker;
		private System.Windows.Forms.ContextMenuStrip rtbContext;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox generateInventareCheckbox;
		private System.Windows.Forms.CheckBox generateRegistruCheckbox;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button runChecksButton;
		private System.Windows.Forms.CheckBox checkDatesCheckBox;
		private System.Windows.Forms.CheckBox checkUACheckbox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dgv_tp;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.DateTimePicker registreDatePicker;
		private System.Windows.Forms.CheckBox generateDateCheckbox;
		private System.Windows.Forms.DataGridViewTextBoxColumn tp_original;
		private System.Windows.Forms.DataGridViewTextBoxColumn tp_short;
		private System.Windows.Forms.DataGridViewTextBoxColumn tp_long;
		private System.Windows.Forms.ToolStripMenuItem viewWarningsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copiazaTotToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem configurareToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem despreToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker loadWorker;
		private System.Windows.Forms.ToolStripMenuItem stergeTotToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.CheckBox displayWarningsInInventareCheckBox;
		private System.Windows.Forms.ToolStripMenuItem knownIssuesToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripMenuItem unelteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeOpenExcelsToolStripMenuItem;
		private System.Windows.Forms.CheckBox checkIndicativeCheckBox;
	}
}

