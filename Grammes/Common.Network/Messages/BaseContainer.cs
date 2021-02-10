using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Messages
{
  using _Enum_;

  public abstract class BaseContainer<TClassContent>
  {
    #region Properties

    public TClassContent Content { get; set; }

    public EnumRequest Request { get; set; }

    #endregion Properties

    #region Constructors

    protected BaseContainer(TClassContent content, EnumRequest request)
    {
      Content = content;
      Request = request;
    }

    #endregion Constructors

    #region Methods

    public MessageContainer GetContainer()
    {
      MessageContainer container = new MessageContainer
      {
        Identifier = Request,
        Payload = this
      };

      return container;
    }

    #endregion Methods
  }
}
