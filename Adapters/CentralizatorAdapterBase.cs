using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Abstract base class for centralizator adapters with shared reading logic.
  /// </summary>
  public abstract class CentralizatorAdapterBase : ICentralizatorAdapter {
    public abstract string AdapterId { get; }
    public abstract string DisplayName { get; }
    public abstract int DetectionPriority { get; }
    public abstract string TerminationColumn { get; }

    // Column mappings - to be defined by derived classes
    protected abstract string SubfondColumn { get; }
    protected abstract string DirectiaColumn { get; }
    protected abstract string CompartimentColumn { get; }
    protected abstract string NrUAColumn { get; }
    protected abstract string IndicativColumn { get; }
    protected abstract string ContinutColumn { get; }
    protected abstract string DateExtremeColumn { get; }
    protected abstract string NrFileColumn { get; }
    protected abstract string ObservatiiColumn { get; }
    protected abstract string AnInceputColumn { get; }
    protected abstract string AnSfarsitColumn { get; }
    protected abstract string TermenPastrareColumn { get; }

    // Meta data labels
    protected virtual string DateArvutilLabel => "Date Arvutil";
    protected virtual string DenumiriAnterioareLabel => "Denumiri anterioare";

    public abstract bool CanHandle(Excel._Worksheet worksheet);

    /// <summary>
    /// Reads a single row from the centralizator using the column mappings.
    /// </summary>
    public virtual CentralizatorItemData ReadRow(Excel._Worksheet worksheet, int row) {
      var data = new CentralizatorItemData();
      data.RowNumber = row;

      // Subfond (optional)
      data.Subfond = ReadCellAsString(worksheet, row, SubfondColumn, "Subfond") ?? "";

      // Directia (optional, uppercase)
      string directia = ReadCellAsString(worksheet, row, DirectiaColumn, "Directia");
      data.Directia = directia?.ToUpper() ?? "";

      // Compartiment (uppercase)
      string compartiment = ReadCellAsString(worksheet, row, CompartimentColumn, "Compartiment");
      data.Compartiment = compartiment?.ToUpper() ?? "";

      // NrUA (defaults to "0" if empty)
      string uaValue = ReadCellAsString(worksheet, row, NrUAColumn, "NrUA") ?? "0";
      if (string.IsNullOrWhiteSpace(uaValue))
        uaValue = "0";
      data.NrUA = uaValue;
      data.ErrorOnNrUA = data.NrUA == "0";

      // Indicativ (optional)
      data.Indicativ = ReadCellAsString(worksheet, row, IndicativColumn, "Indicativ") ?? "";
      data.ErrorOnIndicativ = false;

      // Continut (required - termination detector)
      data.Continut = ReadCellAsStringRequired(worksheet, row, ContinutColumn, "Continut");

      // DateExtreme
      data.DateExtreme = ReadCellAsValue2StringRequired(worksheet, row, DateExtremeColumn, "DateExtreme");
      data.ErrorOnDateExtreme = false;

      // NrFile (integer)
      data.NrFile = ReadCellAsInt(worksheet, row, NrFileColumn, "NrFile");

      // Observatii (optional)
      data.Observatii = ReadCellAsString(worksheet, row, ObservatiiColumn, "Observatii") ?? "";

      // AnInceput (integer, required)
      data.AnInceput = ReadCellAsIntRequired(worksheet, row, AnInceputColumn, "AnInceput");

      // AnSfarsit (integer, required)
      data.AnSfarsit = ReadCellAsIntRequired(worksheet, row, AnSfarsitColumn, "AnSfarsit");

      // TermenPastrare (uppercase, trimmed)
      string termenPastrare = ReadCellAsStringRequired(worksheet, row, TermenPastrareColumn, "TermenPastrare");
      data.TermenPastrare = termenPastrare.Trim().ToUpper();

      return data;
    }

    /// <summary>
    /// Reads company data from the "Date identificare" worksheet.
    /// </summary>
    public virtual DateFirma ReadDateFirma(Excel._Worksheet dateWorksheet) {
      var dateFirma = new DateFirma();
      int? dateRow = FindRowWithLabel(dateWorksheet, "A", DateArvutilLabel, 200);

      if (!dateRow.HasValue) {
        return null;
      }

      int row = dateRow.Value;
      dateFirma.Nume = ReadCellAsStringRequired(dateWorksheet, row + 0, "B", "Nume");
      dateFirma.Judet = ReadCellAsStringRequired(dateWorksheet, row + 1, "B", "Judet");
      dateFirma.Localitate = ReadCellAsStringRequired(dateWorksheet, row + 2, "B", "Localitate");
      dateFirma.Adresa = ReadCellAsStringRequired(dateWorksheet, row + 3, "B", "Adresa");
      dateFirma.CUI = ReadCellAsStringRequired(dateWorksheet, row + 4, "B", "CUI");
      dateFirma.NrInmatriculare = ReadCellAsStringRequired(dateWorksheet, row + 5, "B", "NrInmatriculare");
      dateFirma.CodCAEN = ReadCellAsStringRequired(dateWorksheet, row + 6, "B", "CodCAEN");

      return dateFirma;
    }

    /// <summary>
    /// Reads the list of previous company names.
    /// </summary>
    public virtual List<string> ReadDenumiriAnterioare(Excel._Worksheet dateWorksheet) {
      var result = new List<string>();
      int? startRow = FindRowWithLabel(dateWorksheet, "A", DenumiriAnterioareLabel, 200);

      if (!startRow.HasValue) {
        return result;
      }

      int row = startRow.Value;
      while (true) {
        var cellValue = dateWorksheet.Cells[row, "B"].Value2;
        if (cellValue == null)
          break;
        result.Add(cellValue.ToString());
        row++;
      }

      return result;
    }

    #region Helper Methods

    /// <summary>
    /// Reads a cell value as string. Returns null if cell is empty.
    /// </summary>
    protected string ReadCellAsString(Excel._Worksheet ws, int row, string col, string fieldName) {
      if (string.IsNullOrEmpty(col))
        return null;

      try {
        var cellValue = ws.Cells[row, col].Value;
        if (cellValue == null)
          return null;
        return cellValue.ToString();
      }
      catch (Exception ex) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}.",
          $"Citire {fieldName}",
          ex);
      }
    }

    /// <summary>
    /// Reads a cell value as string. Throws if cell is empty.
    /// </summary>
    protected string ReadCellAsStringRequired(Excel._Worksheet ws, int row, string col, string fieldName) {
      string value = ReadCellAsString(ws, row, col, fieldName);
      if (value == null) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}. Valoarea lipseste.",
          $"Citire {fieldName}");
      }
      return value;
    }

    /// <summary>
    /// Reads a cell using Value2 property (for dates stored as numbers).
    /// </summary>
    protected string ReadCellAsValue2String(Excel._Worksheet ws, int row, string col, string fieldName) {
      if (string.IsNullOrEmpty(col))
        return null;

      try {
        var cellValue = ws.Cells[row, col].Value2;
        if (cellValue == null)
          return null;
        return Convert.ToString(cellValue);
      }
      catch (Exception ex) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}.",
          $"Citire {fieldName}",
          ex);
      }
    }

    /// <summary>
    /// Reads a cell using Value2 property. Throws if cell is empty.
    /// </summary>
    protected string ReadCellAsValue2StringRequired(Excel._Worksheet ws, int row, string col, string fieldName) {
      string value = ReadCellAsValue2String(ws, row, col, fieldName);
      if (value == null) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}. Valoarea lipseste.",
          $"Citire {fieldName}");
      }
      return value;
    }

    /// <summary>
    /// Reads a cell value as integer. Returns 0 if cell is empty.
    /// </summary>
    protected int ReadCellAsInt(Excel._Worksheet ws, int row, string col, string fieldName) {
      if (string.IsNullOrEmpty(col))
        return 0;

      object cellValue = null;
      try {
        cellValue = ws.Cells[row, col].Value;
        if (cellValue == null)
          return 0;
        return Convert.ToInt32(Convert.ToDouble(cellValue));
      }
      catch (Exception) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}. Valoare: '{cellValue}'",
          $"Citire {fieldName}");
      }
    }

    /// <summary>
    /// Reads a cell value as integer. Throws if cell is empty or invalid.
    /// </summary>
    protected int ReadCellAsIntRequired(Excel._Worksheet ws, int row, string col, string fieldName) {
      if (string.IsNullOrEmpty(col)) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}. Coloana nu este configurata.",
          $"Citire {fieldName}");
      }

      object cellValue = null;
      try {
        cellValue = ws.Cells[row, col].Value;
        if (cellValue == null) {
          throw new DataFormatException(
            $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}. Valoarea lipseste.",
            $"Citire {fieldName}");
        }
        return Convert.ToInt32(Convert.ToDouble(cellValue));
      }
      catch (DataFormatException) {
        throw;
      }
      catch (Exception) {
        throw new DataFormatException(
          $"Eroare la citirea campului '{fieldName}' din randul {row}, coloana {col}. Valoare: '{cellValue}'",
          $"Citire {fieldName}");
      }
    }

    /// <summary>
    /// Finds the first row where the specified column contains the given label.
    /// </summary>
    protected int? FindRowWithLabel(Excel._Worksheet ws, string col, string label, int maxRows = 200) {
      for (int row = 1; row <= maxRows; row++) {
        var cellValue = ws.Cells[row, col].Value;
        if (cellValue != null && cellValue.ToString().Trim() == label) {
          return row;
        }
      }
      return null;
    }

    /// <summary>
    /// Reads header cell value for format detection.
    /// </summary>
    protected string ReadHeaderCell(Excel._Worksheet ws, int row, string col) {
      try {
        var cellValue = ws.Cells[row, col].Value;
        if (cellValue == null)
          return null;
        return cellValue.ToString().Trim();
      }
      catch {
        return null;
      }
    }

    #endregion
  }
}
