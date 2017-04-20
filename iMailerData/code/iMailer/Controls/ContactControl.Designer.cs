namespace Infolancers.iMailer
{
    partial class ContactControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilterReset = new System.Windows.Forms.Button();
            this.cboFilterByList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilterBySubCategory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilterByCategory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilterByType = new System.Windows.Forms.TextBox();
            this.txtFilterByEmail = new System.Windows.Forms.TextBox();
            this.chkFilterPreferredColumns = new System.Windows.Forms.CheckBox();
            this.btnRefreshFilterContact = new System.Windows.Forms.Button();
            this.grdFilterContact = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFilterContact)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFilterReset);
            this.groupBox1.Controls.Add(this.cboFilterByList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFilterBySubCategory);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFilterByCategory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFilterByType);
            this.groupBox1.Controls.Add(this.txtFilterByEmail);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 65);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnFilterReset
            // 
            this.btnFilterReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterReset.Location = new System.Drawing.Point(585, 30);
            this.btnFilterReset.Name = "btnFilterReset";
            this.btnFilterReset.Size = new System.Drawing.Size(75, 23);
            this.btnFilterReset.TabIndex = 8;
            this.btnFilterReset.Text = "Reset";
            this.btnFilterReset.UseVisualStyleBackColor = true;
            this.btnFilterReset.Click += new System.EventHandler(this.btnFilterReset_Click);
            // 
            // cboFilterByList
            // 
            this.cboFilterByList.FormattingEnabled = true;
            this.cboFilterByList.Location = new System.Drawing.Point(9, 33);
            this.cboFilterByList.Name = "cboFilterByList";
            this.cboFilterByList.Size = new System.Drawing.Size(103, 21);
            this.cboFilterByList.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(466, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type:";
            // 
            // txtFilterBySubCategory
            // 
            this.txtFilterBySubCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterBySubCategory.Location = new System.Drawing.Point(354, 33);
            this.txtFilterBySubCategory.Name = "txtFilterBySubCategory";
            this.txtFilterBySubCategory.Size = new System.Drawing.Size(110, 20);
            this.txtFilterBySubCategory.TabIndex = 6;
            this.txtFilterBySubCategory.TextChanged += new System.EventHandler(this.FilterContols_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Category:";
            // 
            // txtFilterByCategory
            // 
            this.txtFilterByCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterByCategory.Location = new System.Drawing.Point(239, 33);
            this.txtFilterByCategory.Name = "txtFilterByCategory";
            this.txtFilterByCategory.Size = new System.Drawing.Size(110, 20);
            this.txtFilterByCategory.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "List:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Sub Category:";
            // 
            // txtFilterByType
            // 
            this.txtFilterByType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterByType.Location = new System.Drawing.Point(124, 33);
            this.txtFilterByType.Name = "txtFilterByType";
            this.txtFilterByType.Size = new System.Drawing.Size(110, 20);
            this.txtFilterByType.TabIndex = 6;
            // 
            // txtFilterByEmail
            // 
            this.txtFilterByEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterByEmail.Location = new System.Drawing.Point(469, 33);
            this.txtFilterByEmail.Name = "txtFilterByEmail";
            this.txtFilterByEmail.Size = new System.Drawing.Size(110, 20);
            this.txtFilterByEmail.TabIndex = 6;
            // 
            // chkFilterPreferredColumns
            // 
            this.chkFilterPreferredColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFilterPreferredColumns.AutoSize = true;
            this.chkFilterPreferredColumns.Checked = true;
            this.chkFilterPreferredColumns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterPreferredColumns.Location = new System.Drawing.Point(317, 532);
            this.chkFilterPreferredColumns.Name = "chkFilterPreferredColumns";
            this.chkFilterPreferredColumns.Size = new System.Drawing.Size(166, 17);
            this.chkFilterPreferredColumns.TabIndex = 17;
            this.chkFilterPreferredColumns.Text = "Show Preferred Columns Only";
            this.chkFilterPreferredColumns.UseVisualStyleBackColor = true;
            this.chkFilterPreferredColumns.CheckedChanged += new System.EventHandler(this.chkFilterPreferredColumns_CheckedChanged);
            // 
            // btnRefreshFilterContact
            // 
            this.btnRefreshFilterContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshFilterContact.Location = new System.Drawing.Point(3, 532);
            this.btnRefreshFilterContact.Name = "btnRefreshFilterContact";
            this.btnRefreshFilterContact.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshFilterContact.TabIndex = 16;
            this.btnRefreshFilterContact.Text = "Refresh";
            this.btnRefreshFilterContact.UseVisualStyleBackColor = true;
            this.btnRefreshFilterContact.Click += new System.EventHandler(this.btnRefreshFilterContact_Click);
            // 
            // grdFilterContact
            // 
            this.grdFilterContact.AllowUserToAddRows = false;
            this.grdFilterContact.AllowUserToDeleteRows = false;
            this.grdFilterContact.AllowUserToOrderColumns = true;
            this.grdFilterContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFilterContact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFilterContact.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdFilterContact.Location = new System.Drawing.Point(3, 73);
            this.grdFilterContact.Name = "grdFilterContact";
            this.grdFilterContact.ReadOnly = true;
            this.grdFilterContact.Size = new System.Drawing.Size(681, 453);
            this.grdFilterContact.TabIndex = 14;
            this.grdFilterContact.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdFilterContact_DataError);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(84, 532);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ContactControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkFilterPreferredColumns);
            this.Controls.Add(this.btnRefreshFilterContact);
            this.Controls.Add(this.grdFilterContact);
            this.Name = "ContactControl";
            this.Size = new System.Drawing.Size(698, 556);
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFilterContact)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFilterReset;
        private System.Windows.Forms.ComboBox cboFilterByList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilterBySubCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilterByCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilterByType;
        private System.Windows.Forms.TextBox txtFilterByEmail;
        private System.Windows.Forms.CheckBox chkFilterPreferredColumns;
        internal System.Windows.Forms.Button btnRefreshFilterContact;
        private System.Windows.Forms.DataGridView grdFilterContact;
        private System.Windows.Forms.Button btnSave;
    }
}
