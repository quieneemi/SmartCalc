using System.Text.Json;
using SmartCalc.Core.Interfaces;
using SmartCalc.Core.Models;

namespace SmartCalc.Core;

public class HistoryService : IHistoryService
{
    public IEnumerable<CalcOperation> Read()
    {
        var jsonText = File.ReadAllText("history.json");
        var items = JsonDocument.Parse(jsonText)
            .Deserialize<CalcOperation[]>();
        return items ?? Array.Empty<CalcOperation>();
    }

    public Task WriteAsync(IEnumerable<CalcOperation> items)
        => File.WriteAllTextAsync("history.json", JsonSerializer.Serialize(items));
}