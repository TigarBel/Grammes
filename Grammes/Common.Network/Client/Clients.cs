namespace Common.Network.Client
{
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public class Clients<TGuid, TWsConnection> : ConcurrentDictionary<TGuid, TWsConnection>
    where TWsConnection : WsConnection
  {
    #region Fields

    private readonly int _timeLife;

    private readonly List<int> _counterTime;

    #endregion

    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeLife">Seconds</param>
    public Clients(int timeLife)
    {
      _timeLife = timeLife;
      _counterTime = new List<int>();
    }

    #endregion

    #region Methods

    public void RefreshLifeClient(TGuid guid)
    {
      int index = Keys.ToList().IndexOf(guid);
      _counterTime[index] = _timeLife;
    }

    public new bool TryAdd(TGuid guid, TWsConnection connection)
    {
      if (!base.TryAdd(guid, connection))
      {
        return false;
      }

      _counterTime.Add(_timeLife);
      LifeStartAsync(guid, connection);
      return true;
    }

    private async void LifeStartAsync(TGuid guid, TWsConnection connection)
    {
      while (_counterTime[Keys.ToList().IndexOf(guid)] != 0)
      {
        int ind = Keys.ToList().IndexOf(guid);
        _counterTime[Keys.ToList().IndexOf(guid)]--;
        await Task.Delay(1000);
      }

      _counterTime.RemoveAt(_counterTime[Keys.ToList().IndexOf(guid)]);
      connection.Close(); //WsConnection
    }

    #endregion
  }
}
