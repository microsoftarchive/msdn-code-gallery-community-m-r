#pragma once

#include <functional>

namespace MuPDFWinRT
{
	public value struct RectF
	{
		float32 Left;
		float32 Top;
		float32 Right;
		float32 Bottom;
	};

	struct RectFEqual : public std::binary_function<const RectF, const RectF, bool> 
	{
		bool operator()(const RectF& left,  const RectF& right ) const
		{
			return (left.Left == right.Left) && (left.Top == right.Top) &&
				(left.Right == right.Right) && (left.Bottom == right.Bottom);
		};
	};
}