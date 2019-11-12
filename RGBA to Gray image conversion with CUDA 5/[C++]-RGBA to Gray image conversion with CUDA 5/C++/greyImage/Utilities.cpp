#include "stdafx.h"
#include "Utilities.h"

using namespace Bisque;

// ShowError
void Utilities::ShowError(const wstring& functionName, const char* caption)
{
	/*
    // Retrieve the system error message for the last-error code
    LPVOID message;
    LPVOID buffer;
    DWORD handle = GetLastError(); 

    FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER | 
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr,
        handle,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR) &message,
        0, NULL );

    // Display the error message and exit the process
    buffer = (LPVOID)LocalAlloc(
		LMEM_ZEROINIT, 
        (lstrlen((LPCTSTR)message) + functionName.length() + 40) * sizeof(TCHAR)
		); 

	// Convert caption to wchar
	wchar_t* wCaption;
	CharToWChar(const_cast<char*>(caption), &wCaption);

	StringCchPrintf((LPTSTR)buffer, 
        LocalSize(buffer) / sizeof(TCHAR),
        TEXT("%s failed with error %d: %s"), 
        functionName.c_str(), 
		handle, 
		message
		); 
    MessageBox(NULL, (LPCTSTR)buffer, wCaption, MB_OK); 
	
	delete[] wCaption;
    LocalFree(message);
    LocalFree(buffer);
	*/
}
