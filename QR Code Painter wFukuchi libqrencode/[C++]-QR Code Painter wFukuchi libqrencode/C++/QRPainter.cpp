#include "stdafx.h"
#include "QRPainter.h"
#include <qrencode.h>

CBMBits::CBMBits(BITMAP bm, int cx, int cy)
{
    DWORD cb = (bm.bmWidthBytes + bm.bmWidthBytes % sizeof(DWORD)) * cy;

    if (m_bits = new BYTE[cb])
        memset(m_bits, 0, cb);
}

CBMBits::~CBMBits()
{
    if (m_bits)
        delete[] m_bits;
}

BYTE &CBMBits::operator [](const size_t ix)
{
    return m_bits[ix];
}

CQRPainter::CQRPainter() : m_hbm(NULL)
{
}

CQRPainter::CQRPainter(LPCSTR data) : m_data(data), m_hbm(NULL)
{
    InitializeQR();
}

CQRPainter::CQRPainter(const std::string &data) : m_data(data), m_hbm(NULL)
{
    InitializeQR();
}

CQRPainter::~CQRPainter()
{
    Cleanup();
}

void CQRPainter::Cleanup()
{
    if (m_qrData)
    {
        QRcode_free(m_qrData);
        m_qrData = NULL;
    }
    if (m_hbm)
    {
        DeleteObject(m_hbm);
        m_hbm = NULL;
    }
}

bool CQRPainter::InitializeQR()
{
    Cleanup();
    if (!m_data.empty())
    {
        m_qrData = QRcode_encodeString(m_data.data(), 0, QR_ECLEVEL_L, QR_MODE_8, 0);
    }
    return NULL != m_qrData;
}

bool CQRPainter::InitializeBitmap(HDC hdc)
{
    if (m_hbm)
    {
        DeleteObject(m_hbm);
        m_hbm = NULL;
    }
    if (m_qrData)
    {
        m_hbm = CreateCompatibleBitmap(hdc, m_qrData->width, m_qrData->width);
        SetBitmapBits(hdc);
    }
    return NULL != m_hbm;
}

void CQRPainter::SetBitmapBits(HDC hdc)
{
    assert(m_hbm && m_qrData);
    if (m_hbm && m_qrData)
    {
        BITMAP bm;
        BITMAPINFO bmInfo;

        if (GetObject(m_hbm, sizeof(bm), &bm))
        {
            const int bytesPerPixel = bm.bmBitsPixel / 8;
            CBMBits bmBits(bm, m_qrData->width, m_qrData->width);

            if (bm.bmBitsPixel % 8)
            {
                //TODO
                assert(false);
                return;
            }
            for (int x = 0; x < m_qrData->width; x++)
            {
                for (int scanLine = 0; scanLine < m_qrData->width; scanLine++)
                {
                    const int y = bm.bmHeight - scanLine - 1;

                    if (0 == (m_qrData->data[y * m_qrData->width + x] & 0x1))
                    {
                        for (int ixColorByte = 0; ixColorByte < bytesPerPixel; ixColorByte++)
                            bmBits[scanLine * bm.bmWidthBytes + x * (bytesPerPixel) + ixColorByte] = 0xFF;
                    }
                }
            }
            memset(&bmInfo, 0, sizeof(bmInfo));
            bmInfo.bmiHeader.biSize = sizeof(bmInfo);
            bmInfo.bmiHeader.biWidth = bm.bmWidth;
            bmInfo.bmiHeader.biHeight = bm.bmHeight;
            bmInfo.bmiHeader.biPlanes = bm.bmPlanes;
            bmInfo.bmiHeader.biBitCount = bm.bmBitsPixel;
            bmInfo.bmiHeader.biCompression = BI_RGB;
            SetDIBits(hdc, m_hbm, 0, bm.bmHeight, bmBits, &bmInfo, DIB_RGB_COLORS);
        }
    }
}

bool CQRPainter::InitializeData(LPCSTR data)
{
    if (data)
        m_data = data;
    else
        m_data.erase();
    return InitializeQR();
}

void CQRPainter::PaintData(HWND hwnd, HDC hdc, const RECT &rc)
{
    if (InitializeBitmap(hdc))
    {
        int cxWnd = rc.right - rc.left;
        int cyWnd = rc.bottom - rc.top;
        HDC hdcMem = CreateCompatibleDC(hdc);
        HBITMAP hbmSav = static_cast<HBITMAP>(SelectObject(hdcMem, m_hbm));

        //QR is best captured when square, so force it
        if (cxWnd > cyWnd)
            cxWnd = cyWnd;
        else
            cyWnd = cxWnd;
        if (hbmSav)
        {
            StretchBlt(hdc, rc.left, rc.top, cxWnd, cyWnd, hdcMem, 0, 0, m_qrData->width, m_qrData->width, SRCCOPY);
            SelectObject(hdc, hbmSav);
        }
        if (hdcMem)
            DeleteDC(hdcMem);
    }
}
