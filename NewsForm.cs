using System.Drawing;
using System.Windows.Forms;

namespace ArhivUtility {
  public partial class NewsForm : Form {
    public NewsForm() {
      InitializeComponent();
      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
      rtb.AppendText("Noutati:\n\n");

      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
      rtb.AppendText("0.2.12 (30 mai 2026)\n");
      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
      rtb.AppendText("- [refactor] Performanta la citire a centralizatoarelor a fost imbunatatita\n" +
        "- [fix] S-au actualizat dependintele programului la ultimele versiuni\n" +
        "- [refactor] S-a modificat modul intern de detectare a tipului de centralizator\n");
    }
  }
}
