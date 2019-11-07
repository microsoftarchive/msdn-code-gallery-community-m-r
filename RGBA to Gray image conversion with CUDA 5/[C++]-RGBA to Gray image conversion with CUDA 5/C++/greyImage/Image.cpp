#include "stdafx.h"
#include "Image.h"

using namespace Bisque;

using cv::Mat;
using cv::cvtColor;
using cv::imread;
using cv::imwrite;

using std::cout;
using std::endl;
using std::exception;
using std::runtime_error;

using std::chrono::duration_cast;
using std::chrono::milliseconds;
using std::chrono::microseconds;
using std::chrono::system_clock;
using std::chrono::time_point;

// Forward declarations
void RunRGBAtoGrayKernel(
		unsigned char*	gray,				// gray image: 1 byte per image --> this will be returned from this function
		uchar4*			rgba,				// rgba image: 4 bytes per image
		int				rows,				// image size: number of rows
		int				cols				// image size: number of columns
	);

// Ctor
Image::Image(void)
{
	m_device.gray = nullptr;
	m_device.rgba = nullptr;
}

Image::~Image(void)
{
}

//Converts r8g8b8a8 image to one channel gray
void Image::ConvertRGBAtoGray(const string& imagePath, const string& outputPath)
{
    const char* func = "Image::ConvertRGBAtoGray";

	cudaError hr = cudaSuccess;

	time_point<system_clock> start;
	time_point<system_clock> stop;

	// Load image and initialize kernel
	KernelMap host;
	KernelMap device;

	start = system_clock::now();

	InitializeKernel(host, device, imagePath);

	stop = system_clock::now();
	long long ms = duration_cast<milliseconds>(stop - start).count();
	long long us = duration_cast<microseconds>(stop - start).count();

	cout << "InitializeKernel executed in " << us << "us (" << ms << "ms)" << endl;

	// Run kernel: convert rgba image to gray
	start = system_clock::now();

	GpuTimer gpuTimer;
	gpuTimer.Start();

	RunRGBAtoGrayKernel(
		device.gray, 
		device.rgba, 
		m_host.rgba.rows, 
		m_host.rgba.cols
		);

	gpuTimer.Stop();

	stop = system_clock::now();
	ms = duration_cast<milliseconds>(stop - start).count();
	us = duration_cast<microseconds>(stop - start).count();

	cout << "RunRGBAtoGrayKernel executed in " << us << "us (" << ms << "ms); gpu time: " << gpuTimer.Elapsed() << "ms" << endl;

	// Save gray image to disk
	start = system_clock::now();

	SaveGrayImageToDisk(outputPath);

	stop = system_clock::now();
	ms = duration_cast<milliseconds>(stop - start).count();
	us = duration_cast<microseconds>(stop - start).count();

	cout << "SaveGrayImageToDisk executed in " << us << "us (" << ms << "ms)" << endl;

#if 1			// Change to 1 to enable
	// Validate GPU convertion against CPU result.
	// Only turn it when you want to run validation because CPU calculation will be slow.
	VerifyGpuComputation(host.rgba);
#endif

	// Release cuda
	hr = cudaFree(m_device.gray);
	hr = cudaFree(m_device.rgba);
}

// Initializes CUDA kernel, as well as loads teh image from disk and prepares 
// image rgba and gray-single-channel handles both on the host and the device.
void Image::InitializeKernel(KernelMap& host, KernelMap& device, const string& imagePath)
{
    const char* func = "Image::InitializeKernel";
	CV_FUNCNAME("Image::InitializeKernel");
	__CV_BEGIN__

	cudaError hr = cudaFree(nullptr);																			CHECK_CUDA_ERROR(hr, func, "Could not free CUDA memory space.");

	// Load image
	Mat image = imread(imagePath.c_str(), CV_LOAD_IMAGE_COLOR);
	if (image.empty())
	{
		string msg = "Could not open image file: " + imagePath;
		throw runtime_error(msg);
	}

	// Convert image from BGR to RGB
	cvtColor(image, m_host.rgba, CV_BGR2RGBA);

	// Allocate memory for the gray image (on the host)
	m_host.gray.create(image.rows, image.cols, CV_8UC1);

	CV_ASSERT(m_host.rgba.isContinuous());
	CV_ASSERT(m_host.gray.isContinuous());

	host.rgba = reinterpret_cast<uchar4*>(m_host.rgba.ptr<unsigned char>(0));
	host.gray = m_host.gray.ptr<unsigned char>(0);

	const int numPixels = m_host.rgba.rows * m_host.rgba.cols;

	// Allocate memory on the device for both rgba and gray images, 
	// then fill gray image with zeros to make sure there is no memory left laying around.
	// Finally, copy rgba image to the GPU.
	hr = cudaMalloc(&device.rgba,			 numPixels * sizeof(uchar4));										CHECK_CUDA_ERROR(hr, func, "Could not allocate device memory for RGBA image.");
	hr = cudaMalloc(&device.gray,			 numPixels * sizeof(unsigned char));								CHECK_CUDA_ERROR(hr, func, "Could not allocate device memory for gray image.");
	hr = cudaMemset( device.gray, 0,		 numPixels * sizeof(unsigned char));								CHECK_CUDA_ERROR(hr, func, "Could not fill gray image with constant values.");
	hr = cudaMemcpy( device.rgba, host.rgba, numPixels * sizeof(uchar4), cudaMemcpyHostToDevice);				CHECK_CUDA_ERROR(hr, func, "Could not copy rgba image from host to device.");

	// Save device pointers
	m_device.gray = device.gray;
	m_device.rgba = device.rgba;

	__CV_END__
}

