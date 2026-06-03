using System;
using System.Collections.Generic;
using System.Linq;
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
    public abstract string CentralizatorSheetName { get; }
    public abstract string DateWorksheetName { get; }

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

    public virtual List<CentralizatorItemData> ReadAllRows(Excel._Worksheet worksheet, int startRow, int rowCount) {
      var colIndices = new[] {
        SubfondColumn, DirectiaColumn, CompartimentColumn, NrUAColumn,
        IndicativColumn, ContinutColumn, DateExtremeColumn, NrFileColumn,
        ObservatiiColumn, AnInceputColumn, AnSfarsitColumn, TermenPastrareColumn
      }.Where(c => !string.IsNullOrEmpty(c)).Select(ColLetterToIndex).ToList();

      int minCol = colIndices.Min();
      int maxCol = colIndices.Max();

      Excel.Range range = worksheet.Range[
        worksheet.Cells[startRow, minCol],
        worksheet.Cells[startRow + rowCount - 1, maxCol]
      ];
      object[,] values = (object[,])range.Value;
      object[,] value2s = (object[,])range.Value2;

      var result = new List<CentralizatorItemData>(rowCount);
      for (int i = 0; i < rowCount; i++)
        result.Add(ReadRow(values, value2s, startRow + i, i + 1, minCol));
      return result;
    }

    protected virtual CentralizatorItemData ReadRow(object[,] values, object[,] value2s, int sheetRow, int arrayRow, int minCol) {
      var data = new CentralizatorItemData();
      data.RowNumber = sheetRow;

      data.Subfond = ReadCellAsString(values, arrayRow, SubfondColumn, minCol, "Subfond") ?? "";

      string directia = ReadCellAsString(values, arrayRow, DirectiaColumn, minCol, "Directia");
      data.Directia = directia?.ToUpper() ?? "";

      string compartiment = ReadCellAsString(values, arrayRow, CompartimentColumn, minCol, "Compartiment");
      data.Compartiment = compartiment?.ToUpper() ?? "";

      string uaValue = ReadCellAsString(values, arrayRow, NrUAColumn, minCol, "NrUA") ?? "0";
      if (string.IsNullOrWhiteSpace(uaValue))
        uaValue = "0";
      data.NrUA = uaValue;
      data.ErrorOnNrUA = data.NrUA == "0";

      data.Indicativ = ReadCellAsString(values, arrayRow, IndicativColumn, minCol, "Indicativ") ?? "";
      data.ErrorOnIndicativ = false;

      data.Continut = ReadCellAsStringRequired(values, arrayRow, ContinutColumn, minCol, sheetRow, "Continut");
      data.DateExtreme = ReadCellAsValue2String(value2s, arrayRow, DateExtremeColumn, minCol, "DateExtreme") ?? "";
      data.ErrorOnDateExtreme = false;

      data.NrFile = ReadCellAsInt(values, arrayRow, NrFileColumn, minCol, sheetRow, "NrFile");
      data.Observatii = ReadCellAsString(values, arrayRow, ObservatiiColumn, minCol, "Observatii") ?? "";

      data.AnInceput = ReadCellAsIntRequired(values, arrayRow, AnInceputColumn, minCol, sheetRow, "AnInceput");
      data.AnSfarsit = ReadCellAsIntRequired(values, arrayRow, AnSfarsitColumn, minCol, sheetRow, "AnSfarsit");

      string termenPastrare = ReadCellAsStringRequired(values, arrayRow, TermenPastrareColumn, minCol, sheetRow, "TermenPastrare");
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

    protected static int ColLetterToIndex(string col) {
      int index = 0;
      foreach (char c in col.ToUpper())
        index = index * 26 + (c - 'A' + 1);
      return index;
    }

    private object GetArrayCell(object[,] arr, int arrayRow, string col, int minCol) {
      return arr[arrayRow, ColLetterToIndex(col) - minCol + 1];
    }

    protected string ReadCellAsString(object[,] values, int arrayRow, string col, int minCol, string fieldName) {
      if (string.IsNullOrEmpty(col)) return null;
      try {
        return GetArrayCell(values, arrayRow, col, minCol)?.ToString();
      }
      catch (Exception ex) {
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din coloana {col}.", $"Citire {fieldName}", ex);
      }
    }

    protected string ReadCellAsStringRequired(object[,] values, int arrayRow, string col, int minCol, int sheetRow, string fieldName) {
      string value = ReadCellAsString(values, arrayRow, col, minCol, fieldName);
      if (value == null)
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}, coloana {col}. Valoarea lipseste.", $"Citire {fieldName}");
      return value;
    }

    protected string ReadCellAsValue2String(object[,] value2s, int arrayRow, string col, int minCol, string fieldName) {
      if (string.IsNullOrEmpty(col)) return null;
      try {
        var v = GetArrayCell(value2s, arrayRow, col, minCol);
        return v == null ? null : Convert.ToString(v);
      }
      catch (Exception ex) {
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din coloana {col}.", $"Citire {fieldName}", ex);
      }
    }

    protected string ReadCellAsValue2StringRequired(object[,] value2s, int arrayRow, string col, int minCol, int sheetRow, string fieldName) {
      string value = ReadCellAsValue2String(value2s, arrayRow, col, minCol, fieldName);
      if (value == null)
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}, coloana {col}. Valoarea lipseste.", $"Citire {fieldName}");
      return value;
    }

    protected int ReadCellAsInt(object[,] values, int arrayRow, string col, int minCol, int sheetRow, string fieldName) {
      if (string.IsNullOrEmpty(col)) return 0;
      object cellValue = null;
      try {
        cellValue = GetArrayCell(values, arrayRow, col, minCol);
        if (cellValue == null) return 0;
        return Convert.ToInt32(Convert.ToDouble(cellValue));
      }
      catch (Exception) {
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}, coloana {col}. Valoare: '{cellValue}'", $"Citire {fieldName}");
      }
    }

    protected int ReadCellAsIntRequired(object[,] values, int arrayRow, string col, int minCol, int sheetRow, string fieldName) {
      if (string.IsNullOrEmpty(col))
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}. Coloana nu este configurata.", $"Citire {fieldName}");
      object cellValue = null;
      try {
        cellValue = GetArrayCell(values, arrayRow, col, minCol);
        if (cellValue == null)
          throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}, coloana {col}. Valoarea lipseste.", $"Citire {fieldName}");
        return Convert.ToInt32(Convert.ToDouble(cellValue));
      }
      catch (DataFormatException) { throw; }
      catch (Exception) {
        throw new DataFormatException($"Eroare la citirea campului '{fieldName}' din randul {sheetRow}, coloana {col}. Valoare: '{cellValue}'", $"Citire {fieldName}");
      }
    }

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
