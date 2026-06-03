using System.Drawing;
using System.Windows.Forms;

namespace ArhivUtility {
  public partial class NewsForm : Form {
    public NewsForm() {
      InitializeComponent();
      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
      rtb.AppendText("Noutati:\n\n");

      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
      rtb.AppendText("0.2.13 (04 iunie 2026)\n");
      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
      rtb.AppendText("- [nou] Centralizatoarele \"Noi 2026\" pot fi acum citite; posibil sa existe diferite probleme, se pot raporta lui Bubu\n" +
        "- [fix] Numerele UA duplicate sunt acum corect raportate la verificare\n" +
        "- [nou] Avertizarile sunt acum grupate dupa tip, iar tipul e vizibil in tabelul de avertizari\n");
      rtb.AppendText("\n");

      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
      rtb.AppendText("0.2.12 (30 mai 2026)\n");
      rtb.SelectionFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
      rtb.AppendText("- [refactor] Performanta la citire a centralizatoarelor a fost imbunatatita\n" +
        "- [fix] S-au actualizat dependintele programului la ultimele versiuni\n" +
        "- [refactor] S-a modificat modul intern de detectare a tipului de centralizator\n");
    }
  }
}
