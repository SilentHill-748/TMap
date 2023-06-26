using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMap.WPFCore.Components.Map;

public partial class TemperatureMapView : UserControl
{
    // Count of segments for ruler.
    private const int Segments = 100 + 1;

    public TemperatureMapView()
    {
        InitializeComponent();
    }

    //TODO: Кривое отображение линейки градусов к полоске градиента. Нужно фиксить и сохранить динамический ресайз.
    private void Control_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void AddLine(double width, int column)
    {
        var segment = new Line()
        {
            X2 = width,
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };

        Grid.SetRow(segment, column);
        ruler.Children.Add(segment);
    }
}
