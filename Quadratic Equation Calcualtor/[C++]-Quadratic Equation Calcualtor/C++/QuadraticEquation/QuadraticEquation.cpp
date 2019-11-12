// QuadraticEquation.cpp : main project file.


#include "stdafx.h"
#include "QuadraticEquation.h"

using namespace MyQuadraticEquation;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	MessageBox::Show("This is solution of equations like axx+bx+c=0 by Yegor Isakov LTD");
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	// Create the main window and run it
	Application::Run(gcnew QuadraticEquation());
	return 0;
}
