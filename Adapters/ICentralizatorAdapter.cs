using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Interface for adapters that read different Excel centralizator formats.
  /// </summary>
  public interface ICentralizatorAdapter {
    /// <summary>
    /// Unique identifier for this adapter (e.g., "default", "old-format").
    /// </summary>
    string AdapterId { get; }

    /// <summary>
    /// Human-readable name shown in UI (e.g., "Format Standard 2024").
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Priority for auto-detection. Higher values are checked first.
    /// </summary>
    int DetectionPriority { get; }

    /// <summary>
    /// Column letter used to detect row termination (when null = end of data).
    /// </summary>
    string TerminationColumn { get; }

    /// <summary>
    /// Checks if this adapter can handle the given worksheet format.
    /// </summary>
    /// <param name="worksheet">The worksheet to check.</param>
    /// <returns>True if this adapter can read the worksheet format.</returns>
    bool CanHandle(Excel._Worksheet worksheet);

    /// <summary>
    /// Reads a single row of data from the centralizator worksheet.
    /// </summary>
    /// <param name="worksheet">The worksheet to read from.</param>
    /// <param name="row">The row number (1-based).</param>
    /// <returns>The parsed row data.</returns>
    CentralizatorItemData ReadRow(Excel._Worksheet worksheet, int row);

    /// <summary>
    /// Reads company data from the "Date identificare" worksheet.
    /// </summary>
    /// <param name="dateWorksheet">The "Date identificare" worksheet.</param>
    /// <returns>The company data.</returns>
    DateFirma ReadDateFirma(Excel._Worksheet dateWorksheet);

    /// <summary>
    /// Reads the list of previous company names.
    /// </summary>
    /// <param name="dateWorksheet">The "Date identificare" worksheet.</param>
    /// <returns>List of previous names.</returns>
    List<string> ReadDenumiriAnterioare(Excel._Worksheet dateWorksheet);
  }
}
