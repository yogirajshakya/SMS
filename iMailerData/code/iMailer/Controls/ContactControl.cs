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
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }

        private DataTable _dtFilterContacts;
        private DataTable _dtFilterEmailList;
        public event EventHandler SaveButtonClicked;

        [Browsable(true)]
        public bool ReadOnlyData { get; set; }

        [Browsable(true)]
        public bool LoadDataInitially { get; set; }

        [Browsable(true)]
        public bool LoadNullEmailData { get; set; }

        internal void OnLoad(object sender, EventArgs e)
        {
            if (this.DesignMode == true) return;
            if (_dtFilterEmailList == null)
            {
                this.grdFilterContact.AllowUserToAddRows = !ReadOnlyData;
                this.grdFilterContact.AllowUserToDeleteRows = !ReadOnlyData;
                this.grdFilterContact.ReadOnly = ReadOnlyData;
                this.btnSave.Visible = !ReadOnlyData;

                _dtFilterEmailList = DataAccess.DataManager.CurrentDataProvider.GetContactList("");

                this.cboFilterByList.SelectedIndexChanged -= new System.EventHandler(this.cboFilterByList_SelectedIndexChanged);
                cboFilterByList.DataSource = _dtFilterEmailList;
                cboFilterByList.DisplayMember = "ListName";
                cboFilterByList.ValueMember = "Filter";
                cboFilterByList.SelectedIndex = -1;
                this.cboFilterByList.SelectedIndexChanged += new System.EventHandler(this.cboFilterByList_SelectedIndexChanged);

                btnRefreshFilterContact.PerformClick();
            }
        }

        private void FilterContols_TextChanged(object sender, EventArgs e)
        {
            FilterRecords("");
        }

        private void FilterRecords(string additionalFilter)
        {
            if (_dtFilterContacts == null)
                return;

            Helper.WaitCursor(this);
            StringBuilder sb = new StringBuilder();
            DataView dv = _dtFilterContacts.DefaultView;

            sb.Append(additionalFilter);

            if (!LoadNullEmailData)
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.Append(" Email is not null ");
            }

            if (!string.IsNullOrEmpty(cboFilterByList.Text))
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.Append(cboFilterByList.SelectedValue.ToString());
            }


            if (!string.IsNullOrEmpty(txtFilterByType.Text))
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.AppendFormat(" CustomerType like '{0}%'", txtFilterByType.Text);
            }

            if (!string.IsNullOrEmpty(txtFilterByCategory.Text))
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.AppendFormat(" CustomerCategory like '{0}%'", txtFilterByCategory.Text);
            }

            if (!string.IsNullOrEmpty(txtFilterBySubCategory.Text))
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.AppendFormat(" CustomerSubCategory like '{0}%'", txtFilterBySubCategory.Text);
            }

            if (!string.IsNullOrEmpty(txtFilterByEmail.Text))
            {
                if (sb.Length > 0) sb.AppendLine(" and ");
                sb.AppendFormat(" Email like '{0}%'", txtFilterByEmail.Text);
            }

            dv.RowFilter = sb.ToString();
            Helper.WaitCursor(this, true);
        }

        private void btnRefreshFilterContact_Click(object sender, EventArgs e)
        {
            Helper.WaitCursor(this);
            if (LoadNullEmailData)
                _dtFilterContacts = DataAccess.DataManager.CurrentDataProvider.GetCustomers("Email is not null");
            else
                _dtFilterContacts = DataAccess.DataManager.CurrentDataProvider.GetCustomers("");

            grdFilterContact.DataSource = _dtFilterContacts.DefaultView;
            Helper.SetHeading(grdFilterContact, _dtFilterContacts, true);
            Helper.FormatDataGrid(grdFilterContact, true);
            Helper.ShowPreferredFields(grdFilterContact, chkFilterPreferredColumns.Checked);
            FilterRecords("");
            Helper.WaitCursor(this, true);
        }

        private void chkFilterPreferredColumns_CheckedChanged(object sender, EventArgs e)
        {
            Helper.ShowPreferredFields(grdFilterContact, chkFilterPreferredColumns.Checked);
        }

        private void cboFilterByList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterRecords("");
        }

        private void btnFilterReset_Click(object sender, EventArgs e)
        {
            txtFilterByType.ResetText();
            txtFilterByCategory.ResetText();
            txtFilterBySubCategory.ResetText();
            txtFilterByEmail.ResetText();
            cboFilterByList.ResetText();
            FilterRecords("");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (_dtFilterContacts == null)
                return;

            int value;
            try
            {
                Helper.WaitCursor(this);
                DataTable dt = _dtFilterContacts.GetChanges();
                if (dt == null || dt.Rows.Count == 0)
                {
                    Helper.ShowMessage(true, "");
                    return;
                }
                value = DataAccess.DataManager.CurrentDataProvider.SaveCustomers(dt);
                if (value == dt.Rows.Count)
                {
                    Helper.ShowMessage("Record saved successfully.", "Save", MessageType.Information);
                    _dtFilterContacts.AcceptChanges();
                }
                else
                    Helper.ShowMessage("Unable to save records successfully.", "Save", MessageType.Error);

            }
            finally
            {
                Helper.WaitCursor(this, true);
            }
        }

        private void grdFilterContact_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Helper.ShowMessage("Invalid value. " + e.Exception.Message, "Invalid value", MessageType.Error);
            e.Cancel = true;
        }

        public DataTable FilterContactsTable
        {
            get 
            { 
                return _dtFilterContacts; 
            }
        }

        public DataView FilterContactsView
        {
            get 
            {
                if (_dtFilterContacts != null)
                    return _dtFilterContacts.DefaultView;
                else
                    return null;
            }
        }
     
    }
}
