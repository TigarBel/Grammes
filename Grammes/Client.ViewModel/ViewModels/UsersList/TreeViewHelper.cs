namespace Client.ViewModel.ViewModels.UsersList
{
  using System.Collections.Generic;
  using System.Windows;
  using System.Windows.Controls;

  public class TreeViewHelper
  {
    #region Fields

    private static readonly Dictionary<DependencyObject, TreeViewSelectedItemBehavior> behaviors =
      new Dictionary<DependencyObject, TreeViewSelectedItemBehavior>();

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached(
      "SelectedItem",
      typeof(object),
      typeof(TreeViewHelper),
      new UIPropertyMetadata(null, SelectedItemChanged));

    #endregion

    #region Methods

    public static object GetSelectedItem(DependencyObject obj)
    {
      return obj.GetValue(SelectedItemProperty);
    }

    public static void SetSelectedItem(DependencyObject obj, object value)
    {
      obj.SetValue(SelectedItemProperty, value);
    }

    private static void SelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      if (!(obj is TreeView))
      {
        return;
      }

      if (!behaviors.ContainsKey(obj))
      {
        behaviors.Add(obj, new TreeViewSelectedItemBehavior(obj as TreeView));
      }

      TreeViewSelectedItemBehavior view = behaviors[obj];
      view.ChangeSelectedItem(e.NewValue);
    }

    #endregion

    #region Classes

    private class TreeViewSelectedItemBehavior
    {
      #region Fields

      private readonly TreeView view;

      #endregion

      #region Constructors

      public TreeViewSelectedItemBehavior(TreeView view)
      {
        this.view = view;
        view.SelectedItemChanged += (sender, e) => SetSelectedItem(view, e.NewValue);
      }

      #endregion

      #region Methods

      internal void ChangeSelectedItem(object p)
      {
        var item = (TreeViewItem)view.ItemContainerGenerator.ContainerFromItem(p);
        item.IsSelected = true;
      }

      #endregion
    }

    #endregion
  }
}
