using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HFDesk;
using System.IO;

namespace DeviceTest
{
    public partial class Form1 : Form
    {
        private Int16 st;
        private byte[] m_btTagUID = new byte[8];

        private string m_sTagUIDstring = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            Int16 iUsbPort = 100;
            ReaderInfo.icdev = common.rf_init(iUsbPort, 0);

            if (ReaderInfo.icdev > 0)
            {
                common.rf_beep(ReaderInfo.icdev, 10);

                string strLog = "读写器连接成功！";
                WriteLog(lrtxtLog, strLog, 0);

                ReaderInfo.readerConnerted = true;

                btn_write.Enabled = true;

                btn_connect.Enabled = false;

                //byte[] status = new byte[30];
                //st = common.rf_get_status(icdev, status);
                //lbHardVer.Text = System.Text.Encoding.ASCII.GetString(status);
            }
            else
            {
                string strLog = "读写器连接失败";
                WriteLog(lrtxtLog, strLog, 1);

                return;
            }
        }

        private delegate void WriteLogUnSafe(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType);
        private void WriteLog(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType)
        {
            if (this.InvokeRequired)
            {
                WriteLogUnSafe InvokeWriteLog = new WriteLogUnSafe(WriteLog);
                this.Invoke(InvokeWriteLog, new object[] { logRichTxt, strLog, nType });
            }
            else
            {
                if (nType == 0)
                {
                    logRichTxt.AppendTextEx(strLog, Color.Indigo);
                }
                else
                {
                    logRichTxt.AppendTextEx(strLog, Color.Red);
                }

                //if (ckClearOperationRec.Checked)
                //{
                    if (logRichTxt.Lines.Length > 50)
                    {
                        logRichTxt.Clear();
                    }
                //}

                logRichTxt.Select(logRichTxt.TextLength, 0);
                logRichTxt.ScrollToCaret();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_write.Enabled = false;
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbx_writen_text.Text))
            {
                return;
            }

            byte[] btData = CreateByteArray(tbx_writen_text.Text);

            if (!GetTagUID())
            {
                WriteLog(lrtxtLog, "没有发现标签！", 1);
                common.rf_beep(ReaderInfo.icdev, 20);

            }

            if (WriteData(btData))
            {
                WriteLog(lrtxtLog, "烧录成功！", 0);
                common.rf_beep(ReaderInfo.icdev, 10);
            }
            else
            {
                WriteLog(lrtxtLog, "烧录失败！", 1);
            }
        }

        private byte[] CreateByteArray(string s)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {

                    writer.Write(s);
                    writer.Close();
                }
                return stream.ToArray();
            }
        }

        private bool WriteData(byte[] dataBytes)
        {
            try
            {
                byte[] dataLen = BitConverter.GetBytes(dataBytes.Length);
                byte[] writenDataAll = new byte[dataBytes.Length + 4];

                dataLen.CopyTo(writenDataAll, 2);               //data length stored in the third and forth byte
                dataBytes.CopyTo(writenDataAll, 4);             //usefal data starts from next block--the fifth byte

                int i_totalBytes = dataBytes.Length + 4;    //UOM is byte
                st = 0;

                byte blockIndex = 0;
                int byteIndex = 0;
                while (i_totalBytes > 0 && st == 0)
                {
                    //writing data block by block
                    byte byteNumber = i_totalBytes > 4 ? (byte)4 : (byte)i_totalBytes;

                    byte[] writenData = new byte[4];//the minimum writen unit is block
                    Array.Copy(writenDataAll, byteIndex, writenData, 0, byteNumber);

                    st = ISO15693Commands.rf_writeblock(ReaderInfo.icdev, 0x22, blockIndex, (byte)1, m_btTagUID, (byte)4, writenData);

                    if (st != 0)
                    {
                        return false;
                    }

                    byteIndex += byteNumber;
                    blockIndex += 1;
                    i_totalBytes -= byteNumber;

                    System.Threading.Thread.Sleep(20);
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool GetTagUID()
        {
            //only can inventery 1 tag, because the reader is a shit
            UInt16 byteLen = 0;
            byte[] ary_data = new byte[9];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                int loop = 0;
                bool stopLoop = false;

                /*
                * loop 30 second to find tag
                */
                while (loop < 300 && !stopLoop)
                {
                    st = ISO15693Commands.rf_inventory(ReaderInfo.icdev, 0x36, 0x00, 0x00, out byteLen, ary_data);

                    stopLoop = st == 0 ? true : false;

                    loop++;

                    System.Threading.Thread.Sleep(100);
                }

                if (st != 0)
                {
                    //MessageBox.Show("未发现单个标签");
                    return false;
                }
                else
                {
                    Array.Copy(ary_data, 1, m_btTagUID, 0, 8);

                    byte[] msbFstUID = new byte[8];
                    Array.Copy(m_btTagUID, msbFstUID, 8);
                    Array.Reverse(msbFstUID);

                    m_sTagUIDstring = CCommondMethod.ByteArrayToString(msbFstUID, 0, 8);

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
