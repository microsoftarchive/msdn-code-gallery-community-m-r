using System;
using System.Windows.Input;

namespace InstanceFactory.MessageBoxSample.Windows.Input.Generic
{
  /// <summary>
  /// Generic implementation of <see cref="ICommand"/>.
  /// </summary>
  /// <typeparam name="T">Type of the parameter the command expects.</typeparam>
  /// <remarks>
  /// Copied from http://social.msdn.microsoft.com/Forums/en-US/f457c906-56d3-49c7-91c4-cc35a6ec5d35/icommand-and-mvvm
  /// </remarks>
  public class DelegateCommand<T> : ICommand
  {
    #region Private Properties

    /// <summary>
    /// Gets / sets the action to be executed.
    /// </summary>
    private Action<T> ExecuteAction { get; set; }

    #endregion Private Properties


    #region Public Events

#pragma warning disable 0067 // The event is never used
    /// <summary>
    /// Occurs when changes occur that affect whether the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;
#pragma warning restore 0067 // The event is never used

    #endregion Public Events


    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="DelegateCommand(T)"/> with the action to be executed.
    /// </summary>
    /// <param name="executeAction">Action to be executed.</param>
    public DelegateCommand
      (
      Action<T> executeAction
      )
    {
      ExecuteAction = executeAction;
    }

    #endregion Public Constructors


    #region Public Methods

    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    /// <returns><c>true</c> if this command can be executed; otherwise, <c>false</c>.</returns>
    public bool CanExecute
      (
      object parameter
      )
    {
      return true;
    }

    /// <summary>
    /// Invokes the method to be called.
    /// </summary>
    /// <param name="parameter">Data used by the command.</param>
    public void Execute
      (
      object parameter
      )
    {
      ExecuteAction((T)Convert.ChangeType(parameter, typeof(T)));
    }

    #endregion Public Methods
  }
}
