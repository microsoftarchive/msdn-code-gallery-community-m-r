using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

/// <summary>
/// Numberic only TextBox
/// </summary>
public class NumericTextBox : TextBox
{
    private bool editing;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_PASTE = 0x0302;
    private string format = "F2";

    public NumericTextBox()
    {
        TextAlign = HorizontalAlignment.Right;
    }

    [Category("Data"), DefaultValue(0), Description("Returns integer value from text property")]
    public int Value
    {
        get
        {
            if (!string.IsNullOrEmpty(Text))
            {
                int converted;

                if (int.TryParse(Text, out converted))
                    return converted;
            }
            return 0;
        }
    }
    [DefaultValue(HorizontalAlignment.Right),Description("Set horizontal alignment")]
    public new HorizontalAlignment TextAlign
    {
        get { return base.TextAlign; }
        set { base.TextAlign = value; }
    }
    [Category("Appearance"), DefaultValue("F2")]
    public string Format
    {
        get { return format; }
        set { format = value; }
    }

    public override bool PreProcessMessage(ref Message msg)
    {
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();

        if (msg.Msg == WM_KEYDOWN)
        {
            Keys keys = (Keys)msg.WParam.ToInt32();

            bool numbers = ((keys >= Keys.D0 && keys <= Keys.D9)
                || (keys >= Keys.NumPad0 && keys <= Keys.NumPad9)) && ModifierKeys != Keys.Shift;

            bool ctrl = keys == Keys.Control;
            bool isTab = keys == Keys.Tab;

            bool ctrlZ = keys == Keys.Z && ModifierKeys == Keys.Control,
                 ctrlY = keys == Keys.Y && ModifierKeys == Keys.Control,
                 ctrlX = keys == Keys.X && ModifierKeys == Keys.Control,
                 ctrlC = keys == Keys.C && ModifierKeys == Keys.Control,
                 ctrlV = keys == Keys.V && ModifierKeys == Keys.Control,
                 del = keys == Keys.Delete | keys == Keys.Back,
                 arrows = keys == Keys.Up | keys == Keys.Down | keys == Keys.Left | keys == Keys.Right,
                 minus = keys == Keys.OemMinus,
                 dot = keys == Keys.OemPeriod;

            if (numbers | ctrl | del | arrows | ctrlX | ctrlZ | ctrlY | minus | dot)
                return false;

            else if (ctrlC)
            {
                var hasText = !string.IsNullOrEmpty(SelectedText);

                if (hasText)
                    Clipboard.SetData(DataFormats.Text, SelectedText);

                return !hasText;
            }
            else if (ctrlV)
            {
                var obj = Clipboard.GetDataObject();
                var input = (string)obj.GetData(typeof(string));
                double converted;
                var isConverted = double.TryParse(input, out converted);

                if (isConverted)
                    Text = input;

                return !isConverted;
            }
            else if (isTab || keys == Keys.Return || keys == Keys.Escape)
                return base.PreProcessMessage(ref msg);
            else
                return true;
        }
        else
            return base.PreProcessMessage(ref msg);
    }

    /// <summary>We use the editing flag to prevent TextChanged
    /// events while programmatically setting it.</summary>
    public void BeginUpdate()
    {
        editing = true;
    }

    public void EndUpdate()
    {
        editing = false;
    }

    /// <summary>Allows the value (and the Text property) to be set
    /// without raising the underlying TextChanged event.</summary>
    public void SetValue(double newValue)
    {
        BeginUpdate();
        try
        {
            Text = newValue.ToString(format);
        }
        finally { EndUpdate(); }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        /* Changed PreProcessMessage to allow minus keypresses.
         * We need to make sure any minus characters are the first. */
        var index = -1;

        if (Text.Contains("-"))
        {
            string newText = Text;
            while ((index = newText.LastIndexOf('-')) > 0)
            {
                newText = newText.Remove(index, 1);
            }
            Text = newText;
        }

        if (!editing)
            base.OnTextChanged(e);
    }

    protected override void WndProc(ref Message m)
    {
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();

        if (m.Msg == WM_PASTE)
        {
            var obj = Clipboard.GetDataObject();
            var input = (string)obj.GetData(typeof(string));

            double converted;

            if (!double.TryParse(input, out converted))
                m.Result = (IntPtr)0;
        }
        base.WndProc(ref m);
    }
}