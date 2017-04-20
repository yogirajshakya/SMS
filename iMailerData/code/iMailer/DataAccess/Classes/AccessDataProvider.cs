using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Infolancers.iMailer.DataAccess
{
    class AccessDataProvider : Infolancers.iMailer.DataAccess.IDataProvider
    {
        private OleDbConnection _conn;
        private string _connString;
        private DataTable _dtMapping;
        private Hashtable _MappingFieldsVisibleIndex, _MappingFields;

        public AccessDataProvider(string connectionString)
        {
            _connString = connectionString;
        }

        #region "Public Methods"
            public DataTable GetTemplate(string Condition)
            {
                string sql = string.Empty;
                try
                {
                    sql = "select * from EMAILTEMPLATE ";

                    if (!string.IsNullOrEmpty(Condition))
                        sql += "where " + Condition;

                    return SetColumnCaption(GetData(sql, "EmailTemplate"));
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetTemplate, SQL: " + sql);
                    return null;
                }

            }

            public int SaveTemplate(DataTable dt)
            {
                string sql = string.Empty;
                try
                {
                    sql = "select * from EMAILTEMPLATE where 1=2";
                    return SaveData(sql, dt);
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in SaveTemplate, SQL: " + sql);
                    return -1;
                }
            }

           public DataTable GetCustomers(string Condition)
           {
               string sql = string.Empty;
               try
               {
                   sql = "select * from CUSTOMER ";
                   if (!string.IsNullOrEmpty(Condition))
                       sql += "where " + Condition;

                   return SetColumnCaption(GetData(sql, "Customer"));
               }
               catch (Exception e)
               {
                   Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                   Logger.ErrorLog.ErrorRoutine(e, "in GetCustomers, SQL: " + sql);
                   return null;
               }

           }

           public int SaveCustomers(DataTable dt)
           {
               string sql = string.Empty;
               try
               {
                   sql = "select * from CUSTOMER where 1=2";
                   return SaveData(sql, dt);
               }
               catch (Exception e)
               {
                   Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                   Logger.ErrorLog.ErrorRoutine(e, "in SaveCustomers, SQL: " + sql);
                   return -1;
               }
           }

           public DataTable GetSentEmail(string Condition)
           {
               string sql = string.Empty;
               try
               {
                   sql = "select * from SENTEMAIL ";
                   if (!string.IsNullOrEmpty(Condition))
                       sql += "where " + Condition;

                   return SetColumnCaption(GetData(sql, "SentEmail"));
               }
               catch (Exception e)
               {
                   Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                   Logger.ErrorLog.ErrorRoutine(e, "in GetSentEmail, SQL: " + sql);
                   return null;
               }

           }

           public int SaveSentEmail(DataTable dt)
           {
               string sql = string.Empty;
               try
               {
                   sql = "select * from CUSTOMER where 1=2";
                   return SaveData(sql, dt);
               }
               catch (Exception e)
               {
                   Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                   Logger.ErrorLog.ErrorRoutine(e, "in SaveSentEmail, SQL: " + sql);
                   return -1;
               }
           }

            public int SaveMapping(DataTable dt)
           {
               string sql = string.Empty;
               try
               {
                   sql = "select * from FIELDMAPPING where 1=2";
                   return SaveData(sql, dt);
               }
               catch (Exception e)
               {
                   Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                   Logger.ErrorLog.ErrorRoutine(e, "in SaveMapping, SQL: " + sql);
                   return -1;
               }
               finally
               {
                   _dtMapping = null;
                   _MappingFieldsVisibleIndex = null;
                   _MappingFields = null;
               }

           }
            
            public DataTable GetMapping()
            {
                return GetMapping(false, false);
            }

            public DataTable GetMapping(bool Refresh, bool IncludeSystemFields)
            {
                string sql = string.Empty;
                try
                {
                    if (Refresh || _dtMapping == null)
                    {
                         sql = "select * from FIELDMAPPING";
                         if (!IncludeSystemFields)
                             sql += " where SYSTEMFIELD = 0";
                        _dtMapping = SetColumnCaption(GetData(sql, "FieldMapping"));
                    }

                    return _dtMapping.Copy();
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetMapping, SQL: " + sql);
                    return null;
                }
            }

            public ArrayList GetPreferredFields()
            {
                ArrayList arr = new ArrayList();
                DataTable dt;
                dt = GetMapping();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((bool)dr["Preferred"])
                        {
                            arr.Add(dr["OriginalFieldName"].ToString());
                        }
                    }
                }
                return arr;
            }

            public Hashtable GetMappingFieldsVisibleIndex()
            {
                if (_MappingFieldsVisibleIndex == null)
                {
                    SetMappingFieldsData();
                }
                return _MappingFieldsVisibleIndex;
            }

            public Hashtable GetMappingFields()
            {
                if (_MappingFields == null)
                {
                    SetMappingFieldsData();
                }
                return _MappingFields;
            }

            public Hashtable GetAlternateValue()
            {
                Hashtable oAlternateValues = new Hashtable();
                DataTable dt;
                dt = GetMapping();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        oAlternateValues.Add(dr["OriginalFieldName"].ToString(), dr["AlternateValue"].ToString());
                    }
                }
                return oAlternateValues;
            }

            public ArrayList GetMappingFieldsArray()
            {
                ArrayList arr = new ArrayList();
                DataTable dt;
                dt = GetMapping();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        arr.Add(dr["NewFieldName"].ToString());
                    }
                }
                return arr;
            }

            public DataTable GetContactList(string Condition)
            {
                string sql = string.Empty;
                try
                {
                    sql = "select * from CONTACTLIST ";

                    if (!string.IsNullOrEmpty(Condition))
                        sql += "where " + Condition;

                    return SetColumnCaption(GetData(sql, "ContactList"));
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetContactList, SQL: " + sql);
                    return null;
                }

            }

            public int SaveContactList(DataTable dt)
            {
                string sql = string.Empty;
                try
                {
                    sql = "select * from CONTACTLIST where 1=2";
                    return SaveData(sql, dt);
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in SaveContactList, SQL: " + sql);
                    return -1;
                }
            }

            public DataTable GetScheduleData(DateTime ScheduleDate)
            {
                string sql = string.Empty;
                try
                {
                    if (ScheduleDate == DateTime.MinValue)
                        return null;
                    
                    sql = string.Format("select * from SCHEDULEDATA where NextScheduleDate={0}", ScheduleDate.ToString());

                    return SetColumnCaption(GetData(sql, "ScheduleData"));
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetScheduleData, SQL: " + sql);
                    return null;
                }
            }

            public DataTable GetScheduleData(string Condition)
            {
                string sql = string.Empty;
                try
                {
                    
                    sql = "select * from SCHEDULEDATA ";
                    if (!string.IsNullOrEmpty(Condition))
                        sql += "where " + Condition;

                    return SetColumnCaption(GetData(sql, "ScheduleData"));
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetScheduleData, SQL: " + sql);
                    return null;
                }
            }

            public int SaveScheduleData(DataTable dt)
            {
                string sql = string.Empty;
                try
                {
                    sql = "select * from SCHEDULEDATA where 1=2";
                    return SaveData(sql, dt);
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in SaveScheduleData, SQL: " + sql);
                    return -1;
                }
            }

            public DataTable GetApplicationCodes(string ApplicationType)
            {
                string sql = string.Empty;
                try
                {

                    if (string.IsNullOrEmpty(ApplicationType))
                        return null;
                    
                    sql = string.Format("select * from APPLICATIONCODE where ApplicationCode='{0}'", ApplicationType);

                    return SetColumnCaption(GetData(sql, "ApplicationCode"));
                }
                catch (Exception e)
                {
                    Helper.ShowMessage(string.Format("{0}{1}{1}{2}", "Some critical error occurred.", Environment.NewLine, e.Message), "Error", MessageType.Error);
                    Logger.ErrorLog.ErrorRoutine(e, "in GetApplicationCodes, SQL: " + sql);
                    return null;
                }
            }

        #endregion

        #region "Helper Methods"
            private OleDbConnection Connection
            {
                get
                {
                    if (_conn == null)
                         _conn = new OleDbConnection(_connString);
                
                    return _conn;
                }
            }

            private void OpenConnection()
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }

            private void CloseConnection()
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }

            private OleDbDataAdapter GetDataAdapter(string CommandText)
            {
                return new OleDbDataAdapter(CommandText, Connection);
            }

            private OleDbCommandBuilder GetCommandBuilder(OleDbDataAdapter da)
            {
                 return new OleDbCommandBuilder(da);
            }

            private DataTable GetData(string SQL, string tableName)
            {
                DataSet ds = new DataSet();
                OpenConnection();
                GetDataAdapter(SQL).Fill(ds, tableName);
                CloseConnection();

                return ds.Tables[tableName];
            }

            private int SaveData(string SQL, DataTable dt)
            {
                int result;

                if(dt == null)
                    return -1;
                if(dt.DataSet != null && !dt.DataSet.HasChanges())
                    Helper.ShowMessage(true);

                OleDbDataAdapter da = GetDataAdapter(SQL);
                OleDbCommandBuilder cb = GetCommandBuilder(da);
                OpenConnection();
                da.InsertCommand=cb.GetInsertCommand();
                da.UpdateCommand=cb.GetUpdateCommand();
                da.DeleteCommand=cb.GetDeleteCommand();

                result = da.Update(dt);
                CloseConnection();
                return result;
            }

            private DataTable ChangeContactCaption(DataTable dtContacts)
            {
                DataTable dtMapping;
                DataRow[] drs;

                if(dtContacts == null)
                    return dtContacts;

                dtMapping = GetMapping(false, false);
                if (dtMapping != null)
                {
                    foreach(DataColumn dc in dtContacts.Columns)
                    {
                        drs = dtMapping.Select(string.Format("OriginalFieldName ='{0}'" , dc.ColumnName));
                        if(drs.Length > 0)
                                dc.Caption = drs[0]["NewFieldName"].ToString();
                        else
                             dc.Caption = dc.ColumnName;
                        
                    }
                }
                return dtContacts;
            }

            private DataTable SetColumnCaption(DataTable dt)
            {
                if (dt == null)
                    return dt;

                switch (dt.TableName.ToLower())
                {
                    case "customer":
                        dt = ChangeContactCaption(dt);
                        break;
              
                    case "fieldmapping":
                        dt.Columns["OriginalFieldName"].Caption = "Original Field Name";
                        dt.Columns["NewFieldName"].Caption = "New Field Name";
                        dt.Columns["AlternateValue"].Caption = "Alternate Value";
                        dt.Columns["Preferred"].Caption = "Preferred";
                        break;

                    case "smstemplate":
                        break;

                    case "emailtemplate":
                        dt.Columns["EmailTemplateName"].Caption = "Template Name";
                        dt.Columns["EmailTemplateSubject"].Caption = "Subject";
                        dt.Columns["EmailHTMLText"].Caption = "HTML File";
                        dt.Columns["EmailPlainText"].Caption = "Plain Text";
                        dt.Columns["AttachmentFilePath"].Caption = "Attachment File";
                        break;

                    case "emailsetup":
                        break;

                    default:
                        break;
                }
                return dt;
            }

            private void SetMappingFieldsData()
            {
                DataTable dt;
                _MappingFieldsVisibleIndex = new Hashtable();
                _MappingFields = new Hashtable();

                dt = GetMapping(false, true);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _MappingFields.Add(dr["NewFieldName"].ToString(), dr["OriginalFieldName"].ToString());
                        _MappingFieldsVisibleIndex.Add(dr["OriginalFieldName"].ToString(), dr["DisplayIndex"]);
                    }
                }
            }
    
        #endregion

    }
}
