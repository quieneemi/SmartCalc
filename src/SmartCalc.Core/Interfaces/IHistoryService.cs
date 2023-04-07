using SmartCalc.Core.Models;

namespace SmartCalc.Core.Interfaces;

public interface IHistoryService
{
    public IEnumerable<CalcOperation> Read();
    public Task WriteAsync(IEnumerable<CalcOperation> items);
}