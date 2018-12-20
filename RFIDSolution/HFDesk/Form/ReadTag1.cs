using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RFIDService.ClientData;
using System.IO;
using CustomControl;
using HFDesk.helpClass;

namespace HFDesk
{
    public partial class ReadTag1 : Form
    {
        private byte[] tagUIDbyte = new byte[8];
        private byte[] readBuffer = null;

        Dictionary<string, RFIDConstants> dic_customer_RFID_constants = new Dictionary<string, RFIDConstants>();

        private string ms_cfg_mfg_country = "";
        private string ms_iec_date = "";
        private string ms_iec_verfy = "";
        private string ms_iso = "";
        private string ms_cfg_mfg_name = "";
        private string ms_producttype = "";

        private Int16 st;

        private string tagUIDstring;

        public static bool _read_tag_timer_enable = true;

        private static System.Threading.Timer _ReadTimer = null;

        private ModuleInfo oModuleInfo = null;

        string last_uid = "";

        public ReadTag1()
        {
            InitializeComponent();
        }

        private void ReadTag1_Load(object sender, EventArgs e)
        {
            /*
             * open reader if not connected
             */

            //ms_cfg_mfg_name = System.Configuration.ConfigurationManager.AppSettings["mfg_name"];
            //ms_cfg_mfg_country = System.Configuration.ConfigurationManager.AppSettings["mfg_country"];
            //ms_iec_date = System.Configuration.ConfigurationManager.AppSettings["iec_date"];
            //ms_iec_verfy = System.Configuration.ConfigurationManager.AppSettings["iec_verfy"];
            //ms_iso = System.Configuration.ConfigurationManager.AppSettings["iso"];
            //ms_producttype = System.Configuration.ConfigurationManager.AppSettings["producttype"];


            if (!ReaderInfo.readerConnerted)
            {
                Int16 iUsbPort = 100;
                ReaderInfo.icdev = common.rf_init(iUsbPort, 0);

                if (ReaderInfo.icdev > 0)
                {
                    common.rf_beep(ReaderInfo.icdev, 10);

                    string strLog = "读写器连接成功！";
                    WriteLog(lrtxtLog, strLog, 0);

                    ReaderInfo.readerConnerted = true;

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
            else
            {
                string strLog = "读写器连接成功！";
                WriteLog(lrtxtLog, strLog, 0);
            }

            if (ReaderInfo.readerConnerted)
            {
                /*
             * read rfid tag every 2 second
             */
                _ReadTimer = null;
                _ReadTimer = new System.Threading.Timer(new TimerCallback(ReadRfidTimerCallback), null, 2000, Timeout.Infinite);
                //}
                //else
                //{
                //    _ReadTimer.Change(2000, Timeout.Infinite);
                //}
                InitRFIDConstants();
            }
        }

        /// <summary>
        /// get rfid constants from config file
        /// </summary>
        private void InitRFIDConstants()
        {
            try
            {
                dic_customer_RFID_constants.Clear();

                string valid_cusomers = DealINI.IniReadValue("CUSTOMERS", "valid_customer");

                string[] customers = valid_cusomers.Split('|');

                foreach (string item in customers)
                {
                    string mfg_country = DealINI.IniReadValue(item, "mfg_country");
                    string mfg_name = DealINI.IniReadValue(item, "mfg_name");
                    string iec_date = DealINI.IniReadValue(item, "iec_date");
                    string iec_verfy = DealINI.IniReadValue(item, "iec_verfy");
                    string iso = DealINI.IniReadValue(item, "iso");
                    string producttype = DealINI.IniReadValue(item, "producttype");

                    RFIDConstants rfidConstant = new RFIDConstants(mfg_country, mfg_name, iec_date, iec_verfy, iso, producttype);

                    dic_customer_RFID_constants.Add(item, rfidConstant);

                }

            }
            catch (Exception)
            {
                MessageBox.Show("解析配置文件出错，请检查配置文件!!!");
            }


        }

        public ModuleInfo ParserTagData(byte[] tagBuff)
        {
            try
            {
                MemoryStream memStream = new MemoryStream(tagBuff);
                BinaryReader buffReader = new BinaryReader(memStream);

                if (buffReader.ReadString() != "@@")
                {
                    throw new Exception("数据包开始标志出错");
                }

                string customer = buffReader.ReadString();
                string strProductType = buffReader.ReadString();
                string strModule_ID = buffReader.ReadString();
                DateTime packingDate = DateFormInt16(buffReader.ReadInt16());
                decimal dPmax = (decimal)buffReader.ReadInt32() / 100M;
                decimal dVoc = (decimal)buffReader.ReadInt16() / 100M;
                decimal dIsc = (decimal)buffReader.ReadInt16() / 100M;
                decimal dVpm = (decimal)buffReader.ReadInt16() / 100M;
                decimal dIpm = (decimal)buffReader.ReadInt16() / 100M;
                decimal ff = (decimal)buffReader.ReadInt16() / 100M;
                DateTime cellDate = DateFormInt16(buffReader.ReadInt16());
                
                //ms_cfg_mfg_country = buffReader.ReadString();
                //ms_iec_date = buffReader.ReadString();
                //ms_iec_verfy = buffReader.ReadString();
                //ms_iso = buffReader.ReadString();
                //_Producer = buffReader.ReadString();
            
                string packetEnd = buffReader.ReadString();
                
                if (packetEnd != "##")
                {
                    throw new Exception("数据包结束标志出错");
                }
                else
                {
                    ModuleInfo rfidTag = new ModuleInfo();
                    rfidTag.customer = customer;
                    rfidTag.ProductType = strProductType;
                    rfidTag.Module_ID = strModule_ID;
                    rfidTag.PackedDate = packingDate.ToString("yyyy-MM");
                    rfidTag.Pmax = dPmax.ToString();
                    rfidTag.Voc = dVoc.ToString();
                    rfidTag.Isc = dIsc.ToString();
                    rfidTag.Vpm = dVpm.ToString();
                    rfidTag.Ipm = dIpm.ToString();
                    rfidTag.CellDate = cellDate.ToString("yyyy-MM");


                    rfidTag.FF = ff.ToString();
                    rfidTag.cell_supplier_country = ms_cfg_mfg_country;
                    rfidTag.iec_date = ms_iec_date;
                    rfidTag.iec_verfy = ms_iec_verfy;
                    rfidTag.iso = ms_iso;
                    rfidTag.mfg_name = ms_cfg_mfg_name;

                    SetRFIDConstants(customer);


                    return rfidTag;
                }


                //short length = buffReader.ReadInt16();
                //I_V_Point[] pointArray = new I_V_Point[length];
                //for (int i = 0; i < length; i++)
                //{
                //    pointArray[i] = new I_V_Point();
                //    pointArray[i].Current = buffReader.ReadInt32() * 1.0 / 10000000;
                //    pointArray[i].Voltage = buffReader.ReadInt32() * 1.0 / 10000000;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("解析数据包出错：\r\n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// set rfid's constants by customer
        /// </summary>
        private void SetRFIDConstants(string customer)
        {
            ms_cfg_mfg_name = "";
            ms_cfg_mfg_country = "";
            ms_iec_date = "";
            ms_iec_verfy = "";
            ms_iso = "";
            ms_producttype = "";

            if (dic_customer_RFID_constants.ContainsKey(customer))
            {
                RFIDConstants rc = dic_customer_RFID_constants[customer];

                ms_cfg_mfg_name = rc.mfg_name;
                ms_cfg_mfg_country = rc.mfg_country;
                ms_iec_date = rc.iec_date;
                ms_iec_verfy = rc.iec_verfy;
                ms_iso = rc.iso_desc;
                ms_producttype = rc.product_type;
            }

        }

        private DateTime DateFormInt16(short days)
        {
            return DateTime.Parse("2016-01-01").AddDays(days);
        }

        private void ReadRfidTimerCallback(object sender)
        {
            if (_read_tag_timer_enable)
            {
                common.rf_beep(ReaderInfo.icdev, 10);

                if (GetTagUID())
                {
                    if (last_uid != tagUIDstring)
                    {
                        last_uid = tagUIDstring;
                    }
                    else
                    {
                        _ReadTimer.Change(2000, Timeout.Infinite);
                        return;
                    }

                    System.Threading.Thread.Sleep(20);

                    ErrorCode ec = ReadData();
                    switch (ec)
                    {
                        case ErrorCode.CanNotFindTag:
                            paintBackgroundColor(statusType.FAIL);
                            common.rf_beep(ReaderInfo.icdev, 20);
                            string str = "无法找到标签，请重试！";
                            WriteLog(lrtxtLog, str, 1);
                            break;
                        case ErrorCode.OtherException:
                            paintBackgroundColor(statusType.FAIL);
                            common.rf_beep(ReaderInfo.icdev, 20);
                            str = "其他异常，请重试";
                            WriteLog(lrtxtLog, str, 1);
                            break;
                        case ErrorCode.ReadFail:
                            paintBackgroundColor(statusType.FAIL);
                            common.rf_beep(ReaderInfo.icdev, 20);
                            str = "读取失败，请重试！";
                            WriteLog(lrtxtLog, str, 1);
                            break;
                        case ErrorCode.ReadSuccessful:
                            paintBackgroundColor(statusType.PASS);
                            //common.rf_beep(ReaderInfo.icdev, 10);
                            //string storedDataString = Encoding.ASCII.GetString(readBuffer);
                            //WriteLog(lrtxtLog, storedDataString, 0);
                            oModuleInfo = ParserTagData(readBuffer);
                            ShowIVCurves(double.Parse(oModuleInfo.Isc), double.Parse(oModuleInfo.Ipm), double.Parse(oModuleInfo.Vpm), double.Parse(oModuleInfo.Voc));
                            ShowModuleInfo(true);
                            break;
                        case ErrorCode.TagHasNoData:
                            paintBackgroundColor(statusType.FAIL);
                            common.rf_beep(ReaderInfo.icdev, 20);
                            str = "空标签！";
                            WriteLog(lrtxtLog, str, 1);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //clear all content
                    last_uid = "";
                    ShowModuleInfo(false);
                    ivCurves1.SetOriginalPoints(null, true);
                }
            }

            _ReadTimer.Change(2000, Timeout.Infinite);
        }

        private void ShowIVCurves(double isc, double ipm, double vpm, double voc)
        {
            I_V_Point[] oriPointArray = new I_V_Point[3];
            I_V_Point ivp = new I_V_Point(isc, 0);
            oriPointArray[0] = ivp;
            ivp = new I_V_Point(ipm, vpm);
            oriPointArray[1] = ivp;
            ivp = new I_V_Point(0, voc);
            oriPointArray[2] = ivp;
            ivCurves1.SetOriginalPoints(oriPointArray, true);
        }


        private void btn_readTag_Click(object sender, EventArgs e)
        {
            //paintBackgroundColor(statusType.START);

            //if (!GetTagUID())
            //{
            //    WriteLog(lrtxtLog, "没有发现标签！", 1);

            //    common.rf_beep(ReaderInfo.icdev, 20);

            //    paintBackgroundColor(statusType.FAIL);
            //    return;
            //}
            //else
            //{
            //    System.Threading.Thread.Sleep(20);

            //    ErrorCode ec = ReadData();
            //    switch (ec) { 
            //        case ErrorCode.CanNotFindTag:
            //            paintBackgroundColor(statusType.FAIL);
            //            common.rf_beep(ReaderInfo.icdev, 20);
            //            string str = "无法找到标签，请重试！";
            //            WriteLog(lrtxtLog, str, 1);
            //            break;
            //        case ErrorCode.OtherException:
            //            paintBackgroundColor(statusType.FAIL);
            //            common.rf_beep(ReaderInfo.icdev, 20);
            //            str = "其他异常，请重试";
            //            WriteLog(lrtxtLog, str, 1);
            //            break;
            //        case ErrorCode.ReadFail:
            //            paintBackgroundColor(statusType.FAIL);
            //            common.rf_beep(ReaderInfo.icdev, 20);
            //            str = "读取失败，请重试！";
            //            WriteLog(lrtxtLog, str, 1);
            //            break;
            //        case ErrorCode.ReadSuccessful:
            //            paintBackgroundColor(statusType.PASS);
            //            //common.rf_beep(ReaderInfo.icdev, 10);
            //            oModuleInfo = ParserTagData(readBuffer);
            //            string storedDataString = Encoding.ASCII.GetString(readBuffer);
            //            WriteLog(lrtxtLog, storedDataString, 0);
            //            ShowModuleInfo(true);
            //            break;
            //        case ErrorCode.TagHasNoData:
            //            paintBackgroundColor(statusType.FAIL);
            //            common.rf_beep(ReaderInfo.icdev, 20);
            //            str = "空标签！";
            //            WriteLog(lrtxtLog, str, 1);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        private enum ErrorCode { 
            ReadSuccessful,
            ReadFail,
            TagHasNoData,
            CanNotFindTag,
            OtherException,
        }

        private ErrorCode ReadData()
        {
            // the application note says, max block number per read is 10 blocks.
            try
            {
                byte[] rtnData = new byte[4];    //read first block, get the data length
                byte rtnLen = 0;
                st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, 0x00, (byte)1, tagUIDbyte, out rtnLen, rtnData);
                if (st != 0)
                {
                    //MessageBox.Show("error");
                    return ErrorCode.ReadFail;
                }
                else
                {
                    //bool b_readLengthData = true;

                    byte[] lenthData = new byte[2];
                    //the first two bytes stored data length
                    Array.Copy(rtnData, 2, lenthData, 0, 2);

                    Int32 i_totalBytes = BitConverter.ToInt16(lenthData, 0) + 4;

                    if (i_totalBytes==4)
                    {
                        return ErrorCode.TagHasNoData;
                    }

                    readBuffer = new byte[i_totalBytes - 4];



                    st = 0;
                    byte blockIndex = 1;
                    int byteIndex = 0;

                    while (i_totalBytes > 0 && st == 0)
                    {
                        byte blockLen = 0;
                        //if (i_totalBytes % 4 == 0)
                        //{
                        //    blockLen = (byte)(i_totalBytes / 4);
                        //}
                        //else
                        //{
                        //    blockLen = (byte)(i_totalBytes / 4 + 1);
                        //}

                        blockLen = (byte)((i_totalBytes + 3) / 4);

                        //calculate block number required, max number is 10
                        byte blockNumber = blockLen > (byte)10 ? (byte)10 : blockLen;

                        //byte byteNumber = 0;

                        byte[] readData = new byte[blockNumber * 4];

                        st = ISO15693Commands.rf_readblock(ReaderInfo.icdev, 0x22, blockIndex, blockNumber, tagUIDbyte, out rtnLen, readData);

                        if (st == 0)
                        {
                            int leftDataLength = readBuffer.Length - byteIndex;
                            int copyDataLength = leftDataLength > readData.Length ? readData.Length : leftDataLength;

                            //if (b_readLengthData)
                            //{
                            //    Array.Copy(readData, 2, readBuffer, byteIndex, copyDataLength == readData.Length ? copyDataLength - 2 : copyDataLength);

                            //    //b_readLengthData = true;
                            //}
                            //else
                            //{
                                Array.Copy(readData, 0, readBuffer, byteIndex, copyDataLength);
                            //}
                        }
                        else
                        {
                            return ErrorCode.ReadFail;
                        }

                        byteIndex += rtnLen;
                        //if (b_readLengthData)
                        //{
                        //    byteIndex -= 2;

                        //    b_readLengthData = false;
                        //}
                        blockIndex += blockNumber;
                        i_totalBytes -= rtnLen;

                        System.Threading.Thread.Sleep(20);
                    }

                    return ErrorCode.ReadSuccessful;

                }
            }
            catch (Exception)
            {
                return ErrorCode.OtherException;
            }
        }

       

        private bool GetTagUID()
        {
            //only can inventery 1 tag, because the reader is a shit
            UInt16 byteLen = 0;
            byte[] ary_data = new byte[9];    //the first byte is DSFID, and the other 8 byte containers the UID data
            try
            {
                st = ISO15693Commands.rf_inventory(ReaderInfo.icdev, 0x36, 0x00, 0x00, out byteLen, ary_data);
                if (st != 0)
                {
                    //MessageBox.Show("未发现单个标签");
                    return false;
                }
                else
                {
                    Array.Copy(ary_data, 1, tagUIDbyte, 0, 8);

                    byte[] msbFstUID = new byte[8];
                    Array.Copy(tagUIDbyte, msbFstUID, 8);
                    Array.Reverse(msbFstUID);

                    tagUIDstring = CCommondMethod.ByteArrayToString(msbFstUID, 0, 8);

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region control ui handler
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

                if (ckClearOperationRec.Checked)
                {
                    if (logRichTxt.Lines.Length > 50)
                    {
                        logRichTxt.Clear();
                    }
                }

                logRichTxt.Select(logRichTxt.TextLength, 0);
                logRichTxt.ScrollToCaret();
            }
        }

        enum statusType
        {
            START,
            PASS,
            FAIL
        }

        private delegate void paintBackgroundColorDlgt(statusType st);
        private void paintBackgroundColor(statusType st)
        {
            if (this.InvokeRequired)
            {
                paintBackgroundColorDlgt InvokepaintBackgroundColor = new paintBackgroundColorDlgt(paintBackgroundColor);
                this.Invoke(InvokepaintBackgroundColor, new object[] { st });
            }
            else
            {
                switch (st)
                {
                    case statusType.START:
                        this.BackColor = Color.White;
                        break;
                    case statusType.FAIL:
                        this.BackColor = Color.Red;
                        break;
                    case statusType.PASS:
                        this.BackColor = Color.LightGreen;
                        break;
                    default:
                        break;
                }

                this.Refresh();
            }
        }

        private delegate void EnableControlDlgt(int btnIndex);
        private void EnableControl(int idx)
        {
            if (this.InvokeRequired)
            {
                EnableControlDlgt InvokeEnableControl = new EnableControlDlgt(EnableControl);
                this.Invoke(InvokeEnableControl, new object[] { idx });
            }
            else
            {
                if (idx == 1)
                {
                    //tbx_SerialWrite.Enabled = true;
                }
                else if (idx == 2)
                {
                    //tbx_readSerial.Enabled = true;
                }
            }
        }

        private delegate void ShowModuleInfoDlgt(bool bSuccess);
        private void ShowModuleInfo(bool bSuccess)
        {
            if (this.InvokeRequired)
            {
                ShowModuleInfoDlgt InvokeShowModuleInfo = new ShowModuleInfoDlgt(ShowModuleInfo);
                this.Invoke(InvokeShowModuleInfo, new object[] { bSuccess });
            }
            else
            {
                try
                {
                    if (!bSuccess)
                    {
                        tbx_status.Text = "";
                        tbx_SerialWrite.Text = "";
                        tbx_celldate.Text = "";
                        tbx_ipm.Text = "";
                        tbx_isc.Text = "";
                        tbx_packdate.Text = "";
                        tbx_pmax.Text = "";
                        tbx_prodtype.Text = "";
                        tbx_voc.Text = "";
                        tbx_vpm.Text = "";
                        tbx_ff.Text = "";
                        textBox2.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    else
                    {
                        tbx_SerialWrite.Text = oModuleInfo.Module_ID;
                        tbx_celldate.Text = oModuleInfo.CellDate;
                        tbx_ipm.Text = oModuleInfo.Ipm;
                        tbx_isc.Text = oModuleInfo.Isc;
                        tbx_packdate.Text = oModuleInfo.PackedDate;
                        tbx_pmax.Text = oModuleInfo.Pmax;
                        tbx_prodtype.Text = oModuleInfo.ProductType;
                        tbx_voc.Text = oModuleInfo.Voc;
                        tbx_vpm.Text = oModuleInfo.Vpm;
                        tbx_ff.Text = oModuleInfo.FF;


                        textBox2.Text = ms_cfg_mfg_name;
                        textBox5.Text = ms_cfg_mfg_country;
                        textBox1.Text = ms_iec_date;
                        textBox3.Text = ms_iec_verfy;
                        textBox4.Text = ms_iso;

                        tbx_status.Text = "";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发生异常："+ex.Message);
                }
                

            }
        }

        #endregion

        private void ReadTag1_Activated(object sender, EventArgs e)
        {
            ReadTag1._read_tag_timer_enable = true;
        }

        private void ReadTag1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ReadTimer != null)
            {
                _ReadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

       
        
    }
}
