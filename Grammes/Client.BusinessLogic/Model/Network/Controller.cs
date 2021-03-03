namespace Client.BusinessLogic.Model.Network
{
  using Common.Network;

  public abstract class Controller
  {
    #region Properties

    private InterfaceType Type { get; }

    #endregion

    #region Constructors

    protected Controller(InterfaceType interfaceType)
    {
      Type = interfaceType;
    }

    #endregion
  }
}
