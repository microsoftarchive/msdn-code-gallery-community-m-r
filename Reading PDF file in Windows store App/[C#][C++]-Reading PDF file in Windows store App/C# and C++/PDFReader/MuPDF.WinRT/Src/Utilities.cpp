#include <memory>

#include <Windows.h>

#include "Utilities.h"

Platform::String^ Utilities::ConvertUTF8ToString(char* str)
{
	int length = MultiByteToWideChar(
		CP_UTF8, 
		0, 
		str, 
		-1, 
		nullptr, 
		0);
	if (length == 0)
		throw ref new Platform::FailureException();
	std::unique_ptr<wchar_t[]> convertedStr(new wchar_t[length]);
	length =  MultiByteToWideChar(
		CP_UTF8, 
		0, 
		str, 
		-1, 
		convertedStr.get(),
		length);
	if (length == 0)
		throw ref new Platform::FailureException();
	return ref new Platform::String(convertedStr.get());
}

std::unique_ptr<char[]> Utilities::ConvertStringToUTF8(Platform::String^ string)
{
	int length =  WideCharToMultiByte(
		CP_UTF8, 
		0, 
		string->Data(), 
		-1, 
		nullptr, 
		0, 
		nullptr, 
		nullptr);
	if (length == 0)
		throw ref new Platform::FailureException();
	std::unique_ptr<char[]> ut8String(new char[length]);
	length =  WideCharToMultiByte(
		CP_UTF8, 
		0, 
		string->Data(), 
		-1, 
		ut8String.get(),
		length,
		nullptr, 
		nullptr);
	if (length == 0)
		throw ref new Platform::FailureException();
	return ut8String;
}

MuPDFWinRT::RectF Utilities::CreateRectF(float left, float top, float right, float bottom)
{
	MuPDFWinRT::RectF rect;
	rect.Left = left;
	rect.Top = top;
	rect.Right = right;
	rect.Bottom = bottom;
	return rect;
}