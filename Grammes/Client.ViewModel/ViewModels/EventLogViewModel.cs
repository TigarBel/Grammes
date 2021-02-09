namespace Client.ViewModel.ViewModels
{
  using System;
  using System.Collections.ObjectModel;

  using BusinessLogic.Model.EventLogModel;

  using Prism.Mvvm;

  public class EventLogViewModel : BindableBase
  {
    #region Fields

    private ObservableCollection<EventLogModel> _events;

    #endregion

    #region Properties

    public ObservableCollection<EventLogModel> Events
    {
      get => _events;
      set => SetProperty(ref _events, value);
    }

    #endregion

    #region Constructors

    public EventLogViewModel()
    {
      Events = new ObservableCollection<EventLogModel>
      /*Hard-Code*/
      {
        new EventLogModel
        {
          Event = "Hi1",
          IsSuccessfully = true,
          Time = new DateTime(2001, 10, 10, 10, 10, 10)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi3",
          IsSuccessfully = true,
          Time = new DateTime(2001, 10, 10, 10, 10, 12)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        },
        new EventLogModel
        {
          Event = "Hi2",
          IsSuccessfully = false,
          Time = new DateTime(2001, 10, 10, 10, 10, 11)
        }
      };
      /*Hard-Code*/
    }

    #endregion
  }
}
