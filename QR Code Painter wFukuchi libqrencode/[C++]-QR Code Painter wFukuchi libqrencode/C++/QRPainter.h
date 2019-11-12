#pragma once
#include <qrencode.h>

//Class for storing bitmap bits
class CBMBits
{
public:
    CBMBits(BITMAP bm, int cx, int cy);
    virtual ~CBMBits();

    operator const VOID *() const       { return m_bits;    }
    //For low-level access to bit data
    BYTE &operator [](const size_t ix);


private:
    CBMBits();
    BYTE *m_bits;
};

class CQRPainter
{
public:
    CQRPainter();
    CQRPainter(LPCSTR data);
    CQRPainter(const std::string &data);
    virtual ~CQRPainter();

    bool InitializeData(LPCSTR data);
    void PaintData(HWND hwnd, HDC hdc, const RECT &rc);

protected:
    bool InitializeQR();
    bool InitializeBitmap(HDC hdc);
    void SetBitmapBits(HDC hdc);
    void Cleanup();

private:
    QRcode *m_qrData;
    std::string m_data;
    HBITMAP m_hbm;
};

