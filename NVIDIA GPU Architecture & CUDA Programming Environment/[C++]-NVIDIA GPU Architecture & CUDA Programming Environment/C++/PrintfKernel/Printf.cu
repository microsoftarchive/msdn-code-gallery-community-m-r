/*
 * Example demonstrates how to use printf command in CUDA kernel
 */

#include <cuda_runtime.h>
#include <device_launch_parameters.h>

#include <assert.h>
#include <iostream>
#include <stdio.h>
#include <string>

using namespace std;

// printf is only supported for compute capability 2.0 and higher
#if defined(__CUDA_ARCH__) && (__CUDA_ARCH__ < 200)
#define printf(f, ...) ((void)(f, __VA_ARGS__),0)
#endif

// Forward declarations
cudaError_t printfTest();
void		SetDevice();

// Print kernel
__global__ void printfKernel(float f)
{
	float data = f * threadIdx.x;
	printf("Thread %d, f = %f.\n", threadIdx.x, data);
}

int main()
{
	// Show device info and pick the best available device
	SetDevice();

	// Call kernel
	cudaError_t ce = printfTest();
	if (ce != cudaSuccess)
	{
		cerr << "printfTest failed!" << endl;
		return 1;
	}

	// Reset device before exiting for profiling and tracing tools to show compete traces
	ce = cudaDeviceReset();
	if (ce != cudaSuccess)
	{
		cerr << "cudaDeviceReset failed!" << endl;
		return 1;
	}

    return 0;
}

// Helper function for using CUDA to add vectors in parallel.
cudaError_t printfTest()
{
	cudaError_t ce = cudaSuccess;

	printfKernel<<< 1, 64 >>>(1.2345f);
	ce = cudaDeviceSynchronize();

    if (ce != cudaSuccess) {
        cerr << "cudaDeviceSynchronize returned error code " << to_string(ce) << " after launching printfKernel!\n" << endl;
        goto Error;
    }

Error:
	return ce;
}

// Show device information
/*
Using device: 0
	Name:                    Quadro 5000M
	Compute version:         2.0
	Global memory:           2047.69 mb
	Const memory:            64 kb
	L2 cache size:           512 kb
	Clock rate:              810 mhz
	Timeout enabled:         true
	Multiprocessors:         10
	Max grid size:           65535 : 65535 : 65535
	Max threads per SM:      1536
	Max threads per block:   1024
	Registers per block:     32768
	Shared memory per block: 48 kb
	Memory bus width:        256 bits
	Memory clock rate:       1200 mhz
	Compute mode:            Default
*/
void SetDevice()
{
	// Only show stats on the first run of this code
	static bool showStats = true;

	if (!showStats)
		return;
	
	showStats = false;

	// Set device and display properties
	int deviceCount;
	cudaGetDeviceCount(&deviceCount);

	if (deviceCount == 0)
	{
		cerr << "ERROR: Your system does not have CUDA." << endl;
		exit(EXIT_FAILURE);
	}

	int device;
	for (device = 0; device < deviceCount; ++device)
	{
		cudaDeviceProp p;
		cudaError ce = cudaGetDeviceProperties(&p, device);

		if (ce != cudaSuccess)
		{
			cerr << "ERROR: Device query failed." << endl;
			exit(EXIT_FAILURE);
		}

		cout << "\n\nUsing device: " << device << endl;
		cout << "   Name:                    " << p.name << endl;
		cout << "   Compute capability:      " << p.major << "." << p.minor << endl;
		cout << "   Warp Size:               " << p.warpSize << endl;
		cout << "   Global memory:           " << p.totalGlobalMem / static_cast<float>(1024 * 1024) << " mb" << endl;
		cout << "   Const memory:            " << p.totalConstMem / static_cast<float>(1024) << " kb" << endl;
		cout << "   L2 cache size:           " << p.l2CacheSize / static_cast<float>(1024) << " kb" << endl;
		cout << "   Clock rate:              " << p.clockRate / 1000.f << " mhz"<< endl;
		cout << "   Timeout enabled:         " << (p.kernelExecTimeoutEnabled == 1 ? "true" : "false") << endl;
		cout << "   Multiprocessors:         " << p.multiProcessorCount << endl;
		cout << "   Max grid size:           " << p.maxGridSize[0] << " : " << p.maxGridSize[1] << " : " << p.maxGridSize[2] << endl;
		cout << "   Max threads per SM:      " << p.maxThreadsPerMultiProcessor << endl;
		cout << "   Max threads per block:   " << p.maxThreadsPerBlock << endl;
		cout << "   Registers per block:     " << p.regsPerBlock << endl;
		cout << "   Shared memory per block: " << p.sharedMemPerBlock / static_cast<float>(1024) << " kb" << endl;
		cout << "   Memory bus width:        " << p.memoryBusWidth << " bits" << endl;
		cout << "   Memory clock rate:       " << p.memoryClockRate / 1000.f << " mhz" << endl;
		cout << "   Compute mode:            " << (p.computeMode == 0 ? "Default" : "Exclusive or Prohibitive") << endl;
		cout << "   PCI Bus id:              " << p.pciBusID << endl;
		cout << "   PCI Device id:           " << p.pciDeviceID << endl;
		cout << "   PCI Domain id:           " << p.pciDomainID << endl;
		cout << "   Number of async engines: " << p.asyncEngineCount << endl;
		cout << "   Discrete GPU:            " << (p.integrated == 0 ? "Yes" : "No") << endl;
		cout << "   Can map host memory:     " << (p.canMapHostMemory == 1 ? "Yes" : "No") << endl;
		cout << "   Concurrent kernels:      " << (p.concurrentKernels == 1 ? "Yes" : "No") << endl;
		cout << "   ECC supported:           " << (p.ECCEnabled == 1 ? "Yes" : "No") << endl;
		cout << "   Unified addressing:      " << (p.unifiedAddressing == 1 ? "Yes" : "No") << endl;
		cout << endl << endl;

		// Set best device here
		if (device == 0)
		{			
			ce = cudaSetDevice(0);
			if (ce != cudaSuccess)
			{
				cerr << "ERROR: failed to set CUDA device!" << endl;
				exit(EXIT_FAILURE);
			}
		}
	}
}
