using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace ModelessWindow
{
  /// <summary>
  /// Interaction logic for ModalWindow.xaml
  /// </summary>
  public partial class ModelessWindow : Window
  {
    private bool? ModalDialogResult = null;
    private IntPtr ownerHandle;
    private IntPtr handle;

    public ModelessWindow(Window owner)
    {
      InitializeComponent();
      this.Owner = owner;

      ownerHandle = (new System.Windows.Interop.WindowInteropHelper(this.Owner)).Handle;
      handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;
      NativeMethods.EnableWindow(handle, true);
      NativeMethods.SetForegroundWindow(handle);
      this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
    }

    public bool? ShowModelessDialog()
    {
      NativeMethods.EnableWindow(ownerHandle, false);
      new ShowAndWaitHelper(this).ShowAndWait();
      return ModalDialogResult;
    }

    void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.Closing -= new System.ComponentModel.CancelEventHandler(Window_Closing);
      NativeMethods.EnableWindow(handle, false);
      NativeMethods.EnableWindow(ownerHandle, true);
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      ModalDialogResult = true;
      this.Close();
    }
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      ModalDialogResult = false;
      this.Close();
    }
  }
}
