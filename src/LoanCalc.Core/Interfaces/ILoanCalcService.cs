using LoanCalc.Core.Models;

namespace LoanCalc.Core.Interfaces;

public interface ILoanCalcService
{
    public ResultData Calculate(SourceData data);
}