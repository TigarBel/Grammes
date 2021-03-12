namespace Client.View.UserControls.EventLog
{
  using System.Windows;
  using System.Windows.Controls;

  public static class ExpanderRowHeightBehavior
  {
    #region Fields

    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
      "IsEnabled",
      typeof(bool),
      typeof(ExpanderRowHeightBehavior),
      new PropertyMetadata(false, OnIsEnabledChanged));

    public static readonly DependencyProperty TargetRowProperty = DependencyProperty.RegisterAttached(
      "TargetRow",
      typeof(RowDefinition),
      typeof(ExpanderRowHeightBehavior),
      new PropertyMetadata(null));

    public static readonly DependencyProperty TargetRowPrevHeightProperty = DependencyProperty.RegisterAttached(
      "TargetRowPrevHeight",
      typeof(GridLength),
      typeof(ExpanderRowHeightBehavior),
      new PropertyMetadata(GridLength.Auto));

    #endregion

    #region Methods

    public static bool GetIsEnabled(DependencyObject obj)
    {
      return (bool)obj.GetValue(IsEnabledProperty);
    }

    public static void SetIsEnabled(DependencyObject obj, bool value)
    {
      obj.SetValue(IsEnabledProperty, value);
    }

    public static RowDefinition GetTargetRow(DependencyObject obj)
    {
      return (RowDefinition)obj.GetValue(TargetRowProperty);
    }

    public static Grid GetGridRow(DependencyObject obj)
    {
      return (Grid)obj.GetValue(TargetRowProperty);
    }

    public static void SetTargetRow(DependencyObject obj, RowDefinition value)
    {
      obj.SetValue(TargetRowProperty, value);
    }

    public static GridLength GetTargetRowPrevHeight(DependencyObject obj)
    {
      return (GridLength)obj.GetValue(TargetRowPrevHeightProperty);
    }

    public static void SetTargetRowPrevHeight(DependencyObject obj, GridLength value)
    {
      obj.SetValue(TargetRowPrevHeightProperty, value);
    }

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Expander expander))
      {
        return;
      }

      expander.Collapsed += OnCollapsed;
      expander.Expanded += OnExpanded;
    }

    private static void OnCollapsed(object sender, RoutedEventArgs e)
    {
      if (!(sender is Expander expander))
      {
        return;
      }

      RowDefinition targetRow = GetTargetRow(expander);

      if (targetRow == null)
      {
        return;
      }

      SetTargetRowPrevHeight(expander, targetRow.Height);

      targetRow.Height = GridLength.Auto;
    }

    private static void OnExpanded(object sender, RoutedEventArgs e)
    {
      if (!(sender is Expander expander))
      {
        return;
      }

      RowDefinition targetRow = GetTargetRow(expander);

      if (targetRow == null)
      {
        return;
      }

      GridLength targetRowPrevHeight = GetTargetRowPrevHeight(expander);

      targetRow.MaxHeight = ((Grid)targetRow.Parent).ActualHeight / 2;
      targetRow.Height = targetRowPrevHeight;
    }

    #endregion
  }
}
