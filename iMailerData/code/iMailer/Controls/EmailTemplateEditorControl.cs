using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Infolancers.iMailer
{
    public partial class EmailTemplateEditorControl : UserControl
    {
        public EmailTemplateEditorControl()
        {
            InitializeComponent();
        }

        private string _filename = null;
        private DataTable _dtTemplate;

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            //ArrayList oFields, oFieldsPrefix;
            //oFields = DataAccess.DataManager.CurrentDataProvider.GetMappingFieldsArray();
            //oFieldsPrefix = DataAccess.DataManager.CurrentDataProvider.GetMappingFieldsArray();

            //cboFields.DataSource = oFields;
            //cboFields.SelectedIndex = -1;

            //cboFieldsPrefix.DataSource = oFieldsPrefix;
            //cboFieldsPrefix.SelectedIndex = -1;

            //this.cboFields.SelectedIndexChanged += new System.EventHandler(this.cboFields_SelectedIndexChanged);
            //this.cboFieldsPrefix.SelectedIndexChanged += new System.EventHandler(this.cboFieldsPrefix_SelectedIndexChanged);

            if (this.DesignMode == true) return;

            DataTable dt;
            dt = DataAccess.DataManager.CurrentDataProvider.GetMapping();
            cboFields.DataSource = dt;
            cboFields.DisplayMember = "NewFieldName";
            cboFields.ValueMember = "NewFieldName";
            cboFields.SelectedIndex = -1;

            cboFieldsPrefix.DataSource = dt;
            cboFieldsPrefix.DisplayMember = "NewFieldName";
            cboFieldsPrefix.ValueMember = "NewFieldName";
            cboFieldsPrefix.SelectedIndex = -1;

            this.cboFields.SelectedIndexChanged += new System.EventHandler(this.cboFields_SelectedIndexChanged);
            this.cboFieldsPrefix.SelectedIndexChanged += new System.EventHandler(this.cboFieldsPrefix_SelectedIndexChanged);

            //this.cboFields.SelectedIndexChanged += new System.EventHandler(this.cboFields_SelectedIndexChanged);
            //this.cboFieldsPrefix.SelectedIndexChanged += new System.EventHandler(this.cboFieldsPrefix_SelectedIndexChanged);

            btnLoad.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void LoadFile(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                webBrowser1.Navigate(filename);
            }
            else
            {
                webBrowser1.Navigate("about:blank");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "HTML files (*.html;*.htm)|*.html;*.htm";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                }
                else
                    return;
            }
            LoadFile(_filename);
            lnkHTMLFilePath.Text = _filename;
        }

        private void btnGeneratePlainText_Click(object sender, EventArgs e)
        {
            txtPlainText.Text = webBrowser1.Document.Body.InnerText;
        }

        private void TemplateEditor_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _dtTemplate = DataAccess.DataManager.CurrentDataProvider.GetTemplate("");
            BindControls();
            grdTemplate.DataSource = _dtTemplate;
            if (_dtTemplate.Rows.Count > 0)
                grdTemplate.Rows[0].Selected = true;
            grdTemplate.ReadOnly = true;
            grdTemplate.AllowUserToDeleteRows = false;
            grdTemplate.AllowUserToAddRows = false;
            grdTemplate.Columns["EmailTemplateName"].Width = grdTemplate.Width - 5;
            Helper.ShowColumns(grdTemplate, "EmailTemplateName", false);
            Helper.FormatDataGrid(grdTemplate, false);
            Helper.SetHeading(grdTemplate, _dtTemplate, false);
        }

        private void BindControls()
        {
            if (_dtTemplate == null)
                return;

            this.txtSubject.DataBindings.Clear();
            this.txtTemplateName.DataBindings.Clear();
            this.txtPlainText.DataBindings.Clear();
            this.txtAttachments.DataBindings.Clear();
            this.txtAttachmentPrefix.DataBindings.Clear();
            this.lnkHTMLFilePath.DataBindings.Clear();
            this.txtSubject.DataBindings.Add(new Binding("Text", _dtTemplate, "EmailTemplateSubject"));
            this.txtTemplateName.DataBindings.Add(new Binding("Text", _dtTemplate, "EmailTemplateName"));
            this.txtPlainText.DataBindings.Add(new Binding("Text", _dtTemplate, "EmailPlainText"));
            this.txtAttachments.DataBindings.Add(new Binding("Text", _dtTemplate, "AttachmentFilePath"));
            this.txtAttachmentPrefix.DataBindings.Add(new Binding("Text", _dtTemplate, "AttachmentPrefix"));
            this.lnkHTMLFilePath.DataBindings.Add(new Binding("Text", _dtTemplate, "EmailHTMLText"));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int value;
            DataTable dt;
            if (_dtTemplate == null)
                return;

            this.txtSubject.DataBindings[0].BindingManagerBase.EndCurrentEdit();
            dt = _dtTemplate.GetChanges();

            if (dt == null)
                return;

            value = DataAccess.DataManager.CurrentDataProvider.SaveTemplate(dt);
            if (value == dt.Rows.Count)
            {
                Helper.ShowMessage("Template data saved successfully.", "Save", MessageType.Information);
                _dtTemplate.AcceptChanges();
            }
            else
                Helper.ShowMessage("Unable to save Template data", "Error", MessageType.Error);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            _dtTemplate.Rows.Add(_dtTemplate.NewRow());
            grdTemplate.Rows[grdTemplate.Rows.Count - 1].Selected = true;
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                string strFiles;
                dlg.Filter = "All files (*.*)|*.*";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    strFiles = txtAttachments.Text;
                    if (!string.IsNullOrEmpty(strFiles))
                        strFiles = strFiles + System.Environment.NewLine;
                    strFiles += dlg.FileName;

                    txtAttachments.Text = strFiles;
                }
                else
                    return;
            }
        }

        private void grdTemplate_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.txtSubject.DataBindings[0].BindingManagerBase.EndCurrentEdit();
        }

        private void lnkHTMLFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openToolStripMenuItem_Click(null, null);
        }

        private void lnkHTMLFilePath_TextChanged(object sender, EventArgs e)
        {
            string fileName = lnkHTMLFilePath.Text;
            fileName = lnkHTMLFilePath.Text;
            if (!string.IsNullOrEmpty(fileName) && !System.IO.File.Exists(fileName))
            {
                Helper.ShowMessage(string.Format("File '{0}' not found.", fileName), "Invalid File");
                fileName = string.Empty;
            }

            LoadFile(fileName);
        }

        private void grdTemplate_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.txtSubject.DataBindings[0].BindingManagerBase.EndCurrentEdit();
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboFields.Text))
            {
                txtPlainText.SelectedText = "{" + cboFields.Text + "}";
            }
        }

        private void cboFieldsPrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboFieldsPrefix.Text))
            {
                txtAttachmentPrefix.SelectedText = "{" + cboFieldsPrefix.Text + "}";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void lnkHTMLFilePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                HTMLEditor form;
                form = new HTMLEditor(lnkHTMLFilePath.Text);
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Helper.ShowMessage(ex.Message, "Error", MessageType.Error);
                Logger.ErrorLog.ErrorRoutine(ex, "In HTMLEditor Form");
            }
        }

    }
}
