namespace ArhivUtility {
	partial class ConfigurationForm {
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
			System.Windows.Forms.Label label1;
			this.centralizatorPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.centralizatorTabPage = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.configurationsCombo = new System.Windows.Forms.ComboBox();
			this.addButton = new System.Windows.Forms.Button();
			this.renameButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.centralizatorTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(105, 13);
			label1.TabIndex = 2;
			label1.Text = "Configuratia curenta:";
			// 
			// centralizatorPropertyGrid
			// 
			this.centralizatorPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.centralizatorPropertyGrid.Location = new System.Drawing.Point(3, 3);
			this.centralizatorPropertyGrid.Name = "centralizatorPropertyGrid";
			this.centralizatorPropertyGrid.Size = new System.Drawing.Size(705, 317);
			this.centralizatorPropertyGrid.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.centralizatorTabPage);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControl.Location = new System.Drawing.Point(0, 33);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(719, 349);
			this.tabControl.TabIndex = 1;
			// 
			// centralizatorTabPage
			// 
			this.centralizatorTabPage.Controls.Add(this.centralizatorPropertyGrid);
			this.centralizatorTabPage.Location = new System.Drawing.Point(4, 22);
			this.centralizatorTabPage.Name = "centralizatorTabPage";
			this.centralizatorTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.centralizatorTabPage.Size = new System.Drawing.Size(711, 323);
			this.centralizatorTabPage.TabIndex = 0;
			this.centralizatorTabPage.Text = "Centralizator";
			this.centralizatorTabPage.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(711, 323);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// configurationsCombo
			// 
			this.configurationsCombo.FormattingEnabled = true;
			this.configurationsCombo.Location = new System.Drawing.Point(123, 6);
			this.configurationsCombo.Name = "configurationsCombo";
			this.configurationsCombo.Size = new System.Drawing.Size(327, 21);
			this.configurationsCombo.TabIndex = 3;
			this.configurationsCombo.SelectedIndexChanged += new System.EventHandler(this.configurationsCombo_SelectedIndexChanged);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(456, 6);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 21);
			this.addButton.TabIndex = 4;
			this.addButton.Text = "Adauga";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// renameButton
			// 
			this.renameButton.Location = new System.Drawing.Point(537, 6);
			this.renameButton.Name = "renameButton";
			this.renameButton.Size = new System.Drawing.Size(89, 21);
			this.renameButton.TabIndex = 4;
			this.renameButton.Text = "Redenumeste";
			this.renameButton.UseVisualStyleBackColor = true;
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(632, 6);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 21);
			this.deleteButton.TabIndex = 4;
			this.deleteButton.Text = "Sterge";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// ConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(719, 382);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.renameButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.configurationsCombo);
			this.Controls.Add(label1);
			this.Controls.Add(this.tabControl);
			this.Name = "ConfigurationForm";
			this.Text = "Configurare ArhivUtility";
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.tabControl.ResumeLayout(false);
			this.centralizatorTabPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid centralizatorPropertyGrid;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage centralizatorTabPage;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ComboBox configurationsCombo;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button renameButton;
		private System.Windows.Forms.Button deleteButton;
	}
}