using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using RfidMobile.Classes;

namespace RfidMobile
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$");
            if (!rx.IsMatch(txtServerIP.Text))
            {
                MessageBox.Show("服务器IP地址格式不合法。", "输入错误");
                txtServerIP.SelectAll();
                txtServerIP.Focus();
                return;
            }
            //int port = -1;
            try
            {
                int.Parse(txtPort.Text);
            }
            catch
            {
                MessageBox.Show("端口地址范围为0~65535。", "输入错误");
                txtPort.SelectAll();
                txtPort.Focus();
                return;
            }


            ReaderConfig.SaveConfig(txtServerIP.Text, txtPort.Text);


            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = ReaderConfig.GetXmlNodeValue("Host");
            txtPort.Text = ReaderConfig.GetXmlNodeValue("Port");
        }
    }
}