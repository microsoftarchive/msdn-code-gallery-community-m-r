// program.cpp
//
// Main entry point into the application.
//

#include "stdafx.h"
#include "Image.h"

using std::cout;
using std::cerr;
using std::endl;
using std::exception;
using std::string;

using namespace Bisque;

// Main entry into the application
int main(int argc, char** argv)
{
	string imagePath;
	string outputPath;

	if (argc > 2)
	{
		imagePath = string(argv[1]);
		outputPath = string(argv[2]);
	}
	else
	{
		cerr << "Please provide input and output image files as arguments to this application." << endl;
		exit(1);
	}

	Image image;

	try
	{
		image.ConvertRGBAtoGray(imagePath, outputPath);
	}
	catch(exception& e)
	{
		cerr << endl << "ERROR: " << e.what() << endl;
		exit(1);
	}

	cout << "Done!" << endl << endl;
	return 0;
}
