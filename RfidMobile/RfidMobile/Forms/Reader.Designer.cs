namespace RfidMobile
{
    partial class Reader
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
            this.btnBasicInfo = new System.Windows.Forms.Button();
            this.btnCurves = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelBasicInfo = new System.Windows.Forms.Panel();
            this.txtIscVoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCellDate = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCellSource = new System.Windows.Forms.TextBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPIVF = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDateOfModuleCell = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtModelNumber = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIecCertificateOffer = new System.Windows.Forms.TextBox();
            this.txtIecCertificateLab = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtIecCertificateDate = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMadeIn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panelCurves = new System.Windows.Forms.Panel();
            this.lblCurvesHeader = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.ivCurves1 = new RfidControl.IVCurves();
            this.panelBasicInfo.SuspendLayout();
            this.panelCurves.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBasicInfo
            // 
            this.btnBasicInfo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnBasicInfo.Location = new System.Drawing.Point(3, 3);
            this.btnBasicInfo.Name = "btnBasicInfo";
            this.btnBasicInfo.Size = new System.Drawing.Size(70, 22);
            this.btnBasicInfo.TabIndex = 71;
            this.btnBasicInfo.Text = "Basic Info";
            this.btnBasicInfo.Click += new System.EventHandler(this.OnChangePanel);
            // 
            // btnCurves
            // 
            this.btnCurves.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnCurves.Location = new System.Drawing.Point(76, 3);
            this.btnCurves.Name = "btnCurves";
            this.btnCurves.Size = new System.Drawing.Size(67, 22);
            this.btnCurves.TabIndex = 72;
            this.btnCurves.Text = "I-V Curve";
            this.btnCurves.Click += new System.EventHandler(this.OnChangePanel);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnReset.Location = new System.Drawing.Point(149, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(49, 22);
            this.btnReset.TabIndex = 73;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(193, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 22);
            this.btnExit.TabIndex = 74;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelBasicInfo
            // 
            this.panelBasicInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelBasicInfo.Controls.Add(this.txtIscVoc);
            this.panelBasicInfo.Controls.Add(this.label1);
            this.panelBasicInfo.Controls.Add(this.txtCellDate);
            this.panelBasicInfo.Controls.Add(this.label20);
            this.panelBasicInfo.Controls.Add(this.txtCellSource);
            this.panelBasicInfo.Controls.Add(this.txtSerialNo);
            this.panelBasicInfo.Controls.Add(this.label16);
            this.panelBasicInfo.Controls.Add(this.label14);
            this.panelBasicInfo.Controls.Add(this.txtPIVF);
            this.panelBasicInfo.Controls.Add(this.label11);
            this.panelBasicInfo.Controls.Add(this.txtDateOfModuleCell);
            this.panelBasicInfo.Controls.Add(this.label10);
            this.panelBasicInfo.Controls.Add(this.txtModelNumber);
            this.panelBasicInfo.Controls.Add(this.label13);
            this.panelBasicInfo.Controls.Add(this.txtIecCertificateOffer);
            this.panelBasicInfo.Controls.Add(this.txtIecCertificateLab);
            this.panelBasicInfo.Controls.Add(this.label18);
            this.panelBasicInfo.Controls.Add(this.txtIecCertificateDate);
            this.panelBasicInfo.Controls.Add(this.label15);
            this.panelBasicInfo.Controls.Add(this.txtMadeIn);
            this.panelBasicInfo.Controls.Add(this.label9);
            this.panelBasicInfo.Controls.Add(this.txtManufacturer);
            this.panelBasicInfo.Controls.Add(this.label12);
            this.panelBasicInfo.Location = new System.Drawing.Point(2, 29);
            this.panelBasicInfo.Name = "panelBasicInfo";
            this.panelBasicInfo.Size = new System.Drawing.Size(234, 238);
            // 
            // txtIscVoc
            // 
            this.txtIscVoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtIscVoc.Location = new System.Drawing.Point(88, 129);
            this.txtIscVoc.Name = "txtIscVoc";
            this.txtIscVoc.Size = new System.Drawing.Size(142, 19);
            this.txtIscVoc.TabIndex = 135;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.Text = "Isc,Voc";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCellDate
            // 
            this.txtCellDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCellDate.Location = new System.Drawing.Point(88, 66);
            this.txtCellDate.Name = "txtCellDate";
            this.txtCellDate.Size = new System.Drawing.Size(142, 19);
            this.txtCellDate.TabIndex = 122;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label20.Location = new System.Drawing.Point(4, 66);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 18);
            this.label20.Text = "Cell Manuf Date";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCellSource
            // 
            this.txtCellSource.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCellSource.Location = new System.Drawing.Point(163, 3);
            this.txtCellSource.Name = "txtCellSource";
            this.txtCellSource.Size = new System.Drawing.Size(68, 19);
            this.txtCellSource.TabIndex = 121;
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSerialNo.Location = new System.Drawing.Point(88, 149);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(142, 19);
            this.txtSerialNo.TabIndex = 119;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label16.Location = new System.Drawing.Point(3, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 21);
            this.label16.Text = "Module/Cell Manuf";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label14.Location = new System.Drawing.Point(3, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 18);
            this.label14.Text = "Manuf Country ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPIVF
            // 
            this.txtPIVF.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPIVF.Location = new System.Drawing.Point(88, 108);
            this.txtPIVF.Name = "txtPIVF";
            this.txtPIVF.Size = new System.Drawing.Size(142, 19);
            this.txtPIVF.TabIndex = 120;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(3, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 18);
            this.label11.Text = "Certificate Date";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDateOfModuleCell
            // 
            this.txtDateOfModuleCell.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDateOfModuleCell.Location = new System.Drawing.Point(88, 45);
            this.txtDateOfModuleCell.Name = "txtDateOfModuleCell";
            this.txtDateOfModuleCell.Size = new System.Drawing.Size(142, 19);
            this.txtDateOfModuleCell.TabIndex = 117;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(3, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 18);
            this.label10.Text = "ISO";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtModelNumber
            // 
            this.txtModelNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtModelNumber.Location = new System.Drawing.Point(88, 24);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new System.Drawing.Size(142, 19);
            this.txtModelNumber.TabIndex = 116;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label13.Location = new System.Drawing.Point(3, 215);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 18);
            this.label13.Text = "Certificate Name";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIecCertificateOffer
            // 
            this.txtIecCertificateOffer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtIecCertificateOffer.Location = new System.Drawing.Point(88, 213);
            this.txtIecCertificateOffer.Name = "txtIecCertificateOffer";
            this.txtIecCertificateOffer.Size = new System.Drawing.Size(142, 19);
            this.txtIecCertificateOffer.TabIndex = 115;
            // 
            // txtIecCertificateLab
            // 
            this.txtIecCertificateLab.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtIecCertificateLab.Location = new System.Drawing.Point(88, 192);
            this.txtIecCertificateLab.Name = "txtIecCertificateLab";
            this.txtIecCertificateLab.Size = new System.Drawing.Size(142, 19);
            this.txtIecCertificateLab.TabIndex = 113;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label18.Location = new System.Drawing.Point(3, 111);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 18);
            this.label18.Text = "Pmax,Im,Vm,FF";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIecCertificateDate
            // 
            this.txtIecCertificateDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtIecCertificateDate.Location = new System.Drawing.Point(88, 170);
            this.txtIecCertificateDate.Name = "txtIecCertificateDate";
            this.txtIecCertificateDate.Size = new System.Drawing.Size(142, 19);
            this.txtIecCertificateDate.TabIndex = 114;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(3, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 18);
            this.label15.Text = "Module Mauf Date";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMadeIn
            // 
            this.txtMadeIn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMadeIn.Location = new System.Drawing.Point(88, 87);
            this.txtMadeIn.Name = "txtMadeIn";
            this.txtMadeIn.Size = new System.Drawing.Size(142, 19);
            this.txtMadeIn.TabIndex = 112;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(3, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 18);
            this.label9.Text = "Module Type";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtManufacturer.Location = new System.Drawing.Point(88, 3);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(72, 19);
            this.txtManufacturer.TabIndex = 111;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label12.Location = new System.Drawing.Point(3, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 18);
            this.label12.Text = "Serial No";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelCurves
            // 
            this.panelCurves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelCurves.Controls.Add(this.ivCurves1);
            this.panelCurves.Controls.Add(this.lblCurvesHeader);
            this.panelCurves.Controls.Add(this.label28);
            this.panelCurves.Location = new System.Drawing.Point(263, 29);
            this.panelCurves.Name = "panelCurves";
            this.panelCurves.Size = new System.Drawing.Size(234, 238);
            this.panelCurves.Visible = false;
            // 
            // lblCurvesHeader
            // 
            this.lblCurvesHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lblCurvesHeader.Location = new System.Drawing.Point(1, 9);
            this.lblCurvesHeader.Name = "lblCurvesHeader";
            this.lblCurvesHeader.Size = new System.Drawing.Size(232, 17);
            this.lblCurvesHeader.Text = "I-V Curves Of PV Module ";
            this.lblCurvesHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.label28.Location = new System.Drawing.Point(140, 24);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(89, 17);
            this.label28.Text = "Cell Temp:25℃";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ivCurves1
            // 
            this.ivCurves1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ivCurves1.Location = new System.Drawing.Point(0, 38);
            this.ivCurves1.Name = "ivCurves1";
            this.ivCurves1.Size = new System.Drawing.Size(234, 200);
            this.ivCurves1.TabIndex = 2;
            // 
            // Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.ControlBox = false;
            this.Controls.Add(this.panelCurves);
            this.Controls.Add(this.panelBasicInfo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCurves);
            this.Controls.Add(this.btnBasicInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reader";
            this.Text = "Read Info";
            this.Deactivate += new System.EventHandler(this.Reader_Deactivate);
            this.Load += new System.EventHandler(this.Reader_Load);
            this.Activated += new System.EventHandler(this.Reader_Activated);
            this.panelBasicInfo.ResumeLayout(false);
            this.panelCurves.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBasicInfo;
        private System.Windows.Forms.Button btnCurves;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelBasicInfo;
        private System.Windows.Forms.TextBox txtCellDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPIVF;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDateOfModuleCell;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtModelNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtIecCertificateOffer;
        private System.Windows.Forms.TextBox txtIecCertificateLab;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtIecCertificateDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMadeIn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelCurves;
        private System.Windows.Forms.Label lblCurvesHeader;
        private System.Windows.Forms.Label label28;
        private RfidControl.IVCurves ivCurves1;
        private System.Windows.Forms.TextBox txtCellSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIscVoc;
    }
}