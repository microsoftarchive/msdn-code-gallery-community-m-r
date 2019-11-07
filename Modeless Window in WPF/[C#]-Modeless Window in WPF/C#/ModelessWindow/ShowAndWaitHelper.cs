using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;

namespace ModelessWindow
{
  internal sealed class ShowAndWaitHelper
  {
    private readonly Window _window;
    private DispatcherFrame _dispatcherFrame;
    internal ShowAndWaitHelper(Window window)
    {
      if (window == null)
      {
        throw new ArgumentNullException("panel");
      }
      this._window = window;
    }
    internal void ShowAndWait()
    {
      if (this._dispatcherFrame != null)
      {
        throw new InvalidOperationException("Cannot call ShowAndWait while waiting for a previous call to ShowAndWait to return.");
      }
      this._window.Closed += new EventHandler(this.OnPanelClosed);
      _window.Show();
      this._dispatcherFrame = new DispatcherFrame();
      Dispatcher.PushFrame(this._dispatcherFrame);
    }
    private void OnPanelClosed(object source, EventArgs eventArgs)
    {
      this._window.Closed -= new EventHandler(this.OnPanelClosed);
      if (this._dispatcherFrame == null)
      {
        return;
      }
      this._window.Closed -= new EventHandler(this.OnPanelClosed);
      this._dispatcherFrame.Continue = false;
      this._dispatcherFrame = null;
    }
  }

}
