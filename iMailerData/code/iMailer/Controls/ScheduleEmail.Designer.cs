namespace Infolancers.iMailer
{
    partial class ScheduleEmail
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
            this.tabScheduleEmail = new System.Windows.Forms.TabControl();
            this.pageContactScheduleEmail = new System.Windows.Forms.TabPage();
            this.contactControl1 = new Infolancers.iMailer.ContactControl();
            this.pageScheduleEmail = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtScheduleName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.opField = new System.Windows.Forms.RadioButton();
            this.opSpecificDate = new System.Windows.Forms.RadioButton();
            this.dtOccassionDate = new System.Windows.Forms.DateTimePicker();
            this.cboDBField = new System.Windows.Forms.ComboBox();
            this.cboOccassionType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabScheduleEmail.SuspendLayout();
            this.pageContactScheduleEmail.SuspendLayout();
            this.pageScheduleEmail.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabScheduleEmail
            // 
            this.tabScheduleEmail.Controls.Add(this.pageContactScheduleEmail);
            this.tabScheduleEmail.Controls.Add(this.pageScheduleEmail);
            this.tabScheduleEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabScheduleEmail.Location = new System.Drawing.Point(0, 0);
            this.tabScheduleEmail.Name = "tabScheduleEmail";
            this.tabScheduleEmail.SelectedIndex = 0;
            this.tabScheduleEmail.Size = new System.Drawing.Size(754, 537);
            this.tabScheduleEmail.TabIndex = 0;
            // 
            // pageContactScheduleEmail
            // 
            this.pageContactScheduleEmail.Controls.Add(this.btnNext);
            this.pageContactScheduleEmail.Controls.Add(this.contactControl1);
            this.pageContactScheduleEmail.Location = new System.Drawing.Point(4, 22);
            this.pageContactScheduleEmail.Name = "pageContactScheduleEmail";
            this.pageContactScheduleEmail.Padding = new System.Windows.Forms.Padding(3);
            this.pageContactScheduleEmail.Size = new System.Drawing.Size(746, 511);
            this.pageContactScheduleEmail.TabIndex = 0;
            this.pageContactScheduleEmail.Text = "Contacts";
            this.pageContactScheduleEmail.UseVisualStyleBackColor = true;
            // 
            // contactControl1
            // 
            this.contactControl1.LoadDataInitially = true;
            this.contactControl1.LoadNullEmailData = false;
            this.contactControl1.Location = new System.Drawing.Point(8, 6);
            this.contactControl1.Name = "contactControl1";
            this.contactControl1.ReadOnlyData = false;
            this.contactControl1.Size = new System.Drawing.Size(730, 497);
            this.contactControl1.TabIndex = 0;
            // 
            // pageScheduleEmail
            // 
            this.pageScheduleEmail.Controls.Add(this.groupBox1);
            this.pageScheduleEmail.Location = new System.Drawing.Point(4, 22);
            this.pageScheduleEmail.Name = "pageScheduleEmail";
            this.pageScheduleEmail.Padding = new System.Windows.Forms.Padding(3);
            this.pageScheduleEmail.Size = new System.Drawing.Size(746, 511);
            this.pageScheduleEmail.TabIndex = 1;
            this.pageScheduleEmail.Text = "Schedule";
            this.pageScheduleEmail.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtScheduleName);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.dtOccassionDate);
            this.groupBox1.Controls.Add(this.cboDBField);
            this.groupBox1.Controls.Add(this.cboOccassionType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 497);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schedule";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(41, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtScheduleName
            // 
            this.txtScheduleName.Location = new System.Drawing.Point(228, 27);
            this.txtScheduleName.Name = "txtScheduleName";
            this.txtScheduleName.Size = new System.Drawing.Size(201, 20);
            this.txtScheduleName.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.opField);
            this.panel1.Controls.Add(this.opSpecificDate);
            this.panel1.Location = new System.Drawing.Point(228, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 35);
            this.panel1.TabIndex = 4;
            // 
            // opField
            // 
            this.opField.AutoSize = true;
            this.opField.Location = new System.Drawing.Point(108, 8);
            this.opField.Name = "opField";
            this.opField.Size = new System.Drawing.Size(96, 17);
            this.opField.TabIndex = 0;
            this.opField.Text = "Database Field";
            this.opField.UseVisualStyleBackColor = true;
            // 
            // opSpecificDate
            // 
            this.opSpecificDate.AutoSize = true;
            this.opSpecificDate.Checked = true;
            this.opSpecificDate.Location = new System.Drawing.Point(3, 10);
            this.opSpecificDate.Name = "opSpecificDate";
            this.opSpecificDate.Size = new System.Drawing.Size(89, 17);
            this.opSpecificDate.TabIndex = 0;
            this.opSpecificDate.TabStop = true;
            this.opSpecificDate.Text = "Specific Date";
            this.opSpecificDate.UseVisualStyleBackColor = true;
            // 
            // dtOccassionDate
            // 
            this.dtOccassionDate.Location = new System.Drawing.Point(228, 149);
            this.dtOccassionDate.Name = "dtOccassionDate";
            this.dtOccassionDate.Size = new System.Drawing.Size(200, 20);
            this.dtOccassionDate.TabIndex = 3;
            // 
            // cboDBField
            // 
            this.cboDBField.FormattingEnabled = true;
            this.cboDBField.Location = new System.Drawing.Point(228, 122);
            this.cboDBField.Name = "cboDBField";
            this.cboDBField.Size = new System.Drawing.Size(201, 21);
            this.cboDBField.TabIndex = 2;
            // 
            // cboOccassionType
            // 
            this.cboOccassionType.FormattingEnabled = true;
            this.cboOccassionType.Location = new System.Drawing.Point(228, 54);
            this.cboOccassionType.Name = "cboOccassionType";
            this.cboOccassionType.Size = new System.Drawing.Size(201, 21);
            this.cboOccassionType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Occassion Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Occasion Date Field:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Database Field for Occassion Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Schedule Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Occassion Type:";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(650, 480);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ScheduleEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 537);
            this.Controls.Add(this.tabScheduleEmail);
            this.Name = "ScheduleEmail";
            this.Text = "ScheduleEmail";
            this.Load += new System.EventHandler(this.ScheduleEmail_Load);
            this.tabScheduleEmail.ResumeLayout(false);
            this.pageContactScheduleEmail.ResumeLayout(false);
            this.pageScheduleEmail.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabScheduleEmail;
        private System.Windows.Forms.TabPage pageContactScheduleEmail;
        private System.Windows.Forms.TabPage pageScheduleEmail;
        private ContactControl contactControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton opField;
        private System.Windows.Forms.RadioButton opSpecificDate;
        private System.Windows.Forms.DateTimePicker dtOccassionDate;
        private System.Windows.Forms.ComboBox cboDBField;
        private System.Windows.Forms.ComboBox cboOccassionType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScheduleName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
    }
}