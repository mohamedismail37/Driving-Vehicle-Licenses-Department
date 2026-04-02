namespace DVLD.Applications.ReplacementForDamagedOrLostLicense
{
    partial class frmReplacementForDamagedOrLostLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplacementForDamagedOrLostLicense));
            this.labelTitle = new System.Windows.Forms.Label();
            this.ctrlLicenseInfoWithFilter1 = new DVLD.Licenses.Controls.ctrlLicenseInfoWithFilter();
            this.gbReplacementReason = new System.Windows.Forms.GroupBox();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.rbDamaged = new System.Windows.Forms.RadioButton();
            this.gbApplicationInfoForLicenseReplacement = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblReplacedLicenseID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblOldLicenseID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lbLRApplicationID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llShowNewLicesesInfo = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnReplacement = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbReplacementReason.SuspendLayout();
            this.gbApplicationInfoForLicenseReplacement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.labelTitle.Location = new System.Drawing.Point(142, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(392, 26);
            this.labelTitle.TabIndex = 101;
            this.labelTitle.Text = "Replacement For Damaged License";
            // 
            // ctrlLicenseInfoWithFilter1
            // 
            this.ctrlLicenseInfoWithFilter1.Location = new System.Drawing.Point(12, 38);
            this.ctrlLicenseInfoWithFilter1.Name = "ctrlLicenseInfoWithFilter1";
            this.ctrlLicenseInfoWithFilter1.Size = new System.Drawing.Size(644, 421);
            this.ctrlLicenseInfoWithFilter1.TabIndex = 102;
            // 
            // gbReplacementReason
            // 
            this.gbReplacementReason.Controls.Add(this.rbLostLicense);
            this.gbReplacementReason.Controls.Add(this.rbDamaged);
            this.gbReplacementReason.Location = new System.Drawing.Point(528, 49);
            this.gbReplacementReason.Name = "gbReplacementReason";
            this.gbReplacementReason.Size = new System.Drawing.Size(123, 76);
            this.gbReplacementReason.TabIndex = 103;
            this.gbReplacementReason.TabStop = false;
            this.gbReplacementReason.Text = "Replacement For:";
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Location = new System.Drawing.Point(6, 42);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(85, 17);
            this.rbLostLicense.TabIndex = 1;
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            this.rbLostLicense.CheckedChanged += new System.EventHandler(this.rbLostLicense_CheckedChanged);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.Checked = true;
            this.rbDamaged.Location = new System.Drawing.Point(6, 19);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(111, 17);
            this.rbDamaged.TabIndex = 0;
            this.rbDamaged.TabStop = true;
            this.rbDamaged.Text = "Damaged License";
            this.rbDamaged.UseVisualStyleBackColor = true;
            // 
            // gbApplicationInfoForLicenseReplacement
            // 
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox3);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lblReplacedLicenseID);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label7);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox2);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lblOldLicenseID);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label5);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox12);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lblCreatedBy);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label18);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox10);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lblApplicationDate);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label14);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox7);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lblApplicationFees);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label8);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.pictureBox5);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.lbLRApplicationID);
            this.gbApplicationInfoForLicenseReplacement.Controls.Add(this.label3);
            this.gbApplicationInfoForLicenseReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbApplicationInfoForLicenseReplacement.Location = new System.Drawing.Point(15, 462);
            this.gbApplicationInfoForLicenseReplacement.Name = "gbApplicationInfoForLicenseReplacement";
            this.gbApplicationInfoForLicenseReplacement.Size = new System.Drawing.Size(630, 119);
            this.gbApplicationInfoForLicenseReplacement.TabIndex = 114;
            this.gbApplicationInfoForLicenseReplacement.TabStop = false;
            this.gbApplicationInfoForLicenseReplacement.Text = "Application Info For License Replacement";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(458, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 126;
            this.pictureBox3.TabStop = false;
            // 
            // lblReplacedLicenseID
            // 
            this.lblReplacedLicenseID.AutoSize = true;
            this.lblReplacedLicenseID.Location = new System.Drawing.Point(487, 18);
            this.lblReplacedLicenseID.Name = "lblReplacedLicenseID";
            this.lblReplacedLicenseID.Size = new System.Drawing.Size(28, 15);
            this.lblReplacedLicenseID.TabIndex = 125;
            this.lblReplacedLicenseID.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(309, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 15);
            this.label7.TabIndex = 124;
            this.label7.Text = "Replaced License ID:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(458, 48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 123;
            this.pictureBox2.TabStop = false;
            // 
            // lblOldLicenseID
            // 
            this.lblOldLicenseID.AutoSize = true;
            this.lblOldLicenseID.Location = new System.Drawing.Point(487, 51);
            this.lblOldLicenseID.Name = "lblOldLicenseID";
            this.lblOldLicenseID.Size = new System.Drawing.Size(28, 15);
            this.lblOldLicenseID.TabIndex = 122;
            this.lblOldLicenseID.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(309, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 121;
            this.label5.Text = "Old License ID:";
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(458, 85);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(23, 20);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox12.TabIndex = 115;
            this.pictureBox12.TabStop = false;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(487, 88);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(28, 15);
            this.lblCreatedBy.TabIndex = 114;
            this.lblCreatedBy.Text = "???";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(309, 88);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 15);
            this.label18.TabIndex = 113;
            this.label18.Text = "Created By:";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(132, 50);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(23, 20);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 109;
            this.pictureBox10.TabStop = false;
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Location = new System.Drawing.Point(161, 53);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(28, 15);
            this.lblApplicationDate.TabIndex = 108;
            this.lblApplicationDate.Text = "???";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 15);
            this.label14.TabIndex = 107;
            this.label14.Text = "Application Date: ";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(132, 85);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(23, 20);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 100;
            this.pictureBox7.TabStop = false;
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Location = new System.Drawing.Point(161, 88);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(28, 15);
            this.lblApplicationFees.TabIndex = 99;
            this.lblApplicationFees.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 15);
            this.label8.TabIndex = 98;
            this.label8.Text = "Application Fees:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(132, 17);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(23, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 94;
            this.pictureBox5.TabStop = false;
            // 
            // lbLRApplicationID
            // 
            this.lbLRApplicationID.AutoSize = true;
            this.lbLRApplicationID.Location = new System.Drawing.Point(161, 20);
            this.lbLRApplicationID.Name = "lbLRApplicationID";
            this.lbLRApplicationID.Size = new System.Drawing.Size(28, 15);
            this.lbLRApplicationID.TabIndex = 93;
            this.lbLRApplicationID.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 92;
            this.label3.Text = "L.R.Application ID:";
            // 
            // llShowNewLicesesInfo
            // 
            this.llShowNewLicesesInfo.AutoSize = true;
            this.llShowNewLicesesInfo.Enabled = false;
            this.llShowNewLicesesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowNewLicesesInfo.Location = new System.Drawing.Point(146, 599);
            this.llShowNewLicesesInfo.Name = "llShowNewLicesesInfo";
            this.llShowNewLicesesInfo.Size = new System.Drawing.Size(135, 15);
            this.llShowNewLicesesInfo.TabIndex = 113;
            this.llShowNewLicesesInfo.TabStop = true;
            this.llShowNewLicesesInfo.Text = "Show New License Info";
            this.llShowNewLicesesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowNewLicesesInfo_LinkClicked);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Enabled = false;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseHistory.Location = new System.Drawing.Point(10, 599);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(130, 15);
            this.llShowLicenseHistory.TabIndex = 112;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show Licenses History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // btnReplacement
            // 
            this.btnReplacement.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReplacement.Enabled = false;
            this.btnReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplacement.Image = ((System.Drawing.Image)(resources.GetObject("btnReplacement.Image")));
            this.btnReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplacement.Location = new System.Drawing.Point(451, 587);
            this.btnReplacement.Name = "btnReplacement";
            this.btnReplacement.Size = new System.Drawing.Size(200, 36);
            this.btnReplacement.TabIndex = 111;
            this.btnReplacement.Text = "Issue Replacement";
            this.btnReplacement.UseVisualStyleBackColor = true;
            this.btnReplacement.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(327, 587);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 36);
            this.btnClose.TabIndex = 110;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmReplacementForDamagedOrLostLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 635);
            this.Controls.Add(this.gbApplicationInfoForLicenseReplacement);
            this.Controls.Add(this.llShowNewLicesesInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.btnReplacement);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbReplacementReason);
            this.Controls.Add(this.ctrlLicenseInfoWithFilter1);
            this.Controls.Add(this.labelTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplacementForDamagedOrLostLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replacement For Damaged License";
            this.Load += new System.EventHandler(this.frmReplacementForDamagedOrLostLicense_Load);
            this.gbReplacementReason.ResumeLayout(false);
            this.gbReplacementReason.PerformLayout();
            this.gbApplicationInfoForLicenseReplacement.ResumeLayout(false);
            this.gbApplicationInfoForLicenseReplacement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private Licenses.Controls.ctrlLicenseInfoWithFilter ctrlLicenseInfoWithFilter1;
        private System.Windows.Forms.GroupBox gbReplacementReason;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.RadioButton rbDamaged;
        private System.Windows.Forms.GroupBox gbApplicationInfoForLicenseReplacement;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblReplacedLicenseID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblOldLicenseID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbLRApplicationID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llShowNewLicesesInfo;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Button btnReplacement;
        private System.Windows.Forms.Button btnClose;
    }
}