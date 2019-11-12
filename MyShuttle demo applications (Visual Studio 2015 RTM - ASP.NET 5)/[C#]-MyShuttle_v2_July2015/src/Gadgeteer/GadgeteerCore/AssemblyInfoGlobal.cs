////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if DEBUG 
[assembly: AssemblyConfiguration("Debug")] 
#else 
[assembly: AssemblyConfiguration("Release")] 
#endif 
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("Microsoft .NET Gadgeteer")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// VERSION NUMBERING SCHEME:
// First number is Microsoft .NET Gadgeteer major version - update this if the hardware is changed fundamentally. 
// Second number refers to .NET Micro Framework version, 42 for NETMF 4.2
// Third number is Gadgeteer Core version in format XYY.  X = installer release number, Y=00 if release version, Y!=00 if development version.  
// The fourth number (revision) can be non-zero if building a new dll without making a new installer, but should be zero when making an installer.

// AssemblyInfo is defined in each project's Properties/AssemblyInfo.cs file since this differs depending on the NETMF target version

// This must be 2.42.XYY.ZZZ where X = public release number and Y = development release number,
// and Z can be incremented between development releases but reset to zero when an installer is built.
[assembly: AssemblyInformationalVersion("2.43.900.0")]
[assembly: AssemblyFileVersion("2.43.900.0")]

