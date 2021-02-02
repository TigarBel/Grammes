namespace Client.ViewModel
{
  using System;
  using System.Windows;
  using System.Windows.Controls;

  public class TemplateSelectorViewModel : DataTemplateSelector
  {
    #region Fields

    public readonly string[] Views = { "ConnectView", "MainView", "MessagesView" };

    #endregion

    #region Constructors

    public TemplateSelectorViewModel()
    {
      Console.WriteLine();
    }

    #endregion

    #region Methods

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      var element = container as FrameworkElement;

      if (element != null && item != null && item is int)
      {
        int currentItem = 0;

        int.TryParse(item.ToString(), out currentItem);

        if (currentItem < 0 || currentItem > Views.Length - 1)
        {
          throw new ArgumentException("Select view not found!");
        }

        return element.FindResource(Views[currentItem]) as DataTemplate;
      }

      return null;
    }

    #endregion
  }
}
