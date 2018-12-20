using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RfidMobile.API
{
    public interface IDevice
    {
        bool ScanBarcode(out string barcode);

        void Open();

        void Close();

        void PlaySound();

        bool IsTagWrited();

        byte[] ReadTagBuff();

        string ReadTagID();

        bool WriteTagBuff(byte[] buff);
    }
}
