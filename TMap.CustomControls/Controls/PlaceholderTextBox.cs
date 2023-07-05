using System;
using System.Windows;
using System.Windows.Controls;

namespace TMap.CustomControls.Controls;

public class PlaceholderTextBox : TextBox
{
    public static readonly DependencyProperty PlaceholderProperty
        = DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsEmptyProperty
        = DependencyProperty.Register("IsEmpty", typeof(bool), typeof(PlaceholderTextBox), new PropertyMetadata(true));

    static PlaceholderTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
    }

    public PlaceholderTextBox()
    {
        GotFocus += PlaceholderTextBox_GotFocus;
        LostFocus += PlaceholderTextBox_LostFocus;
    }

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public bool IsEmpty
    {
        get => (bool)GetValue(IsEmptyProperty);
        private set => SetValue(IsEmptyProperty, value);
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        IsEmpty = string.IsNullOrEmpty(Text);

        base.OnTextChanged(e);
    }

    private string _previousText = string.Empty;
    private void PlaceholderTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (Text == string.Empty)
            Text = _previousText;
    }

    private void PlaceholderTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        _previousText = Text;

        Text = string.Empty;
    }
}
