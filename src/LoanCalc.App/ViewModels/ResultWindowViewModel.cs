using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LoanCalc.Core.Models;
using SkiaSharp;

namespace LoanCalc.App.ViewModels;

public class ResultWindowViewModel
{
    public string TotalPayment { get; set; }
    public string Overpayment { get; set; }
    public Payment[] MonthlyPayments { get; set; }
    
    public IEnumerable<ISeries> PieSeries { get; set; }

    public ResultWindowViewModel(ResultData data)
    {
        TotalPayment = data.TotalRepayment.ToString("C");
        Overpayment = data.Overpayment.ToString("C");
        MonthlyPayments = data.MonthlyPayments;

        PieSeries = new ISeries[]
        {
            new PieSeries<decimal>
            {
                Values = new []{ data.Overpayment },
                Name = "Overpayment",
                Stroke = new SolidColorPaint(SKColors.White),
                Fill = new SolidColorPaint(new SKColor(249, 210, 86))
            },
            new PieSeries<decimal>
            {
                Values = new []{ data.TotalRepayment - data.Overpayment},
                Name = "Loan",
                Stroke = new SolidColorPaint(SKColors.White),
                Fill = new SolidColorPaint(new SKColor(153, 111, 244))
            }
        };
    }
}