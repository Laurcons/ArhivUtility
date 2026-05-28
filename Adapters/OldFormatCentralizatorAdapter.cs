using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Adapter for the old centralizator format.
  /// In this format, G=Compartiment (no separate Directia column).
  /// </summary>
  public class OldFormatCentralizatorAdapter : CentralizatorAdapterBase {
    public override string AdapterId => "old-format";
    public override string DisplayName => "Format Vechi (pre-2024)";
    public override int DetectionPriority => 5;
    public override string TerminationColumn => "O";

    // Column mappings for old format
    // Note: DirectiaColumn is null - old format has no separate Directia
    protected override string SubfondColumn => "E";
    protected override string DirectiaColumn => null;
    protected override string CompartimentColumn => "G";
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
    /// Detects the old format: column G has "Compartiment" header, column H does not.
    /// </summary>
    public override bool CanHandle(Excel._Worksheet worksheet) {
      string headerG = ReadHeaderCell(worksheet, 1, "G");
      string headerH = ReadHeaderCell(worksheet, 1, "H");

      if (headerG == null)
        return false;

      string headerGUpper = headerG.ToUpper();
      bool gHasCompartiment = headerGUpper.Contains("COMPARTIMENT") ||
                              headerGUpper.Contains("SERVICII") ||
                              headerGUpper.Contains("BIROU");

      // Column H should NOT contain compartiment-related headers in old format
      bool hHasCompartiment = false;
      if (headerH != null) {
        string headerHUpper = headerH.ToUpper();
        hHasCompartiment = headerHUpper.Contains("COMPARTIMENT") ||
                           headerHUpper.Contains("SERVICII") ||
                           headerHUpper.Contains("BIROU");
      }

      return gHasCompartiment && !hHasCompartiment;
    }
  }
}
