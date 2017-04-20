using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Infolancers.iMailer.Emailer;

namespace Infolancers.iMailer.Emailer
{
    /// <summary>
    /// Very basic sample application 
    /// in order to show how Infolancers.iMailer.Emailer can be used.
    /// </summary>
    /// <example>
    /// Have a look at method btnSend_Click:
    /// 
    /// Send using an array of anonymous type:
    /// _mmm.DataSource = CreateAnonymousDataList();
    /// 
    /// Or send using a simple data table:
    /// _mmm.DataSource = CreateDataTable();
    /// </example>
    public partial class FrmMain : Form
    {
        private MailMergeSender _mailSender;
        private MailMergeMessage _mmm;
    	private string _outputFolder = string.Empty;

        private long _TemplateId;
        private string _subject, _plainText, _htmlFile, _attachments;
        private System.Collections.Generic.List<FileAttachment> _fileAttachments;

        private string _BatchId;
        private DataTable _dtContacts;
        private DataTable _dtTemplate;

        public DataTable ResultDataTable{get;set;}
        public Main MainForm { get; set; }

        public FrmMain(long TemplateId,string BatchId, DataTable contacts)
        {
            InitializeComponent();
            _TemplateId = TemplateId;
            _dtContacts = contacts;
            _BatchId = BatchId;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_dtContacts == null || _TemplateId == 0)
            {
                this.Close();
                return;
            }

            if (Helper.EmailMessageOutput == MessageOutput.Directory)
            {
                _outputFolder = Helper.GetOutputFolder(_BatchId);
                if (!System.IO.Directory.Exists(_outputFolder))
                    System.IO.Directory.CreateDirectory(_outputFolder);

                //if (DialogResult.Cancel == MessageBox.Show("Mail output will be written into the folder\n" + _outputFolder + "\nand the folder will open in Windows Explorer", "Please note", MessageBoxButtons.OKCancel))
                //{
                //    this.Close();
                //    return;
                //}

                // open the output folder in new windows explorer window
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = _outputFolder,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            SetTemplateData();
            Setup();
            btnSend.PerformClick();
        }


        private void Setup()
        {
            // read content
            string html = ReadHtmlText(_htmlFile);

            // insert your own e-mail address here!
            MailMergeAddress myMailAddress = new MailMergeAddress(MailAddressType.TestAddress, 
                                    Helper.FromEmailAddress, Helper.FromEmailName, Encoding.Default);
            // create the mail message
			//_mmm = new MailMergeMessage("CRON Job Status Report for Domain '{DomainName:\"{0}{empty:[name not registered!]}\"}'", null, html);
            _mmm = new MailMergeMessage(_subject, _plainText, html, _fileAttachments);
        	//_mmm.PlainText =_plainText;

            // adjust mail specific settings
            _mmm.CharacterEncoding = Encoding.GetEncoding("iso-8859-1");
            _mmm.CultureInfo = new System.Globalization.CultureInfo("en-US");
            _mmm.TextTransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
            _mmm.BinaryTransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            // add recipients, from address and test address to use.
            // the address part of the test address will be used instead of the other addresses.
            _mmm.MailMergeAddresses.Add(new MailMergeAddress(MailAddressType.To, "<{Email}>", "{FirstName} {LastName}", Encoding.Default));
            _mmm.MailMergeAddresses.Add(new MailMergeAddress(MailAddressType.From, myMailAddress.Address, myMailAddress.DisplayName, Encoding.Default));
            _mmm.MailMergeAddresses.Add(myMailAddress);

            // base directory for html images
            _mmm.FileBaseDir = Helper.EmailHTMLFileImagesFolder; //GetMailDemoFilesDir();

            // setup the mail sender
            _mailSender = null;
            _mailSender = new MailMergeSender();

            SetupEventHandlers();

            //_mailSender.LocalHostName = "mail." + Environment.MachineName;
            _mailSender.MaxFailures = Helper.EmailMaxFailures;
            _mailSender.DelayBetweenMessages = Helper.EmailDelayBetweenMessages;

			_mailSender.MailOutputDirectory = _outputFolder;
            _mailSender.MessageOutput = Helper.EmailMessageOutput;  // change to MessageOutput.SmtpServer if you like, but be careful :)

            // smtp details - change to your demands
            _mailSender.SmtpHost = Helper.EmailSmtpHost;
            _mailSender.SmtpPort = Helper.EmailSmtpPort;
            _mailSender.SetSmtpAuthentification(Helper.EmailSmtpAuthentificationUserName, Helper.EmailSmtpAuthentificationPassword);
            _mailSender.LocalHostName = Helper.EmailLocalHostName;
        }

