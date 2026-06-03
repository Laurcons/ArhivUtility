using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Adapter for the current (2024+) centralizator format.
  /// Column layout: E=Subfond, G=Directia, H=Compartiment, L=NrUA, N=Indicativ,
  /// O=Continut, P=DateExtreme, Q=NrFile, R=Observatii, U=AnInceput, V=AnSfarsit, W=TermenPastrare
  /// </summary>
  public class DefaultCentralizatorAdapter : CentralizatorAdapterBase {
    public override string AdapterId => "default";
    public override string DisplayName => "Standard 2024";
    public override int DetectionPriority => 10;
    public override string CentralizatorSheetName => "Arhivatorul_template";
    public override string DateWorksheetName => "Date identificare";
    public override string TerminationColumn => "O";

    // Column mappings for current format
    protected override string SubfondColumn => "E";
    protected override string DirectiaColumn => "G";
    protected override string CompartimentColumn => "H";
    protected override string NrUAColumn => "L";
    protected override string IndicativColumn => "N";
    protected override string ContinutColumn => "O";
    protected override string DateExtremeColumn => "P";
    protected override string NrFileColumn => "Q";
    protected override string ObservatiiColumn => "R";
    protected override string AnInceputColumn => "U";
    protected override string AnSfarsitColumn => "V";
    protected override string TermenPastrareColumn => "W";

    /// <summary>
    /// Detects the current format by checking for "Servicii" or "Compartiment" header in column H.
    /// </summary>
    public override bool CanHandle(Excel._Worksheet worksheet) {
      string headerH = ReadHeaderCell(worksheet, 1, "H");
      if (headerH == null)
        return false;

      // Check if column H contains expected headers for the new format
      string headerUpper = headerH.ToUpper();
      return headerUpper.Contains("SERVICII") ||
             headerUpper.Contains("COMPARTIMENT") ||
             headerUpper.Contains("BIROU");
    }
  }
}
