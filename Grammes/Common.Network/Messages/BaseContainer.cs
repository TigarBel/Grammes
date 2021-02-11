namespace Common.Network.Messages
{
  public abstract class BaseContainer<TClassContent>
  {
    #region Properties

    public TClassContent Content { get; set; }

    public DispatchType Request { get; set; }

    #endregion

    #region Constructors

    protected BaseContainer(TClassContent content, DispatchType request)
    {
      Content = content;
      Request = request;
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
