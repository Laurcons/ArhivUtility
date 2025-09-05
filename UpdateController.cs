using AutoUpdaterDotNET;

namespace ArhivUtility {
  internal static class UpdateController {

    public static void PerformUpdates() {
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
