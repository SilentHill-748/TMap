using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace TMap.CustomControls;

public class SpacingItemsControl : ItemsControl
{
    public static readonly DependencyProperty SpaceProperty =
        DependencyProperty.Register("Space", typeof(Thickness), typeof(SpacingItemsControl), new PropertyMetadata(new Thickness(0, 0, 0, 5)));

    public static readonly DependencyProperty SpaceForFirstElementProperty =
        DependencyProperty.Register("SpaceForFirstElement", typeof(bool), typeof(SpacingItemsControl), new PropertyMetadata(true));

    public static readonly DependencyProperty SpaceForLastElementProperty =
        DependencyProperty.Register("SpaceForLastElement", typeof(bool), typeof(SpacingItemsControl), new PropertyMetadata(true));

    public Thickness Space
    {
        get => (Thickness)GetValue(SpaceProperty);
        set => SetValue(SpaceProperty, value);
    }

    public bool SpaceForFirstElement
    {
        get => (bool)GetValue(SpaceForFirstElementProperty);
        set => SetValue(SpaceForFirstElementProperty, value);
    }

    public bool SpaceForLastElement
    {
        get => (bool)GetValue(SpaceForLastElementProperty);
        set => SetValue(SpaceForLastElementProperty, value);
    }

    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        var spaceForFirst = new Thickness(Space.Left, 0, Space.Right, Space.Bottom);
        var spaceForLast = new Thickness(Space.Left, Space.Top, Space.Right, 0);

        for (int i = 0; i < Items.Count; i++)
        {
            var element = (FrameworkElement)ItemContainerGenerator.ContainerFromIndex(i);
            
            if (SpaceForFirstElement && i == 0)
                element.Margin = spaceForFirst;
            else if (SpaceForLastElement && i == Items.Count - 1)
                element.Margin = spaceForLast;
            else
                element.Margin = Space;
        }

        base.OnItemsChanged(e);
    }
}
