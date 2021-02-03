namespace Client.View.UserControls.UsersList
{
  using System.Windows;
  using System.Windows.Controls;

  public class ExtendedTreeView : TreeView
  {
    #region Fields

    public static readonly DependencyProperty SelectedItemExProperty = DependencyProperty.Register(
      "SelectedItemEx",
      typeof(object),
      typeof(ExtendedTreeView),
      new FrameworkPropertyMetadata(default(object))
      {
        BindsTwoWayByDefault = true 
      });

    #endregion

    #region Properties

    public object SelectedItemEx
    {
      get => GetValue(SelectedItemExProperty);
      set => SetValue(SelectedItemExProperty, value);
    }

    #endregion

    #region Methods

    protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
    {
      SelectedItemEx = e.NewValue;
    }

    #endregion
  }
}
