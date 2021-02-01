using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.UserControls.Common
{
  using Prism.Commands;
  using Prism.Mvvm;

  public class LeafViewModel : BindableBase
  {
    #region Constants

    #endregion

    #region Fields

    private string _leftButtonText;

    private string _rightButtonText;

    #endregion

    #region Properties

    public string LeftButtonText
    {
      get => _leftButtonText;
      set => SetProperty(ref _leftButtonText, value);
    }

    public string RightButtonText
    {
      get => _rightButtonText;
      set => SetProperty(ref _rightButtonText, value);
    }

    public DelegateCommand LeftSendCommand { get; }

    public DelegateCommand RightSendCommand { get; }

    #endregion

    #region Constructors

    public LeafViewModel()
    {
      LeftButtonText = "<-Left";
      RightButtonText = "Right->";
      LeftSendCommand = new DelegateCommand(ExecuteSendCommand);
      RightSendCommand = new DelegateCommand(ExecuteSendCommand);
    }

    #endregion

    #region Methods

    private void ExecuteSendCommand()
    {

    }

    #endregion

  }
}
