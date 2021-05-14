using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Newtonsoft.Json;

namespace ArhivUtility {
	public class Configuration {
		public CentralizatorConfiguration Centralizator { get; set; }
		public readonly static string DefaultConfigName = "prestabilita";
		public static System.Collections.Specialized.StringDictionary Configs {
			get => Properties.Settings.Default.Configurations;
			set => Properties.Settings.Default.Configurations = value;
		}
		public static string LastConfiguration {
			get => Properties.Settings.Default.LastConfiguration;
			set => Properties.Settings.Default.LastConfiguration = value;
		}

		static Configuration() {
			if (Configs == null)
				Configs = new System.Collections.Specialized.StringDictionary();
		}

		public static Configuration GetCurrentConfiguration() {
			return GetConfiguration(LastConfiguration);
		}

		public static Configuration GetConfiguration(string configName) {
			if (!Configs.ContainsKey(configName)) {
				LastConfiguration = DefaultConfigName;
				return GetDefaultConfiguration();
			}
			var configString = Configs[configName];
			var config = JsonConvert.DeserializeObject<Configuration>(configString);
			return config;
		}

		public static Configuration GetDefaultConfiguration() {
			if (!Configs.ContainsKey(DefaultConfigName)) {
				CreateDefaultConfiguration();
			}
			return GetConfiguration(DefaultConfigName);
		}

		public static void CreateDefaultConfiguration() {
			CreateConfiguration(DefaultConfigName);
		}

		public static void CreateConfiguration(string name) {
			var config = new Configuration {
				Centralizator = new CentralizatorConfiguration() {
					OldCompartimentColumn = "G",
					NewServicesColumn = "H",
					NrUAColumn = "L",
					IndicativColumn = "N",
					ContinutColumn = "O",
					DateExtremeColumn = "P",
					NrFileColumn = "Q",
					ObservatiiColumn = "R",
					AnInceputColumn = "U",
					AnSfarsitColumn = "V",
					TermenPastrareColumn = "W",
					DateArvutilName = "Date Arvutil",
					DenumiriAnterioareName = "Denumiri anterioare"
				}
			};
			var configStr = JsonConvert.SerializeObject(config);
			Configs[name] = configStr;
		}
	}

	[DisplayName("Centraliz")]
	public class CentralizatorConfiguration {

		public string OldCompartimentColumn { get; set; }
		public string NewServicesColumn { get; set; }
		public string NrUAColumn { get; set; }
		public string IndicativColumn { get; set; }
		public string ContinutColumn { get; set; }
		public string DateExtremeColumn { get; set; }
		public string NrFileColumn { get; set; }
		public string ObservatiiColumn { get; set; }
		public string AnInceputColumn { get; set; }
		public string AnSfarsitColumn { get; set; }
		public string TermenPastrareColumn { get; set; }
		public string DateArvutilName { get; set; }
		public string DenumiriAnterioareName { get; set; }
	}
}
