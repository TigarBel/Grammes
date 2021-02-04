namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System.Windows.Controls;
  using System.Windows.Media;

  using BusinessLogic.Model.MessagesModel;

  using Prism.Mvvm;

  public class MessageViewModel : BindableBase
  {
    #region Fields

    private MessageModel _message;

    private Brush _brush;

    #endregion

    #region Properties

    public MessageModel Message
    {
      get => _message;
      set => SetProperty(ref _message, value);
    }

    public Brush BrushMessage
    {
      get => _brush;
      set => SetProperty(ref _brush, value);
    }

    #endregion

    #region Constructors

    public MessageViewModel(MessageModel message)
    {
      Message = message;
      if (Message.IsRead)
      {
        BrushMessage = Brushes.White;
      }
      else
      {
        BrushMessage = Brushes.LightGray;
      }
    }

    #endregion
  }
}
