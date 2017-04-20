namespace Infolancers.iMailer.Controls
{
    partial class ImportControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageSelectCSV = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnShowMapping = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileToImport = new System.Windows.Forms.TextBox();
            this.gpbEncoding = new System.Windows.Forms.GroupBox();
            this.rdbOEM = new System.Windows.Forms.RadioButton();
            this.rdbUnicode = new System.Windows.Forms.RadioButton();
            this.rdbAnsi = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.grdPreview = new System.Windows.Forms.DataGridView();
            this.gpbSeparator = new System.Windows.Forms.GroupBox();
            this.txtSeparatorOtherChar = new System.Windows.Forms.TextBox();
            this.rdbSeparatorOther = new System.Windows.Forms.RadioButton();
            this.rdbTab = new System.Windows.Forms.RadioButton();
            this.rdbSemicolon = new System.Windows.Forms.RadioButton();
            this.chkFirstRowColumnNames = new System.Windows.Forms.CheckBox();
            this.pageMapping = new System.Windows.Forms.TabPage();
            this.btnShowImport = new System.Windows.Forms.Button();
            this.grpMapping = new System.Windows.Forms.GroupBox();
            this.grdMapping = new System.Windows.Forms.DataGridView();
            this.pageImportToDB = new System.Windows.Forms.TabPage();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.grdImport = new System.Windows.Forms.DataGridView();
            this.pageContacts = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowPreferred = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.grdContacts = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.pageSelectCSV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gpbEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).BeginInit();
            this.gpbSeparator.SuspendLayout();
            this.pageMapping.SuspendLayout();
            this.grpMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMapping)).BeginInit();
            this.pageImportToDB.SuspendLayout();
            this.grpImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdImport)).BeginInit();
            this.pageContacts.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pageSelectCSV);
            this.tabControl1.Controls.Add(this.pageMapping);
            this.tabControl1.Controls.Add(this.pageImportToDB);
            this.tabControl1.Controls.Add(this.pageContacts);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 562);
            this.tabControl1.TabIndex = 10;
            // 
            // pageSelectCSV
            // 
            this.pageSelectCSV.BackColor = System.Drawing.Color.Transparent;
            this.pageSelectCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pageSelectCSV.Controls.Add(this.label2);
            this.pageSelectCSV.Controls.Add(this.numericUpDown1);
            this.pageSelectCSV.Controls.Add(this.btnShowMapping);
            this.pageSelectCSV.Controls.Add(this.label1);
            this.pageSelectCSV.Controls.Add(this.txtFileToImport);
            this.pageSelectCSV.Controls.Add(this.gpbEncoding);
            this.pageSelectCSV.Controls.Add(this.btnBrowse);
            this.pageSelectCSV.Controls.Add(this.btnPreview);
            this.pageSelectCSV.Controls.Add(this.grdPreview);
            this.pageSelectCSV.Controls.Add(this.gpbSeparator);
            this.pageSelectCSV.Controls.Add(this.chkFirstRowColumnNames);
            this.pageSelectCSV.Location = new System.Drawing.Point(4, 22);
            this.pageSelectCSV.Name = "pageSelectCSV";
            this.pageSelectCSV.Padding = new System.Windows.Forms.Padding(3);
            this.pageSelectCSV.Size = new System.Drawing.Size(617, 536);
            this.pageSelectCSV.TabIndex = 0;
            this.pageSelectCSV.Text = "Open CSV";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Load only first rows";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(434, 115);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnShowMapping
            // 
            this.btnShowMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowMapping.Location = new System.Drawing.Point(503, 506);
            this.btnShowMapping.Name = "btnShowMapping";
            this.btnShowMapping.Size = new System.Drawing.Size(108, 23);
            this.btnShowMapping.TabIndex = 10;
            this.btnShowMapping.Text = "&Next";
            this.btnShowMapping.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSV file to load:";
            // 
            // txtFileToImport
            // 
            this.txtFileToImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileToImport.Location = new System.Drawing.Point(100, 7);
            this.txtFileToImport.Name = "txtFileToImport";
            this.txtFileToImport.Size = new System.Drawing.Size(398, 20);
            this.txtFileToImport.TabIndex = 1;
            // 
            // gpbEncoding
            // 
            this.gpbEncoding.Controls.Add(this.rdbOEM);
            this.gpbEncoding.Controls.Add(this.rdbUnicode);
            this.gpbEncoding.Controls.Add(this.rdbAnsi);
            this.gpbEncoding.Location = new System.Drawing.Point(154, 34);
            this.gpbEncoding.Name = "gpbEncoding";
            this.gpbEncoding.Size = new System.Drawing.Size(138, 94);
            this.gpbEncoding.TabIndex = 7;
            this.gpbEncoding.TabStop = false;
            this.gpbEncoding.Text = "Encoding";
            // 
            // rdbOEM
            // 
            this.rdbOEM.AutoSize = true;
            this.rdbOEM.Location = new System.Drawing.Point(6, 63);
            this.rdbOEM.Name = "rdbOEM";
            this.rdbOEM.Size = new System.Drawing.Size(49, 17);
            this.rdbOEM.TabIndex = 2;
            this.rdbOEM.Text = "OEM";
            this.rdbOEM.UseVisualStyleBackColor = true;
            // 
            // rdbUnicode
            // 
            this.rdbUnicode.AutoSize = true;
            this.rdbUnicode.Location = new System.Drawing.Point(6, 42);
            this.rdbUnicode.Name = "rdbUnicode";
            this.rdbUnicode.Size = new System.Drawing.Size(65, 17);
            this.rdbUnicode.TabIndex = 1;
            this.rdbUnicode.Text = "Unicode";
            this.rdbUnicode.UseVisualStyleBackColor = true;
            // 
            // rdbAnsi
            // 
            this.rdbAnsi.AutoSize = true;
            this.rdbAnsi.Checked = true;
            this.rdbAnsi.Location = new System.Drawing.Point(6, 19);
            this.rdbAnsi.Name = "rdbAnsi";
            this.rdbAnsi.Size = new System.Drawing.Size(50, 17);
            this.rdbAnsi.TabIndex = 0;
            this.rdbAnsi.TabStop = true;
            this.rdbAnsi.Text = "ANSI";
            this.rdbAnsi.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(504, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(108, 22);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(504, 111);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 25);
            this.btnPreview.TabIndex = 6;
            this.btnPreview.Text = "Load Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // grdPreview
            // 
            this.grdPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPreview.Location = new System.Drawing.Point(6, 142);
            this.grdPreview.Name = "grdPreview";
            this.grdPreview.Size = new System.Drawing.Size(605, 358);
            this.grdPreview.TabIndex = 3;
            // 
            // gpbSeparator
            // 
            this.gpbSeparator.Controls.Add(this.txtSeparatorOtherChar);
            this.gpbSeparator.Controls.Add(this.rdbSeparatorOther);
            this.gpbSeparator.Controls.Add(this.rdbTab);
            this.gpbSeparator.Controls.Add(this.rdbSemicolon);
            this.gpbSeparator.Location = new System.Drawing.Point(9, 34);
            this.gpbSeparator.Name = "gpbSeparator";
            this.gpbSeparator.Size = new System.Drawing.Size(129, 94);
            this.gpbSeparator.TabIndex = 5;
            this.gpbSeparator.TabStop = false;
            this.gpbSeparator.Text = "Separator";
            // 
            // txtSeparatorOtherChar
            // 
            this.txtSeparatorOtherChar.Location = new System.Drawing.Point(73, 66);
            this.txtSeparatorOtherChar.MaxLength = 1;
            this.txtSeparatorOtherChar.Name = "txtSeparatorOtherChar";
            this.txtSeparatorOtherChar.Size = new System.Drawing.Size(24, 20);
            this.txtSeparatorOtherChar.TabIndex = 3;
            this.txtSeparatorOtherChar.Text = ",";
            // 
            // rdbSeparatorOther
            // 
            this.rdbSeparatorOther.AutoSize = true;
            this.rdbSeparatorOther.Location = new System.Drawing.Point(6, 65);
            this.rdbSeparatorOther.Name = "rdbSeparatorOther";
            this.rdbSeparatorOther.Size = new System.Drawing.Size(54, 17);
            this.rdbSeparatorOther.TabIndex = 2;
            this.rdbSeparatorOther.Text = "Other:";
            this.rdbSeparatorOther.UseVisualStyleBackColor = true;
            // 
            // rdbTab
            // 
            this.rdbTab.AutoSize = true;
            this.rdbTab.Location = new System.Drawing.Point(6, 42);
            this.rdbTab.Name = "rdbTab";
            this.rdbTab.Size = new System.Drawing.Size(46, 17);
            this.rdbTab.TabIndex = 1;
            this.rdbTab.Text = "TAB";
            this.rdbTab.UseVisualStyleBackColor = true;
            // 
            // rdbSemicolon
            // 
            this.rdbSemicolon.AutoSize = true;
            this.rdbSemicolon.Checked = true;
            this.rdbSemicolon.Location = new System.Drawing.Point(6, 19);
            this.rdbSemicolon.Name = "rdbSemicolon";
            this.rdbSemicolon.Size = new System.Drawing.Size(74, 17);
            this.rdbSemicolon.TabIndex = 0;
            this.rdbSemicolon.TabStop = true;
            this.rdbSemicolon.Text = "Semicolon";
            this.rdbSemicolon.UseVisualStyleBackColor = true;
            // 
            // chkFirstRowColumnNames
            // 
            this.chkFirstRowColumnNames.AutoSize = true;
            this.chkFirstRowColumnNames.Checked = true;
            this.chkFirstRowColumnNames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstRowColumnNames.Location = new System.Drawing.Point(305, 43);
            this.chkFirstRowColumnNames.Name = "chkFirstRowColumnNames";
            this.chkFirstRowColumnNames.Size = new System.Drawing.Size(156, 17);
            this.chkFirstRowColumnNames.TabIndex = 4;
            this.chkFirstRowColumnNames.Text = "First row has column names";
            this.chkFirstRowColumnNames.UseVisualStyleBackColor = true;
            // 
            // pageMapping
            // 
            this.pageMapping.BackColor = System.Drawing.Color.Transparent;
            this.pageMapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pageMapping.Controls.Add(this.btnShowImport);
            this.pageMapping.Controls.Add(this.grpMapping);
            this.pageMapping.Location = new System.Drawing.Point(4, 22);
            this.pageMapping.Name = "pageMapping";
            this.pageMapping.Padding = new System.Windows.Forms.Padding(3);
            this.pageMapping.Size = new System.Drawing.Size(860, 546);
            this.pageMapping.TabIndex = 1;
            this.pageMapping.Text = "Mapping";
            // 
            // btnShowImport
            // 
            this.btnShowImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowImport.Location = new System.Drawing.Point(744, 520);
            this.btnShowImport.Name = "btnShowImport";
            this.btnShowImport.Size = new System.Drawing.Size(108, 23);
            this.btnShowImport.TabIndex = 11;
            this.btnShowImport.Text = "&Next";
            this.btnShowImport.UseVisualStyleBackColor = true;
            // 
            // grpMapping
            // 
            this.grpMapping.Controls.Add(this.grdMapping);
            this.grpMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMapping.Location = new System.Drawing.Point(3, 3);
            this.grpMapping.Name = "grpMapping";
            this.grpMapping.Size = new System.Drawing.Size(854, 540);
            this.grpMapping.TabIndex = 0;
            this.grpMapping.TabStop = false;
            this.grpMapping.Text = "Map Database Fields";
            // 
            // grdMapping
            // 
            this.grdMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMapping.Location = new System.Drawing.Point(3, 16);
            this.grdMapping.Name = "grdMapping";
            this.grdMapping.Size = new System.Drawing.Size(848, 495);
            this.grdMapping.TabIndex = 0;
            // 
            // pageImportToDB
            // 
            this.pageImportToDB.BackColor = System.Drawing.Color.Transparent;
            this.pageImportToDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pageImportToDB.Controls.Add(this.grpImport);
            this.pageImportToDB.Location = new System.Drawing.Point(4, 22);
            this.pageImportToDB.Name = "pageImportToDB";
            this.pageImportToDB.Size = new System.Drawing.Size(860, 546);
            this.pageImportToDB.TabIndex = 2;
            this.pageImportToDB.Text = "Import to Database";
            // 
            // grpImport
            // 
            this.grpImport.Controls.Add(this.btnImport);
            this.grpImport.Controls.Add(this.grdImport);
            this.grpImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpImport.Location = new System.Drawing.Point(0, 0);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(860, 546);
            this.grpImport.TabIndex = 0;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "Import";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(777, 517);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // grdImport
            // 
            this.grdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdImport.Location = new System.Drawing.Point(6, 19);
            this.grdImport.Name = "grdImport";
            this.grdImport.Size = new System.Drawing.Size(846, 492);
            this.grdImport.TabIndex = 0;
            // 
            // pageContacts
            // 
            this.pageContacts.BackColor = System.Drawing.Color.Transparent;
            this.pageContacts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pageContacts.Controls.Add(this.groupBox1);
            this.pageContacts.Location = new System.Drawing.Point(4, 22);
            this.pageContacts.Name = "pageContacts";
            this.pageContacts.Size = new System.Drawing.Size(860, 546);
            this.pageContacts.TabIndex = 3;
            this.pageContacts.Text = "View Imported Contacts";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowPreferred);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.grdContacts);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 546);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Imported Contacts";
            // 
            // chkShowPreferred
            // 
            this.chkShowPreferred.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowPreferred.AutoSize = true;
            this.chkShowPreferred.Checked = true;
            this.chkShowPreferred.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPreferred.Location = new System.Drawing.Point(589, 521);
            this.chkShowPreferred.Name = "chkShowPreferred";
            this.chkShowPreferred.Size = new System.Drawing.Size(166, 17);
            this.chkShowPreferred.TabIndex = 3;
            this.chkShowPreferred.Text = "Show Preferred Columns Only";
            this.chkShowPreferred.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(777, 517);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // grdContacts
            // 
            this.grdContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdContacts.Location = new System.Drawing.Point(6, 19);
            this.grdContacts.Name = "grdContacts";
            this.grdContacts.Size = new System.Drawing.Size(846, 492);
            this.grdContacts.TabIndex = 0;
            // 
            // ImportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ImportControl";
            this.Size = new System.Drawing.Size(625, 562);
            this.tabControl1.ResumeLayout(false);
            this.pageSelectCSV.ResumeLayout(false);
            this.pageSelectCSV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gpbEncoding.ResumeLayout(false);
            this.gpbEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).EndInit();
            this.gpbSeparator.ResumeLayout(false);
            this.gpbSeparator.PerformLayout();
            this.pageMapping.ResumeLayout(false);
            this.grpMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMapping)).EndInit();
            this.pageImportToDB.ResumeLayout(false);
            this.grpImport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdImport)).EndInit();
            this.pageContacts.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdContacts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageSelectCSV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnShowMapping;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileToImport;
        private System.Windows.Forms.GroupBox gpbEncoding;
        private System.Windows.Forms.RadioButton rdbOEM;
        private System.Windows.Forms.RadioButton rdbUnicode;
        private System.Windows.Forms.RadioButton rdbAnsi;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.DataGridView grdPreview;
        private System.Windows.Forms.GroupBox gpbSeparator;
        private System.Windows.Forms.TextBox txtSeparatorOtherChar;
        private System.Windows.Forms.RadioButton rdbSeparatorOther;
        private System.Windows.Forms.RadioButton rdbTab;
        private System.Windows.Forms.RadioButton rdbSemicolon;
        private System.Windows.Forms.CheckBox chkFirstRowColumnNames;
        private System.Windows.Forms.TabPage pageMapping;
        private System.Windows.Forms.Button btnShowImport;
        private System.Windows.Forms.GroupBox grpMapping;
        private System.Windows.Forms.DataGridView grdMapping;
        private System.Windows.Forms.TabPage pageImportToDB;
        private System.Windows.Forms.GroupBox grpImport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView grdImport;
        private System.Windows.Forms.TabPage pageContacts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowPreferred;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdContacts;

    }
}
