using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using System.Data;

namespace Infolancers.iMailer
{
    public enum MessageType
    {
        Error=1,
        Warning=2,
        Information=3,
        Question=4
    }

    public enum ControlType
    {
        Form=1,
        Control=2,
        DataGridView=3
    }

    class Helper
    {
        #region "ShowMessage"
            public static DialogResult ShowMessage(bool NothingToSave, string Message)
            {
                if (NothingToSave)
                {
                    if (string.IsNullOrEmpty(Message))
                        Message = "Nothing to save.";

                    return ShowMessage(Message, "Save", MessageType.Information);
                }
                else
                {
                    if (string.IsNullOrEmpty(Message))
                        Message = "There are changes to save. Do you want to save this?";

                    return ShowMessage(Message, "Save?", MessageType.Question);
                }
            }

            public static DialogResult ShowMessage(bool AskForSave)
            {
                return ShowMessage("There are changes to save. Do you want to save this?", "Save?", MessageType.Question);
            }

            public static DialogResult ShowMessage(IWin32Window owner, string message)
            {
                return ShowMessage(owner, message,"",MessageType.Information);   
            }

            public static DialogResult ShowMessage(string message, string caption)
            {
                return ShowMessage(null, message, caption, MessageType.Information);
            }

            public static DialogResult ShowMessage(string message, string caption, MessageType type)
            {
                return ShowMessage(null, message, caption, type);
            }

            public static DialogResult ShowMessage(IWin32Window owner, string message, string caption)
            {
                return ShowMessage(owner, message, caption, MessageType.Information);
            }

            public static DialogResult ShowMessage(string message)
            {
            
                return ShowMessage(null, message, "", MessageType.Information);   
            }

