using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Infolancers.iMailer
{
    public partial class Main : Form
    {
        internal readonly string _HOME_URL;
        internal readonly string _HEADER_URL;
        internal readonly string _FOOTER_URL;
        internal readonly string _OPTIONS_URL;
        private CommandExecutor _CommandExecutor;
        private ArrayList _tabPages;

        public Main()
        {
            InitializeComponent();
            _CommandExecutor = new CommandExecutor(this);
            _HOME_URL = System.IO.Path.Combine(Helper.SystemFilePath, "home.html");
            if (!File.Exists(_HOME_URL))
            {
                Helper.ShowMessage(string.Format("System file '{0}' does not exist.", _HOME_URL), "Missing System File");
            }

            _HEADER_URL = System.IO.Path.Combine(Helper.SystemFilePath, "header.html");
            if (!File.Exists(_HEADER_URL))
            {
                Helper.ShowMessage(string.Format("System file '{0}' does not exist.", _HEADER_URL), "Missing System File");
            }

            _FOOTER_URL = System.IO.Path.Combine(Helper.SystemFilePath, "Footer.htm");
            if (!File.Exists(_FOOTER_URL))
            {
                Helper.ShowMessage(string.Format("System file '{0}' does not exist.", _FOOTER_URL), "Missing System File");
            }

            _OPTIONS_URL = System.IO.Path.Combine(Helper.SystemFilePath, "leftbar.html");
            if (!File.Exists(_OPTIONS_URL))
            {
                Helper.ShowMessage(string.Format("System file '{0}' does not exist.", _OPTIONS_URL), "Missing System File");
            }
        }
       

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                AddTabPages();
                OpenDefaultHTMLPages();
                AddBrowserHandlers();
                ShowPages(pageHome);
            }
            catch (Exception ex)
            {
                Helper.ShowMessage(string.Format("{0}{1}{2}", "Some critical error occurred.", Environment.NewLine, ex.Message), "Error", MessageType.Error);
                Logger.ErrorLog.ErrorRoutine(ex, "Main_Load");
            }

        }

        private void AddBrowserHandlers()
        {
            this.browserMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.browserHeader.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.browserOption.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.browserFooter.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            bool result;
            try
            {
                result = ProcessCommand(e.Url.ToString());
                e.Cancel = !result;
            }
         
            catch (Exception ex)
            {
                Helper.ShowMessage(string.Format("{0}{1}{2}","Some critical error occurred.", Environment.NewLine, ex.Message), "Error", MessageType.Error);
                Logger.ErrorLog.ErrorRoutine(ex, "webBrowser_Navigating");
            }

        }

        public bool ProcessCommand(string actionUrl)
        {
            if (string.IsNullOrEmpty(actionUrl))
                return false;

            return _CommandExecutor.ProcessAction(actionUrl);
        }

        private void OpenDefaultHTMLPages()
        {
            browserMain.Navigate(_HOME_URL);
            browserHeader.Navigate(_HEADER_URL);
            browserOption.Navigate(_OPTIONS_URL);
            browserFooter.Navigate(_FOOTER_URL);
        }

        private void AddTabPages()
        {
            if (_tabPages == null)
            {
                _tabPages = new ArrayList();
                _tabPages.Add(pageHome);
                _tabPages.Add(pageImport);
                _tabPages.Add(pageExport);
                _tabPages.Add(pageEmailSetting);
                _tabPages.Add(pageSMSSetting);
                _tabPages.Add(pageContact);
                _tabPages.Add(pageSchedule);
                _tabPages.Add(pageAdvanceSetting);
                _tabPages.Add(pageDatabase);
                _tabPages.Add(pageTemplate);
                _tabPages.Add(pageList);
                _tabPages.Add(pageSendEmail);
                _tabPages.Add(pageSendSMS);
                _tabPages.Add(pageEmailHistory);
                _tabPages.Add(pageSMSHistory);
                _tabPages.Add(pageApplicationCode);
                _tabPages.Add(pageDate);
                _tabPages.Add(pageFilterContact);
            }
            
        }
        public void ShowPages(TabPage page)
        {
            ShowPages(page.Name);
        }

        public void ShowPages(string pages)
        {
            string firstPageName;
            char separator = ',';
            if (_tabPages == null) return;
            
            if (pages == null) pages = "pageHome";
            firstPageName = pages.Split(separator)[0];

            for (int i = tabDetail.TabPages.Count - 1; i >= 0; i--)
            {
                tabDetail.TabPages.Remove(tabDetail.TabPages[i]);
            }

            foreach (TabPage oPage in _tabPages)
            {
                if (pages.ToLower().Contains(oPage.Name.ToLower()))
                {
                    tabDetail.TabPages.Add(oPage);
                    if (firstPageName.ToLower() == oPage.Name.ToLower())
                    {
                        tabDetail.SelectedTab = oPage;
                    }
                }
            }
        }
        
        #region "Send Email"
        private DataTable _dtFilterContacts;
        private DataTable _dtFilterEmailList;
        private DataTable _dtSentEmail;
        private DataTable _dtTemplate;

        //internal void OnSendEmailLoad()
        //{
        //    if (_dtFilterEmailList == null)
        //    {
        //        _dtFilterEmailList = DataAccess.DataManager.CurrentDataProvider.GetContactList("");

        //        this.cboFilterByList.SelectedIndexChanged -= new System.EventHandler(this.cboFilterByList_SelectedIndexChanged);
        //        cboFilterByList.DataSource = _dtFilterEmailList;
        //        cboFilterByList.DisplayMember = "ListName";
        //        cboFilterByList.ValueMember = "Filter";
        //        cboFilterByList.SelectedIndex = -1;
        //        this.cboFilterByList.SelectedIndexChanged += new System.EventHandler(this.cboFilterByList_SelectedIndexChanged);

        //        btnRefreshFilterContact.PerformClick();
        //    }
        //}

        //private void FilterContols_TextChanged(object sender, EventArgs e)
        //{
        //    FilterRecords("");
        //}

        //private void FilterRecords(string additionalFilter)
        //{
        //    if (_dtFilterContacts == null)
        //        return;

        //    Helper.WaitCursor(this);
        //    StringBuilder sb = new StringBuilder();
        //    DataView dv = _dtFilterContacts.DefaultView;

        //    sb.Append(additionalFilter);

        //    if (sb.Length > 0) sb.AppendLine(" and ");
        //    sb.Append(" Email is not null ");

        //    if (!string.IsNullOrEmpty(cboFilterByList.Text))
        //    {
        //        if (sb.Length > 0) sb.AppendLine(" and ");
        //        sb.Append(cboFilterByList.SelectedValue.ToString());
        //    }


        //    if (!string.IsNullOrEmpty(txtFilterByType.Text))
        //    {
        //        if (sb.Length > 0) sb.AppendLine(" and ");
        //        sb.AppendFormat(" CustomerType like '{0}%'", txtFilterByType.Text);
        //    }

        //    if (!string.IsNullOrEmpty(txtFilterByCategory.Text))
        //    {
        //        if (sb.Length > 0) sb.AppendLine(" and ");
        //        sb.AppendFormat(" CustomerCategory like '{0}%'", txtFilterByCategory.Text);
        //    }

        //    if (!string.IsNullOrEmpty(txtFilterBySubCategory.Text))
        //    {
        //        if (sb.Length > 0) sb.AppendLine(" and ");
        //        sb.AppendFormat(" CustomerSubCategory like '{0}%'", txtFilterBySubCategory.Text);
        //    }

        //    if (!string.IsNullOrEmpty(txtFilterByEmail.Text))
        //    {
        //        if (sb.Length > 0) sb.AppendLine(" and ");
        //        sb.AppendFormat(" Email like '{0}%'", txtFilterByEmail.Text);
        //    }

        //    dv.RowFilter = sb.ToString();
        //    Helper.WaitCursor(this, true);
        //}

        //private void btnRefreshFilterContact_Click(object sender, EventArgs e)
        //{
        //    Helper.WaitCursor(this);
        //    _dtFilterContacts = DataAccess.DataManager.CurrentDataProvider.GetCustomers("Email is not null");
        //    grdFilterContact.DataSource = _dtFilterContacts.DefaultView;
        //    Helper.SetHeading(grdFilterContact, _dtFilterContacts, true);
        //    Helper.FormatDataGrid(grdFilterContact, true);
        //    Helper.ShowPreferredFields(grdFilterContact, chkFilterPreferredColumns.Checked);
        //    FilterRecords("");
        //    Helper.WaitCursor(this, true);
        //}

        //private void chkFilterPreferredColumns_CheckedChanged(object sender, EventArgs e)
        //{
        //    Helper.ShowPreferredFields(grdFilterContact, chkFilterPreferredColumns.Checked);
        //}

        //private void cboFilterByList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FilterRecords("");
        //}
 
        //private void btnFilterReset_Click(object sender, EventArgs e)
        //{
        //    txtFilterByType.ResetText();
        //    txtFilterByCategory.ResetText();
        //    txtFilterBySubCategory.ResetText();
        //    txtFilterByEmail.ResetText();
        //    cboFilterByList.ResetText();
        //    FilterRecords("");
        //}

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_dtTemplate == null)
            {
                _dtTemplate = DataAccess.DataManager.CurrentDataProvider.GetTemplate("");
                cboTemplate.DataSource = _dtTemplate;
                cboTemplate.DisplayMember = "EmailTemplateName";
                cboTemplate.ValueMember = "EmailTemplateId";
            }

            tabSendEmail.SelectedTab = tabSendEmail.TabPages["pageSelectTemplate"];
        }

        private void SendEmailToCustomer()
        {
            _dtSentEmail = DataAccess.DataManager.CurrentDataProvider.GetSentEmail("1=2");
            if (_dtSentEmail == null)
                return;

            grdSendEmailStatus.DataSource = _dtSentEmail;
            grdSendEmailStatus.AllowUserToAddRows = false;
            grdSendEmailStatus.AllowUserToDeleteRows = false;
            grdSendEmailStatus.ReadOnly = true;
            Helper.ShowColumns(grdSendEmailStatus, "Email,Status,Details", false);
            Helper.SetHeading(grdSendEmailStatus, _dtSentEmail, true);
            Helper.FormatDataGrid(grdSendEmailStatus, false);
            SendEmail_Resize(null, null);
        }

        private void SendEmail_Resize(object sender, EventArgs e)
        {
            if (grdSendEmailStatus.DataSource == null)
                return;
            grdSendEmailStatus.Columns["Email"].Width = (int)(grdSendEmailStatus.Width * .25);
            grdSendEmailStatus.Columns["Status"].Width = (int)(grdSendEmailStatus.Width * .25);
            grdSendEmailStatus.Columns["Details"].Width = (int)(grdSendEmailStatus.Width * .40);
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            string batch;
            long templateId;
            batch = DateTime.Now.ToString("yyyyMMddHHmmss");
            _dtFilterContacts = contactSendEmail.FilterContactsTable;
            if (_dtFilterContacts != null && !string.IsNullOrEmpty(cboTemplate.Text))
            {
                if (!long.TryParse(cboTemplate.SelectedValue.ToString(), out templateId))
                    return;

                SendEmailToCustomer();

                Emailer.FrmMain form;
                form = new Emailer.FrmMain(templateId, batch, _dtFilterContacts);
                form.ResultDataTable = _dtSentEmail;
                //form.MainForm = this;
                form.ShowDialog(this);
            }
            else
                Helper.ShowMessage("Either no contact is selected or no template is selected.", "Insufficient Data", MessageType.Information);

        }

        public void StatusUpdated()
        {
            grdSendEmailStatus.Refresh();
        }

       #endregion "Send Email"

     
    }
}

