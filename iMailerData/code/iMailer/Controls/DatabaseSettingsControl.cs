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
    public partial class DatabaseSettingsControl : UserControl
    {
        public DatabaseSettingsControl()
        {
            InitializeComponent();
        }
        private DataTable dtMapping;
        private void FieldMapping_Load(object sender, EventArgs e)
        {
            if (DesignMode == true) return;
            BindGrid();
        }

        private void BindGrid()
        {
            dtMapping = DataAccess.DataManager.CurrentDataProvider.GetMapping();
            dtMapping.DefaultView.Sort = "DisplayIndex";
            grdMapping.DataSource = dtMapping.DefaultView;
            grdMapping.AllowUserToDeleteRows = false;
            grdMapping.AllowUserToAddRows = false; grdMapping.Columns["OriginalFieldName"].HeaderText = "Original Field Name";
            grdMapping.Columns["NewFieldName"].HeaderText = "New Field Name";
            grdMapping.Columns["OriginalFieldName"].ReadOnly = true;

            Helper.SetHeading(grdMapping, dtMapping, false);
            Helper.FormatDataGrid(grdMapping, true);
            FieldMapping_Resize(grdMapping, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int value;
            DataTable dt;
            SetIndex(dtMapping);
            dt = dtMapping.GetChanges();
            if (dt.Rows.Count > 0)
            {
                value = DataAccess.DataManager.CurrentDataProvider.SaveMapping(dt);
                if (value == dt.Rows.Count)
                {
                    Helper.ShowMessage("Mappings saved successfully.", "Save", MessageType.Information);
                    BindGrid();
                }
                else
                    Helper.ShowMessage("Unable to save Mappings.", "Error", MessageType.Error);
            }
            else
                Helper.ShowMessage("No changes to save.", "Save", MessageType.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void grdMapping_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (grdMapping.Columns[e.ColumnIndex].Name == "NewFieldName" &&
               string.IsNullOrEmpty(grdMapping.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            {
                grdMapping.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdMapping.Rows[e.RowIndex].Cells["OriginalFieldName"].Value;
            }
        }

        private void SetIndex(DataTable dt)
        {
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                dt.DefaultView[i]["DisplayIndex"] = i + 1;
            }
        }

        private void FieldMapping_Resize(object sender, EventArgs e)
        {
            if (grdMapping.DataSource == null)
                return;
            grdMapping.Columns["OriginalFieldName"].Width = (int)(grdMapping.Width * .25);
            grdMapping.Columns["NewFieldName"].Width = (int)(grdMapping.Width * .25);
            grdMapping.Columns["Preferred"].Width = (int)(grdMapping.Width * .10);
            grdMapping.Columns["DisplayIndex"].Width = (int)(grdMapping.Width * .15);
            grdMapping.Columns["AlternateValue"].Width = (int)(grdMapping.Width * .15);
        }

    }
}
