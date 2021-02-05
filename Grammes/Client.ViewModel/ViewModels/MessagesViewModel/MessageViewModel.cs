namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;

  using Prism.Mvvm;

  public class MessageViewModel : BindableBase
  {
    #region Fields

    private bool _isOutgoingMessage;
    
    private bool _isReadingMessage;

    #endregion

    #region Properties

    public string Message { get; set; }

    public string Time { get; set; }

    public bool IsOutgoingMessage
    {
      get => _isOutgoingMessage;
      set => SetProperty(ref _isOutgoingMessage, value);
    }

    public bool IsReadingMessage
    {
      get => _isReadingMessage;
      set => SetProperty(ref _isReadingMessage, value);
    }

    #endregion

    #region Constructors

    public MessageViewModel(string message, DateTime time, bool isOutgoingMessage, bool isReadingMessage)
    {
      Message = message;
      Time = time.ToString("hh:mm:ss");
      IsOutgoingMessage = isOutgoingMessage;
      IsReadingMessage = isReadingMessage;
    }

    #endregion
  }
}