            public static DialogResult ShowMessage(IWin32Window owner, string message, string caption, MessageType type)
            {
                switch(type)
                {
                    case MessageType.Error:
                        return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    case MessageType.Information:
                        return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    case MessageType.Warning:
                        return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    case MessageType.Question:
                        return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    default:
                        return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        #endregion "ShowMessage"

        #region "WaitCursor"
            public static void WaitCursor(Control octl)
            {
                WaitCursor(octl, false);
            }

            public static void WaitCursor(Control octl, bool reset)
            {
                if(reset)
                    octl.Cursor = System.Windows.Forms.Cursors.Default;
                else
                    octl.Cursor = GetWaitCursor();
            }
        
            public static void WaitCursor(Form frm)
            {
                WaitCursor(frm, false);
            }

            public static void WaitCursor(Form frm, bool reset)
            {
                if (reset)
                    frm.Cursor = System.Windows.Forms.Cursors.Default;
                else
                    frm.Cursor = GetWaitCursor();
            }

            private static Cursor GetWaitCursor()
            {
                return System.Windows.Forms.Cursors.WaitCursor;
            }

        #endregion "WaitCursor"

        #region "Formatting"

            public static void SetBackground(Control ctl, ControlType ctype)
            {
                string strFile;
                Form frm;

                if (ctl == null)
                    return;

                if (ctype == ControlType.DataGridView)
                    strFile = System.Configuration.ConfigurationManager.AppSettings["DatagridBackgroundImage"];
                else
                    strFile = System.Configuration.ConfigurationManager.AppSettings["BackgroundImage"];

                if (string.IsNullOrEmpty(strFile))
                    return;
                strFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strFile);
                if (ctype == ControlType.Form)
                {
                    frm = ctl.FindForm();
                    if (frm != null)
                    {
                        frm.BackgroundImage = System.Drawing.Image.FromFile(strFile);
                        frm.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                else
                {
                    ctl.BackgroundImage = System.Drawing.Image.FromFile(strFile);
                    ctl.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }

            public static void FormatDataGrid(DataGridView dv, bool AutoSizeColumns)
            {
                string strBackColor, strForeColor;

                if (dv == null)
                    return;
                SetBackground(dv, ControlType.DataGridView);

                strBackColor = System.Configuration.ConfigurationManager.AppSettings["DataGridHeaderBackColor"];
                if (string.IsNullOrEmpty(strBackColor))
                    strBackColor = "Cyan";

                strForeColor = System.Configuration.ConfigurationManager.AppSettings["DataGridHeaderForeColor"];
                if (string.IsNullOrEmpty(strForeColor))
                    strForeColor = "Black";

                dv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(strBackColor);
                dv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromName(strForeColor);
                dv.ColumnHeadersDefaultCellStyle.Font = new Font(dv.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
                //dv.BackgroundColor = Color.FromName("White");
                dv.EnableHeadersVisualStyles = false; 

                if (AutoSizeColumns)
                    dv.AutoResizeColumns();
            }

            public static void SetHeading(DataGridView grd, DataTable dt, bool UseProperColumnType)
            {
                ArrayList arr = null;
                DataTable dtMapping;
                DataRowView rv;
                int currIndex;
                string col;

                if (grd == null)
                    return;

                if (dt == null)
                    return;

                foreach (DataGridViewColumn gc in grd.Columns)
                {
                    if (dt.Columns.Contains(gc.Name))
                    {
                        gc.HeaderText = dt.Columns[gc.Name].Caption;
                        if (UseProperColumnType)
                        {
                            if (dt.Columns[gc.Name].DataType == typeof(DateTime))
                            {
                                if (arr == null)
                                    arr = new ArrayList();
                                arr.Add(gc);
                            }
                        }
                    }
                }
                dtMapping = DataAccess.DataManager.CurrentDataProvider.GetMapping();
                if (dtMapping != null)
                    dtMapping.DefaultView.Sort = "DisplayIndex";

                currIndex = 1;
                for(int i=0;  i < dtMapping.DefaultView.Count -1; i++)
                {
                    rv = dtMapping.DefaultView[i];
                    col = rv["OriginalFieldName"].ToString();
                    if (grd.Columns.Contains(col) && grd.Columns[col].Visible )
                    {
                        grd.Columns[col].DisplayIndex = currIndex++;
                    }
                }

                if (UseProperColumnType)
                {
                    AddDateColumn(grd, arr);
                }
            }

            public static void ShowPreferredFields(DataGridView grd, bool ShowPreferredOnly)
            {
                ArrayList arr;

                foreach (DataGridViewColumn gc in grd.Columns)
                {
                    gc.Visible = (!ShowPreferredOnly);
                }

                if (!ShowPreferredOnly)
                    return; 

                arr = DataAccess.DataManager.CurrentDataProvider.GetPreferredFields();

                if (arr == null)
                    return;

                foreach (DataGridViewColumn gc in grd.Columns)
                {
                    gc.Visible = (arr.Contains(gc.Name));
                }
            }

            private static void AddDateColumn(DataGridView grd, ArrayList columns)
            {
                if (columns == null)
                    return;

                foreach (DataGridViewColumn gc in columns)
                {
                    if (grd.Columns.Contains(gc.DataPropertyName) &&
                         gc.CellType == typeof(CalendarColumn))
                        continue;

                    CalendarColumn col = new CalendarColumn();
                    col.Name = gc.DataPropertyName;
                    col.DataPropertyName = gc.DataPropertyName;
                    col.HeaderText = gc.HeaderText;
                    col.DisplayIndex = gc.DisplayIndex;
                    grd.Columns.Add(col);

                    gc.Visible = false; 
                    grd.Columns.Remove(gc);

                }
            }

            public static void ShowColumns(DataGridView grd, string columns, bool ShowAll)
            {
                string[] arr;

                if (grd == null)
                    return;

                if (string.IsNullOrEmpty(columns))
                    return;

                foreach (DataGridViewColumn gc in grd.Columns)
                {
                    gc.Visible = ShowAll;
                }

                if (!ShowAll)
                {
                    arr = columns.Split(',');
                    foreach (string col in arr)
                    {
                        if (grd.Columns.Contains(col.Trim()))
                            grd.Columns[col.Trim()].Visible = true;
                    }
                }
            }


        #endregion "Formatting"

        #region "Email Setup"
            public static string FromEmailAddress
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["FromEmailAddress"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set FromEmailAddress value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string FromEmailName
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["FromEmailName"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set FromEmailName value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string FromAdminEmailAddress
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["FromAdminEmailAddress"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set FromAdminEmailAddress value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string FromAdminEmailName
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["FromAdminEmailName"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set FromAdminEmailName value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string GetOutputFolder(string BatchId)
            {
                if(string.IsNullOrEmpty(BatchId))
                    return System.IO.Path.GetTempPath() + @"\emails";
                else
                    return System.IO.Path.GetTempPath() + @"\emails_" + BatchId;
            }

            public static Emailer.MessageOutput EmailMessageOutput
            {
                get
                {
                    string msgOutput;
                    Emailer.MessageOutput value;

                    msgOutput = System.Configuration.ConfigurationManager.AppSettings["EmailMessageOutput"];
                    if (string.IsNullOrEmpty(msgOutput))
                        msgOutput = "Directory";
                    
                    switch (msgOutput.ToLower())
                    {
                        case "smtpserver":
                            value = Emailer.MessageOutput.SmtpServer;
                            break;

                        case "none":
                            value = Emailer.MessageOutput.None;
                            break;

                        case "pickupdirectoryfromiis":
                            value = Emailer.MessageOutput.PickupDirectoryFromIis;
                            break;

                        default:
                            value = Emailer.MessageOutput.Directory;
                            break;
                    }
                    //return Emailer.MessageOutput.SmtpServer
                    //return Emailer.MessageOutput.Directory; // change to MessageOutput.SmtpServer if you like, but be careful :)
                    return value;
                }
            }

            public static string EmailSmtpHost
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["EmailSmtpHost"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set EmailSmtpHost value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static int EmailSmtpPort
            {
                get
                {
                    int value = 0;
                    if (!(int.TryParse(System.Configuration.ConfigurationManager.AppSettings["EmailSmtpPort"], out value)))
                    {
                        ShowMessage("Please set EmailSmtpPort value in config file.", "Incomplete Email Setup");
                        value = 25;
                    }

                    return value;
                }
            }

            public static string EmailLocalHostName
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["EmailLocalHostName"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set EmailLocalHostName value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string EmailSmtpAuthentificationUserName
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["EmailSmtpAuthentificationUserName"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set EmailSmtpAuthentificationUserName value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static string EmailSmtpAuthentificationPassword
            {
                get
                {
                    string value = string.Empty;
                    value = System.Configuration.ConfigurationManager.AppSettings["EmailSmtpAuthentificationPassword"];
                    if (string.IsNullOrEmpty(value))
                        ShowMessage("Please set EmailSmtpAuthentificationPassword value in config file.", "Incomplete Email Setup");

                    return value;
                }
            }

            public static int EmailMaxFailures
            {
                get
                {
                    int value = 0;
                    if (!(int.TryParse(System.Configuration.ConfigurationManager.AppSettings["EmailMaxFailures"], out value)))
                    {
                        ShowMessage("Please set EmailMaxFailures value in config file.", "Incomplete Email Setup");
                        value = 1;
                    }

                    return value;
                }
            }

            public static int EmailDelayBetweenMessages
            {
                get
                {
                    int value = 0;
                    if (!(int.TryParse(System.Configuration.ConfigurationManager.AppSettings["EmailDelayBetweenMessages"], out value)))
                    {
                        ShowMessage("Please set EmailDelayBetweenMessages value in config file.", "Incomplete Email Setup");
                        value = 1000;
                    }

                    return value;
                }
            }

            public static string EmailHTMLFileImagesFolder
            {
                get
                {
                    return System.IO.Path.GetFullPath(Application.StartupPath + @"\HTMLTemplates");
                }
            }


        #endregion "Email Setup"

        #region "Logging"
        public static bool EventLogEnabled
        {
            get
            {
                bool enabled = false;
                bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["EnableEventLog"], out enabled);
                return enabled;
            }
        }

        public static Logger.EventLogType EventLogType
        {
            get
            {
                string logType;
                logType = System.Configuration.ConfigurationManager.AppSettings["EventLogType"];
                if (string.IsNullOrEmpty(logType))
                    logType = "Custom";

                if (logType.ToLower() == "windows")
                    return Logger.EventLogType.Windows;
                else
                    return Logger.EventLogType.Custom;
            }
        }

        #endregion "Logging"

        #region "Company Information"
        public static string CompanyName
        {
            get
            {
                return "Infolancers";
            }
        }

        public static string CompanyEmail
        {
            get
            {
                return "test@Infolancers";
            }
        }

        public static string CompanyPhone
        {
            get
            {
                return "11111000000";
            }
        }

        public static string CompanyAddress
        {
            get
            {
                return "New Delhi";
            }
        }
        #endregion "Company Information"

        #region "Resource Path"
        public static string SystemFilePath
        {
            get
            {
                string strBaseDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();

                return System.IO.Path.Combine(strBaseDirectory, "System");
            }
        }

        public static string SystemDBPath
        {
            get
            {
                return System.IO.Path.Combine(SystemFilePath, "DB", "iMailer.mdb");
            }
        }

        #endregion "Resource Path"
    }
}
