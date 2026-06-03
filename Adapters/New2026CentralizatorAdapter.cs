using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Adapter for the new (2026+) centralizator format.
  /// Column layout: E=Directia, G=Serviciul, J=NrFile, L=NrUA, M=Indicativ,
  /// N=Continut, Q=LunaInceput, R=LunaSfarsit, S=AnInceput, T=AnSfarsit,
  /// U=TermenPastrare, Z=DateExtreme, AA=Observatii
  /// </summary>
  public class New2026CentralizatorAdapter : CentralizatorAdapterBase {
    public override string AdapterId => "new2026";
    public override string DisplayName => "Nou 2026";
    public override int DetectionPriority => 15;
    public override string TerminationColumn => "N";
    public override string CentralizatorSheetName => "Centralizator";
    public override string DateWorksheetName => "Date-identificare";

    protected override string SubfondColumn => null;
    protected override string DirectiaColumn => "E";
    protected override string CompartimentColumn => "G";
    protected override string NrUAColumn => "L";
    protected override string IndicativColumn => "M";
    protected override string ContinutColumn => "N";
    protected override string DateExtremeColumn => "Z";
    protected override string NrFileColumn => "J";
    protected override string ObservatiiColumn => "AA";
    protected override string AnInceputColumn => "S";
    protected override string AnSfarsitColumn => "T";
    protected override string TermenPastrareColumn => "U";

    private string LunaInceputColumn => "Q";
    private string LunaSfarsitColumn => "R";

    public override bool CanHandle(Excel._Worksheet worksheet) {
      string headerA = ReadHeaderCell(worksheet, 1, "A");
      string headerD = ReadHeaderCell(worksheet, 1, "D");
      if (headerA == null || headerD == null)
        return false;
      return headerA.ToUpper().Contains("ZI") &&
             headerD.ToUpper().Contains("INDICATIV");
    }

    protected override CentralizatorItemData ReadRow(object[,] values, object[,] value2s, int sheetRow, int arrayRow, int minCol) {
      var data = base.ReadRow(values, value2s, sheetRow, arrayRow, minCol);
      data.DateExtreme = SynthesizeDateExtreme(values, arrayRow, minCol, sheetRow, data.AnInceput, data.AnSfarsit);
      return data;
    }

    private string SynthesizeDateExtreme(object[,] values, int arrayRow, int minCol, int sheetRow, int anInceput, int anSfarsit) {
      if (anInceput != anSfarsit)
        return $"{anInceput}-{anSfarsit}";

      string lunaInceput = ReadCellAsString(values, arrayRow, LunaInceputColumn, minCol, "LunaInceput")?.Trim();
      string lunaSfarsit = ReadCellAsString(values, arrayRow, LunaSfarsitColumn, minCol, "LunaSfarsit")?.Trim();

      if (string.IsNullOrEmpty(lunaInceput) || string.IsNullOrEmpty(lunaSfarsit) ||
          lunaInceput == "0" || lunaSfarsit == "0")
        return $"{anInceput}-{anSfarsit}";

      return lunaInceput == lunaSfarsit ? lunaInceput : $"{lunaInceput}-{lunaSfarsit}";
    }

    public override DateFirma ReadDateFirma(Excel._Worksheet dateWorksheet) {
      var dateFirma = new DateFirma();

      dateFirma.Nume = ReadLabeledCell(dateWorksheet, "FOND") ?? "";
      dateFirma.CUI = ReadLabeledCell(dateWorksheet, "CUI/CIF") ?? "";
      dateFirma.NrInmatriculare = ReadLabeledCell(dateWorksheet, "J") ?? "";
      dateFirma.CodCAEN = ReadLabeledCell(dateWorksheet, "COD CAEN") ?? "";
      dateFirma.Judet = ReadLabeledCell(dateWorksheet, "JUDET") ?? "";
      dateFirma.Localitate = ReadLabeledCell(dateWorksheet, "LOCALITATE") ?? "";

      string strada = ReadLabeledCell(dateWorksheet, "STRADA") ?? "";
      string nrStrada = ReadLabeledCell(dateWorksheet, "NR. STRADA") ?? "";
      dateFirma.Adresa = $"Str. {strada} Nr. {nrStrada}".Trim();

      if (string.IsNullOrWhiteSpace(dateFirma.Nume))
        return null;

      return dateFirma;
    }

    public override List<string> ReadDenumiriAnterioare(Excel._Worksheet dateWorksheet) {
      var result = new List<string>();
      for (int row = 1; row <= 200; row++) {
        var label = dateWorksheet.Cells[row, "A"].Value;
        if (label == null)
          continue;
        if (label.ToString().Trim().ToUpper() != "DENUMIRI ANTERIOARE")
          continue;
        var value = dateWorksheet.Cells[row, "B"].Value;
        if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
          result.Add(value.ToString().Trim());
      }
      return result;
    }

    private string ReadLabeledCell(Excel._Worksheet ws, string label) {
      for (int row = 1; row <= 200; row++) {
        var cellA = ws.Cells[row, "A"].Value;
        if (cellA == null)
          continue;
        if (cellA.ToString().Trim().ToUpper() == label.ToUpper()) {
          var cellB = ws.Cells[row, "B"].Value;
          return cellB?.ToString().Trim();
        }
      }
      return null;
    }
  }
}
