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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace ModelessWindow
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void ShowNormal_Click(object sender, RoutedEventArgs e)
    {
      NormalWindow win = new NormalWindow();
      PrintMessage("Show a New Window.");
      win.Show();
    }

    private void ShowNormalModal_Click(object sender, RoutedEventArgs e)
    {
      NormalWindow win = new NormalWindow();
      PrintMessage("Show Modal Dialog Window.");
      win.ShowDialog();
    }

    private void ShowModeless_Click(object sender, RoutedEventArgs e)
    {
      bool? ModalDialogResult = null;

      ModelessWindow modalwin = new ModelessWindow(this);
      PrintMessage("Show Modal Less Dialog Window.");
      ModalDialogResult = modalwin.ShowModelessDialog();
      PrintMessage(string.Format("The return value : {0}", ModalDialogResult));
    }

    private void PrintMessage(string msg)
    {
      message.Text = DateTime.Now + " -  " + msg + "\n" + message.Text;
    }
  }
}
