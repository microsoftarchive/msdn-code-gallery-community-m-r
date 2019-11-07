#pragma once

#include <memory>

#include <Winerror.h>

#include "RectF.h"

namespace Utilities
{
	inline void ThrowIfFailed(HRESULT hr)
	{
		if (FAILED(hr))
		{
			throw Platform::Exception::CreateException(hr);
		}
	}

	Platform::String^ ConvertUTF8ToString(char* str);

	std::unique_ptr<char[]> ConvertStringToUTF8(Platform::String^ string);

	MuPDFWinRT::RectF CreateRectF(float left, float top, float right, float bottom);
} 