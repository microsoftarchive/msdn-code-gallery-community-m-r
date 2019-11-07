using InstanceFactory.MessageBoxSample.UI.Popups;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace InstanceFactory.MessageBoxSample
{
  /// <summary>
  /// The app's main page
  /// </summary>
  public sealed partial class MainPage : Page
  {
    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="MainPage"/>.
    /// </summary>
    public MainPage()
    {
      InitializeComponent();
    }

    #endregion Public Constructors


    #region Private Methods

    /// <summary>
    /// Handles the click event of an item in the list.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="args">Event-Arguments.</param>
    private async void OnItemClickedAsync
      (
      object sender,
      ItemClickEventArgs args
      )
    {
      TextBlock item = args.ClickedItem as TextBlock;

      // A text block is not clicked => do nothing
      if (item == null)
      {
        return;
      }

      // Call the method depending on the clicked item.
      switch (item.Name)
      {
        case "ShowInformationTextBlock":
          await ShowInformationAsync();
          break;
        case "ShowWarningTextBlock":
          await ShowWarningAsync();
          break;
        case "ShowErrorTextBlock":
          await ShowErrorAsync();
          break;
        default:
          return; // Unknown: do nothing
      }
    }

    /// <summary>
    /// Shows an informational message box.
    /// </summary>
    /// <returns>Nothing.</returns>
    private async Task ShowInformationAsync()
    {
      // Create the message box.
      MessageBox messageBox = new MessageBox("This is an information.", "Showing information", MessageBoxSymbol.Information);

      // Add the commands
      messageBox.CommandList.Add(new UICommand("OK", action: null, commandId: "Confirmed"));
      messageBox.CommandList.Add(new UICommand("Abort", action: null, commandId: "Aborted"));

      // Set the index of the focus and cancel command
      messageBox.FocusCommandIndex =
      messageBox.CancelCommandIndex = 1;

      // Show the message box
      string selectedCommandId = (string)(await messageBox.ShowAsync()).Id;

      // Show the value selected
      ResultTextBlock.Text = selectedCommandId;
    }

    /// <summary>
    /// Shows a warning message box, using the <see cref="UICommand"/> action to set a value.
    /// </summary>
    /// <returns>Nothing.</returns>
    private async Task ShowWarningAsync()
    {
      // Create the message box.
      MessageBox messageBox = new MessageBox("This is a warning sample.", "Showing warning", MessageBoxSymbol.Warning);

      // Keeps the selected command
      string selectedCommand = null;

      // Add the commands
      messageBox.CommandList.Add(new UICommand("Yes", command => { selectedCommand = "Handled: Yes"; }));
      messageBox.CommandList.Add(new UICommand("No", command => { selectedCommand = "Handled: No"; }));
      messageBox.CommandList.Add(new UICommand("Cancel", command => { selectedCommand = "Handled: Cancel"; }));

      // Set the index of the focus and cancel command
      messageBox.FocusCommandIndex =
      messageBox.CancelCommandIndex = 2;

      // Show the message box, ognore the result
      await messageBox.ShowAsync();

      // Show the value selected
      ResultTextBlock.Text = selectedCommand;
    }

    /// <summary>
    /// Shows an error message box and use the label of the pressed button.
    /// </summary>
    /// <returns>Nothing.</returns>
    private async Task ShowErrorAsync()
    {
      // Create the message box.
      MessageBox messageBox = new MessageBox("This is an error sample.", "Showing error", MessageBoxSymbol.Error);

      // Add the commands
      messageBox.CommandList.Add(new UICommand("Retry"));
      messageBox.CommandList.Add(new UICommand("Continue"));
      messageBox.CommandList.Add(new UICommand("Ignore"));
      messageBox.CommandList.Add(new UICommand("Cancel"));

      // Set the index of the focus and cancel command to the last command.
      messageBox.FocusCommandIndex =
      messageBox.CancelCommandIndex = messageBox.CommandList.Count - 1;

      // Show the message box
      string pressedButton = (await messageBox.ShowAsync()).Label;

      // Show the value selected
      ResultTextBlock.Text = String.Format("Pressed button: {0}", pressedButton);
    }

    #endregion Private Methods
  }
}
