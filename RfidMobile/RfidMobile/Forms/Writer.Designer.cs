namespace RfidMobile
{
    partial class Writer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtChange = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.panelBasicInfo = new System.Windows.Forms.Panel();
            this.txtCellDate = new System.Windows.Forms.TextBox();
            this.txtCellSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtModelNumber = new System.Windows.Forms.TextBox();
            this.txtPIVF = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDateOfModuleCell = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSerialize = new System.Windows.Forms.TextBox();
            this.lblSerialize = new System.Windows.Forms.Label();
            this.panelCurves = new System.Windows.Forms.Panel();
            this.lblCurvesHeader = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.ivCurves1 = new RfidControl.IVCurves();
            this.panelBasicInfo.SuspendLayout();
            this.panelCurves.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtChange
            // 
            this.BtChange.Enabled = false;
            this.BtChange.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.BtChange.Location = new System.Drawing.Point(174, 28);
            this.BtChange.Name = "BtChange";
            this.BtChange.Size = new System.Drawing.Size(58, 22);
            this.BtChange.TabIndex = 98;
            this.BtChange.Text = "IV曲线";
            // 
            // btnWrite
            // 
            this.btnWrite.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnWrite.Location = new System.Drawing.Point(40, 28);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(58, 22);
            this.btnWrite.TabIndex = 94;
            this.btnWrite.Text = "写入";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnReset.Location = new System.Drawing.Point(107, 28);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(58, 22);
            this.btnReset.TabIndex = 93;
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panelBasicInfo
            // 
            this.panelBasicInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelBasicInfo.Controls.Add(this.txtCellDate);
            this.panelBasicInfo.Controls.Add(this.txtCellSource);
            this.panelBasicInfo.Controls.Add(this.label3);
            this.panelBasicInfo.Controls.Add(this.label1);
            this.panelBasicInfo.Controls.Add(this.txtSerialNo);
            this.panelBasicInfo.Controls.Add(this.txtModelNumber);
            this.panelBasicInfo.Controls.Add(this.txtPIVF);
            this.panelBasicInfo.Controls.Add(this.label13);
            this.panelBasicInfo.Controls.Add(this.txtDateOfModuleCell);
            this.panelBasicInfo.Controls.Add(this.label15);
            this.panelBasicInfo.Controls.Add(this.label16);
            this.panelBasicInfo.Controls.Add(this.label12);
            this.panelBasicInfo.Location = new System.Drawing.Point(3, 51);
            this.panelBasicInfo.Name = "panelBasicInfo";
            this.panelBasicInfo.Size = new System.Drawing.Size(234, 238);
            // 
            // txtCellDate
            // 
            this.txtCellDate.BackColor = System.Drawing.SystemColors.Menu;
            this.txtCellDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtCellDate.Location = new System.Drawing.Point(87, 74);
            this.txtCellDate.Name = "txtCellDate";
            this.txtCellDate.ReadOnly = true;
            this.txtCellDate.Size = new System.Drawing.Size(142, 22);
            this.txtCellDate.TabIndex = 88;
            // 
            // txtCellSource
            // 
            this.txtCellSource.BackColor = System.Drawing.SystemColors.Menu;
            this.txtCellSource.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtCellSource.Location = new System.Drawing.Point(87, 51);
            this.txtCellSource.Name = "txtCellSource";
            this.txtCellSource.ReadOnly = true;
            this.txtCellSource.Size = new System.Drawing.Size(142, 22);
            this.txtCellSource.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 18);
            this.label3.Text = "电池生产日期";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.Text = "电池片厂商";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.BackColor = System.Drawing.SystemColors.Menu;
            this.txtSerialNo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtSerialNo.Location = new System.Drawing.Point(87, 136);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.ReadOnly = true;
            this.txtSerialNo.Size = new System.Drawing.Size(142, 22);
            this.txtSerialNo.TabIndex = 64;
            // 
            // txtModelNumber
            // 
            this.txtModelNumber.BackColor = System.Drawing.SystemColors.Menu;
            this.txtModelNumber.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtModelNumber.Location = new System.Drawing.Point(87, 3);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.ReadOnly = true;
            this.txtModelNumber.Size = new System.Drawing.Size(142, 22);
            this.txtModelNumber.TabIndex = 61;
            // 
            // txtPIVF
            // 
            this.txtPIVF.BackColor = System.Drawing.SystemColors.Menu;
            this.txtPIVF.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtPIVF.Location = new System.Drawing.Point(87, 97);
            this.txtPIVF.Multiline = true;
            this.txtPIVF.Name = "txtPIVF";
            this.txtPIVF.ReadOnly = true;
            this.txtPIVF.Size = new System.Drawing.Size(142, 36);
            this.txtPIVF.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label13.Location = new System.Drawing.Point(15, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 29);
            this.label13.Text = "Pmax,Im,Vm,FF";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDateOfModuleCell
            // 
            this.txtDateOfModuleCell.BackColor = System.Drawing.SystemColors.Menu;
            this.txtDateOfModuleCell.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtDateOfModuleCell.Location = new System.Drawing.Point(87, 27);
            this.txtDateOfModuleCell.Name = "txtDateOfModuleCell";
            this.txtDateOfModuleCell.ReadOnly = true;
            this.txtDateOfModuleCell.Size = new System.Drawing.Size(142, 22);
            this.txtDateOfModuleCell.TabIndex = 62;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(3, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 18);
            this.label15.Text = "组件生产日期";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label16.Location = new System.Drawing.Point(3, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 18);
            this.label16.Text = "组件型号";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label12.Location = new System.Drawing.Point(3, 136);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 18);
            this.label12.Text = "组件序列号";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSerialize
            // 
            this.txtSerialize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtSerialize.Location = new System.Drawing.Point(40, 4);
            this.txtSerialize.Name = "txtSerialize";
            this.txtSerialize.Size = new System.Drawing.Size(132, 22);
            this.txtSerialize.TabIndex = 92;
            this.txtSerialize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialize_KeyDown);
            // 
            // lblSerialize
            // 
            this.lblSerialize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.lblSerialize.Location = new System.Drawing.Point(0, 6);
            this.lblSerialize.Name = "lblSerialize";
            this.lblSerialize.Size = new System.Drawing.Size(39, 20);
            this.lblSerialize.Text = "条码";
            this.lblSerialize.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelCurves
            // 
            this.panelCurves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelCurves.Controls.Add(this.ivCurves1);
            this.panelCurves.Controls.Add(this.lblCurvesHeader);
            this.panelCurves.Controls.Add(this.label28);
            this.panelCurves.Location = new System.Drawing.Point(277, 53);
            this.panelCurves.Name = "panelCurves";
            this.panelCurves.Size = new System.Drawing.Size(234, 238);
            this.panelCurves.Visible = false;
            // 
            // lblCurvesHeader
            // 
            this.lblCurvesHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lblCurvesHeader.Location = new System.Drawing.Point(1, 3);
            this.lblCurvesHeader.Name = "lblCurvesHeader";
            this.lblCurvesHeader.Size = new System.Drawing.Size(232, 17);
            this.lblCurvesHeader.Text = "I-V Curves Of PV Module ";
            this.lblCurvesHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label28.Location = new System.Drawing.Point(140, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(89, 17);
            this.label28.Text = "Cell Temp:25℃";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_close.Location = new System.Drawing.Point(174, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(58, 22);
            this.btn_close.TabIndex = 101;
            this.btn_close.Text = "退出";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // ivCurves1
            // 
            this.ivCurves1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ivCurves1.Location = new System.Drawing.Point(0, 38);
            this.ivCurves1.Name = "ivCurves1";
            this.ivCurves1.Size = new System.Drawing.Size(234, 200);
            this.ivCurves1.TabIndex = 3;
            // 
            // Writer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.panelCurves);
            this.Controls.Add(this.BtChange);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.panelBasicInfo);
            this.Controls.Add(this.txtSerialize);
            this.Controls.Add(this.lblSerialize);
            this.Name = "Writer";
            this.Text = "写入标签信息";
            this.Load += new System.EventHandler(this.Writer_Load);
            this.panelBasicInfo.ResumeLayout(false);
            this.panelCurves.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtChange;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panelBasicInfo;
        private System.Windows.Forms.TextBox txtCellDate;
        private System.Windows.Forms.TextBox txtCellSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtModelNumber;
        private System.Windows.Forms.TextBox txtPIVF;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDateOfModuleCell;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSerialize;
        private System.Windows.Forms.Label lblSerialize;
        private System.Windows.Forms.Panel panelCurves;
        private System.Windows.Forms.Label lblCurvesHeader;
        private System.Windows.Forms.Label label28;
        private RfidControl.IVCurves ivCurves1;
        private System.Windows.Forms.Button btn_close;
    }
}