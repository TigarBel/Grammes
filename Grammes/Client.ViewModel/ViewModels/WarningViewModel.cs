using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.ViewModels
{
  using Common;

  public class WarningViewModel : LeafViewModel
  {
    public WarningViewModel()
      : base("Accept", "Cancel")
    {
      
    }

  }
}
