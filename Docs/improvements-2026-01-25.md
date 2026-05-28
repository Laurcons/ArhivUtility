# Improvements To Do, as of 2026-01-25

## Code Review Report: ArhivUtility

**Project**: C# Windows Forms utility for processing archival Excel data (.NET Framework 4.7.2)

---

## Performance Issues

| Issue | Location | Impact | Resolution |
|-------|----------|--------|------------|
| **Static Excel COM object** | `Work.cs` - static `_application` field | Single-threaded only; cannot process multiple files concurrently | Won't Fix: Multi threading not necessary |
| **No COM object disposal** | `Work.cs` - workbook operations | Memory leaks; orphaned Excel.exe processes | Nice to Have: Memory leaks and orphaned processes are bad UX but there are built-in ways to deal with them |
| **Full in-memory loading** | `DoReadWork()` | Large files consume excessive memory; no streaming | Won't Fix: No need identified for lowering memory usage |
| **Synchronous blocking** | `UpdateController.cs` - `StartSync()` | Blocks app startup while checking for updates | Won't Fix |
| **Legacy threading** | `BackgroundWorker` usage | Outdated pattern; `async/await` would be more efficient | Nice to Have: `async/await` is a lot more idiomatic but the code is not that asynchronous anyway. The current BackgroundWorker methods only exist to avoid blocking the UI thread |
| **Inefficient string parsing** | `ConvertUAToInt()` in `Data.cs` | Character-by-character iteration instead of regex | Won't Fix: it does the job just fine as it is now |

---

## Maintainability Issues

| Issue | Location | Recommendation | Resolution |
|-------|----------|----------------|------------|
| **Static class abuse** | `Work.cs` entirely static | Convert to instance class with DI for testability | Nice to Have: it would be great to get rid of static classes, however testing is not a priority at all |
| **Magic strings** | Column letters ("A", "B", "L") and sheet names hardcoded throughout `Work.cs` | Use constants or pull from Configuration | Important: There should be some sort of Adapter class between the column letters and the final Centralizator objects. Multiple Adapters might be necessary for different layouts |
| **Unused configuration** | `Configuration.cs` defines column mappings but code uses hardcoded values | Wire up the existing config system | Won't Fix: Adapter classes should work better |
| **Deep nesting** | `DoCheckWork()`, `DoWriteWork()` - 4-5 levels | Extract methods; use early returns | Nice to Have |
| **Boolean parameter lists** | `DoCheckWork(bool, bool, bool)`, `DoWriteWork(...)` | Use options objects or builder pattern | Nice to Have |
| **No unit tests** | No test project exists | Static state makes testing impossible | Won't Fix: No unit testing needed |
| **No code comments** | Entire codebase | Add XML docs for public methods | Nice to Have: this is a one-man project, documentation is a lower priority |
| **Tight UI coupling** | `Form1.cs` directly calls static `Work` methods | Introduce service interfaces | TBA |

---

## Key Recommendations

### High Priority

1. Convert `Work` class to instance-based with proper COM object lifecycle (`IDisposable`)
2. Replace hardcoded column/sheet names with `Configuration` values
3. Add `Marshal.ReleaseComObject()` calls to prevent Excel process leaks

### Medium Priority

4. Replace `BackgroundWorker` with `async/await` and `IProgress<T>`
5. Extract deeply nested loops into smaller methods
6. Add telemetry consent mechanism (`TrackingService.cs` sends data silently)

### Low Priority

7. Upgrade from .NET Framework 4.7.2 to .NET 6+ for modern features
8. Use a library like ClosedXML/EPPlus instead of COM interop (removes Excel dependency)
9. Add logging framework for debugging

---

## Quick Wins

- Define `const` fields for column letters at the top of `Work.cs`
- Add `using` statements or `try/finally` blocks around workbook operations
- Replace `DataFormatException` catch-all blocks with specific exception handling
- Change `Form_Resize` to use `TableLayoutPanel` or `FlowLayoutPanel` instead of manual positioning
