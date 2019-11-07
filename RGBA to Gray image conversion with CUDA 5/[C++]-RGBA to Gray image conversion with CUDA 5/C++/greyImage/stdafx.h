#pragma once

#define NOMINMAX			// Use standard library min/max

#include <cassert>
#include <chrono>
#include <cmath>
#include <iomanip>
#include <iostream>
#include <string>

#include <opencv2\core\core.hpp>
#include <opencv2\highgui\highgui.hpp>
#include <opencv2\opencv.hpp>

#include <cuda.h>
#include <cuda_runtime.h>
#include <cuda_runtime_api.h>

// My headers
#include "GpuTimer.h"
#include "Utilities.h"

// Load libraries
#pragma comment(lib, "cudart")

// opencv requires debug libraries when running indebug mode
#if _DEBUG
#pragma comment(lib, "opencv_core243d")
#pragma comment(lib, "opencv_imgproc243d")
#pragma comment(lib, "opencv_highgui243d")
#pragma comment(lib, "opencv_ml243d")
#pragma comment(lib, "opencv_video243d")
#pragma comment(lib, "opencv_features2d243d")
#pragma comment(lib, "opencv_calib3d243d")
#pragma comment(lib, "opencv_objdetect243d")
#pragma comment(lib, "opencv_contrib243d")
#pragma comment(lib, "opencv_legacy243d")
#pragma comment(lib, "opencv_flann243d")
#else
#pragma comment(lib, "opencv_core243")
#pragma comment(lib, "opencv_imgproc243")
#pragma comment(lib, "opencv_highgui243")
#pragma comment(lib, "opencv_ml243")
#pragma comment(lib, "opencv_video243")
#pragma comment(lib, "opencv_features2d243")
#pragma comment(lib, "opencv_calib3d243")
#pragma comment(lib, "opencv_objdetect243")
#pragma comment(lib, "opencv_contrib243")
#pragma comment(lib, "opencv_legacy243")
#pragma comment(lib, "opencv_flann243")
#endif