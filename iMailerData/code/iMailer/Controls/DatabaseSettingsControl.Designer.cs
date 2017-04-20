namespace Infolancers.iMailer
{
    partial class DatabaseSettingsControl
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
            this.grpFieldMapping = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grdMapping = new System.Windows.Forms.DataGridView();
            this.grpFieldMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFieldMapping
            // 
            this.grpFieldMapping.Controls.Add(this.btnClose);
            this.grpFieldMapping.Controls.Add(this.btnSave);
            this.grpFieldMapping.Controls.Add(this.grdMapping);
            this.grpFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.grpFieldMapping.Name = "grpFieldMapping";
            this.grpFieldMapping.Size = new System.Drawing.Size(664, 583);
            this.grpFieldMapping.TabIndex = 1;
            this.grpFieldMapping.TabStop = false;
            this.grpFieldMapping.Text = "Field Mapping";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(571, 554);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(490, 554);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdMapping
            // 
            this.grdMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMapping.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdMapping.Location = new System.Drawing.Point(6, 19);
            this.grdMapping.Name = "grdMapping";
            this.grdMapping.Size = new System.Drawing.Size(652, 529);
            this.grdMapping.TabIndex = 0;
            this.grdMapping.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMapping_CellValidated);
            // 
            // DatabaseSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpFieldMapping);
            this.Name = "DatabaseSettings";
            this.Size = new System.Drawing.Size(664, 583);
            this.Load += new System.EventHandler(this.FieldMapping_Load);
            this.Resize += new System.EventHandler(this.FieldMapping_Resize);
            this.grpFieldMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMapping)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFieldMapping;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView grdMapping;
    }
}
