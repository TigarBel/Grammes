namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public abstract class BaseContainer<TClassContent>
  {
    #region Properties

    public DispatchType Request { get; }

    public string Author { get; protected set; }

    public TClassContent Content { get; set; }

    #endregion

    #region Constructors

    protected BaseContainer(DispatchType request, TClassContent content)
    {
      Request = request;
      Content = content;
    }

    #endregion

    #region Methods

    public Container GetContainer()
    {
      var container = new Container
      {
        Identifier = Request,
        Payload = this
      };

      return container;
    }

    #endregion
  }
}
