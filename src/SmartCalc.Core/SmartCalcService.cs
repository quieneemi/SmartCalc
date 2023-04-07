using org.matheval;
using SmartCalc.Core.Interfaces;

namespace SmartCalc.Core;

public class SmartCalcService : ISmartCalcService
{
    public double Evaluate(string expression, double xValue = 1)
        => new Expression(expression)
            .Bind("x", xValue)
            .Eval<double>();
}