using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Infolancers.iMailer
{
	public partial class frmImport : Form
	{
		public frmImport()
		{
			InitializeComponent();
		}

		private string fileCSV;		//full file name
		private string dirCSV;		//directory of file to import
		private string fileNevCSV;	//name (with extension) of file to import - field
        private int counter = 0;
        private DataTable dtCSV;
        private DataTable dtMapping = new DataTable();
        private DataTable dtContacts = new DataTable();
        DataSet ds = new DataSet();
        private bool dataPreviewed;

        public string FileNevCSV	//name (with extension) of file to import - property
		{
			get { return fileNevCSV; }
            set { fileNevCSV = value; dataPreviewed = false; }
		}

		private string strFormat;	//CSV separator character
		private string strEncoding; //Encoding of CSV file


		// Browses file with OpenFileDialog control

		private void btnFileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialogCSV = new OpenFileDialog();

			openFileDialogCSV.InitialDirectory = Application.ExecutablePath.ToString();
			openFileDialogCSV.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			openFileDialogCSV.FilterIndex = 1;
			openFileDialogCSV.RestoreDirectory = true;

			if (openFileDialogCSV.ShowDialog() == DialogResult.OK)
			{
				this.txtFileToImport.Text = openFileDialogCSV.FileName.ToString();
			}

		}



		// Delimiter character selection
		private void Format()
		{
			try
			{

				if (rdbSemicolon.Checked)
				{
					strFormat = "Delimited(;)";
				}
				else if (rdbTab.Checked)
				{
					strFormat = "TabDelimited";
				}
				else if (rdbSeparatorOther.Checked)
				{
					strFormat = "Delimited(" + txtSeparatorOtherChar.Text.Trim() + ")";
				}
				else
				{
					strFormat = "Delimited(;)";
				}


			}
			catch (Exception ex)
			{
                Helper.ShowMessage(ex.Message, "Format");
			}
			finally
			{
			}
		}


		// Encoding selection
		private void Encoding()
		{
			try
			{

				if (rdbAnsi.Checked)
				{
					strEncoding = "ANSI";
				}
				else if (rdbUnicode.Checked)
				{
					strEncoding = "Unicode";
				}
				else if (rdbOEM.Checked)
				{
					strEncoding = "OEM";
				}
				else
				{
					strEncoding = "ANSI";
				}


			}
			catch (Exception ex)
			{
				Helper.ShowMessage(ex.Message, "Encoding");
			}
			finally
			{
			}
		}



		/* Schema.ini File (Text File Driver)

		When the Text driver is used, the format of the text file is determined by using a
		schema information file. The schema information file, which is always named Schema.ini
		and always kept in the same directory as the text data source, provides the IISAM 
		with information about the general format of the file, the column name and data type
		information, and a number of other data characteristics*/

		private void writeSchema()
		{
			try
			{
				FileStream fsOutput = new FileStream(this.dirCSV + "\\schema.ini", FileMode.Create, FileAccess.Write);
				StreamWriter srOutput = new StreamWriter(fsOutput);
				string s1, s2, s3, s4, s5;

				s1 = "[" + this.FileNevCSV + "]";
				s2 = "ColNameHeader=" + chkFirstRowColumnNames.Checked.ToString();
				s3 = "Format=" + this.strFormat;
				s4 = "MaxScanRows=25";
				s5 = "CharacterSet=" + this.strEncoding;

				srOutput.WriteLine(s1.ToString() + "\r\n" + s2.ToString() + "\r\n" + s3.ToString() + "\r\n" + s4.ToString() + "\r\n" + s5.ToString());
				srOutput.Close();
				fsOutput.Close();
			}
			catch (Exception ex)
			{
				Helper.ShowMessage(ex.Message, "writeSchema");
			}
			finally
			{ }
		}

		/*
		 * Loads the csv file into a DataSet.
		 * 
		 * If the numberOfRows parameter is -1, it loads oll rows, otherwise it
		 * loads the first specified number of rows (for preview)
		 */

		public DataSet LoadCSV(int numberOfRows)
		{
			try
			{
                Helper.WaitCursor(this);
				// Creates and opens an ODBC connection
				string strConnString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + this.dirCSV.Trim() + ";Extensions=asc,csv,tab,txt;Persist Security Info=False";
				string sql_select;
				OdbcConnection conn;
				conn = new OdbcConnection(strConnString.Trim());
				conn.Open();

				//Creates the select command text
				if (numberOfRows == -1)
				{
					sql_select = "select * from [" + this.FileNevCSV.Trim() + "]";
				}
				else
				{
					sql_select = "select top " + numberOfRows + " * from [" + this.FileNevCSV.Trim() + "]";
				}

				//Creates the data adapter
				OdbcDataAdapter obj_oledb_da = new OdbcDataAdapter(sql_select, conn);
                ds = new DataSet();
				//Fills dataset with the records from CSV file
				obj_oledb_da.Fill(ds, "csv");
                dtCSV = ds.Tables["csv"];
				//closes the connection
				conn.Close();
			}
			catch (Exception e) //Error
			{
                Helper.ShowMessage(e.Message, "Error - LoadCSV", MessageType.Error);
			}
            Helper.WaitCursor(this, true);
			return ds;
		}


		// Checks if a file was given.
		private bool fileCheck()
		{
			if ((fileCSV == "") || (fileCSV == null) || (dirCSV == "") || (dirCSV == null) || (FileNevCSV == "") || (FileNevCSV == null))
			{
                Helper.ShowMessage("Select a CSV file to load first!", "File name missing", MessageType.Error);
				return false;
			}
			else
			{
                if (rdbSeparatorOther.Checked &&
                    string.IsNullOrEmpty(txtSeparatorOtherChar.Text.Trim()))
                {
                    Helper.ShowMessage("Select a seperator first!", "Seperator missing", MessageType.Error);
                    return false;
                }
                else
                    return true;
			}
		}




		private void btnPreview_Click(object sender, EventArgs e)
		{
			loadPreview();
		}


        /* Loads the preview of CSV file in the DataGridView control.
         */

		private void loadPreview()
		{
			try
			{
                if (!fileCheck())
                    return;
				// select format, encoding, an write the schema file
				Format();
				Encoding();
				writeSchema();

				// loads the first 500 rows from CSV file, and fills the
				// DataGridView control.
				this.grdPreview.DataSource = LoadCSV((int)this.numericUpDown1.Value);
				this.grdPreview.DataMember = "csv";
                Helper.FormatDataGrid(grdPreview, true);
                grdPreview.AllowUserToDeleteRows = false;
                grdPreview.AllowUserToAddRows = false;
                grdPreview.ReadOnly = true;
                dataPreviewed = true;
			}
			catch (Exception e)
			{
				Helper.ShowMessage(e.Message, "Error - loadPreview", MessageType.Error);
			}
		}

		/*
		 * Makes file and directory information, and suggests a table name
		 */

		private void tbFile_TextChanged(object sender, EventArgs e)
		{
			// full file name
			this.fileCSV = this.txtFileToImport.Text;

			// creates a System.IO.FileInfo object to retrive information from selected file.
			System.IO.FileInfo fi = new System.IO.FileInfo(this.fileCSV);
			// retrives directory
			this.dirCSV = fi.DirectoryName.ToString();
			// retrives file name with extension
			this.FileNevCSV = fi.Name.ToString();
            dataPreviewed = false;
			// database table name
			//this.txtTableName.Text = fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length).Replace(" ", "_");
		}

		private void txtSeparatorOtherChar_TextChanged(object sender, EventArgs e)
		{
			this.rdbSeparatorOther.Checked = true;
            dataPreviewed = false;
		}

        private void btnShowMapping_Click(object sender, EventArgs e)
        {
            counter = 1;
            DataTable dt;

            if (!dataPreviewed)
            {
                if (!fileCheck())
                    return;
                loadPreview();
            }

            dt = Infolancers.iMailer.DataAccess.DataManager.CurrentDataProvider.GetMapping();
            if (!dt.Columns.Contains("MappedField"))
            {
                dt.Columns.Add("MappedField");
                dt.Columns["NewFieldName"].Caption = "Database Field";
            }
            if (!dt.Columns.Contains("CopyIfNotBlankFlag"))
            {
                dt.Columns.Add("CopyIfNotBlankFlag", typeof(bool));
                dt.Columns["CopyIfNotBlankFlag"].Caption = "Copy if not blank?";
            }
            tabControl1.SelectedTab = tabControl1.TabPages["pageMapping"];
            dtMapping = dt;
            dtMapping.DefaultView.RowFilter = " OriginalFieldName <> 'CustomerId' and OriginalFieldName <> 'ImportId'";
            grdMapping.DataSource = dtMapping.DefaultView;
            grdMapping.AllowUserToDeleteRows = false;
            grdMapping.AllowUserToAddRows = false;

            AddComboBoxColumn("MappedField", "Mapped CSV Field");
            grdMapping.Columns["MappedField"].Visible = false;
            grdMapping.Columns["OriginalFieldName"].Visible = false;
            grdMapping.Columns["Preferred"].Visible = false;
            grdMapping.Columns["NewFieldName"].ReadOnly = true;
            //grdMapping.Columns["CopyIfNotBlankFlag"].DisplayIndex = grdMapping.Columns.Count -1;
            grdMapping.Columns["NewFieldName"].Width = 200;
            grdMapping.Columns["MappedField"].Width = 200;
            grdMapping.Columns["CopyIfNotBlankFlag"].Width = 150;

            Helper.SetHeading(grdMapping, dtMapping, false);
            Helper.ShowColumns(grdMapping, "NewFieldName,MappedField,CopyIfNotBlankFlag", false);
            Helper.FormatDataGrid(grdMapping, false);
        }

        private void btnShowImport_Click(object sender, EventArgs e)
        {
            DataView dv;
            counter = 2;
            tabControl1.SelectedTab = tabControl1.TabPages["pageImportToDB"];
            dv = new DataView(dtMapping);
            dv.RowFilter = "MappedField <> ''";

            dtMapping.Columns["NewFieldName"].Caption = "Database Field";
            dtMapping.Columns["MappedField"].Caption = "CSV Field";
            dtMapping.Columns["CopyIfNotBlankFlag"].Caption = "Copy if not blank?";

            grdImport.DataSource = dv;
            grdImport.AllowUserToDeleteRows = false;
            grdImport.AllowUserToAddRows = false;
            grdImport.Columns["NewFieldName"].ReadOnly = true;
            grdImport.Columns["MappedField"].ReadOnly = true;
            grdImport.Columns["NewFieldName"].Width = 200;
            grdImport.Columns["MappedField"].Width = 200;
            grdImport.Columns["CopyIfNotBlankFlag"].Width = 150;
            Helper.SetHeading(grdImport, dtMapping, false);
            Helper.ShowColumns(grdImport, "NewFieldName,MappedField,CopyIfNotBlankFlag", false);
            Helper.FormatDataGrid(grdImport, false);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string batchId;
            DataTable dt;
            counter = 3;
            batchId = DateTime.Now.ToString("yyyyMMddHHmmss");
            ImportData(batchId);
            tabControl1.SelectedTab = tabControl1.TabPages["pageContacts"];
            dt = DataAccess.DataManager.CurrentDataProvider.GetCustomers("ImportId = '" + batchId + "'");
            grdContacts.DataSource = dt;
            grdContacts.AllowUserToDeleteRows = false;
            grdContacts.AllowUserToAddRows = false;
            Helper.ShowPreferredFields(grdContacts, chkShowPreferred.Checked);
            Helper.SetHeading(grdContacts, dt, false);
            Helper.FormatDataGrid(grdContacts, true);
            grdContacts.ReadOnly = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddComboBoxColumn(string DataPropertyName, string HeaderText )
        {
            if (grdMapping.Columns.Contains(DataPropertyName) &&
                grdMapping.Columns[DataPropertyName].CellType != typeof(DataGridViewTextBoxCell))
                return;

            DataGridViewColumn gc;
            gc = grdMapping.Columns[DataPropertyName];

            DataGridViewComboBoxColumn comboboxColumn;
            comboboxColumn = CreateComboBoxColumn();
            comboboxColumn.Name = DataPropertyName;
            comboboxColumn.HeaderText = HeaderText;
            comboboxColumn.DataPropertyName = DataPropertyName;
            SetDefaultColumnMapping(comboboxColumn);
            grdMapping.Columns.Add(comboboxColumn);

            if (gc != null)
            {
                comboboxColumn.DisplayIndex = gc.DisplayIndex;
                grdMapping.Columns.Remove(gc);
            }
        }

        private DataGridViewComboBoxColumn CreateComboBoxColumn()
        {
            System.Collections.SortedList sl;
            DataGridViewComboBoxColumn column =
                new DataGridViewComboBoxColumn();
            {
                column.DropDownWidth = 160;
                column.Width = 120;
                column.MaxDropDownItems = 20;
                column.FlatStyle = FlatStyle.Flat;
            }
            sl = new System.Collections.SortedList();
            foreach (DataColumn col in dtCSV.Columns)
            {
                sl.Add(col.ColumnName, col.ColumnName);
            }
            foreach (string value in sl.Values)
            {
                column.Items.Add(value);
            }
           
            return column;
        }

        private void SetDefaultColumnMapping(DataGridViewComboBoxColumn column)
        {
            foreach (DataRow dr in dtMapping.Rows)
            {
                foreach(string col in column.Items)
                {
                    if (dr["NewFieldName"].ToString().Replace(" ", "").Replace("-", "").ToLower() == col.Replace(" ", "").Replace("-", "").ToLower() ||
                        dr["OriginalFieldName"].ToString().Replace(" ", "").Replace("-", "").ToLower() == col.Replace(" ", "").Replace("-", "").ToLower())
                    {
                        dr["MappedField"] = col;
                        if(dr["OriginalFieldName"].ToString().ToLower()=="Email")
                            dr["CopyIfNotBlankFlag"] = true;
                    }
                }
            }

        }

        private void ImportData(string batchId)
        {
            DataTable dtCustomer;
            DataRow drTrg;
            bool boolValid;
            int value;
            string trgCol, srcCol;
            LoadCSV(-1);
            dtCustomer = DataAccess.DataManager.CurrentDataProvider.GetCustomers("1=2");
            if (dtCustomer != null)
            {
                dtMapping.DefaultView.RowFilter = "MappedField <> '' and OriginalFieldName <> 'CustomerId'";

                foreach (DataRow drSrc in dtCSV.Rows)
                {
                    if (drSrc.RowState != DataRowState.Deleted && drSrc.RowState != DataRowState.Detached)
                    {
                        drTrg = dtCustomer.NewRow();
                        boolValid = true;
                        foreach (DataRowView drv in dtMapping.DefaultView)
                        {
                            srcCol = drv["MappedField"].ToString();
                            trgCol = drv["OriginalFieldName"].ToString();
                            if ((drv["CopyIfNotBlankFlag"] != DBNull.Value && (bool)drv["CopyIfNotBlankFlag"]) && 
                                ((drSrc[srcCol] == DBNull.Value) || string.IsNullOrEmpty(drSrc[srcCol].ToString())))
                            {
                                boolValid = false;
                                break;
                            }   
                            drTrg[trgCol] = drSrc[srcCol];
                            drTrg["ImportId"] = batchId;
                        }
                        if (boolValid)
                            dtCustomer.Rows.Add(drTrg);
                        else
                            drTrg = null;
                    }
                }
                if (dtCustomer.Rows.Count == 0)
                {
                    Helper.ShowMessage(true, "No record found to import.");
                    return;
                }

                value = DataAccess.DataManager.CurrentDataProvider.SaveCustomers(dtCustomer);
                if (value == dtCustomer.Rows.Count)
                    Helper.ShowMessage(value.ToString() + " Contacts imported and saved successfully.", "Import", MessageType.Information);
                else
                    Helper.ShowMessage("Unable to import and save contacts", "Error", MessageType.Error);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex > counter)
            {
                Helper.ShowMessage("Please complete previous step first.");
                e.Cancel = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           Helper.SetBackground(tabControl1.SelectedTab, ControlType.Control);
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            Helper.SetBackground(tabControl1, ControlType.Form);
            Helper.SetBackground(tabControl1.SelectedTab, ControlType.Control);
        }

        private void chkShowPreferred_CheckedChanged(object sender, EventArgs e)
        {
            Helper.ShowPreferredFields(grdContacts, chkShowPreferred.Checked);
            Helper.SetHeading(grdContacts, (DataTable) grdContacts.DataSource, false);
            Helper.FormatDataGrid(grdContacts, true);
        }
		
	}
}