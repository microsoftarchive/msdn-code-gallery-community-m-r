using InstanceFactory.MessageBoxSample.Windows.Input.Generic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace InstanceFactory.MessageBoxSample.UI.Popups
{
  /// <summary>
  /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
  /// </summary>
  /// <typeparam name="TResult">The type of the result returned when the messagebox gets closed.</typeparam>
  public sealed partial class MessageBox : UserControl
  {
    #region Private Properties

    /// <summary>
    /// Gets / sets the list of the wait handles, used to show the message box async.
    /// </summary>
    /// <remarks>
    /// The sequence of the handles matches to the sequence of the commands in the <see cref="CommandList"/>.
    /// </remarks>
    private List<ManualResetEvent> WaitHandleList { get; set; }

    #endregion Private Properties


    #region Public Properties

    /// <summary>
    /// Gets / sets the title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets / sets the message.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Gets / sets the symbol.
    /// </summary>
    public string Symbol { get; private set; }

    /// <summary>
    /// Gets / sets the list of the commands that appear in the command bar of the message box. These commands makes the dialog actionable.
    /// </summary>
    public List<IUICommand> CommandList { get; private set; }

    /// <summary>
    /// Gets / sets the index of the command you want to use as the cancel command. This is the command that fires when users press the ESC key.
    /// </summary>
    /// <remarks>
    /// Any value less then 0 indicates the escape key does not close the popup.
    /// </remarks>
    public int CancelCommandIndex { get; set; }

    /// <summary>
    /// Gets / sets the index of the command you set the focus on when the messagebox opens.
    /// </summary>
    /// <remarks>
    /// Any value less then 0 indicates the enter key does not close the popup.
    /// </remarks>
    public int FocusCommandIndex { get; set; }

    #endregion Public Properties


    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="MessageBox"/> with a message, title and symbol.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="title">The title to be displayed.</param>
    /// <param name="symbol">The symbol to be displayed.</param>
    public MessageBox
      (
      string message,
      string title = null,
      MessageBoxSymbol symbol = MessageBoxSymbol.None
      )
    {
      // Initialize the componen.
      InitializeComponent();

      // Set the properties
      Message = message;
      Title = title;

      // Convert the symbol passed to the symbol character to be displayed
      switch (symbol)
      {
        case MessageBoxSymbol.Error:
          Symbol = new String((char) 0x26D4, 1);
          break;
        case MessageBoxSymbol.Information:
          Symbol = new String((char) 0x24D8, 1);
          break;
        case MessageBoxSymbol.Warning:
          Symbol = new String((char) 0x26A0, 1);
          break;
        default:
          break;
      }

      // Intialize the command list
      CommandList = new List<IUICommand>();

      // Initializes the list of the wait handles
      WaitHandleList = new List<ManualResetEvent>();

      // Any value less then 0 indicates the escape key does not close the popup.
      CancelCommandIndex = -1;

      // Set self as data context
      DataContext = this;

      // Handle right tap / mouse clicks to make sure the app bar will not open while the message box is open
      RightTapped += OnRightTapped;
    }

    #endregion Public Constructors


    #region Private Methods

    /// <summary>
    /// Invoked when the user right taps / right clicks.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="args">Event arguments.</param>
    private void OnRightTapped
      (
      object sender, 
      RightTappedRoutedEventArgs args
      )
    {
      // Just set the handled flag to make sure an app bar will not open.
      args.Handled = true;
    }

    /// <summary>
    /// Adds the commands to the popup and creates the machting wait handles.
    /// </summary>
    private void AddCommandsToPopup()
    {
      // In case no commmand is set, add a continue command without an action.
      if (CommandList.Count == 0)
      {
        CommandList.Add(new UICommand("Continue"));
      }

      // Remove exiting buttons in case ShowAsync was called more than once.
      ControlStackPanel.Children.Clear();

      // Create the command delegate used when a button is pressed
      DelegateCommand<ManualResetEvent> commandDelegate = new DelegateCommand<ManualResetEvent>(OnButtonPressed);

      // Iterate over the list of commands.
      foreach (IUICommand command in CommandList)
      {
        // Create a new wait handle for the command
        ManualResetEvent waitHandle = new ManualResetEvent(false);

        // Add it to the list.
        WaitHandleList.Add(waitHandle);

        // Create the button and attach the wait handle.
        Button button = new Button()
        {
          Command = commandDelegate,
          CommandParameter = waitHandle,
          Content = command.Label,
          Style = (Style)Application.Current.Resources["ButtonStyle"]
        };
        
        // Add the button to the popup
        ControlStackPanel.Children.Add(button);
      }
    }

    /// <summary>
    /// Invoked when a button was pressed.
    /// </summary>
    /// <param name="waitHandle">The wait handle assiciated with the button.</param>
    private void OnButtonPressed
      (
      ManualResetEvent waitHandle
      )
    {
      // Just signal the wait handle.
      waitHandle.Set();
    }

    #endregion Private Methods


    #region Protected Methods

    /// <summary>
    /// Invoked when the user presses a key.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    protected override void OnKeyDown
      (
      KeyRoutedEventArgs args
      )
    {
      switch (args.Key)
      {
        case VirtualKey.Escape:
          if (CancelCommandIndex >= 0
            && CancelCommandIndex < WaitHandleList.Count)
          {
            // Signal the wait handle assiciated to the cancel command.
            WaitHandleList[CancelCommandIndex].Set();
            // The key stroke is handled
            args.Handled = true;
            // get out
            return;
          }
          break;

        case VirtualKey.Left:
          {
            // Set the focus to the previous button when cursor left is pressed
            args.Handled = true;
            // Find the button currently having the focus
            int index = ControlStackPanel.Children.IndexOf(FocusManager.GetFocusedElement() as UIElement);
            // Check if we need to wrap
            index = index == 0 || index == -1 ? ControlStackPanel.Children.Count - 1 : --index;
            // Set the focus
            ((Control)ControlStackPanel.Children[index]).Focus(FocusState.Programmatic);
            return;
          }

        case VirtualKey.Right:
          {
            // Set the focus to the next button when cursor right is pressed
            args.Handled = true;
            // Find the button currently having the focus
            int index = ControlStackPanel.Children.IndexOf(FocusManager.GetFocusedElement() as UIElement);
            // Check if we need to wrap
            index = index == ControlStackPanel.Children.Count - 1 || index == -1 ? 0 : ++index;
            // Set the focus
            ((Control)ControlStackPanel.Children[index]).Focus(FocusState.Programmatic);
            return;
          }

        default:
          break;
      }

      // Let the base class handle the event
      base.OnKeyDown(args);
    }

    #endregion Protected Methods


    #region Public Methods

    /// <summary>
    /// Begins an asynchronous operation showing a dialog.
    /// </summary>
    /// <returns>Nothing.</returns>
    public async Task<IUICommand> ShowAsync()
    {
      // Add all commands to the command bar and create the matching wait handles.
      AddCommandsToPopup();

      // Keeps the index of the signaled wait handle.
      int signaledHandle = -1;

      // Find the control currently having the focus
      Control focusedControl = FocusManager.GetFocusedElement() as Control;

      // Start the thread to wait for user input.
      Task waitForUserInputTask = Task.Run(() =>
      {
        // Wait for a handle to be signaled.
        signaledHandle = ManualResetEvent.WaitAny(WaitHandleList.ToArray());
      });

      // Open the message box with a popup.
      Popup popup = PopupHelper.CreateFullScreenWithChild(this);

      // Set the focus on the defined button
      if (FocusCommandIndex >= 0
        && FocusCommandIndex < CommandList.Count)
      {
        ((Button)(ControlStackPanel.Children[FocusCommandIndex])).Focus(FocusState.Programmatic);
      }

      // Wait for the wait thread to finish (one of the events to be signaled)
      await Task.WhenAny(new Task[] { waitForUserInputTask });

      // Free all wait handles.
      while (WaitHandleList.Count > 0)
      {
        WaitHandleList[0].Dispose();
        WaitHandleList.RemoveAt(0);
      }

      try
      {
        // Invoke the event handler of the selected command in case it is defined.
        if (CommandList[signaledHandle].Invoked != null)
        {
          CommandList[signaledHandle].Invoked.Invoke(CommandList[signaledHandle]);
        }
      }
      catch (Exception)
      {
        // re-throw any exception caused by the event handler.
        throw;
      }
      finally
      {
        // Make sure the popup will be closed.
        popup.IsOpen = false;

        // Release the message box from the popup
        popup.Child = null;

        // Reset the focus
        if (focusedControl != null)
        {
          focusedControl.Focus(FocusState.Programmatic);
        }
      }

      // Return the selected command
      return (CommandList[signaledHandle]);
    }

    #endregion Public Methods
  }
}
