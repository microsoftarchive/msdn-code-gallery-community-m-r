Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports AForge
Imports ImageFunctions.Controls

Namespace ImageFunctions.Classes
	Friend Class Corners


		'#region Corners
		'#region Moravec Corner Detection

		'private void DoMoravec()
		'{
		'	ControlPanel.Controls.Clear(); // Remove any previous controls that were present
		'	MoravecProperties = new MoravecCornerProperties();
		'	MoravecProperties.Dock = DockStyle.Fill;
		'	ControlPanel.Controls.Add(MoravecProperties);
		'}

		'void m_ImageComplete(List<IntPoint> Corners)
		'{
		'	ImageCornerDetectionCompleted(Corners, Color.Blue);
		'}
		'#endregion

		'#region Susan Corner Detection

		'private void DoSusan()
		'{
		'	ControlPanel.Controls.Clear(); // Remove any previous controls that were present
		'	SusanProperties = new SusanCornerProperties();
		'	SusanProperties.Dock = DockStyle.Fill;
		'	ControlPanel.Controls.Add(SusanProperties);
		'}

		'private void s_ImageComplete(List<IntPoint> Corners)
		'{
		'	ImageCornerDetectionCompleted(Corners, Color.Red);
		'}

		'#endregion

		'#region Harris

		'private void DoHarris()
		'{
		'	ControlPanel.Controls.Clear(); // Remove any previous controls that were present
		'	HarrisProperties = new HarrisCornerProperties();
		'	HarrisProperties.Dock = DockStyle.Fill;
		'	ControlPanel.Controls.Add(HarrisProperties);
		'}

		'void h_ImageComplete(List<IntPoint> Corners)
		'{
		'	ImageCornerDetectionCompleted(Corners, Color.Chartreuse);
		'}

		'#endregion

		'#region FAST Corner Detection
		'private void DoFast()
		'{
		'	ControlPanel.Controls.Clear(); // Remove any previous controls that were present
		'	FastProperties = new FASTCornerProperties();
		'	FastProperties.Dock = DockStyle.Fill;
		'	ControlPanel.Controls.Add(FastProperties);
		'}

		'void f_ImageComplete(List<IntPoint> Corners)
		'{
		'	ImageCornerDetectionCompleted(Corners, Color.LightCyan);
		'}

		'#endregion



	End Class
End Namespace
