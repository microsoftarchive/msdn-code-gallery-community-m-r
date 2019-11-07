//-----------------------------------------------------------------------------
// File: GpuTimer.h
//
// Desc: Allows to measure GPU elapsed time.
//
//-----------------------------------------------------------------------------

#pragma once

namespace Bisque
{
	class GpuTimer
	{
	public:
		GpuTimer(void)
		{
			cudaEventCreate(&m_start);
			cudaEventCreate(&m_stop);
		}

		~GpuTimer(void)
		{
			cudaEventDestroy(m_start);
			cudaEventDestroy(m_stop);
		}

		// Starts timer
		void Start()
		{
			cudaEventRecord(m_start, 0);
		}

		// Stops timer
		void Stop()
		{
			cudaEventRecord(m_stop, 0);
		}

		// Returns elapsed time in milliseconds
		float Elapsed()
		{
			float elapsed;
			cudaEventSynchronize(m_stop);
			cudaEventElapsedTime(&elapsed, m_start, m_stop);
			return elapsed;
		}

	private:
		cudaEvent_t m_start;
		cudaEvent_t m_stop;
	};
}
