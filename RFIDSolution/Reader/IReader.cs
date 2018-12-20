using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reader
{
    public delegate void ReciveDataCallback(byte[] btAryReceiveData);
    public delegate void SendDataCallback(byte[] btArySendData);
    public delegate void AnalyDataCallback(MessageTran msgTran);

    public interface IReader
    {
        ReciveDataCallback ReceiveCallback;
        SendDataCallback SendCallback;
        AnalyDataCallback AnalyCallback;


        int OpenCom(string strPort, int nBaudrate, out string strException);

        void CloseCom();
    }
}
