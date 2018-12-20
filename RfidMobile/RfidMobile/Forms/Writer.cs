using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using RfidMobile.API;
using RfidMobile.Classes;
using System.IO;

namespace RfidMobile
{
    public partial class Writer : Form
    {
        private const string beepWavPath = @"\windows\Barcodebeep.wav";

        private string m_strModuleID = "";
        private string m_strTagUID = "";

        private ModuleInfo m_objModuleInfo = null;


        public Writer()
        {
            InitializeComponent();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (m_objModuleInfo == null)
            {
                MessageBox.Show("没有数据");
                return;
            }

            m_strTagUID = ReaderConfig._rfidReader.ReadTagID();

            if (m_strTagUID == "")
            {
                MessageBox.Show("没有找到标签");
                return;
            }

            #region chech region state

            if (string.IsNullOrEmpty(m_objModuleInfo.ProductType))
            {
                MessageBox.Show("产品类型不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.CellDate))
            {
                MessageBox.Show("电池片生产日期不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.PackedDate))
            {
                MessageBox.Show("包装时间不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.Pmax))
            {
                MessageBox.Show("Pmax不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.Voc))
            {
                MessageBox.Show("Voc不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.Vpm))
            {
                MessageBox.Show("Vpm不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.Ipm))
            {
                MessageBox.Show("Ipm不能为空");
                return;
            }
            if (string.IsNullOrEmpty(m_objModuleInfo.Isc))
            {
                MessageBox.Show("Isc不能为空");
                return;
            }
            #endregion

            byte[] m_aryData2Write = TagDataFormat.CreateByteArray(m_objModuleInfo);

            string m_strModuleBasicInfo = m_objModuleInfo.ProductType + "|" + m_objModuleInfo.PackedDate.Replace("-", ".") + "|"
                    + m_objModuleInfo.Pivf + "|" + m_objModuleInfo.Module_ID + "|" + m_objModuleInfo.CellDate.Replace("-", ".") + "|3";

            if (ReaderConfig._rfidReader.WriteTagBuff(m_aryData2Write))
            {
                ReaderConfig._rfidReader.PlaySound();

                WcfCaller.WriteLog(ex =>
                {
                    if (ex == null)
                    {
                        m_objModuleInfo = null;
                        ChangeState(SystemState.ResetAll,null);
                        PlaySound();
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }, new string[] { m_strTagUID, m_strModuleID, m_strModuleBasicInfo });
            }
            else
            {
                MessageBox.Show("烧录失败");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtSerialize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 238)
            {
                /*
                 * this is fired by scan button
                 */
                string sModuleID = "";
                ReaderConfig._rfidReader.ScanBarcode(out sModuleID);
                txtSerialize.Text = sModuleID;
                m_strModuleID = sModuleID;

                if (sModuleID.Trim()!="")
                {
                    GetSerialBasicInfo(sModuleID);
                }
            }
            else if (e.KeyCode==Keys.Enter)
            {
                /*
                 * this is fired by user input
                 */
                m_strModuleID = txtSerialize.Text.Trim();

                GetSerialBasicInfo(m_strModuleID);
            }
        }

        private void GetSerialBasicInfo(string serial)
        {
            WcfCaller.GetModuleInfo(
                    (o, ex) =>
                    {
                        if (ex == null)
                        {
                            m_objModuleInfo = o;

                            ChangeState(SystemState.ShowModuleInfo, o);
                            
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }


                    },
                    new string[] { serial, "" }
                );
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
                switch (ss) { 
                    case SystemState.ShowModuleInfo:
                        txtSerialize.Text = "";
                        txtSerialNo.Text = string.IsNullOrEmpty(o.Module_ID) ? "" : o.Module_ID;
                        txtModelNumber.Text = string.IsNullOrEmpty(o.ProductType) ? "" : o.ProductType;
                        txtDateOfModuleCell.Text = string.IsNullOrEmpty(o.CellDate) ? "" : o.CellDate;
                        txtCellSource.Text = string.IsNullOrEmpty(o.Cellsource) ? "" : o.Cellsource;
                        txtCellDate.Text = string.IsNullOrEmpty(o.CellDate) ? "" : o.CellDate;
                        txtPIVF.Text = string.IsNullOrEmpty(o.Pivf) ? "" : o.Pivf;
                        break;
                    case SystemState.ResetAll:
                        txtSerialNo.Text = "";
                        txtModelNumber.Text = "";
                        txtDateOfModuleCell.Text = "";
                        txtCellSource.Text = "";
                        txtCellDate.Text = "";
                        txtPIVF.Text = "";
                        break;
                    default:
                        break;
                }

                
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ChangeState(SystemState.ResetAll, null);
        }

        private void PlaySound()
        {
            try
            {
                if (File.Exists(beepWavPath))
                {
                    SoundPlayer player = new SoundPlayer(beepWavPath);
                    player.Play();
                }
            }
            catch
            {
            }
        }

        private void Writer_Load(object sender, EventArgs e)
        {

        }
    }
}