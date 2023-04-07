using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using ReactiveUI;
using SkiaSharp;
using SmartCalc.Core.Interfaces;
using Splat;

namespace SmartCalc.Plot.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public string Expression { get; }
    public ObservableCollection<ISeries> Series { get; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    public MainWindowViewModel(string expression)
    {
        Expression = expression;
        Series = new ObservableCollection<ISeries>();
        XAxes = new Axis[]
        {
            new Axis
            {
                SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) 
                { 
                    StrokeThickness = 1, 
                    PathEffect = new DashEffect(new float[] { 3, 3 }) 
                } 
            }
        };
        YAxes = new Axis[]
        {
            new Axis
            {
                SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) 
                { 
                    StrokeThickness = 1, 
                    PathEffect = new DashEffect(new float[] { 3, 3 }) 
                } 
            }
        };

        _calcService = Locator.Current.GetService<ISmartCalcService>();
        
        this.WhenAnyValue(
                x => x.YAxisMinimum,
                x => x.YAxisMaximum,
                x => x.XAxisMinimum,
                x => x.XAxisMaximum)
            .Subscribe(_ => Repaint());
    }
    
    private void Repaint()
    {
        if (_calcService is null) return;
        
        Series.Clear();

        var points = new List<ObservablePoint>();
        double x = XAxisMinimum;
        for (; x < XAxisMaximum; x += 0.3D)
        {
            double? y = _calcService.Evaluate(Expression, x);

            if (y < YAxisMinimum || y > YAxisMaximum) y = null;
            
            points.Add(new ObservablePoint(x, y));
        }

        Series.Add(new LineSeries<ObservablePoint>
        {
            Values = points.ToArray(),
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            IsHoverable = false,
            Stroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 3 }
        });
    }

    [ObservableProperty] private int _yAxisMinimum = -5;
    [ObservableProperty] private int _yAxisMaximum = 5;
    [ObservableProperty] private int _xAxisMinimum = -5;
    [ObservableProperty] private int _xAxisMaximum = 5;
    
    private readonly ISmartCalcService? _calcService;
}