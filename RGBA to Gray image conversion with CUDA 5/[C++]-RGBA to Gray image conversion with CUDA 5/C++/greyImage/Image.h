//-----------------------------------------------------------------------------
// File: Image.h
//
// Desc: Responsible for preparing CUDA device.
//		 Opens an immage and alocates required memory on the GPU.
//		 Saves modified image to disk
//
//-----------------------------------------------------------------------------

#pragma once

namespace Bisque
{
	using cv::Mat;
	using std::string;

	// class Image
	class Image
	{
	public:
		Image(void);
		~Image(void);

		void ConvertRGBAtoGray(const string& imagePath, const string& outputPath);

	private:
		struct ImageHandle
		{
			Mat rgba;
			Mat gray;
		};

		// struct KernelMap
		struct KernelMap
		{
			uchar4*			rgba;				// rgba image: 4 bytes per image
			unsigned char*	gray;				// gray image: 1 byte per image
		};

	private:
		void RGBAtoGrayOnCPU		(unsigned char* gray, const uchar4* const rgba, int rows, int cols);
		void InitializeKernel		(KernelMap& host, KernelMap& device, const string& imagePath);
		void SaveGrayImageToDisk	(const string& imagePath);
		void VerifyGpuComputation	(const uchar4* const rgba);

	private:
		KernelMap		m_device;
		ImageHandle		m_host;
	};
}