// Copies gray image from device to the host and saves it to disk
void Image::SaveGrayImageToDisk(const string& outputPath)
{
    const char* func = "Image::SaveGrayImageToDisk";
	cudaError hr = cudaSuccess;

	const int numPixels = m_host.rgba.rows * m_host.rgba.cols;

	// Copy gray image from device to the host
	hr = cudaMemcpy(
		m_host.gray.ptr<unsigned char>(0), 
		m_device.gray,
		numPixels * sizeof(unsigned char),
		cudaMemcpyDeviceToHost
		);																										CHECK_CUDA_ERROR(hr, func, "Could not copy gray image from device to the host.");

	// Save image to disk
	imwrite(outputPath.c_str(), m_host.gray);
}

// Verifies correctness of the GPU computation running the same algorithm on the CPU
// and comparing pixel values. 
//
// NOTE: Because we recompute pixels on the CPU, call this function only when you need
//		 to validate GPU computation.
void Image::VerifyGpuComputation(const uchar4* const rgba)
{
    const char* func = "Image::VerifyGpuComputation";
	cudaError hr = cudaSuccess;

	const int numPixels = m_host.rgba.rows * m_host.rgba.cols;
	unsigned char* gpu = new unsigned char[numPixels];				// image computed on GPU
	unsigned char* ref = new unsigned char[numPixels];				// reference image that will be computed on CPU

	// Copy device gray image to the host
	hr = cudaMemcpy(
		gpu, 
		m_device.gray, 
		numPixels * sizeof(unsigned char), 
		cudaMemcpyDeviceToHost
		);																									CHECK_CUDA_ERROR(hr, func, "Could not copy gray image from device to the host.");

	// Copmute gray image on the CPU
	time_point<system_clock> start = system_clock::now();
	time_point<system_clock> stop  = system_clock::now();

	RGBAtoGrayOnCPU(ref, rgba, m_host.rgba.rows, m_host.rgba.cols);

	stop = system_clock::now();
	long long ms = duration_cast<milliseconds>(stop - start).count();
	long long us = duration_cast<microseconds>(stop - start).count();

	cout << "RGBAtoGrayOnCPU executed in " << us << "us (" << ms << "ms)" << endl;

	// Compare results
	// NOTE: Since this sample works with 8-bit images and ranges of colors between 1 and 255 per channel,
	//		 we only allow color to differ by value of 1 between GPU and CPU computations.
	//		 As this is a gray image with only one channel of color intensity, we allow intensity error
	//		 to be no more than 1 between the same pixel in GPU and CPU images, and total percent of errors
	//		 is expected not to be higher than 0.001
	Utilities::CheckGpuComputationEps(ref, gpu, numPixels, 1, .001);
	//Utilities::CheckGpuComputationExact(ref, gpu, numPixels);
	//Utilities::CheckGpuComputationAutodesk(ref, gpu, numPixels, 1.0, 100);

	// Clean up
	delete[] gpu;
	delete[] ref;
}

// Converts RGBA image to grayscale intensity on CPU.
// NOTE: This is a reference computation for validating results on GPU.
//		 Otherwise, do not call this function.
void Image::RGBAtoGrayOnCPU(unsigned char* gray, const uchar4* const rgba, int rows, int cols)
{
	for (int r = 0; r < rows; ++r)
	{
		for (int c = 0; c < cols; ++c)
		{
			int	   idx		 = c + cols * r;		// current pixel index
			uchar4 pixel	 = rgba[idx];
			float  intensity = 0.2126f * pixel.x + 0.7152f * pixel.y + 0.0722f * pixel.z;

			gray[idx]		 = static_cast<unsigned char>(intensity);
		}
	}
}
