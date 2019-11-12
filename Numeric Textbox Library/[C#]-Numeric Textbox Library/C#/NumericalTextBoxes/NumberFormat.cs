
using System.Diagnostics;

using System;
using System.ComponentModel;
//using System.Drawing;
using Microsoft.Win32;


[System.Runtime.InteropServices.ComVisible(true)]
[Description("Select properties for textbox"), Category("NumericText")]
public enum NumberFormat
{
    UnsignedInteger = 0,
    SignedInteger = 1,
    DecimalValue = 2,
    FloatValue = 3,
}