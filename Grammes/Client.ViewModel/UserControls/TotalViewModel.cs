namespace Client.ViewModel.UserControls
{
  using System.Windows.Controls;

  using Prism.Commands;
  using Prism.Mvvm;

  public class TotalViewModel : BindableBase
  {

    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;
    
    private DelegateCommand _checkBoxCommand;

    #endregion

    #region Propertyes

    public int ContentPresenter
    {
      get => _contentPresenter;
      set => SetProperty(ref _contentPresenter, value);
    }

    public string[] NameViews
    {
      get => _nameViews; 
      set => SetProperty(ref _nameViews, value);
    }
    
    public DelegateCommand CheckBoxCommand
    {
      get => _checkBoxCommand;
      set => SetProperty(ref _checkBoxCommand, value);
    }

    #endregion

    #region Constructors

    public TotalViewModel()
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      CheckBoxCommand = new DelegateCommand(OnChange);
    }

    #endregion

    #region Methods

    private void OnChange()
    {
      if (ContentPresenter == NameViews.Length - 1)
      {
        ContentPresenter = 0;
      }
      else
      {
        ContentPresenter++;
      }
    }

    #endregion

  }
}
