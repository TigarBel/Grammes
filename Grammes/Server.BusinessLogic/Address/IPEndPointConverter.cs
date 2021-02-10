namespace Server.BusinessLogic.Address
{
  using System;
  using System.Net;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  public class IPEndPointConverter : JsonConverter
  {
    #region Methods

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(IPEndPoint);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      var ep = (IPEndPoint)value;
      var jo = new JObject();
      jo.Add("Address", JToken.FromObject(ep.Address, serializer));
      jo.Add("Port", ep.Port);
      jo.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JObject jo = JObject.Load(reader);
      var address = jo["Address"].ToObject<IPAddress>(serializer);
      int port = (int)jo["Port"];
      return new IPEndPoint(address, port);
    }

    #endregion
  }
}
