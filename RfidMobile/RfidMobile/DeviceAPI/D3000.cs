using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HelpClasses;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;


namespace RfidMobile.API
{
    public class D3000 : IDevice
    {
        #region device api
        [DllImport("DeviceAPI.dll", EntryPoint = "Barcode1D_init")]
        private static extern bool Barcode1D_init();

        [DllImport("DeviceAPI.dll", EntryPoint = "Barcode1D_scan")]
        private static extern int Barcode1D_scan(byte[] pszData);

        [DllImport("DeviceAPI.dll", EntryPoint = "Barcode1D_free")]
        private static extern void Barcode1D_free();

        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ISO15693_init")]
        private static extern bool RF_ISO15693_init();

        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ISO15693_free")]
        private static extern void RF_ISO15693_free();
        /// <summary>
        /// RFID模式切换（需执行该命令之后才可进行相应卡操作，默认ISO14443A模式）
        /// </summary>
        /// <param name="iMode">iMode：0表示ISO14443A；1表示ISO14443B；2表示ISO15693</param>
        /// <returns>0：   成功;其他：失败</returns>
        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ModeSwitch")]
        private static extern int RF_ModeSwitch(int iMode);
        /// <summary>
        /// 读取卡片UID
        /// </summary>
        /// <param name="iMode">输入：0 多张卡呼叫 不带AFI；1 单张卡呼叫 不带AFI；2 多张卡呼叫 带AFI；3 单张卡呼叫 带AFI</param>
        /// <param name="iAFI">输入：iAFI ： AFI值</param>
        /// <param name="pszData">输出：pszData：UID内容 ；UID为8字节，pszData建议11个字节</param>
        /// <returns></returns>；
        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ISO15693_inventory")]
        private static extern int RF_ISO15693_inventory(int iMode, int iAFI, byte[] pszData);
        /// <summary>
        /// 读取卡片内部数据
        /// </summary>
        /// <param name="iMode">输入iMode：（值范围为 0-7）	0  非SELECT状态，不传UID	NXP I CODE SLI 标签	
        ///1  SELECT状态，不传UID     NXP I CODE SLI 标签
        ///2  非SELECT状态，传UID     NXP I CODE SLI 标签 
        ///3  SELECT状态，传UID        NXP I CODE SLI 标签
        ///4  非SELECT状态 不传UID     TI 标签
        ///5  SELECT状态    不传UID    TI 标签
        ///6  非SELECT状态  传UID     TI 标签
        ///7  SELECT状态    传UID      TI 标签</param>
        /// <param name="pszUID">输入：pszUID：UID</param>
        /// <param name="iLenUID">输入：iLenUID：UID长度</param>
        /// <param name="startblock">输入：Startblock：起始块号</param>
        /// <param name="blocknum">输入：Blocknum：一共读取多少块（1-10）</param>
        /// <param name="pszData">输出：pszData 返回数据信息建议一次最多只读10个块，每个块有4个字节，pszData建议41字节</param>
        /// <returns>0：   成功;其他：失败</returns>
        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ISO15693_read_sm")]
        private static extern int RF_ISO15693_read_sm(int iMode, byte[] pszUID, int iLenUID, int startblock, int blocknum, byte[] pszData);
        /// <summary>
        /// 向卡片内部写入数据
        /// </summary>
        /// <param name="iMode">输入iMode：（值范围为 0-7）	0  非SELECT状态，不传UID	NXP I CODE SLI 标签	
        ///1  SELECT状态，不传UID     NXP I CODE SLI 标签
        ///2  非SELECT状态，传UID     NXP I CODE SLI 标签 
        ///3  SELECT状态，传UID        NXP I CODE SLI 标签
        ///4  非SELECT状态 不传UID     TI 标签
        ///5  SELECT状态    不传UID    TI 标签
        ///6  非SELECT状态  传UID     TI 标签
        ///7  SELECT状态    传UID      TI 标签</param>
        /// <param name="pszUID">UID</param>
        /// <param name="iLenUID">UID长度</param>
        /// <param name="startblock">起始块号</param>
        /// <param name="blocknum">一共写入多少块（每块4字节）</param>
        /// <param name="pszData">写入的数据信息（建议一次最多10个块，40个字节）</param>
        /// <param name="iWriteLen">iWriteLen：写入的数据长度（4字节的整数倍）</param>
        /// <returns>0：   成功;其他：失败</returns>
        [DllImport("DeviceAPI.dll", EntryPoint = "RF_ISO15693_write_sm")]
        private static extern int RF_ISO15693_write_sm(int iMode, byte[] pszUID, int iLenUID, int startblock, int blocknum, byte[] pszData, int iWriteLen);
        #endregion
        
