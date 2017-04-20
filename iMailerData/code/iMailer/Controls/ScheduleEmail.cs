using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Infolancers.iMailer
{
    public partial class ScheduleEmail : Form
    {
        public ScheduleEmail()
        {
            InitializeComponent();
        }

        DataTable _dtOccassionCode;
        DataTable _dtFilterScheduleEmailContact;
        DataTable _dtMappings;
        private void ScheduleEmail_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == true)
                return;

            _dtOccassionCode = DataAccess.DataManager.CurrentDataProvider.GetApplicationCodes("OCCASION_TYPE");
            cboOccassionType.DataSource = _dtOccassionCode;
            cboOccassionType.DisplayMember = "Description";
            cboOccassionType.ValueMember = "ApplicationCode";
            cboOccassionType.SelectedIndex = -1;

       
            

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _dtFilterScheduleEmailContact = DataAccess.DataManager.CurrentDataProvider.GetTemplate("");

            if (_dtMappings == null)
            {
                _dtMappings = DataAccess.DataManager.CurrentDataProvider.GetMapping();
                cboDBField.DataSource = _dtMappings;
                cboDBField.DisplayMember = "NewFieldName";
                cboDBField.ValueMember = "NewFieldName";
                cboDBField.SelectedIndex = -1;
            }

            tabScheduleEmail.SelectedTab = tabScheduleEmail.TabPages["pageScheduleEmail"];
        }

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    if (_dtTemplate == null)
        //    {
        //        _dtTemplate = DataAccess.DataManager.CurrentDataProvider.GetTemplate("");
        //        cboTemplate.DataSource = _dtTemplate;
        //        cboTemplate.DisplayMember = "EmailTemplateName";
        //        cboTemplate.ValueMember = "EmailTemplateId";
        //    }

        //    tabSendEmail.SelectedTab = tabSendEmail.TabPages["pageSelectTemplate"];
        //}

    }
}
