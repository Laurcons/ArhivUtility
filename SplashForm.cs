using System;
using System.Reflection;
using System.Windows.Forms;

namespace ArhivUtility {
  public partial class SplashForm : Form {
    public SplashForm() {
      InitializeComponent();
    }

    private void SplashForm_Load(object sender, EventArgs e) {
      label2.Text = AssemblyCopyright;
    }

    public string AssemblyCopyright {
      get {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0) {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }
  }
}
