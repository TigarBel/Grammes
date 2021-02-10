using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Messages
{
  using _Enum_;

  public class ConnectionResponse : BaseContainer<Response>
  {
    public ConnectionResponse(Response response)
      : base(response, EnumRequest.ConnectionResponse)
    {

    }
  }
}
