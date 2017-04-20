using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infolancers.iMailer
{
    class CommandExecutor
    {
        private Main _MainForm;
        public CommandExecutor(Main frmMain)
        {
            _MainForm = frmMain;
        }
        
        
        public bool ProcessAction(string actionUrl)
        {
            string url;
            if (string.IsNullOrEmpty(actionUrl))
                return false;

            if (actionUrl.Contains("#"))
                url = actionUrl.Substring(actionUrl.IndexOf("#"));
            else
                url = actionUrl;

            switch (url.ToLower())
            {
                case "#home": OpenHomePage();
                    return true;

                case "#dbsettings": OpenDBFieldMapping();
                    return true;

                case "#managecontacts": OpenContactPage();
                    return true;

                case "#messagedrafts": OpenEmailTemplatePage();
                    return true;

                case "#importcontacts": ImportContacts();
                    return true;

                case "#exportcontacts": ExportContacts();
                    return true;

                case "#sendmessage":
                    OpenSendEmailPage();
                    return true;
                default: return false;
            }
        }

        private void OpenEmailTemplatePage()
        {
            //EmailTemplateEditor form;
            //form = new EmailTemplateEditor();
            //form.ShowDialog(_MainForm);
            _MainForm.ShowPages(_MainForm.pageTemplate);
        }

        private void ImportContacts()
        {
            _MainForm.ShowPages(_MainForm.pageImport);
            //frmImport form;
            //form = new frmImport();
            //form.ShowDialog(_MainForm);
        }

        private void OpenSendEmailPage()
        {
            _MainForm.ShowPages(_MainForm.pageSendEmail);
            //_MainForm.OnSendEmailLoad();
            //MailMergeTest.FrmMain form;
            //form = new MailMergeTest.FrmMain();
            //form.ShowDialog(_MainForm);
        }

        private void OpenContactPage()
        {
            _MainForm.ShowPages(_MainForm.pageContact);
            //_MainForm.btnLoad.PerformClick();
            //Helper.ErrorHandler.ShowMessage("Open ContactPage");
        }

        private void ExportContacts()
        {
            _MainForm.ShowPages(_MainForm.pageFilterContact);
            //_MainForm.contactControl1.OnLoad();
        }

        private void OpenHelpPage()
        {

            //Helper.ErrorHandler.ShowMessage("Open HelpPage");
        }

        private void OpenHomePage()
        {
            //_MainForm.browserMain.Navigate(_MainForm._HOME_URL);
            _MainForm.ShowPages(_MainForm.pageHome);
            //Helper.ErrorHandler.ShowMessage("Open HomePage");
        }

        private void OpenDBFieldMapping()
        {
            //if (_MainForm.pageDatabase.Controls.Count == 0)
            //{
            //    DatabaseSettings ctl = new DatabaseSettings();
            //    ctl.Location = new System.Drawing.Point(0, 0);
            //    ctl.Size = new System.Drawing.Size(100, 100);
            //    _MainForm.pageDatabase.Controls.Add(ctl);
            //    ctl.Dock = System.Windows.Forms.DockStyle.Fill;
            //}
            _MainForm.ShowPages(_MainForm.pageDatabase);
            //FieldMapping form;
            //form = new FieldMapping();
            //form.ShowDialog(_MainForm);
        }

    }
}
