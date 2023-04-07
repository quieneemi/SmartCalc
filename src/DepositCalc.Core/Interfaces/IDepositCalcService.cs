using DepositCalc.Core.Models;

namespace DepositCalc.Core.Interfaces;

public interface IDepositCalcService
{
    ResultData Calculate(SourceData data);
}