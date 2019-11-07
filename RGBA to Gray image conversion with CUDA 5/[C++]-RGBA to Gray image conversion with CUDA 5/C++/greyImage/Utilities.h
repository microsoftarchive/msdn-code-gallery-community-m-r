#pragma once

#include "stdafx.h"

namespace Bisque
{
	using std::cout;
	using std::cerr;
	using std::endl;
	using std::abs;
	using std::max;
	using std::min;
	using std::runtime_error;
	using std::setprecision;
	using std::string;
	using std::wstring;

	class Utilities
	{
	public:

		static void ShowError(const wstring& functionName, const char* caption);

		// Compares GPU computation to CPU results using Autodesk method.
		// NOTE: Tolerance is in PIXELS, and not a percentage of input pixels.
		template<typename T>
		static void CheckGpuComputationAutodesk(const T* const ref, const T* const gpu, int numPixels, double variance, int tolerance)
		{
			int numBadPixels = 0;

			for (int i = 0; i < numPixels; ++i)
			{
				T up	= max(ref[i], gpu[i]);
				T lw	= min(ref[i], gpu[i]);
				T delta = abs(up - lw);

				if (delta > variance)
				{
					numBadPixels++;
				}
			}

			if (numBadPixels > tolerance)
			{
				cerr << "CheckGpuComputationAutodesk Violation:" << endl;
				cerr << "   Too many bad pixels in the GPU image." << endl;
				cerr << "   Number of bad pixels: " << numBadPixels << endl;
				cerr << "   Tolerance:            " << tolerance << endl;

				throw runtime_error("GPU copmutation results failed eps check.");
			}

			cout << "CheckGpuComputationAutodesk succeeded!" << endl;
		}
		
		// Compares GPU computation to CPU resuls up to predefined precision eps1 with the total percent of errors below eps2.
		template<typename T>
		static void CheckGpuComputationEps(const T* const ref, const T* const gpu, int numPixels, double eps1, double eps2)
		{
			assert(eps1 >= 0 && eps2 >= 0);

			unsigned long long totalDiff	= 0;
			unsigned long      numSmallDifs = 0;

			// Chack for delta exceeding eps1
			for (int i = 0; i < numPixels; ++i)
			{
				// Find delat between pixel values in refderence image and the one computed on gpu
				T up	= max(ref[i], gpu[i]);
				T lw	= min(ref[i], gpu[i]);
				T delta = abs(up - lw);

				if (delta > 0 && delta <= eps1)
				{
					numSmallDifs++;
				}
				else if (delta > eps1)
				{
					cerr << "CheckGpuCopmutationEps EPS1 Violation:" << endl;
					cerr << "   Difference at position " << i << " exceeds tolerance of " << eps1 << endl;
					cerr << "   Reference image: " << setprecision(17) << static_cast<int>(ref[i]) << endl;
					cerr << "   GPU image:       " << static_cast<int>(gpu[i]) << endl;

					throw runtime_error("GPU copmutation results failed eps check.");
				}

				totalDiff += (delta * delta);
			}

			// Check for percentage of small differences exceeding eps2
			double pctSmallDifs = static_cast<double>(numSmallDifs) / static_cast<double>(numPixels);

			if (pctSmallDifs > eps2)
			{
					cerr << "CheckGpuCopmutationEps EPS2 Violation:" << endl;
					cerr << "   Total percentage of non-zero pixel differences between GPU and CPU images exceeded EPS2 limit." << endl;
					cerr << "   Percent of non-zero pixel differences: " << 100.0 * pctSmallDifs << "%" << endl;
					cerr << "   EPS2:                                  " << 100.0 * eps2 << "%" << endl;

					throw runtime_error("GPU copmutation results failed eps check.");
			}

			cout << "CheckGpuCopmutationEps succeeded!" << endl;
		}

		// Compares GPU computation to CPU results with the expectation of exact match
		template<typename T>
		static void CheckGpuComputationExact(const T* const ref, const T* const gpu, int numPixels)
		{
			for (int i = 0; i < numPixels; ++i)
			{
				if (ref[i] != gpu[i])
				{
					cerr << "CheckGpuComputationExact Violation:" << endl;
					cerr << "   Difference at position " << i << endl;
					cerr << "   Reference image: " << setprecision(17) << static_cast<int>(ref[i]) << endl;
					cerr << "   GPU image:       " << static_cast<int>(gpu[i]) << endl;

					throw runtime_error("GPU copmutation results failed eps check.");
				}
			}

			cout << "CheckGpuComputationExact succeeded!" << endl;
		}

		template<typename T>
		static void CheckRuntimeError(T error, const string& functionName, const string& fileName, const int line, const char* message)
		{
			if (error != cudaSuccess)
			{
				cerr << "CUDA ERROR:" << endl;
				cerr << "   file: " << fileName << endl;
				cerr << "   line: " << line << endl;
				cerr << "   func: " << functionName << endl;
				cerr << "   desc: " << cudaGetErrorString(error) << endl;
				throw runtime_error(message);
			}
		}
	};
	
// If an error occurred, display the HRESULT and exit
#ifndef CHECK_CUDA_ERROR
#define CHECK_CUDA_ERROR(error, functionName, message) Utilities::CheckRuntimeError( (error), functionName, __FILE__, __LINE__, (message) );
#endif

}
