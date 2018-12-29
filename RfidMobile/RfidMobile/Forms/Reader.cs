using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RfidMobile.Classes;
using RfidControl;

namespace RfidMobile
{
    public partial class Reader : Form
    {
        private static System.Threading.Timer _ReadTimer = null;

        //Dictionary<string, RFIDConstants> dic_customer_RFID_constants = new Dictionary<string, RFIDConstants>();

        public static bool _read_tag_timer_enable = true;

        private string m_sLastReadTagId = "";

        private ModuleInfo m_objModuleInfo = null;

        public Reader()
        {
            InitializeComponent();
        }

        private void Reader_Load(object sender, EventArgs e)
        {
            m_sLastReadTagId = "";
            /*
             * read rfid tag every 2 second
             */
            _ReadTimer = null;
            _ReadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(ReadRfidTimerCallback), null, 2000, Timeout.Infinite);

        }

        private void OnChangePanel(object sender, EventArgs e)
        {
            if (sender == btnBasicInfo)
            {
                panelBasicInfo.Visible = true;
                panelBasicInfo.Left = 2;
                panelCurves.Visible = false;
            }
            else
            {
                panelBasicInfo.Visible = false;
                panelCurves.Visible = true;
                panelCurves.Left = 2;
            }
        }

        /*
         * timer callback function, read Tag every 2 seconds
         */
        private void ReadRfidTimerCallback(object sender)
        {

            if (_read_tag_timer_enable)
            {
                try
                {
                    string tagId = ReaderConfig._rfidReader.ReadTagID();

                    if (!string.IsNullOrEmpty(tagId))
                    {
                        if (m_sLastReadTagId != tagId)
                        {
                            m_sLastReadTagId = tagId;
                        }
                        else
                        {
                            _ReadTimer.Change(2000, Timeout.Infinite);
                            return;
                        }

                        System.Threading.Thread.Sleep(20);

                        byte[] buff = ReaderConfig._rfidReader.ReadTagBuff();

                        if (buff != null)
                        {
                            m_objModuleInfo = TagDataFormat.ParserTagData(buff);

                            SetRFIDConstants(m_objModuleInfo.customer);

                            ChangeState(SystemState.ShowModuleInfo, m_objModuleInfo);

                            ReaderConfig._rfidReader.PlaySound();
                        }

                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("Current");
                        //dt.Columns.Add("Voltage");
                        //dt.TableName = "dtPointsInfo";
                        //for (int i = 0; i < tag.PointArray.Length; i++)
                        //{
                        //    DataRow dr = dt.NewRow();
                        //    dr["Current"] = tag.PointArray[i].Current * 10000000;
                        //    dr["Voltage"] = tag.PointArray[i].Voltage * 10000000;
                        //    dt.Rows.Add(dr);
                        //}
                        //DataSet ds = new DataSet();
                        //ds.Tables.Add(dt);

                        //object[] args2 = new object[4];
                        //args2[0] = tagId;
                        //args2[1] = tag.SerialNo;
                        //args2[2] = tag.Profile;
                        //args2[3] = ds.GetXml();
                        //this.Invoke(new ReadLogDelegate(ReadLog), args2);
                        //dt = null; ds = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作异常，原因：" + ex.Message, "异常");
                    //FileLog.WriteError(ex.Message);
                }
            }
            

            _ReadTimer.Change(2000, Timeout.Infinite);
        }

        /// <summary>
        /// set rfid's constants by customer
        /// </summary>
        private void SetRFIDConstants(string customer)
        {
            if (ReaderConfig._dic_rfidConstants.ContainsKey(customer))
            {
                RFIDConstants rc = ReaderConfig._dic_rfidConstants[customer];

                m_objModuleInfo.mfg_name = rc.mfg_name;
                m_objModuleInfo.cell_supplier_country = rc.mfg_country;
                m_objModuleInfo.iec_date = rc.iec_date;
                m_objModuleInfo.iec_verfy = rc.iec_verfy;
                m_objModuleInfo.iso = rc.iso_desc;
                m_objModuleInfo.ProductType = rc.product_type;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_ReadTimer != null)
            {
                _ReadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            this.DialogResult = DialogResult.Cancel;
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


        private delegate void ChangeStateDlgt(SystemState ss, ModuleInfo o);
        private void ChangeState(SystemState ss, ModuleInfo o)
        {
            if (this.InvokeRequired)
            {
                ChangeStateDlgt InvokeChangeState = new ChangeStateDlgt(ChangeState);
                this.Invoke(InvokeChangeState, new object[] { ss, o });
            }
            else
            {
                switch (ss)
                {
                    case SystemState.ShowModuleInfo:

                        txtManufacturer.Text = "ZNSHINE";// string.IsNullOrEmpty(o.mfg_name) ? "" : o.mfg_name;
                        txtCellSource.Text = "ZNSHINE";//string.IsNullOrEmpty(o.Cellsource) ? "" : o.Cellsource;
                        txtSerialNo.Text = string.IsNullOrEmpty(o.Module_ID) ? "" : o.Module_ID;
                        txtModelNumber.Text = string.IsNullOrEmpty(o.ProductType) ? "" : o.ProductType;
                        //modify by xue lei on 2018-12-20 日期显示 yyyy-mm
                        txtDateOfModuleCell.Text = string.IsNullOrEmpty(o.CellDate) ? "" : Convert.ToDateTime( o.CellDate).ToString("yyyy-MM");
                        //modify by xue lei on 2018-12-20 日期显示 yyyy-mm
                        txtCellDate.Text = string.IsNullOrEmpty(o.CellDate) ? "" : Convert.ToDateTime(o.CellDate).ToString("yyyy-MM");
                        txtPIVF.Text = o.Pmax + "Wp," + o.Ipm + "A," + o.Vpm + "V," + o.FF;
                        txtIecCertificateDate.Text = "20181217";//string.IsNullOrEmpty(o.iec_date) ? "" : o.iec_date;
                        txtIecCertificateOffer.Text = "BIS";//string.IsNullOrEmpty(o.iec_verfy) ? "" : o.iec_verfy;
                        txtIecCertificateLab.Text = "ISO9001";//string.IsNullOrEmpty(o.iso) ? "" : o.iso;
                        txtMadeIn.Text = "CN";//string.IsNullOrEmpty(o.cell_supplier_country) ? "" : o.cell_supplier_country;
                        //add by xue lei on 2018-12-20 正信要求显示Isc和Voc
                        string isc = (string.IsNullOrEmpty(o.Isc) ? "" : o.Isc);
                        string voc = (string.IsNullOrEmpty(o.Voc) ? "" : o.Voc);
                        txtIscVoc.Text = isc+","+voc;
                        ShowIVCurves(double.Parse(o.Isc), double.Parse(o.Ipm), double.Parse(o.Vpm), double.Parse(o.Voc));
                        break;
                    case SystemState.ResetAll:
                        txtManufacturer.Text = "";
                        txtSerialNo.Text = "";
                        txtModelNumber.Text = "";
                        txtDateOfModuleCell.Text = "";
                        txtCellSource.Text = "";
                        txtCellDate.Text = "";
                        txtPIVF.Text = "";
                        txtIecCertificateDate.Text = "";
                        txtIecCertificateOffer.Text = "";
                        txtIecCertificateLab.Text = "";
                        txtMadeIn.Text = "";

                        ivCurves1.SetOriginalPoints(null, true);
                        break;
                    default:
                        break;
                }


            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ChangeState(SystemState.ResetAll, null);

            m_sLastReadTagId = "";
        }

        private void Reader_Deactivate(object sender, EventArgs e)
        {
            if (_ReadTimer != null)
            {
                _ReadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            this.DialogResult = DialogResult.Cancel;
        }

        private void Reader_Activated(object sender, EventArgs e)
        {
            _read_tag_timer_enable = true;
        }
    }
}