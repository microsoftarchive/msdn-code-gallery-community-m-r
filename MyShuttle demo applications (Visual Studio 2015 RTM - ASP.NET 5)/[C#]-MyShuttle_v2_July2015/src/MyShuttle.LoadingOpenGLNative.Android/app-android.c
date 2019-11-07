/* San Angeles Observation OpenGL ES version example
 * Copyright 2009 The Android Open Source Project
 * All rights reserved.
 *
 * This source is free software; you can redistribute it and/or
 * modify it under the terms of EITHER:
 *   (1) The GNU Lesser General Public License as published by the Free
 *       Software Foundation; either version 2.1 of the License, or (at
 *       your option) any later version. The text of the GNU Lesser
 *       General Public License is included with this source in the
 *       file LICENSE-LGPL.txt.
 *   (2) The BSD-style license that is included with this source in
 *       the file LICENSE-BSD.txt.
 *
 * This source is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the files
 * LICENSE-LGPL.txt and LICENSE-BSD.txt for more details.
 */
#include <jni.h>
#include <sys/time.h>
#include <time.h>
#include <android/log.h>
#include <stdint.h>
#include "app.h"
#include "importgl.h"

int   gAppAlive   = 1;

static int  sWindowWidth  = 320;
static int  sWindowHeight = 480;
static int  sDemoStopped  = 0;
static long sTimeOffset   = 0;
static long sTimeStopped  = 0;

static long
_getTime(void)
{
    struct timeval  now;

    gettimeofday(&now, NULL);
    return (long)(now.tv_sec*1000 + now.tv_usec/1000);
}

/* Call to initialize the graphics state */
void
nativeInit( JNIEnv*  env )
{
    importGLInit();
    appInit();
    gAppAlive  = 1;
}

void
nativeResize( JNIEnv*  env, jobject  thiz, jint w, jint h )
{
    sWindowWidth  = w;
    sWindowHeight = h;
    __android_log_print(ANDROID_LOG_INFO, "LoadingOpenGL", "resize w=%d h=%d", w, h);
}

/* Call to finalize the graphics state */
void
nativeDone( JNIEnv*  env )
{
    appDeinit();
    importGLDeinit();
}

/* This is called to indicate to the render loop that it should
 * stop as soon as possible.
 */

void _pause()
{
  /* we paused the animation, so store the current
   * time in sTimeStopped for future nativeRender calls */
    sDemoStopped = 1;
    sTimeStopped = _getTime();
}

void _resume()
{
  /* we resumed the animation, so adjust the time offset
   * to take care of the pause interval. */
    sDemoStopped = 0;
    sTimeOffset -= _getTime() - sTimeStopped;
}

void
nativePause( JNIEnv*  env )
{
    _pause();
}

void
nativeResume( JNIEnv*  env )
{
    _resume();
}

/* Call to render the next GL frame */
void
nativeRender( JNIEnv*  env )
{
    appRender(sWindowWidth, sWindowHeight);
}
