using System.IO;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace ArhivUtility {
  internal static class UpdateController {

    public static void PerformUpdates() {
      if (Path.GetFileName(Application.ExecutablePath) != "ArhivUtility.exe") {
        MessageBox.Show(
          "Executabilul nu este denumit \"ArhivUtility.exe\".\n" +
          "Redenumiti fisierul si reincercati pentru a putea primi actualizari automate.",
          "Actualizare imposibila",
          MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        return;
      }

      // update checker
      AutoUpdater.RunUpdateAsAdmin = false;
      AutoUpdater.Mandatory = true;
      AutoUpdater.UpdateMode = Mode.ForcedDownload;
      //AutoUpdater.ApplicationExitEvent += () => { Work.StopApplication(); };
      AutoUpdater.Synchronous = true;
      AutoUpdater.Start("https://static.laurcons.ro/arhivutility/autoupdater.xml");
    }
  }
}
