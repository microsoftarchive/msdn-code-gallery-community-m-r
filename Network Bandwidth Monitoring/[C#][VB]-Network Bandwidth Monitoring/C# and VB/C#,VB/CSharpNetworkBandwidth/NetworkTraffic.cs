using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Linq;

/// <summary>
/// Class that gets the network traffic from the performance counter.
/// Based on: http://pastebin.com/f371375d6
/// </summary>
public class NetworkTraffic
{
    private PerformanceCounter bytesSentPerformanceCounter;
    private PerformanceCounter bytesReceivedPerformanceCounter;
    private int pid;
    private bool countersInitialized;

    public NetworkTraffic(int processID)
    {
        pid = processID;
        TryToInitializeCounters();
    }

    private void TryToInitializeCounters()
    {
        if (!countersInitialized)
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory(".NET CLR Networking 4.0.0.0");

            var instanceNames = category.GetInstanceNames().Where(i => i.Contains(string.Format("p{0}", pid)));

            if (instanceNames.Any())
            {
                bytesSentPerformanceCounter = new PerformanceCounter();
                bytesSentPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0";
                bytesSentPerformanceCounter.CounterName = "Bytes Sent";
                bytesSentPerformanceCounter.InstanceName = instanceNames.First();
                bytesSentPerformanceCounter.ReadOnly = true;

                bytesReceivedPerformanceCounter = new PerformanceCounter();
                bytesReceivedPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0";
                bytesReceivedPerformanceCounter.CounterName = "Bytes Received";
                bytesReceivedPerformanceCounter.InstanceName = instanceNames.First();
                bytesReceivedPerformanceCounter.ReadOnly = true;

                countersInitialized = true;
            }
        }
    }

    public float GetBytesSent()
    {
        float bytesSent = 0;

        try
        {
            TryToInitializeCounters();
            bytesSent = bytesSentPerformanceCounter.RawValue;
        }
        catch { }

        return bytesSent;
    }

    public float GetBytesReceived()
    {
        float bytesSent = 0;

        try
        {
            TryToInitializeCounters();
            bytesSent = bytesReceivedPerformanceCounter.RawValue;
        }
        catch { }

        return bytesSent;
    }
}
