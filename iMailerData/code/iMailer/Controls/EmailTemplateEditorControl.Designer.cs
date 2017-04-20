namespace Infolancers.iMailer
{
    partial class EmailTemplateEditorControl
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
            this.txtAttachmentPrefix = new System.Windows.Forms.TextBox();
            this.cboFieldsPrefix = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lnkHTMLFilePath = new System.Windows.Forms.LinkLabel();
            this.lnkHTMLOpenFile = new System.Windows.Forms.LinkLabel();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAttachments = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grdTemplate = new System.Windows.Forms.DataGridView();
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFields = new System.Windows.Forms.ComboBox();
            this.lblPlainText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGeneratePlainText = new System.Windows.Forms.Button();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAttachmentPrefix
            // 
            this.txtAttachmentPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttachmentPrefix.Location = new System.Drawing.Point(102, 92);
            this.txtAttachmentPrefix.Name = "txtAttachmentPrefix";
            this.txtAttachmentPrefix.Size = new System.Drawing.Size(516, 20);
            this.txtAttachmentPrefix.TabIndex = 37;
            // 
            // cboFieldsPrefix
            // 
            this.cboFieldsPrefix.FormattingEnabled = true;
            this.cboFieldsPrefix.Location = new System.Drawing.Point(218, 68);
            this.cboFieldsPrefix.Name = "cboFieldsPrefix";
            this.cboFieldsPrefix.Size = new System.Drawing.Size(212, 21);
            this.cboFieldsPrefix.Sorted = true;
            this.cboFieldsPrefix.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Attachment Prefix:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Insert Field:";
            // 
            // lnkHTMLFilePath
            // 
            this.lnkHTMLFilePath.AutoSize = true;
            this.lnkHTMLFilePath.Location = new System.Drawing.Point(89, 123);
            this.lnkHTMLFilePath.Name = "lnkHTMLFilePath";
            this.lnkHTMLFilePath.Size = new System.Drawing.Size(67, 13);
            this.lnkHTMLFilePath.TabIndex = 35;
            this.lnkHTMLFilePath.TabStop = true;
            this.lnkHTMLFilePath.Text = "Full File Path";
            this.lnkHTMLFilePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHTMLFilePath_LinkClicked);
            this.lnkHTMLFilePath.TextChanged += new System.EventHandler(this.lnkHTMLFilePath_TextChanged);
            // 
            // lnkHTMLOpenFile
            // 
            this.lnkHTMLOpenFile.AutoSize = true;
            this.lnkHTMLOpenFile.Location = new System.Drawing.Point(3, 123);
            this.lnkHTMLOpenFile.Name = "lnkHTMLOpenFile";
            this.lnkHTMLOpenFile.Size = new System.Drawing.Size(59, 13);
            this.lnkHTMLOpenFile.TabIndex = 29;
            this.lnkHTMLOpenFile.TabStop = true;
            this.lnkHTMLOpenFile.Text = "Select File:";
            this.lnkHTMLOpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHTMLFile_LinkClicked);
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Location = new System.Drawing.Point(92, 74);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(333, 20);
            this.txtTemplateName.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.webBrowser1);
            this.groupBox1.Location = new System.Drawing.Point(6, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(621, 168);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTML";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 16);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(615, 149);
            this.webBrowser1.TabIndex = 0;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(3, 25);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 33;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Attachements:";
            // 
            // txtAttachments
            // 
            this.txtAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttachments.Location = new System.Drawing.Point(86, 9);
            this.txtAttachments.Multiline = true;
            this.txtAttachments.Name = "txtAttachments";
            this.txtAttachments.Size = new System.Drawing.Size(532, 54);
            this.txtAttachments.TabIndex = 34;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(431, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(115, 23);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.Text = "Refresh";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // grdTemplate
            // 
            this.grdTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTemplate.Location = new System.Drawing.Point(0, 0);
            this.grdTemplate.Name = "grdTemplate";
            this.grdTemplate.Size = new System.Drawing.Size(425, 72);
            this.grdTemplate.TabIndex = 18;
            this.grdTemplate.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTemplate_CellValidated);
            this.grdTemplate.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTemplate_RowLeave);
            // 
            // txtPlainText
            // 
            this.txtPlainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlainText.Location = new System.Drawing.Point(6, 32);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPlainText.Size = new System.Drawing.Size(612, 118);
            this.txtPlainText.TabIndex = 4;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(431, 29);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(115, 23);
            this.btnAddNew.TabIndex = 19;
            this.btnAddNew.Text = "Add New Template";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
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
            // cboFields
            // 
            this.cboFields.FormattingEnabled = true;
            this.cboFields.Location = new System.Drawing.Point(218, 5);
            this.cboFields.Name = "cboFields";
            this.cboFields.Size = new System.Drawing.Size(212, 21);
            this.cboFields.Sorted = true;
            this.cboFields.TabIndex = 2;
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cboFields);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblPlainText);
            this.panel1.Controls.Add(this.txtPlainText);
            this.panel1.Controls.Add(this.btnGeneratePlainText);
            this.panel1.Location = new System.Drawing.Point(6, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 153);
            this.panel1.TabIndex = 31;
            // 
            // btnGeneratePlainText
            // 
            this.btnGeneratePlainText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePlainText.Location = new System.Drawing.Point(503, 4);
            this.btnGeneratePlainText.Name = "btnGeneratePlainText";
            this.btnGeneratePlainText.Size = new System.Drawing.Size(115, 23);
            this.btnGeneratePlainText.TabIndex = 3;
            this.btnGeneratePlainText.Text = "Generate Plain Text";
            this.btnGeneratePlainText.UseVisualStyleBackColor = true;
            this.btnGeneratePlainText.Click += new System.EventHandler(this.btnGeneratePlainText_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(92, 100);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(535, 20);
            this.txtSubject.TabIndex = 28;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(3, 102);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 27;
            this.lblSubject.Text = "Subject:";
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Location = new System.Drawing.Point(3, 75);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(91, 21);
            this.lblTemplateName.TabIndex = 23;
            this.lblTemplateName.Text = "Template Name:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(431, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save Template";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtAttachmentPrefix);
            this.panel2.Controls.Add(this.txtAttachments);
            this.panel2.Controls.Add(this.cboFieldsPrefix);
            this.panel2.Controls.Add(this.btnAddFile);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(6, 472);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(624, 117);
            this.panel2.TabIndex = 38;
            // 
            // EmailTemplateEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lnkHTMLFilePath);
            this.Controls.Add(this.lnkHTMLOpenFile);
            this.Controls.Add(this.txtTemplateName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.grdTemplate);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.btnSave);
            this.Name = "EmailTemplateEditorControl";
            this.Size = new System.Drawing.Size(639, 592);
            this.Load += new System.EventHandler(this.TemplateEditor_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAttachmentPrefix;
        private System.Windows.Forms.ComboBox cboFieldsPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkHTMLFilePath;
        private System.Windows.Forms.LinkLabel lnkHTMLOpenFile;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAttachments;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView grdTemplate;
        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFields;
        private System.Windows.Forms.Label lblPlainText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGeneratePlainText;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
    }
}