        private StringBuilder buffBuilder = new StringBuilder();
        //private delegate void addNewData(string msg);

        #region interface functions
        public bool ScanBarcode(out string barcode)
        {
            byte[] pszData = new byte[64];
            int iRes = Barcode1D_scan(pszData);

            if (iRes > 0)
            {
                barcode = System.Text.Encoding.GetEncoding(0).GetString(pszData, 0, iRes).Trim();

                if (barcode.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                barcode = string.Empty;
                return false;
            }
        }

        public void Open()
        {
            /*
             *  1. init module
             *  2. change model to 15693 protocal
             *  3. init barcode scanner
             */
            if (!RF_ISO15693_init() || RF_ModeSwitch(2) != 0 || !Barcode1D_init())
            {
                throw new Exception("D3000.Open() Fail");
            }
        }

        public void Close()
        {
            try
            {
 
                RF_ISO15693_free();
                Barcode1D_free();
            }
            catch (Exception)
            {
                throw new Exception("D3000.Close() Fail");
            }
           
        }

        public void PlaySound()
        {
            string filePath = @"\windows\Barcodebeep.wav";
            try
            {
                if (File.Exists(filePath))
                {

                    SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
                
            }
            catch { }
        }

        public bool WriteTagBuff(byte[] usefulData)
        {
            try
            {
                byte[] lengthData = BitConverter.GetBytes(usefulData.Length);        //the useful data length, stored in the third and forth byte
                byte[] writenDataAll = new byte[usefulData.Length + 4];

                lengthData.CopyTo(writenDataAll, 2);
                usefulData.CopyTo(writenDataAll, 4);             //usefal data starts from next block--the fifth byte

                int i_totalBytes = usefulData.Length + 4;    //UOM is byte
                int iRes = 0;

                byte blockIndex = 0;
                int byteIndex = 0;
                while (i_totalBytes > 0 && iRes == 0)
                {
                    //writing data block by block
                    byte byteNumber = i_totalBytes > 4 ? (byte)4 : (byte)i_totalBytes;

                    byte[] writenData = new byte[4];//the minimum writen unit is block
                    Array.Copy(writenDataAll, byteIndex, writenData, 0, byteNumber);

                    iRes = RF_ISO15693_write_sm(0, null, 0, blockIndex, 1, writenData, 4);

                    if (iRes != 0)
                    {
                        return false;
                    }

                    byteIndex += byteNumber;
                    blockIndex += 1;
                    i_totalBytes -= byteNumber;

                    System.Threading.Thread.Sleep(10);
                }

                //PlaySound();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] ReadTagBuff()
        {
            byte[] ret = null;

            try
            {

                StringBuilder buffBuilder = new StringBuilder();
                short buffLen = 0;
                //short version = 0;

                string tagUID = "";
                if (SearchSingleTag(out tagUID))
                {
                    if (tagUID.Length <= 1)
                    {
                        throw new Exception("读取到空白标签信息");
                    }

                    string blockStringData = "";
                    byte[] blockBytes;
                    if (ReadBlockData(0, 1, out blockStringData) == false)
                    {
                        throw new Exception("读取标签信息失败,请放置好标签");
                    }
                    //if (D3000_CLR_ISO15693.dataSingleBlcok.Trim().Length <= 1)
                    //{
                    //    throw new Exception("读取到空白标签信息");
                    //}
                    else
                    {
                        blockBytes = StreamBuilder.HexToByteArray(blockStringData.Trim());
                        //version = BitConverter.ToInt16(blockBytes, 0);
                        buffLen = BitConverter.ToInt16(blockBytes, 2);
                    }

                    int blockNum = (buffLen + 3) / 4;

                    for (int i = 0; i < blockNum; i++)
                    {
                        int iReadBlock = 1;
                        string str = "";
                        if (ReadBlockData(i + 1, iReadBlock,out str) == false)
                        {
                            throw new Exception("读取标签信息失败,请放置好标签");
                        }
                        if (str.Trim().Length >= 1)
                        {
                            buffBuilder.Append(str.Trim());
                        }
                    }

                }
                byte[] tagBuff = StreamBuilder.HexToByteArray(buffBuilder.ToString().Trim());
                ret = new byte[buffLen];
                Array.Copy(tagBuff, ret, buffLen);
                
                //PlaySound();
                return ret;
            }
            catch (Exception)
            {
                return ret;
            }

        }

        public string ReadTagID()
        {
            string tagId = "";

            //本次扫描的TagID
            if (SearchSingleTag(out tagId))
            {
                try
                {
                    if (tagId.Length <= 1)
                    {
                        return "";
                    }
                }
                catch (Exception)
                {
                    tagId = "";
                }

            }

            return tagId;
        }

        public bool IsTagWrited()
        {
            string tagHead = "";
            string tag = "";
            if (SearchSingleTag(out tag))
            {
                if (tag.Length <= 1)
                {
                    return false;
                }

                if (ReadBlockData(1, 1, out tagHead) == false)
                {
                    throw new Exception("读取标签信息失败,请放置好标签");
                }
            }

            byte[] headBuff = StreamBuilder.HexToByteArray(tagHead);
            //判断是否以 "@@"字符串开头
            string sValue = System.Text.Encoding.ASCII.GetString(headBuff, 0, headBuff.Length);

            if (sValue.IndexOf("@@") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        /// <summary>
        /// Scan UID  寻卡，模式为：单卡不带AFI
        /// </summary>
        /// <returns></returns>
        private bool SearchSingleTag(out string tagUID)
        {
            byte[]  tagIDbytes=new byte[64];
            //data_Single_UID = new byte[64];  //   64/4=16
            //寻卡指令   RF_ISO15693_inventory(1, 0, data_Single_UID);
            int iRes = RF_ISO15693_inventory(1, 0, tagIDbytes);

            if (iRes == 0x00)
            {
                /*
                 * the first 3 byte?
                 */
                //tagUID = BitConverter.ToString(tagIDbytes, 3, 8).Replace("-", "");

                byte[] tagUIDbyte = new byte[8];
                Array.Copy(tagIDbytes, 3, tagUIDbyte, 0, 8);

                byte[] msbFstUID = new byte[8];
                Array.Copy(tagUIDbyte, msbFstUID, 8);
                Array.Reverse(msbFstUID);

                tagUID = BitConverter.ToString(msbFstUID, 0, 8).Replace("-", "");

                return true;
            }
            else
            {
                tagUID = string.Empty;
                return false;
            }
        }


        /// <summary>
        /// scan UID        寻卡 模式为：多卡，不带AFI
        /// </summary>
        /// <returns></returns>
        private bool SearchMultipleTags(out string[] tagUID)
        {
            byte[] tagBytes = new byte[255];
            int iRes = RF_ISO15693_inventory(0, 0, tagBytes);

            if (iRes == 0x00)
            {
                int iDataCount = tagBytes[0] / 10;         //每一张UID的数据量为10
                tagUID = new string[iDataCount];
                for (int i = 0; i < iDataCount; i++)
                {
                    tagUID[i] = BitConverter.ToString(tagBytes, 3 + i * 10, 8).Replace("-", "");
                }
                return true;
            }
            else
            {
                tagUID = null;
                return false;
            }

        }

        /// <summary>
        /// transfor  string to byte ,data's length is 8    将字符转换成byte类型。字符长度长于8的，取低8位，字符不满8位的，高位补零
        /// </summary>
        /// <param name="dataToWrite"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool getByteData(string dataToWrite, out byte[] data)
        {
            try
            {
                data = new byte[4];
                string[] dataStr = new string[] { "00", "00", "00", "00" };
                int L = dataToWrite.Length;
                if (L > 8)
                {
                    dataToWrite = dataToWrite.Substring(L - 8, 8);
                }
                if (L < 8)
                {
                    for (int i = L; i < 8; i++)
                    {
                        dataToWrite = "0" + dataToWrite;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    dataStr[i] = dataToWrite.Substring(2 * i, 2);
                    data[i] = Convert.ToByte(dataStr[i], 16);

                }
                return true;
            }
            catch (Exception)
            {
                data = new byte[] { 0, 0, 0, 0 };
                return false;
            }
        }


        /// <summary>
        ///read data in block ,block's data is dataSingleBlcok 读取指定block的数据 数据内容保存在dataSingleBlcok中，不指定UID的模式
        /// </summary>
        /// <param name="iblock"></param>
        /// <returns></returns>
        private  bool ReadBlockData(int startBlock, int iblockNum, out string rtnStringData)
        {
            byte[] pszData = new byte[255];
            int iRes = RF_ISO15693_read_sm(0, null, 0, startBlock, iblockNum, pszData);

            if (iRes == 0x00)
            {
                rtnStringData = BitConverter.ToString(pszData, 1, pszData[0]).Replace("-", "");
                return true;
            }
            else
            {
                rtnStringData = string.Empty;
                return false;
            }

        }

        
    }

    
}
