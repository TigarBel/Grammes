using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.UserControls
{
  using Common;

  public class ConnectViewModel : LeafViewModel
  {

    #region Constants

    private const string LEFT_BUTTON_TEXT = "Test";

    private const string RIGHT_BUTTON_TEXT = "Connect";

    #endregion

    #region Constructors

    public ConnectViewModel() : base(LEFT_BUTTON_TEXT, RIGHT_BUTTON_TEXT)
    {
      Console.WriteLine();
      Console.WriteLine();
    }

    #endregion
  }
}
