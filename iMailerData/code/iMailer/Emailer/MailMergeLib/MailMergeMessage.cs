using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Represents an email message that can be sent using the Infolancers.iMailer.Emailer.MailMergeSender class.
	/// </summary>
	partial class MailMergeMessage : IDisposable
	{
		#region *** Private content fields ***

		private MailMessage _mailMessage;

		#endregion

		#region *** Private fields for Encoding and Globalization ***

		private CultureInfo _cultureInfo = Thread.CurrentThread.CurrentCulture;

		#endregion

		#region *** Private lists for tracking errors ***

		private readonly List<string> _badAttachmentFiles = new List<string>();
		private readonly List<string> _badMailAddr = new List<string>();
		private List<string> _badInlineFiles = new List<string>();
		private List<string> _badVariableNames = new List<string>();

		#endregion

		#region *** Private fields for Attachments ***

		private readonly List<FileAttachment> _inlineAttExternal = new List<FileAttachment>();
		private List<FileAttachment> _inlineAtt = new List<FileAttachment>();

		#endregion

		#region *** Private address related fields ***

		// special mail headers
		public const string CReturnReceipt = "return-receipt-to";
		public const string CConfirmReading = "x-confirm-reading-to";
		public const string CDispositionNotification = "disposition-notification-to";
		public const string CToHeader = "to";
		private readonly TextVariableManager _textVariableManager;

		#endregion

		#region *** Private special attribute fields ***

		private const string cXmailer = "x-mailer";

		#endregion

		private bool _disposed;

		#region *** Constructor ***

		/// <summary>
		/// Creates an empty mail merge message.
		/// </summary>
		public MailMergeMessage()
		{
			IgnoreEmptyRecipientAddr = true;
			DeliveryNotificationOptions = DeliveryNotificationOptions.None;
			Priority = MailPriority.Normal;
			Xmailer = null;
			Headers = new NameValueCollection();
			StringAttachments = new List<StringAttachment>();
			FileAttachments = new List<FileAttachment>();
			StreamAttachments = new List<StreamAttachment>();
			BinaryTransferEncoding = TransferEncoding.Base64;
			TextTransferEncoding = TransferEncoding.SevenBit;
			CharacterEncoding = Encoding.Default;

			_textVariableManager = new TextVariableManager
			                       	{
			                       		CultureInfo = CultureInfo,
			                       		ShowNullAs = string.Empty,
			                       		ShowEmptyAs = string.Empty
			                       	};

			_textVariableManager.GetBindingSource().CurrentChanged += RaiseDataChangedEvent;
			_textVariableManager.GetBindingSource().CurrentItemChanged += RaiseDataChangedEvent;
			_textVariableManager.GetBindingSource().PositionChanged += RaiseDataChangedEvent;
			_textVariableManager.GetBindingSource().ListChanged += RaiseDataChangedEvent;

			MailMergeMessage msg = this;
			MailMergeAddresses = new MailMergeAddressCollection(ref msg);

			FileBaseDir = Environment.CurrentDirectory;
		}

		/// <summary>
		/// Creates a new mail merge message.
		/// </summary>
		/// <param name="subject">Mail message subject.</param>
		public MailMergeMessage(string subject)
			: this()
		{
			Subject = subject;
		}

		/// <summary>
		/// Creates a new mail merge message.
		/// </summary>
		/// <param name="subject">Mail message subject.</param>
		/// <param name="plainText">Plain text of the mail message.</param>
		public MailMergeMessage(string subject, string plainText)
			: this(subject)
		{
			PlainText = plainText;
		}

		/// <summary>
		/// Creates a new mail merge message.
		/// </summary>
		/// <param name="subject">Mail message subject.</param>
		/// <param name="plainText">Plain text part of the mail message.</param>
		/// <param name="htmlText">HTML message part of the mail message.</param>
		public MailMergeMessage(string subject, string plainText, string htmlText)
			: this(subject, plainText)
		{
			HtmlText = htmlText;
		}

		/// <summary>
		/// Creates a new mail merge message.
		/// </summary>
		/// <param name="subject">Mail message subject.</param>
		/// <param name="plainText">Plain text part of the mail message.</param>
		/// <param name="fileAtt">File attachments of the mail message.</param>
		public MailMergeMessage(string subject, string plainText, List<FileAttachment> fileAtt)
			: this(subject, plainText, string.Empty, fileAtt)
		{
		}

		/// <summary>
		/// Creates a new mail merge message.
		/// </summary>
		/// <param name="subject">Mail message subject.</param>
		/// <param name="plainText">Plain text part of the mail message.</param>
		/// <param name="htmlText">HTML message part of the mail message.</param>
		/// <param name="fileAtt">File attachments of the mail message.</param>
		public MailMergeMessage(string subject, string plainText, string htmlText, List<FileAttachment> fileAtt)
			: this(subject, plainText, htmlText)
		{
			FileAttachments = fileAtt;
		}

		#endregion

		/// <summary>
		/// Gets or sets the mail message subject.
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Gets or sets the mail message plain text content.
		/// </summary>
		public string PlainText { get; set; }

		/// <summary>
		/// Gets or sets the mail message HTML content.
		/// </summary>
		public string HtmlText { get; set; }

		/// <summary>
		/// Gets or sets the data source the TextVariableManager binds to.
		/// </summary>
		public object DataSource
		{
			get { return _textVariableManager.DataSource; }
			set { _textVariableManager.DataSource = value; }
		}

		/// <summary>
		/// Gets or sets the specific list in the data source to bind to.
		/// </summary>
		public string DataMember
		{
			get { return _textVariableManager.DataMember; }
			set { _textVariableManager.DataMember = value; }
		}

		/// <summary>
		/// Gets the current item in the list.
		/// </summary>
		public object CurrentDataItem
		{
			get { return _textVariableManager.CurrentDataItem; }
		}

		/// <summary>
		/// Gets the total number of items in the underlying list.
		/// </summary>
		public int DataItemCount
		{
			get { return _textVariableManager.DataItemCount; }
		}

		/// <summary>
		/// Gets or sets the index of the current item in the underlying list.
		/// </summary>
		public int CurrentDataPosition
		{
			get { return _textVariableManager.CurrentDataPosition; }
			set { _textVariableManager.CurrentDataPosition = value; }
		}

		/// <summary>
		/// Gets or sets the encoding to be used for any text content (plain text and/or HTML)
		/// </summary>
		public Encoding CharacterEncoding { get; set; }

		/// <summary>
		/// Gets or sets the transfer encoding for any text (e.g. SevenBit)
		/// </summary>
		public TransferEncoding TextTransferEncoding { get; set; }

		/// <summary>
		/// Gets or sets the transfer encoding for any binary content (e.g. Base64)
		/// </summary>
		public TransferEncoding BinaryTransferEncoding { get; set; }

		/// <summary>
		/// Gets or sets the culture info to apply for any variable formatting (like date, time etc.)
		/// </summary>
		public CultureInfo CultureInfo
		{
			get { return _cultureInfo; }
			set
			{
				_cultureInfo = value;
				_textVariableManager.CultureInfo = _cultureInfo;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// Converts the HtmlText property into plain text (without tags or html entities)
		/// If the converter is null, the ParsingHtmlConverter will be used. If this fails,
		/// a simple RegExHtmlConverter will be used.
		/// </summary>
		/// <param name="converter">
		/// The IHtmlConverter to be used for converting. If the converter is null, the 
		/// ParsingHtmlConverter will be used. If this fails,  RegExHtmlConverter will be 
		/// used. Usage of a parsing converter is recommended.
		/// </param>
		/// <returns>Returns the plain text representation of the HTML string.</returns>
		public string ConvertHtmlToPlainText(IHtmlConverter converter = null)
		{
			try
			{
				return converter == null
				       	? (new ParsingHtmlConverter()).ToPlainText(HtmlText)
				       	: converter.ToPlainText(HtmlText);
			}
			catch (FileNotFoundException)
			{
				// HtmlAgilityPack.dll not found
				return ((IHtmlConverter) new RegExHtmlConverter()).ToPlainText(HtmlText);
			}
		}

		/// <summary>
		/// Event rising after data of the DataSource, DataMember, CurrentDataItem,
		/// DataItemCount or CurrentDataPosition have changed.
		/// </summary>
		public event EventHandler<EventArgs> OnDataChanged;

		private void RaiseDataChangedEvent(object obj, EventArgs e)
		{
			if (OnDataChanged != null) OnDataChanged(this, e);
		}

		/// <summary>
		/// Gets the TextVariableManager used by the MailMergeMessage
		/// for processing the message text parts.
		/// </summary>
		/// <returns>Returns the TextVariableManager used by the current MailMergeMessage.</returns>
		public TextVariableManager GetTextVariableManager()
		{
			return _textVariableManager;
		}


		/// <summary>
		/// Returns the BindingSource object used internally.
		/// </summary>
		/// <returns>Returns the BindingSource object used internally.</returns>
		public BindingSource GetBindingSource()
		{
			return _textVariableManager.GetBindingSource();
		}

		/// <summary>
		/// Sets the variables (placeholders) to be used as datasource of the mail merge message.
		/// </summary>
		/// <param name="vars">Dictionary variables</param>
		public void SetVariables(Dictionary<string, string> vars)
		{
			var dt = new DataTable("Table");
			foreach (string key in vars.Keys)
			{
				dt.Columns.Add(key, vars[key].GetType());
			}
			var values = new string[vars.Count];

			int i = 0;
			foreach (string value in vars.Values)
			{
				values[i++] = value;
			}

			dt.Rows.Add(values); // added 2010-09-23, thanks to dale_burrell
			DataSource = dt;
		}


		/// <summary>
		/// Gets the MailMessage representation of the MailMergeMessage.
		/// </summary>
		/// <returns>Returns a MailMessage ready to be sent by an SmtpClient.</returns>
		/// <exception cref="MailMergeMessageException">Throws a general MailMergeMessageException, which contains a list of exceptions giving more details.</exception>
		public MailMessage GetMailMessage()
		{
			if (_mailMessage != null)
			{
				_mailMessage.Dispose();
			}
			_mailMessage = new MailMessage();
			AddSubjectToMailMessage();
			AddAttributesToMailMessage();
			AddAddressesToMailMessage();
			AddBodyToMailMessage();
			AddAttachmentsToMailMessage();

			var exceptions = new List<Exception>();

			if (_mailMessage.Headers[CToHeader] == null && _mailMessage.To.Count == 0 && _mailMessage.CC.Count == 0 &&
			    _mailMessage.Bcc.Count == 0)
				exceptions.Add(new AddressException("No recipients.", _badMailAddr, null));
			if (string.IsNullOrWhiteSpace(_mailMessage.From.ToString()))
				exceptions.Add(new AddressException("No from address.", _badMailAddr, null));
			if (_mailMessage.AlternateViews.Count == 0 && _mailMessage.Body.Length == 0 && _mailMessage.Attachments.Count == 0 &&
			    _mailMessage.Subject.Length == 0)
				exceptions.Add(new EmtpyContentException("Message is empty.", null));
			if (_badMailAddr.Count > 0)
				exceptions.Add(
					new AddressException(string.Format("Bad mail address(es): {0}", string.Join(", ", _badMailAddr.ToArray())),
					                     _badMailAddr, null));
			if (_badInlineFiles.Count > 0)
				exceptions.Add(
					new AttachmentException(
						string.Format("Inline attachment(s) missing or not readable: {0}", string.Join(", ", _badInlineFiles.ToArray())),
						_badInlineFiles, null));
			if (_badAttachmentFiles.Count > 0)
				exceptions.Add(
					new AttachmentException(
						string.Format("File attachment(s) missing or not readable: {0}", string.Join(", ", _badAttachmentFiles.ToArray())),
						_badAttachmentFiles, null));
			if (_badVariableNames.Count > 0)
				exceptions.Add(
					new VariableException(
						string.Format("Variable(s) for placeholder(s) not found: {0}", string.Join(", ", _badVariableNames.ToArray())),
						_badVariableNames, null));

			// Finally throw general exception
			if (exceptions.Count > 0)
				throw new MailMergeMessageException("Building of message failed.", exceptions, _mailMessage, null);

			return _mailMessage;
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~MailMergeMessage()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			if (! _disposed)
			{
				if (disposing && _mailMessage != null)
				{
					foreach (Attachment att in _mailMessage.Attachments) // 2010-09-08, thanks to floritheripper
						att.Dispose();

					// Dispose managed resources.
					_mailMessage.Dispose();
				}
			}
			_disposed = true;
		}

		#region *** Content methods and properties ***

		/// <summary>
		/// Gets or sets files that will be attached to a mail message.
		/// File names may contain placeholders.
		/// </summary>
		public List<FileAttachment> FileAttachments { get; set; }

		/// <summary>
		/// Gets or sets streams that will be attached to a mail message.
		/// </summary>
		public List<StreamAttachment> StreamAttachments { get; set; }

		/// <summary>
		/// Gets inline attachments (linked resources of the HTML body) of a mail message.
		/// They are generated automatically with all image sources pointing to local files.
		/// </summary>
		public List<FileAttachment> InlineAttachments
		{
			get { return _inlineAtt; }
		}

		/// <summary>
		/// Gets or sets string attachments that will be attached to a mail message.
		/// String attachments can be text or binary.
		/// </summary>
		public List<StringAttachment> StringAttachments { get; set; }

		/// <summary>
		/// Gets or sets the local base directory of HTML content.
		/// It useful for retrieval of inline attachments (linked resources of the HTML body).
		/// </summary>
		public string FileBaseDir
		{
			get { return _textVariableManager.FileBaseDir; }
			set { _textVariableManager.FileBaseDir = value; }
		}

		/// <summary>
		/// Replaces all variables in the text with their corresponding values.
		/// Used for subject, body and attachment.
		/// </summary>
		/// <param name="text">Text to search and replace.</param>
		/// <returns>Returns the text with all variables replaced.</returns>
		private StringBuilder SearchAndReplaceVars(StringBuilder text)
		{
			_textVariableManager.Text = text;
			StringBuilder toReturn = _textVariableManager.Process();
			_badVariableNames = _textVariableManager.BadVariables;
			_badInlineFiles = _textVariableManager.BadFiles;
			return toReturn;
		}

		/// <summary>
		/// Adds external inline attachments (linked resources of the HTML body) of a mail message.
		/// They are normally generated automatically with all image sources pointing to local files,
		/// but with this method such files can be added as well.
		/// </summary>
		/// <param name="att"></param>
		public void AddExternalInlineAttachment(FileAttachment att)
		{
			_inlineAttExternal.Add(att);
		}

		/// <summary>
		/// Clears external inline attachments (linked resources of the HTML body) of a mail message.
		/// They are normally generated automatically with all image sources pointing to local files.
		/// This method only removes attachments formerly added with AddExternalInlineAttachment.
		/// </summary>
		public void ClearExternalInlineAttachment()
		{
			_inlineAttExternal.Clear();
		}


		/// <summary>
		/// Prepares the mail message subject:
		/// Replacing placeholders with their values and setting correct encoding.
		/// </summary>
		private void AddSubjectToMailMessage()
		{
			var subject = new StringBuilder(Subject);
			subject = SearchAndReplaceVars(subject);
			_mailMessage.Subject = subject.ToString();
			_mailMessage.SubjectEncoding = CharacterEncoding;
		}

		/// <summary>
		/// Prepares the mail message body (plain text and/or HTML:
		/// Replacing placeholders with their values and setting correct encoding.
		/// </summary>
		private void AddBodyToMailMessage()
		{
			_badInlineFiles.Clear();
			_mailMessage.AlternateViews.Clear();
			_mailMessage.Body = null;

			// First plain body, then html body. This sequence is needed for many mail clients!
			// If either plain body or html body is missing, System.Net.Mail will automatically
			// switch to a single body part (the same way as if _mailMessage.Body would have been assigned).
			// But then TransferEncoding can be set as needed - this is not possible when using MailMessage.Body.

			AddPlainTextBodyToMailMessage();
			AddHtmlTextBodyToMailMessage();
		}


		private void AddPlainTextBodyToMailMessage()
		{
			// create the plain text body part
			if (string.IsNullOrEmpty(PlainText))
				return;

			StringBuilder plainText = SearchAndReplaceVars(new StringBuilder(PlainText));
			var plainBb = new PlainBodyBuilder(plainText.ToString())
			              	{
			              		TextTransferEncoding = TextTransferEncoding,
			              		CharacterEncoding = CharacterEncoding
			              	};
			_mailMessage.AlternateViews.Add(plainBb.Body);
		}

		private void AddHtmlTextBodyToMailMessage()
		{
			if (string.IsNullOrEmpty(HtmlText))
				return;

			// create the alternate HTML mail body part with any linked resources
			StringBuilder htmlText = SearchAndReplaceVars(new StringBuilder(HtmlText));
			var htmlBb = new HtmlBodyBuilder(htmlText.ToString(), SearchAndReplaceVars(new StringBuilder(Subject)).ToString())
			             	{
			             		DocBaseUrl = FileBaseDir,
			             		TextTransferEncoding = TextTransferEncoding,
			             		BinaryTransferEncoding = BinaryTransferEncoding,
			             		CharacterEncoding = CharacterEncoding
			             	};
			htmlBb.InlineAtt.AddRange(_inlineAttExternal);
			_inlineAtt = htmlBb.InlineAtt;
			_badInlineFiles.AddRange(htmlBb.BadInlineFiles);
			_mailMessage.AlternateViews.Add(htmlBb.Body);
		}


		/// <summary>
		/// Prepares the mail message file and string attachments:
		/// Replacing placeholders with their values and setting correct encoding.
		/// </summary>
		private void AddAttachmentsToMailMessage()
		{
			_badAttachmentFiles.Clear();

			foreach (FileAttachment fa in FileAttachments)
			{
				string filename = MakeFullPath(SearchAndReplaceVars(new StringBuilder(fa.Filename)).ToString());
				string displayName = SearchAndReplaceVars(new StringBuilder(fa.DisplayName)).ToString();

				try
				{
					_mailMessage.Attachments.Add(
						new AttachmentBuilder(new FileAttachment(filename, displayName, fa.MimeType), CharacterEncoding,
						                      TextTransferEncoding, BinaryTransferEncoding).Attachment);
				}
				catch (FileNotFoundException)
				{
					_badAttachmentFiles.Add(filename);
				}
				catch (IOException)
				{
					_badAttachmentFiles.Add(filename);
				}
			}

			foreach (StreamAttachment sa in StreamAttachments)
			{
				string displayName = SearchAndReplaceVars(new StringBuilder(sa.DisplayName)).ToString();
				_mailMessage.Attachments.Add(
					new AttachmentBuilder(new StreamAttachment(sa.Stream, displayName, sa.MimeType), CharacterEncoding,
					                      TextTransferEncoding, BinaryTransferEncoding).Attachment);
			}

			foreach (StringAttachment sa in StringAttachments)
			{
				string displayName = SearchAndReplaceVars(new StringBuilder(sa.DisplayName)).ToString();
				_mailMessage.Attachments.Add(
					new AttachmentBuilder(new StringAttachment(sa.Content, displayName, sa.MimeType), CharacterEncoding,
					                      TextTransferEncoding, BinaryTransferEncoding).Attachment);
			}
		}


		/// <summary>
		/// Calculates the full path of the file name, using the base directory if set.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns>The full path of the file.</returns>
		private string MakeFullPath(string filename)
		{
			return Tools.MakeFullPath(FileBaseDir, filename);
		}

		#endregion

		#region *** Address methods and properties ***

		/// <summary>
		/// Gets the collection of recipients and sender addresses of the message.
		/// </summary>
		public MailMergeAddressCollection MailMergeAddresses { get; private set; }


		/// <summary>
		/// If true, empty merge recipient addresses will be skipped.
		/// If false, empty addresses will throw an exception.
		/// </summary>
		public bool IgnoreEmptyRecipientAddr { get; set; }


		/// <summary>
		/// Prepares all recipient address and the corresponding header fields of a mail message.
		/// </summary>
		private void AddAddressesToMailMessage()
		{
			#region *** Clear MailMessage headers ***

			/*
			 * Not really necessary because we always work on a NEW instance of a MailMessage
			 */

			//cc, _bcc, _sender, _from, _replyto;
			_mailMessage.To.Clear();
			_mailMessage.CC.Clear();
			_mailMessage.Bcc.Clear();
			_mailMessage.ReplyToList.Clear();
			_mailMessage.Sender = null;

			#endregion

			_badMailAddr.Clear();

			MailMergeAddress testAddress = null;
			foreach (MailMergeAddress mmAddr in MailMergeAddresses.Where(mmAddr => mmAddr.AddrType == MailAddressType.TestAddress))
			{
				testAddress = new MailMergeAddress(MailAddressType.TestAddress, mmAddr.Address, mmAddr.DisplayName,
				                                   mmAddr.DisplayNameCharacterEncoding);
			}

			// ShowNullsAs MUST be string.empty with email addresses!
			TextVariableManager txtMgr = _textVariableManager.Clone();
			txtMgr.ShowNullAs = txtMgr.ShowEmptyAs = string.Empty;

			foreach (MailMergeAddress mmAddr in MailMergeAddresses)
			{
				try
				{
					MailAddress mailAddr;
					// use the address part the test mail address (if set) but use the original display name
					if (testAddress != null)
					{
						testAddress.DisplayName = mmAddr.DisplayName;
						testAddress.TextVariableManager = txtMgr;
						mailAddr = testAddress.GetMailAddress();
					}
					else
					{
						mmAddr.TextVariableManager = txtMgr;
						mailAddr = mmAddr.GetMailAddress();
					}

					_badVariableNames.AddRange(txtMgr.BadVariables);
					_badInlineFiles.AddRange(txtMgr.BadFiles);

					if (IgnoreEmptyRecipientAddr && mailAddr == null)
						continue;

					switch (mmAddr.AddrType)
					{
						case MailAddressType.To:
							_mailMessage.To.Add(mailAddr);
							break;
						case MailAddressType.CC:
							_mailMessage.CC.Add(mailAddr);
							break;
						case MailAddressType.Bcc:
							_mailMessage.Bcc.Add(mailAddr);
							break;
						case MailAddressType.ReplyTo:
							_mailMessage.ReplyToList.Add(mailAddr);
							break;
						case MailAddressType.ConfirmReadingTo:
							_mailMessage.Headers.Remove(CConfirmReading);
							_mailMessage.Headers.Remove(CDispositionNotification);
							_mailMessage.Headers.Add(CConfirmReading, mailAddr.Address);
							_mailMessage.Headers.Add(CDispositionNotification, mailAddr.Address);
							break;
						case MailAddressType.ReturnReceiptTo:
							_mailMessage.Headers.Remove(CReturnReceipt);
							_mailMessage.Headers.Add(CReturnReceipt, mailAddr.Address);
							break;
						case MailAddressType.Sender:
							_mailMessage.Sender = mailAddr;
							break;
						case MailAddressType.From:
							_mailMessage.From = mailAddr;
							break;
					}
				}
				catch (FormatException)
				{
					_badMailAddr.Add(mmAddr.ToString());
				}
			}
		}

		#endregion

		#region *** Special attributes related properties and methods ***

		/// <summary>
		/// Gets or sets the user defined headers of a mail message.
		/// </summary>
		public NameValueCollection Headers { get; set; }

		/// <summary>
		/// Gets or sets the "x-mailer" header to be used.
		/// </summary>
		public string Xmailer { get; set; }

		/// <summary>
		/// Gets or sets the priority of a mail message.
		/// </summary>
		public MailPriority Priority { get; set; }

		/// <summary>
		/// Gets or sets the delivery notification options of a mail message.
		/// </summary>
		public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }

		/// <summary>
		/// Sets all attributes of a mail message.
		/// </summary>
		private void AddAttributesToMailMessage()
		{
			// if we're on .NET 4 or later, set 
			// _mailMessage.HeadersEncoding
			PropertyInfo pi = _mailMessage.GetType().GetProperty("HeadersEncoding");
			if (pi != null)
				pi.SetValue(_mailMessage, CharacterEncoding, null);

			// first delete all headers we have a value to store, so that no values are appended
			for (int i = 0; i < Headers.Count; i++)
			{
				_mailMessage.Headers.Remove(Headers.GetKey(i));
			}
			// now add our headers to the message
			for (int i = 0; i < Headers.Count; i++)
			{
				_mailMessage.Headers.Add(Headers.GetKey(i), Headers.Get(i));
			}

			_mailMessage.Priority = Priority;
			_mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions;

			if (!string.IsNullOrEmpty(Xmailer))
			{
				_mailMessage.Headers.Add(cXmailer, Xmailer);
			}
		}

		#endregion
	}
}