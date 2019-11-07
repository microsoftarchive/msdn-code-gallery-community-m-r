//-----------------------------------------------------------------------------
// File: ImageKernel.cu
//
// Desc: CUDA kernel to convert RGBA image to gray.
//		 
//		 This kernel assumes 4-bytes per pixel RGBA image with channels Red, Green, Blue, and Alpha
//		 represented each by one byte (8-bits) and a range of values between 0 and 255 (2^8 - 1).
//
//		 Grey scale images are represented by a single intensity value per pixel 
//		 where each pixel is only 1 byte.
//
//		 Human eye perseives red, green, and blue colors unequally (humans are more sensitive to green and least to blue) 
//		 and for that reason we will use weighted formula (http://en.wikipedia.org/wiki/Grayscale):
//
//		I = 0.2126 * R + 0.7152 * G + 0.0722 * B
//		 
//		 
//
//-----------------------------------------------------------------------------

#include "stdafx.h"
#include "Image.h"

using namespace Bisque;

using std::ceilf;

#define N (1024 * 1024)				// blocks
#define BLOCK_WIDTH 32				// threads per block

// Converts RGBA image to gray scale intensity using the following formula:
// I = 0.2126 * R + 0.7152 * G + 0.0722 * B
__global__ 
void rgba_to_grayscale(unsigned char* const gray, const uchar4* const rgba, int rows, int cols)
{
	int r			=  blockIdx.y * blockDim.y + threadIdx.y;		// current row
	int c			=  blockIdx.x * blockDim.x + threadIdx.x;		// current column

	if ((r < rows) && (c < cols))
	{
		int idx			= c + cols * r;		// current pixel index

		uchar4 pixel	= rgba[idx];
		float intensity = 0.2126f * pixel.x + 0.7152f * pixel.y + 0.0722f * pixel.z;

		gray[idx]		= (unsigned char)intensity;
	}
}

// Runs r8g8b8a8 to gray kernel
void RunRGBAtoGrayKernel(
		unsigned char*	gray,				// gray image: 1 byte per image --> this will be returned from this function
		uchar4*			rgba,				// rgba image: 4 bytes per image
		int				rows,				// image size: number of rows
		int				cols				// image size: number of columns
	)
{
	const char* func = "RunGrayKernel";

	cudaError hr = cudaSuccess;

	int x = static_cast<int>(ceilf(static_cast<float>(cols) / BLOCK_WIDTH));
	int y = static_cast<int>(ceilf(static_cast<float>(rows) / BLOCK_WIDTH));

	const dim3 grid (x, y, 1);								// number of blocks
	const dim3 block(BLOCK_WIDTH, BLOCK_WIDTH, 1);			// block width: number of threads per block
		
	rgba_to_grayscale<<<grid, block>>>(gray, rgba, rows, cols);

	hr = cudaDeviceSynchronize();																CHECK_CUDA_ERROR(hr, func, "rgba_to_grayscale failed.");
}
