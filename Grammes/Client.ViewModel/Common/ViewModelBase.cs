namespace Client.ViewModel.Common
{
  using System;
  using System.Collections;
  using System.ComponentModel;

  using Prism.Mvvm;

  public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
  {
    #region Fields

    /// <summary>
    /// Manages validation errors for an object, notifying when the error state changes.
    /// </summary>
    protected readonly ErrorsContainer<string> _errorsContainer;

    #endregion

    #region Properties

    /// <summary>
    /// Gets a value that indicates whether the entity has validation errors.
    /// </summary>
    public bool HasErrors => _errorsContainer.HasErrors;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the validation errors have changed for a property or for the entire entity.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    #endregion

    #region Constructors

    protected ViewModelBase()
    {
      _errorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the validation errors for a specified property.
    /// </summary>
    public IEnumerable GetErrors(string propertyName)
    {
      return _errorsContainer.GetErrors(propertyName);
    }

    /// <summary>
    /// Validation rules should be in this method
    /// </summary>
    public abstract void Check();

    //public override void Check()
    //{
    //  _errorsContainer.ClearErrors(() => Name);

    //  if (!IsNameUnique()) {
    //    _errorsContainer.SetErrors(() => Name, new[] { Resources.NameMustBeUnique });
    //  }

    //  if (Name.Length > UserSettings.USERNAME_MAX_LENGTH) {
    //    _errorsContainer.SetErrors(() => Name, new[] { string.Format(Resources.NameMustBeShorterThan, UserSettings.USERNAME_MAX_LENGTH) });
    //  }

    //  if (!new Regex(UserSettings.USERNAME_MASK, RegexOptions.IgnoreCase).IsMatch(Name)) {
    //    _errorsContainer.SetErrors(
    //      () => Name,
    //      new[] { string.Format(Resources.UserNameUnacceptableSymbols, UserSettings.USERNAME_UNACCEPTABLE_SYMBOLS) });
    //  }
    //}

    /// <summary>
    /// The action that invoked if when errors are added for an object.
    /// </summary>
    /// <param name = "propertyName">The name of the property for which verification errors are retrieved</param>
    protected void RaiseErrorsChanged(string propertyName)
    {
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    #endregion
  }
}
