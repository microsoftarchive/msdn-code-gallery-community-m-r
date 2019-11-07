using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadWriteIniFileExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			FilePath.Text = "test.ini";
			Section.Text = "section2";
			Key.Text = "s2k2";
			Value.Text = "4";
			ReadIniFile();
		}

		private void ReadValue_Click(object sender, RoutedEventArgs e)
		{
			string value = IniFileHelper.ReadValue(Section.Text, Key.Text, System.IO.Path.GetFullPath(FilePath.Text));
			IniFile.Text = value;
		}

		private void ReadSections_Click(object sender, RoutedEventArgs e)
		{
			string[] values = IniFileHelper.ReadSections(System.IO.Path.GetFullPath(FilePath.Text));
			if (values != null)
			{
				string value = string.Join(Environment.NewLine, values);
				IniFile.Text = value;
			}
			else
			{
				IniFile.Text = "Reading sections failed.";
			}
		}

		private void ReadKeys_Click(object sender, RoutedEventArgs e)
		{
			string[] values = IniFileHelper.ReadKeys(Section.Text, System.IO.Path.GetFullPath(FilePath.Text));
			if (values != null)
			{
				string value = string.Join(Environment.NewLine, values);
				IniFile.Text = value;
			}
			else
			{
				IniFile.Text = "Reading keys failed.";
			}
		}

		private void ReadKeyValuePairs_Click(object sender, RoutedEventArgs e)
		{
			string[] values = IniFileHelper.ReadKeyValuePairs(Section.Text, System.IO.Path.GetFullPath(FilePath.Text));
			if (values != null)
			{
				string value = string.Join(Environment.NewLine, values);
				IniFile.Text = value;
			}
			else
			{
				IniFile.Text = "Reading key value pairs failed.";
			}
		}

		private void ReadFile_Click(object sender, RoutedEventArgs e)
		{
			ReadIniFile();
		}

		private void WriteValue_Click(object sender, RoutedEventArgs e)
		{
			bool result = IniFileHelper.WriteValue(Section.Text, Key.Text, Value.Text, System.IO.Path.GetFullPath(FilePath.Text));
			ReadIniFile();
		}

		private void DeleteSection_Click(object sender, RoutedEventArgs e)
		{
			bool result = IniFileHelper.DeleteSection(Section.Text, System.IO.Path.GetFullPath(FilePath.Text));
			ReadIniFile();
		}

		private void DeleteKey_Click(object sender, RoutedEventArgs e)
		{
			bool result = IniFileHelper.DeleteKey(Section.Text, Key.Text, System.IO.Path.GetFullPath(FilePath.Text));
			ReadIniFile();
		}

		private void ReadIniFile()
		{
			try
			{
				string text = File.ReadAllText(FilePath.Text);
				IniFile.Text = text;
			}
			catch (Exception)
			{
			}
		}
	}
}
