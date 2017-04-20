namespace Infolancers.iMailer
{
    partial class EmailTemplateEditor
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
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboFields = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPlainText = new System.Windows.Forms.Label();
            this.btnGeneratePlainText = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.grdTemplate = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtAttachments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lnkHTMLOpenFile = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lnkHTMLFilePath = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFieldsPrefix = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAttachmentPrefix = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPlainText
            // 
            this.txtPlainText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPlainText.Location = new System.Drawing.Point(0, 32);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPlainText.Size = new System.Drawing.Size(629, 123);
            this.txtPlainText.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cboFields);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblPlainText);
            this.panel1.Controls.Add(this.txtPlainText);
            this.panel1.Controls.Add(this.btnGeneratePlainText);
            this.panel1.Location = new System.Drawing.Point(295, 365);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 155);
            this.panel1.TabIndex = 11;
            // 
            // cboFields
            // 
            this.cboFields.FormattingEnabled = true;
            this.cboFields.Location = new System.Drawing.Point(218, 5);
            this.cboFields.Name = "cboFields";
            this.cboFields.Size = new System.Drawing.Size(170, 21);
            this.cboFields.Sorted = true;
            this.cboFields.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Insert Field:";
            // 
            // lblPlainText
            // 
            this.lblPlainText.AutoSize = true;
            this.lblPlainText.Location = new System.Drawing.Point(3, 9);
            this.lblPlainText.Name = "lblPlainText";
            this.lblPlainText.Size = new System.Drawing.Size(57, 13);
            this.lblPlainText.TabIndex = 0;
            this.lblPlainText.Text = "Plain Text:";
            // 
            // btnGeneratePlainText
            // 
            this.btnGeneratePlainText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePlainText.Location = new System.Drawing.Point(511, 3);
            this.btnGeneratePlainText.Name = "btnGeneratePlainText";
            this.btnGeneratePlainText.Size = new System.Drawing.Size(115, 23);
            this.btnGeneratePlainText.TabIndex = 3;
            this.btnGeneratePlainText.Text = "Generate Plain Text";
            this.btnGeneratePlainText.UseVisualStyleBackColor = true;
            this.btnGeneratePlainText.Click += new System.EventHandler(this.btnGeneratePlainText_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(417, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Template";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Location = new System.Drawing.Point(295, 29);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(91, 21);
            this.lblTemplateName.TabIndex = 4;
            this.lblTemplateName.Text = "Template Name:";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(295, 56);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 6;
            this.lblSubject.Text = "Subject:";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(384, 54);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(540, 20);
            this.txtSubject.TabIndex = 7;
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplateName.Location = new System.Drawing.Point(384, 28);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(540, 20);
            this.txtTemplateName.TabIndex = 5;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(295, 3);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(115, 23);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New Template";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // grdTemplate
            // 
            this.grdTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTemplate.Location = new System.Drawing.Point(6, 3);
            this.grdTemplate.Name = "grdTemplate";
            this.grdTemplate.Size = new System.Drawing.Size(283, 645);
            this.grdTemplate.TabIndex = 0;
            this.grdTemplate.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTemplate_CellValidated);
            this.grdTemplate.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTemplate_RowLeave);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(539, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(115, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Refresh";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtAttachments
            // 
            this.txtAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttachments.Location = new System.Drawing.Point(396, 529);
            this.txtAttachments.Multiline = true;
            this.txtAttachments.Name = "txtAttachments";
            this.txtAttachments.Size = new System.Drawing.Size(525, 66);
            this.txtAttachments.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Attachements:";
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(295, 539);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 13;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.webBrowser1);
            this.groupBox1.Location = new System.Drawing.Point(295, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 257);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTML";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 16);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(626, 238);
            this.webBrowser1.TabIndex = 0;
            // 
            // lnkHTMLOpenFile
            // 
            this.lnkHTMLOpenFile.AutoSize = true;
            this.lnkHTMLOpenFile.Location = new System.Drawing.Point(295, 86);
            this.lnkHTMLOpenFile.Name = "lnkHTMLOpenFile";
            this.lnkHTMLOpenFile.Size = new System.Drawing.Size(59, 13);
            this.lnkHTMLOpenFile.TabIndex = 8;
            this.lnkHTMLOpenFile.TabStop = true;
            this.lnkHTMLOpenFile.Text = "Select File:";
            this.lnkHTMLOpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHTMLFile_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(661, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lnkHTMLFilePath
            // 
            this.lnkHTMLFilePath.AutoSize = true;
            this.lnkHTMLFilePath.Location = new System.Drawing.Point(381, 86);
            this.lnkHTMLFilePath.Name = "lnkHTMLFilePath";
            this.lnkHTMLFilePath.Size = new System.Drawing.Size(67, 13);
            this.lnkHTMLFilePath.TabIndex = 15;
            this.lnkHTMLFilePath.TabStop = true;
            this.lnkHTMLFilePath.Text = "Full File Path";
            this.lnkHTMLFilePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHTMLFilePath_LinkClicked);
            this.lnkHTMLFilePath.TextChanged += new System.EventHandler(this.lnkHTMLFilePath_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 606);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Attachment Prefix:";
            // 
            // cboFieldsPrefix
            // 
            this.cboFieldsPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboFieldsPrefix.FormattingEnabled = true;
            this.cboFieldsPrefix.Location = new System.Drawing.Point(513, 599);
            this.cboFieldsPrefix.Name = "cboFieldsPrefix";
            this.cboFieldsPrefix.Size = new System.Drawing.Size(170, 21);
            this.cboFieldsPrefix.Sorted = true;
            this.cboFieldsPrefix.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 606);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Insert Field:";
            // 
            // txtAttachmentPrefix
            // 
            this.txtAttachmentPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAttachmentPrefix.Location = new System.Drawing.Point(396, 626);
            this.txtAttachmentPrefix.Name = "txtAttachmentPrefix";
            this.txtAttachmentPrefix.Size = new System.Drawing.Size(525, 20);
            this.txtAttachmentPrefix.TabIndex = 17;
            // 
            // EmailTemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(939, 653);
            this.Controls.Add(this.txtAttachmentPrefix);
            this.Controls.Add(this.cboFieldsPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lnkHTMLFilePath);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lnkHTMLOpenFile);
            this.Controls.Add(this.txtTemplateName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAttachments);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.grdTemplate);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Name = "EmailTemplateEditor";
            this.Text = "Template Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplateEditor_FormClosing);
            this.Load += new System.EventHandler(this.TemplateEditor_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPlainText;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGeneratePlainText;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView grdTemplate;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtAttachments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkHTMLOpenFile;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboFields;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkHTMLFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFieldsPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAttachmentPrefix;
    }
}