        private void Send()
        {
            progress.Minimum = 0;
            progress.Maximum = _mmm.DataItemCount;
            progress.Step = 1;
			progress.Value = 0;

            txtMerge.Clear();
            textBox1.Clear();
            textBox2.Clear();

            btnCancel.Enabled = true;

            if (Helper.EmailMessageOutput == MessageOutput.Directory)
            {
                if (!System.IO.Directory.Exists(_outputFolder))
                    System.IO.Directory.CreateDirectory(_outputFolder);
            }

            try
            {
                _mailSender.SendAllAsync(_mmm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + " " + ex.Source);
            }
        }


        private string ReadHtmlText(string file)
        {
            //return ReadTextFile(GetMailDemoFilesDir() + @"\cron_testmail.html");
            return ReadTextFile(file);
        }

		private string ReadTextFile(string file)
		{
			using (System.IO.StreamReader sr = new System.IO.StreamReader(file))
			{
				return sr.ReadToEnd();
			}
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _mailSender.SendCancel();
            _mailSender = null;
            btnCancel.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
        	lblDelayTime.Text = string.Format("Delay between messages: {0} ms.", _mailSender.DelayBetweenMessages);
            Setup();

			// Send using an array of anonymous type
            _mmm.DataSource = _dtContacts.DefaultView; //CreateAnonymousDataList();
			// Send using a DataTable
			// _mmm.DataSource = CreateDataTable();

            Send();
        }

        private void SetupEventHandlers()
        {
            #region SetEventHandlers
            _mailSender.OnBeforeSend += ((obj, args) =>
                                             {
                                                 UpdateStatus(args.MailMergeMessage.MailMergeAddresses.ToString(
                                                                   MailAddressType.To), "Sending email", true);
                                                 string text = "Before: " +
                                                               args.MailMergeMessage.MailMergeAddresses.ToString(
                                                                   MailAddressType.To);
                                                 if (textBox1.InvokeRequired)
                                                 {
                                                     textBox1.Invoke(
                                                         (MethodInvoker) (() => textBox1.Text += text + Environment.NewLine));
                                                 }
                                                 else
                                                 {
                                                     textBox1.Text += text + Environment.NewLine;
                                                 }
                                             });
            _mailSender.OnAfterSend += ((obj, args) =>
                                            {
                                                UpdateStatus(args.MailMergeMessage.MailMergeAddresses.ToString(
                                                                  MailAddressType.To), "Sent email", false);
                                                string text = "After: " +
                                                              args.MailMergeMessage.MailMergeAddresses.ToString(
                                                                  MailAddressType.To) +
                                                              (args.Error != null ? args.Error.Message : "");
                                                if (textBox1.InvokeRequired)
                                                {
                                                    textBox1.Invoke(
                                                        (MethodInvoker) (() => textBox1.Text += text + Environment.NewLine));
                                                }
                                                else
                                                {
                                                    textBox1.Text += text + Environment.NewLine;
                                                }
                                            });
            _mailSender.OnSendFailure += ((obj, args) =>
                                              {
                                                  string errorMsg = args.Error.Message;
                                                  MailMergeMessage.MailMergeMessageException ex = args.Error as MailMergeMessage.MailMergeMessageException;
                                                  if (ex != null && ex.Exceptions.Count > 0)
                                                  {
                                                      errorMsg = string.Format("{0}", ex.Exceptions[0].Message);
                                                  }
                                                  string text = string.Format("Error: {0}", errorMsg);
                                                  if (textBox2.InvokeRequired)
                                                  {
                                                      textBox2.Invoke(
                                                          (MethodInvoker) (() => textBox2.Text += text + Environment.NewLine));
                                                  }
                                                  else
                                                  {
                                                      textBox2.Text += text + Environment.NewLine;
                                                  }
                                                  UpdateStatus(args.MailMergeMessage.MailMergeAddresses.ToString(
                                                                    MailAddressType.To), "Sending email failed: " + errorMsg, false);

                                              });

            _mailSender.OnMergeBegin += ((obj, args) =>
                                             {
                                                 string text = string.Format("MergeStarttime: {0} - ", args.StartTime.ToString());
                                                 if (txtMerge.InvokeRequired)
                                                 {
                                                     txtMerge.Invoke((MethodInvoker) (() => txtMerge.Text += text + Environment.NewLine));
                                                 }
                                                 else
                                                 {
                                                     txtMerge.Text += text + Environment.NewLine;
                                                 }
                                             });
            _mailSender.OnMergeComplete += ((obj, args) =>
                                                {
                                                    string text = string.Format("MergeEndTime: {0}", args.EndTime.ToString());
                                                    if (txtMerge.InvokeRequired)
                                                    {
                                                        txtMerge.Invoke((MethodInvoker) (() =>
                                                                                             {
                                                                                                 txtMerge.Text += text + Environment.NewLine;
                                                                                                 btnSend.Enabled = true;
                                                                                             }));
                                                    }
                                                    else
                                                    {
                                                        txtMerge.Text += text + Environment.NewLine;
                                                        btnSend.Enabled = true;
                                                    }
                                                });
            _mailSender.OnMergeProgress += ((obj, args) =>
                                                {
                                                    string text = string.Format("Total: {0} / Sent: {1} / Error: {2}", args.TotalMsg, args.SentMsg,
                                                                                args.ErrorMsg);
                                                    if (progress.InvokeRequired)
                                                    {
                                                        progress.Invoke((MethodInvoker) (() => progress.PerformStep()));
                                                        label1.BeginInvoke((MethodInvoker) (() => label1.Text = text));
                                                    }
                                                    else
                                                    {
                                                        progress.PerformStep();
                                                        label1.Text = text;
                                                    }
                                                });
            #endregion
        }

