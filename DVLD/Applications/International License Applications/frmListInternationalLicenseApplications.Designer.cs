namespace DVLD.Applications.International_License_Applications
{
    partial class frmListInternationalLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListInternationalLicenseApplications));
            this.lblNumRecords = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbStatusFilteration = new System.Windows.Forms.ComboBox();
            this.txtFilteration = new System.Windows.Forms.TextBox();
            this.cbFiltertion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAllLDLApplications = new System.Windows.Forms.DataGridView();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmsOperations = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLDLApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsOperations.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumRecords
            // 
            this.lblNumRecords.AutoSize = true;
            this.lblNumRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRecords.Location = new System.Drawing.Point(110, 588);
            this.lblNumRecords.Name = "lblNumRecords";
            this.lblNumRecords.Size = new System.Drawing.Size(35, 17);
            this.lblNumRecords.TabIndex = 38;
            this.lblNumRecords.Text = "???";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(12, 588);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(92, 17);
            this.lblRecords.TabIndex = 37;
            this.lblRecords.Text = "# Records: ";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1031, 588);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 36);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbStatusFilteration
            // 
            this.cbStatusFilteration.FormattingEnabled = true;
            this.cbStatusFilteration.Items.AddRange(new object[] {
            "Activated",
            "Deactivated"});
            this.cbStatusFilteration.Location = new System.Drawing.Point(268, 208);
            this.cbStatusFilteration.Name = "cbStatusFilteration";
            this.cbStatusFilteration.Size = new System.Drawing.Size(121, 21);
            this.cbStatusFilteration.TabIndex = 35;
            this.cbStatusFilteration.SelectedIndexChanged += new System.EventHandler(this.cbStatusFilteration_SelectedIndexChanged);
            // 
            // txtFilteration
            // 
            this.txtFilteration.Location = new System.Drawing.Point(268, 209);
            this.txtFilteration.Name = "txtFilteration";
            this.txtFilteration.Size = new System.Drawing.Size(218, 20);
            this.txtFilteration.TabIndex = 34;
            this.txtFilteration.TextChanged += new System.EventHandler(this.txtFilteration_TextChanged);
            // 
            // cbFiltertion
            // 
            this.cbFiltertion.FormattingEnabled = true;
            this.cbFiltertion.Items.AddRange(new object[] {
            "None",
            "Int. License ID",
            "Application ID",
            "Driver ID",
            "L. License ID",
            "Issue Date",
            "Expiration Date",
            "Is Active"});
            this.cbFiltertion.Location = new System.Drawing.Point(93, 208);
            this.cbFiltertion.Name = "cbFiltertion";
            this.cbFiltertion.Size = new System.Drawing.Size(169, 21);
            this.cbFiltertion.TabIndex = 33;
            this.cbFiltertion.SelectedIndexChanged += new System.EventHandler(this.cbFiltertion_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "Filter By:";
            // 
            // dgvAllLDLApplications
            // 
            this.dgvAllLDLApplications.AllowUserToAddRows = false;
            this.dgvAllLDLApplications.AllowUserToDeleteRows = false;
            this.dgvAllLDLApplications.AllowUserToOrderColumns = true;
            this.dgvAllLDLApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllLDLApplications.ContextMenuStrip = this.cmsOperations;
            this.dgvAllLDLApplications.Location = new System.Drawing.Point(13, 258);
            this.dgvAllLDLApplications.Name = "dgvAllLDLApplications";
            this.dgvAllLDLApplications.ReadOnly = true;
            this.dgvAllLDLApplications.Size = new System.Drawing.Size(1136, 317);
            this.dgvAllLDLApplications.TabIndex = 31;
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewApplication.Image")));
            this.btnAddNewApplication.Location = new System.Drawing.Point(1071, 182);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(78, 70);
            this.btnAddNewApplication.TabIndex = 30;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(634, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(407, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 26);
            this.label1.TabIndex = 28;
            this.label1.Text = "International License Applications";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(499, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // cmsOperations
            // 
            this.cmsOperations.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsOperations.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsOperations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowPersonDetails,
            this.showLicenseToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.cmsOperations.Name = "cmsOperations";
            this.cmsOperations.Size = new System.Drawing.Size(340, 118);
            // 
            // tsmiShowPersonDetails
            // 
            this.tsmiShowPersonDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsmiShowPersonDetails.Image = ((System.Drawing.Image)(resources.GetObject("tsmiShowPersonDetails.Image")));
            this.tsmiShowPersonDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiShowPersonDetails.Name = "tsmiShowPersonDetails";
            this.tsmiShowPersonDetails.Size = new System.Drawing.Size(339, 38);
            this.tsmiShowPersonDetails.Text = "Show Person Details";
            this.tsmiShowPersonDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsmiShowPersonDetails.Click += new System.EventHandler(this.tsmiShowPersonDetails_Click);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showLicenseToolStripMenuItem.Image")));
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.showLicenseToolStripMenuItem.Text = "Show License Details";
            this.showLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPersonLicenseHistoryToolStripMenuItem.Image")));
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // frmListInternationalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 636);
            this.Controls.Add(this.lblNumRecords);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbStatusFilteration);
            this.Controls.Add(this.txtFilteration);
            this.Controls.Add(this.cbFiltertion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAllLDLApplications);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListInternationalLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List International License Applications";
            this.Load += new System.EventHandler(this.frmListInternationalLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLDLApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsOperations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumRecords;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbStatusFilteration;
        private System.Windows.Forms.TextBox txtFilteration;
        private System.Windows.Forms.ComboBox cbFiltertion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvAllLDLApplications;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cmsOperations;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
    }
}