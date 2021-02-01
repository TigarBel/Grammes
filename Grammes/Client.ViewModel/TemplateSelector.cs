namespace Client.ViewModel
{
  using System.Windows;
  using System.Windows.Controls;

  public class TemplateSelector : DataTemplateSelector
  {
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      FrameworkElement element = container as FrameworkElement;

      if (element != null && item != null && item is int) {
        int currentItem = 0;

        int.TryParse(item.ToString(), out currentItem);

        if (currentItem == 0)
          return element.FindResource("ConnectView") as DataTemplate;
        if (currentItem == 1)
          return element.FindResource("UsersListView") as DataTemplate;
        if (currentItem == 2)
          return element.FindResource("MessagesView") as DataTemplate;
      }
      return null;
    }
  }
}
