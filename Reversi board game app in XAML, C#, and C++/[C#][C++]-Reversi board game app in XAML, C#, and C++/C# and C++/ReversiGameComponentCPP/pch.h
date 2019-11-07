//
// pch.h
// Header for standard system include files.
//

#pragma once

// This macro allows use of std::min and std::max which otherwise
// get hidden by the macros of the same name that are indirectly included
// by windows.h.
#define NOMINMAX

// STL headers
#include <algorithm>
#include <array>
#include <assert.h>
#include <iostream>
#include <limits>
#include <map>
#include <math.h>
#include <mutex>
#include <regex>
#include <random>
#include <string>
#include <sstream>
#include <vector>

// Windows Header Files:
#include <windows.h>

// For Platform::Collection types
#include <collection.h>

// For create_async and create_task
#include <ppltasks.h>
