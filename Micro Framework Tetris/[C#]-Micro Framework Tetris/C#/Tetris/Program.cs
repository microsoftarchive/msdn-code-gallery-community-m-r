using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace Tetris
{
    public class Program : Microsoft.SPOT.Application
    {
        private Presentation.MainWindow mainWindow;

        public static Program program;

        public static void Main()
        {
            program = new Program();

            Microsoft.SPOT.Touch.Touch.Initialize(program);

            Window mainWindow = program.CreateWindow();

            program.Run(mainWindow);
        }

        public Window CreateWindow()
        {
            mainWindow = new Presentation.MainWindow();
            mainWindow.Height = SystemMetrics.ScreenHeight;
            mainWindow.Width = SystemMetrics.ScreenWidth;
            mainWindow.Visibility = Visibility.Visible;
            Buttons.Focus(mainWindow);

            return mainWindow;
        }
    }
}
