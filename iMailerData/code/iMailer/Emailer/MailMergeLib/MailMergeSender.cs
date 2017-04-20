using System;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Enumeration of message output types
	/// </summary>
	public enum MessageOutput
	{
		None,
		SmtpServer,
		Directory,
		PickupDirectoryFromIis
	}

	/// <summary>
	/// Sends MailMergeMessages to an SMTP server. It uses System.Net.Mail.SmtpClient for low level operations.
	/// </summary>
	public class MailMergeSender : IDisposable
	{
		// Note: SmtpClient only allows 1 Send or SendAsync operation at a time,
		// even with more than 1 instance of the class.
		private static readonly object _locker = new object();
		private readonly SmtpClient _smtp = new SmtpClient();

		private NetworkCredential _basicAuthenticationInfo;
		private BackgroundWorker _bgSender;
		private bool _disposed;
		private int _maxFailures = 1;
		private int _retryDelayTime;
		private bool _sendCancel;
		// We use BackgroundWorker, using ThreadPool, with a max of 30 threads at a time.
		// At the same time, SmtpClient only allows 1 Send or SendAsync operation at a time - so what.

		public MailMergeSender()
		{
			Timeout = 100000;
			MessageOutput = MessageOutput.SmtpServer;
			MailOutputDirectory = null;
			ReadyMerged = true;
			ReadySent = true;
			DelayBetweenMessages = 0;
			EnableSsl = false;
			SmtpPort = 25;
			SmtpHost = "localhost";
		}

		/// <summary>
		/// Gets or sets the name or IP address of the SMTP host to be used for sending mails.
		/// </summary>
		public string SmtpHost { get; set; }

		/// <summary>
		/// Gets or set the port of the SMTP host to be used for sending mails.
		/// </summary>
		public int SmtpPort { get; set; }


		/// <summary>
		/// Gets or sets the name of the local machine sent to the SMTP server in the hello command
		/// of an SMTP transaction. Defaults to the windows machine name.
		/// </summary>
		public string LocalHostName
		{
			get
			{
				// member "localHostName" changed to "clientDomain" in .NET 2.0.50727.4927
				// so we have to check which member should be invoked
				const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
				MemberInfo[] mi = typeof (SmtpClient).FindMembers(MemberTypes.Field, flags,
				                                                  (mInfo, objSearch) =>
				                                                  Array.IndexOf((string[]) objSearch, mInfo.Name) >= 0,
				                                                  new[] {"localHostName", "clientDomain"});
				if (mi.Length == 1)
					return typeof (SmtpClient).GetField(mi[0].Name, flags).GetValue(_smtp) as string;

				return Environment.MachineName;
			}
			set
			{
				// member "localHostName" changed to "clientDomain" in .NET 2.0.50727.4927
				// so we have to check which member should be invoked

				const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

				MemberInfo[] mi = typeof (SmtpClient).FindMembers(MemberTypes.Field, flags,
				                                                  (mInfo, objSearch) =>
				                                                  Array.IndexOf((string[]) objSearch, mInfo.Name) >= 0,
				                                                  new[] {"localHostName", "clientDomain"});

				// if neither localHostName nore clientDomain are valid members, 
				// SmtpClient will just use System.Environment.MachineName
				if (mi.Length == 1)
					typeof (SmtpClient).GetField(mi[0].Name, flags).SetValue(_smtp, value);
			}
		}

		/// <summary>
		/// Returns true, if the transaction of sending a message has completed, else false.
		/// </summary>
		public bool ReadySent { get; private set; }

		/// <summary>
		/// Returns true, if the mail merge transaction has completed, else false.
		/// </summary>
		public bool ReadyMerged { get; private set; }

		/// <summary>
		/// Gets or sets the name of the output directory of sent mail messages
		/// (only used if messages are not sent to SMTP server)
		/// </summary>
		public string MailOutputDirectory { get; set; }

		/// <summary>
		/// Gets or sets the location where to send mail messages.
		/// </summary>
		public MessageOutput MessageOutput { get; set; }

		/// <summary>
		/// Gets or sets usage of SSL for sending mails.
		/// </summary>
		public bool EnableSsl { get; set; }

		/// <summary>
		/// Gets or sets the timeout for sending a message, after which a time-out exception will raise.
		/// Time-out value in milliseconds. The default value is 100,000 (100 seconds). 
		/// </summary>
		public int Timeout { get; set; }

		/// <summary>
		/// Gets or sets the number of failures (1-5) after which a retry to send will be performed.
		/// </summary>
		public int MaxFailures
		{
			get { return _maxFailures; }
			set { _maxFailures = (value >= 1 && value < 5) ? value : 1; }
		}

		/// <summary>
		/// Gets or sets the delay time in milliseconds (0-10000) to elaps between retries to send the message.
		/// </summary>
		public int RetryDelayTime
		{
			get { return _retryDelayTime; }
			set { _retryDelayTime = (value >= 0 && value <= 10000) ? value : 0; }
		}

		/// <summary>
		/// Gets or sets the delay time in milliseconds (0-10000) to elaps between the messages.
		/// Mainly used for debug purposes.
		/// </summary>
		public int DelayBetweenMessages { get; set; }

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// Set authentification details for loggin into an SMTP server.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public void SetSmtpAuthentification(string username, string password)
		{
			// set authentication values for smtp login
			if ((username != null && password != null) && (username.Length > 0 || password.Length > 0))
			{
				_basicAuthenticationInfo = new NetworkCredential(username, password);
			}
		}


		/// <summary>
		/// Sends mail messages asyncronously to all recipients supplied in the data table.
		/// </summary>
		/// <param name="mailMergeMessage">Mail merge message.</param>
		/// <param name="mailDataTable">DataTable to use as a data source.</param>
		[Obsolete(
			"Depreciated - this will be removed eventually. Use SendAllAsync(MailMergeMessage mailMergeMessage) instead.", false)
		]
		public void SendAsync(MailMergeMessage mailMergeMessage, DataTable mailDataTable)
		{
			mailMergeMessage.DataSource = mailDataTable;
			SendAllAsync(mailMergeMessage);
		}

		/// <summary>
		/// Sends mail messages asyncronously to all recipients supplied in the data source
		/// of the mail merge message.
		/// </summary>
		/// <param name="mailMergeMessage">Mail merge message.</param>
		public void SendAllAsync(MailMergeMessage mailMergeMessage)
		{
			if (mailMergeMessage == null)
				throw new NullReferenceException("MailMergeMessage must not be null.");

			_bgSender = new BackgroundWorker {WorkerSupportsCancellation = true};
			_bgSender.DoWork += bgSender_DoSendMulti;
			_bgSender.RunWorkerCompleted += ((sender, e) => ((BackgroundWorker) sender).Dispose());
			_bgSender.RunWorkerAsync(mailMergeMessage);
		}

		/// <summary>
		/// Worker method doing the job sending out mails to all recipients of a DataTable.
		/// </summary>
		/// <remarks>The method rises events before and after mail merge, and during progress after each processed mail.</remarks>
		/// <param name="sender"></param>
		/// <param name="e">A MailMergeMessage.</param>
		private void bgSender_DoSendMulti(object sender, DoWorkEventArgs e)
		{
			// Wait until it is safe to enter.
			lock (_locker)
			{
				ReadyMerged = false;
				int sentMsgCount = 0;
				int errorMsgCount = 0;

				EventHandler<MailSenderAfterSendEventArgs> afterSend = (obj, args) =>
				                                                       	{
				                                                       		if (args.Error == null)
				                                                       			sentMsgCount++;
				                                                       		else
				                                                       			errorMsgCount++;
				                                                       	};

				OnAfterSend += afterSend;
				var mmm = e.Argument as MailMergeMessage;
				if (mmm == null) return;

				DateTime startTime = DateTime.Now;

				if (OnMergeBegin != null)
				{
					OnMergeBegin(_smtp, new MailSenderMergeBeginEventArgs(startTime, mmm));
				}


				for (int pos = 0; pos < mmm.DataItemCount; pos++)
				{
					mmm.CurrentDataPosition = pos;

					if (_sendCancel || ((BackgroundWorker) sender).CancellationPending)
					{
						break;
					}

					Send(mmm, true);

					if (OnMergeProgress != null)
					{
						OnMergeProgress(_smtp,
						                new MailSenderMergeProgressEventArgs(startTime, mmm.DataItemCount, sentMsgCount,
						                                                     errorMsgCount, mmm, false));
					}

					if (_sendCancel || ((BackgroundWorker) sender).CancellationPending)
					{
						break;
					}

					Thread.Sleep(DelayBetweenMessages);
				}

				if (OnMergeProgress != null)
				{
					OnMergeProgress(_smtp,
					                new MailSenderMergeProgressEventArgs(startTime, mmm.DataItemCount, sentMsgCount,
					                                                     errorMsgCount, null, true));
				}

				if (OnMergeComplete != null)
				{
					OnMergeComplete(_smtp,
					                new MailSenderMergeCompleteEventArgs(_sendCancel, startTime, DateTime.Now, sentMsgCount));
				}

				OnAfterSend -= afterSend;
				ReadyMerged = true;
				Thread.Sleep(DelayBetweenMessages);

				// end lock
			}
		}

		/// <summary>
		/// Sends a single mail message asyncronously.
		/// </summary>
		/// <remarks>The method rises events before and after sending, as well as on send failure.</remarks>
		/// <param name="mailMergeMessage">Mail merge message.</param>
		public void SendAsync(MailMergeMessage mailMergeMessage)
		{
			_bgSender = new BackgroundWorker {WorkerSupportsCancellation = true};
			_bgSender.DoWork += bgSender_DoSendSingle;
			_bgSender.RunWorkerCompleted += ((sender, e) => ((BackgroundWorker) sender).Dispose());
			_bgSender.RunWorkerAsync(mailMergeMessage);
		}

		/// <summary>
		/// Worker method doing the job sending out a single mail.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">A MailMergeMessage.</param>
		private void bgSender_DoSendSingle(object sender, DoWorkEventArgs e)
		{
			// Wait until it is safe to enter.
			lock (_locker)
			{
				if (_sendCancel == false && ((BackgroundWorker) sender).CancellationPending == false)
				{
					Send(e.Argument as MailMergeMessage, true);
				}
			}
		}

		/// <summary>
		/// Sends mail messages syncronously to all recipients supplied in the data source
		/// of the mail merge message.
		/// </summary>
		/// <param name="mailMergeMessage">Mail merge message.</param>
		public void SendAll(MailMergeMessage mailMergeMessage)
		{
			lock (_locker)
			{
				for (int i = 0; i < mailMergeMessage.DataItemCount; i++)
				{
					mailMergeMessage.CurrentDataPosition = i;
					Send(mailMergeMessage);
				}
			}
		}

		/// <summary>
		/// Sends a single mail merge message.
		/// </summary>
		/// <param name="mailMergeMessage">Message to send.</param>
		public void Send(MailMergeMessage mailMergeMessage)
		{
			lock (_locker)
			{
				Send(mailMergeMessage, false);
			}
		}

		/// <summary>
		/// This is the procedure taking care of sending the message.
		/// </summary>
		/// <param name="mailMergeMessage">Mail merge message to send.</param>
		/// <param name="isAsync">If true, no exceptions are thrown. If false, exceptions are thrown.</param>
		/// <exception>isAsync must be false for exceptions to be thrown.
		/// If the MailMergeMessage is the cause of the exception, MailMergeMessage.MailMergeMessageException is thrown.
		/// If the SMTP transaction is the cause, SmtpFailedRecipientsException, SmtpFailedRecipientException or SmtpException can be expected.
		/// </exception>
		private void Send(MailMergeMessage mailMergeMessage, bool isAsync)
		{
			PrepareSettings();
			DateTime startTime = DateTime.Now;
			Exception sendException = null;
			MailMessage msg = null;

			ReadySent = false;

			try
			{
				msg = mailMergeMessage.GetMailMessage();
			}
			catch (Exception ex)
			{
				sendException = ex;
				if (sendException.GetType() == typeof (MailMergeMessage.MailMergeMessageException))
				{
					// get the message, although due to exceptions it will not be complete
					msg = ((MailMergeMessage.MailMergeMessageException) sendException).MailMessage;
				}
			}

			// the client can rely on the sequence of events: OnBeforeSend, OnSendFailure (if any), OnAfterSend
			if (OnBeforeSend != null)
			{
				OnBeforeSend(_smtp, new MailSenderBeforeSendEventArgs(sendException, _sendCancel, mailMergeMessage, startTime));
			}

			// if there was an exception in building the message and sender is running asnc,
			// only trigger events and return
			if (sendException != null)
			{
				if (OnSendFailure != null)
				{
					OnSendFailure(_smtp, new MailSenderSendFailureEventArgs(sendException, 1, 1, 0, mailMergeMessage));
				}
				if (OnAfterSend != null)
				{
					OnAfterSend(_smtp,
					            new MailSenderAfterSendEventArgs(sendException, _sendCancel, mailMergeMessage, startTime, DateTime.Now));
				}
				ReadySent = true;

				if (!isAsync)
				{
					throw sendException;
				}

				return;
			}

			int failureCounter = 0;
			do
			{
				try
				{
					sendException = null;
					if (MessageOutput != MessageOutput.None)
					{
						_smtp.Send(msg);

						foreach (Attachment att in msg.Attachments) // 2010-09-08, thanks to floritheripper
							att.Dispose();

						// Fix by Abdelkrim 2009-02-04: when _smtp.Send throws less than _maxFailures exceptions,
						// and succeeds after an exception, we MUST break the while loop here (else: infinite)
						break;
					}
				}
				catch (Exception ex)
				{
					if (ex.GetType() == typeof (SmtpFailedRecipientsException) || ex.GetType() == typeof (SmtpFailedRecipientException) ||
					    ex.GetType() == typeof (SmtpException))
					{
						failureCounter++;
						sendException = ex;
						if (OnSendFailure != null)
						{
							OnSendFailure(_smtp,
							              new MailSenderSendFailureEventArgs(sendException, failureCounter, _maxFailures, _retryDelayTime,
							                                                 mailMergeMessage));
						}
						Thread.Sleep(_retryDelayTime);
					}
					else
					{
						failureCounter = _maxFailures;
						sendException = ex;
						if (OnSendFailure != null)
						{
							OnSendFailure(_smtp, new MailSenderSendFailureEventArgs(sendException, 1, 1, 0, mailMergeMessage));
						}
					}
				}
			} while (failureCounter < _maxFailures && failureCounter > 0);

			if (OnAfterSend != null)
			{
				OnAfterSend(_smtp,
				            new MailSenderAfterSendEventArgs(sendException, _sendCancel, mailMergeMessage, startTime, DateTime.Now));
			}

			ReadySent = true;

			if (sendException != null && ! isAsync)
			{
				throw sendException;
			}
		}

		/// <summary>
		/// Setup SmtpClient as needed.
		/// </summary>
		private void PrepareSettings()
		{
			_smtp.Timeout = Timeout;
			_smtp.EnableSsl = EnableSsl;

			if (! ReadyMerged && ! ReadySent)
				_sendCancel = false;

			if (_basicAuthenticationInfo != null)
			{
				_smtp.UseDefaultCredentials = false;
				_smtp.Credentials = _basicAuthenticationInfo;
			}

			switch (MessageOutput)
			{
				case MessageOutput.Directory:
					_smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					_smtp.PickupDirectoryLocation = MailOutputDirectory;
					break;
				case MessageOutput.PickupDirectoryFromIis:
					_smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
					_smtp.PickupDirectoryLocation = null;
					break;
				default:
					_smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
					_smtp.Host = SmtpHost;
					_smtp.Port = SmtpPort;
					break;
			}
		}

		/// <summary>
		/// Event rising before sending a mail message.
		/// </summary>
		public event EventHandler<MailSenderBeforeSendEventArgs> OnBeforeSend;

		/// <summary>
		/// Event rising after sending a mail message.
		/// </summary>
		public event EventHandler<MailSenderAfterSendEventArgs> OnAfterSend;

		/// <summary>
		/// Event rising, if an error occurs when sending a mail message.
		/// </summary>
		public event EventHandler<MailSenderSendFailureEventArgs> OnSendFailure;

		/// <summary>
		/// Event rsing before starting with mail merge.
		/// </summary>
		public event EventHandler<MailSenderMergeBeginEventArgs> OnMergeBegin;

		/// <summary>
		/// Event rising during mail merge progress, i.e. after each message sent.
		/// </summary>
		public event EventHandler<MailSenderMergeProgressEventArgs> OnMergeProgress;

		/// <summary>
		/// Event rising after completing mail merge.
		/// </summary>
		public event EventHandler<MailSenderMergeCompleteEventArgs> OnMergeComplete;

		/// <summary>
		/// Cancel any transactions sending or merging mail.
		/// </summary>
		public void SendCancel()
		{
			if (_bgSender == null || !_bgSender.IsBusy) return;

			_sendCancel = true;
			_bgSender.CancelAsync();
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~MailMergeSender()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			if (! _disposed)
			{
				if (disposing && _bgSender != null)
				{
					// Dispose managed resources.
					_bgSender.Dispose();
				}

				// Clean up unmanaged resources here.
			}
			_disposed = true;
		}
	}
}