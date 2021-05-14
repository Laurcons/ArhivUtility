using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ArhivUtility {
	public partial class ConfigurationForm : Form {
		private Configuration TempConfig;
		
		public ConfigurationForm() {
			InitializeComponent();
		}

		private void ConfigurationForm_Load(object sender, EventArgs e) {
			if (Configuration.Configs.Keys.Count == 0) {
				Configuration.CreateDefaultConfiguration();
			}
			LoadConfigs();
		}

		private void configurationsCombo_SelectedIndexChanged(object sender, EventArgs e) {
			Configuration.LastConfiguration = (string)configurationsCombo.SelectedItem;
			LoadConfig();
		}

		private void LoadConfigs() {
			// fill cbx with the configurations
			var keys =
				Configuration.Configs.Keys
					.Cast<string>()
					.ToArray();
			configurationsCombo.Items.AddRange(keys);
			configurationsCombo.SelectedItem = Configuration.LastConfiguration;
		}

		private void LoadConfig() {
			string config = (string)configurationsCombo.SelectedItem;
			Configuration.LastConfiguration = config;
			TempConfig = Configuration.GetCurrentConfiguration();
			centralizatorPropertyGrid.SelectedObject = TempConfig.Centralizator;
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			if (Configuration.LastConfiguration == Configuration.DefaultConfigName) {
				MessageBox.Show("Nu se poate sterge configuratia prestabilita!", "Atentie!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Configuration.Configs.Remove(Configuration.LastConfiguration);
			Configuration.LastConfiguration = Configuration.DefaultConfigName;
		}

		private void addButton_Click(object sender, EventArgs e) {
			Configuration.CreateConfiguration("nou");
		}
	}

	#region Classes

	public class CentralizatorPropertyGridData {

	}
	#endregion

}
