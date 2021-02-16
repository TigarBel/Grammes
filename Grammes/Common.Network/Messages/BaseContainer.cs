namespace Common.Network.Messages
{
  using System;

  public abstract class BaseContainer<TClassContent>
  {
    #region Properties

    public DispatchType Request { get; private set; }

    protected string _author;

    public DateTime TimePoint { get; private set; }

    public TClassContent Content { get; set; }
    #endregion

    #region Constructors

    protected BaseContainer(DispatchType request, DateTime timePoint, TClassContent content)
    {
      Request = request;
      TimePoint = timePoint;
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
