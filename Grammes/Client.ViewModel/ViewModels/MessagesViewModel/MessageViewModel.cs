namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;

  using BusinessLogic.Model;

  using Prism.Mvvm;

  public class MessageViewModel : BindableBase
  {
    #region Fields

    private MessageModel _model;

    #endregion

    #region Properties
    
    public MessageModel Model
    {
      get => _model;
      set => SetProperty(ref _model, value);
    }

    #endregion

    #region Constructors

    public MessageViewModel(string message, DateTime time, bool isOutgoingMessage, bool isReadingMessage)
    {
      _model = new MessageModel(message, time, isOutgoingMessage, isReadingMessage);
    }

    public MessageViewModel(MessageModel model)
    {
      _model = model;
    }

    #endregion
  }
}
