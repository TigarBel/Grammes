namespace Client.BusinessLogic.Model.UsersListModel
{
  using System;
  using System.Collections.Generic;

  public class UsersListModel
  {
    #region Fields
    
    private List<string> _onlineList;

    private List<string> _offlineList;

    private List<string> _groupList;

    #endregion

    #region Properties

    public List<string> OnlineList
    {
      get => _onlineList;
      set => SetPropertyOtherNull(ref _onlineList, value, "Online");
    }

    public List<string> OfflineList
    {
      get => _offlineList;
      set => SetPropertyOtherNull(ref _offlineList, value, "Offline");
    }

    public List<string> GroupList
    {
      get => _groupList;
      set => SetPropertyOtherNull(ref _groupList, value, "Group");
    }

    #endregion

    #region Constructors

    public UsersListModel()
    {
      OnlineList = new List<string>();
      OfflineList = new List<string>();
      GroupList = new List<string>();
    }

    #endregion

    #region Methods

    private void SetPropertyOtherNull<TProperty>(ref TProperty property, TProperty value, string headerException)
    {
      if (value != null)
      {
        property = value;
      }
      else
      {
        throw new ArgumentNullException($"{headerException} list users don't might be null!");
      }
    }

    #endregion
  }
}
