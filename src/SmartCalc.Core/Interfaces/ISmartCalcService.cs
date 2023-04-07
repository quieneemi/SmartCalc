namespace SmartCalc.Core.Interfaces;

public interface ISmartCalcService
{
    double Evaluate(string expression, double xValue = 1);
}