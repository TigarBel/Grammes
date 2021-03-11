namespace Client.BusinessLogic.Model.Network
{
  using Common.Network;

  public abstract class BaseController<TClient>
  {
    #region Fields

    protected TClient _client;

    #endregion

    #region Properties

    public InterfaceType Type { get; }

    #endregion

    #region Constructors

    protected BaseController(InterfaceType interfaceType)
    {
      Type = interfaceType;
    }

    #endregion
  }
}