        private void UpdateStatus(string email, string status, bool AddNew)
        {
            DataRow dr = null;
            DataRow[] filtered;

            if (ResultDataTable == null)
                return;

            if (AddNew)
            {
                dr = ResultDataTable.NewRow();
                dr["Email"] = email;
                dr["status"] = status;
                ResultDataTable.Rows.Add(dr);
            }
            else
            {
                filtered = ResultDataTable.Select(string.Format("Email = '{0}'", email));
                if (filtered.Length > 0)
                    dr = filtered[0];

                if (dr != null)
                {
                    dr["status"] = status;
                }
            }

            //if(MainForm != null)
            //    MainForm.StatusUpdated();
        }

        private void SetTemplateData()
        {
            FileAttachment fa;
            string filePrefix, displayName;

            if(_dtTemplate == null)
                _dtTemplate = DataAccess.DataManager.CurrentDataProvider.GetTemplate("EmailTemplateId=" + _TemplateId.ToString());

            if (_dtTemplate != null && _dtTemplate.Rows.Count > 0)
            {
                _subject = _dtTemplate.Rows[0]["EmailTemplateSubject"].ToString();
                _plainText = _dtTemplate.Rows[0]["EmailPlainText"].ToString();
                _htmlFile = _dtTemplate.Rows[0]["EmailHTMLText"].ToString();
                _attachments = _dtTemplate.Rows[0]["AttachmentFilePath"].ToString();
                filePrefix = _dtTemplate.Rows[0]["AttachmentPrefix"].ToString();
                _fileAttachments = new System.Collections.Generic.List<FileAttachment>();
                if (!string.IsNullOrEmpty(_attachments))
                {
                    foreach (string file in _attachments.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            displayName = System.IO.Path.GetFileName(file);
                            if (!string.IsNullOrEmpty(filePrefix))
                                displayName = filePrefix + displayName;

                            fa = new FileAttachment(file, displayName);
                            _fileAttachments.Add(fa);
                        }
                    }
                }
            }
            else
                Helper.ShowMessage("Unable to find Email Template with ID " + _TemplateId.ToString(), "Invalid Template", MessageType.Error);
        }
    }
}