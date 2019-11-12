#include <stdlib.h>
#include <math.h>
#include <float.h>
#include <assert.h>
#include <GLES/gl.h>

#include "logger.h"

#include "app.h"
#include "texture.h"

//float to fixed point
#define fX(x) ((int)(x * (1  << 16)))

#define LOG_TAG "MyShuttleGLSample"

static GLint vertices[][3] = {
    { fX(-1), fX(-1), 0 },
    { fX(-1), fX( 1), 0 },
    { fX( 1), fX( 1), 0 },
    { fX( 1), fX(-1), 0 }
};

static int texCoords[8] = {
	fX(0), fX(1),
    fX(0), fX(0),
    fX(1), fX(0),
    fX(1), fX(1)
};

GLubyte indices[] = {
    0, 1, 2,
    2, 3, 0
};

GLuint textures[1] = { 0 };

GLfloat _angle;

// Called from the app framework.
void appInit()
{
	glDisable(GL_DITHER);
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_FASTEST);
	glClearColor(28/255.0f, 37/255.0f, 48/255.0f, 1);
	glEnable(GL_CULL_FACE);
	glShadeModel(GL_SMOOTH);
	glEnable(GL_TEXTURE_2D);

	GLuint texture;
	glGenTextures(1, &texture);
	glBindTexture(GL_TEXTURE_2D, texture);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, texture_width, texture_height, 0, GL_RGBA, GL_UNSIGNED_BYTE, (GLvoid*) texture_data);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

	textures[0] = texture;
}


// Called from the app framework.
void appDeinit()
{
	glDeleteTextures(1, textures);
}

// Called from the app framework.
/* The tick is current time in milliseconds, width and height
 * are the image dimensions to be rendered.
 */
void appRender(int width, int height)
{
    GLfloat ratio;

	glViewport(0, 0, width, height);

	ratio = (GLfloat) width / height;
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glFrustumf(-ratio, ratio, -1, 1, 1, 10);

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glTranslatef(0, 0, -3.0f);
	glRotatef(_angle, 0, 1, 0);

	glEnableClientState(GL_VERTEX_ARRAY);
	glEnableClientState(GL_TEXTURE_COORD_ARRAY);

	glEnable(GL_BLEND);
	glColorMask(GL_TRUE, GL_TRUE, GL_TRUE, GL_FALSE);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	glFrontFace(GL_CW);
	glVertexPointer(3, GL_FIXED, 0, vertices);
	glTexCoordPointer(2, GL_FIXED, 0, texCoords);

	glDrawElements(GL_TRIANGLES, 36, GL_UNSIGNED_BYTE, indices);

	_angle += 3.0f;

	if(_angle > 90)
	{
		_angle -= 180;
	}
}
