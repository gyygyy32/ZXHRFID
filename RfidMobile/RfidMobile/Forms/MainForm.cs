using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RfidMobile.Classes;

namespace RfidMobile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            try
            {
                ReaderConfig.InitRfidReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reader reader = new Reader();

            reader.Owner = this;

            reader.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ReaderConfig.ReleaseReader();
            }
            catch (Exception)
            {
            }
            Application.Exit(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            return;
            Writer riter = new Writer();
            riter.Owner = this;

            riter.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Config dlg = new Config();
            dlg.Owner = this;

            dlg.ShowDialog();
        }
    }
}