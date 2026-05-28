using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace ArhivUtility.Adapters {
  /// <summary>
  /// Factory for creating and auto-detecting centralizator adapters.
  /// </summary>
  public static class CentralizatorAdapterFactory {
    private static readonly List<ICentralizatorAdapter> _adapters;

    static CentralizatorAdapterFactory() {
      _adapters = new List<ICentralizatorAdapter> {
        new DefaultCentralizatorAdapter(),
        new OldFormatCentralizatorAdapter()
      };
    }

    /// <summary>
    /// Gets all registered adapters.
    /// </summary>
    public static IReadOnlyList<ICentralizatorAdapter> Adapters => _adapters.AsReadOnly();

    /// <summary>
    /// Auto-detects the correct adapter for the given worksheet.
    /// Iterates through adapters by priority (highest first) and returns the first matching one.
    /// </summary>
    /// <param name="worksheet">The worksheet to analyze.</param>
    /// <returns>The detected adapter.</returns>
    /// <exception cref="DataFormatException">Thrown when no adapter can handle the worksheet format.</exception>
    public static ICentralizatorAdapter DetectAdapter(Excel._Worksheet worksheet) {
      var sortedAdapters = _adapters.OrderByDescending(a => a.DetectionPriority);

      foreach (var adapter in sortedAdapters) {
        try {
          if (adapter.CanHandle(worksheet)) {
            return adapter;
          }
        }
        catch {
          // Ignore detection errors and try next adapter
        }
      }

      throw new DataFormatException(
        "Nu s-a putut detecta formatul centralizatorului. " +
        "Asigurati-va ca fisierul contine antetul corect in primul rand.",
        "Detectare format centralizator");
    }

    /// <summary>
    /// Gets a specific adapter by its ID.
    /// </summary>
    /// <param name="adapterId">The adapter ID (e.g., "default", "old-format").</param>
    /// <returns>The adapter with the specified ID.</returns>
    /// <exception cref="ArgumentException">Thrown when no adapter with the given ID exists.</exception>
    public static ICentralizatorAdapter GetAdapter(string adapterId) {
      var adapter = _adapters.FirstOrDefault(a => a.AdapterId == adapterId);
      if (adapter == null) {
        throw new ArgumentException($"Nu exista un adaptor cu ID-ul '{adapterId}'.", nameof(adapterId));
      }
      return adapter;
    }

    /// <summary>
    /// Registers a custom adapter. Useful for plugins or testing.
    /// </summary>
    /// <param name="adapter">The adapter to register.</param>
    public static void RegisterAdapter(ICentralizatorAdapter adapter) {
      if (adapter == null)
        throw new ArgumentNullException(nameof(adapter));

      // Remove existing adapter with same ID
      _adapters.RemoveAll(a => a.AdapterId == adapter.AdapterId);
      _adapters.Add(adapter);
    }
  }
}
