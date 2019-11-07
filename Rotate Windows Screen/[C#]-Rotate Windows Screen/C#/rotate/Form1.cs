using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace rotate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const byte VK_RETURN = 0x0D;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const byte VK_CTRL = 0x11;
        const byte VK_SNAPSHOT = 0x2C;
        const byte VK_ALT = 0x12;
        const byte VK_RIGHT = 0x27;
        const byte VK_UP = 0x26;
        const byte VK_LEFT = 0x25;
        const byte VK_DOWN = 0x28;



        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        

        private void btnDefault_Click(object sender, EventArgs e)
        {
            keybd_event(VK_CTRL, 0, 0, 0);
            keybd_event(VK_ALT, 0, 0, 0);
            keybd_event(VK_UP, 0, 0, 0);

            Thread.Sleep(100);

            keybd_event(VK_CTRL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_UP, 0, KEYEVENTF_KEYUP, 0);
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            keybd_event(VK_CTRL, 0, 0, 0);
            keybd_event(VK_ALT, 0, 0, 0);
            keybd_event(VK_RIGHT, 0, 0, 0);
            
            Thread.Sleep(100);

            keybd_event(VK_CTRL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_RIGHT, 0, KEYEVENTF_KEYUP, 0);
        }

        private void btnRotateDown_Click(object sender, EventArgs e)
        {
            keybd_event(VK_CTRL, 0, 0, 0);
            keybd_event(VK_ALT, 0, 0, 0);
            keybd_event(VK_DOWN, 0, 0, 0);

            Thread.Sleep(100);

            keybd_event(VK_CTRL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_DOWN, 0, KEYEVENTF_KEYUP, 0);
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            keybd_event(VK_CTRL, 0, 0, 0);
            keybd_event(VK_ALT, 0, 0, 0);
            keybd_event(VK_LEFT, 0, 0, 0);

            Thread.Sleep(100);

            keybd_event(VK_CTRL, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LEFT, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
