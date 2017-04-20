using System;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Argument used by the event after sending of a message is completed.
	/// </summary>
	public class MailSenderAfterSendEventArgs : EventArgs
	{
		public readonly bool Cancelled;
		public readonly DateTime EndTime;
		public readonly Exception Error;
		public readonly MailMergeMessage MailMergeMessage;
		public readonly DateTime StartTime;

		internal MailSenderAfterSendEventArgs(Exception error, bool cancelled, MailMergeMessage mailMergeMessage,
		                                      DateTime startTime, DateTime endTime)
		{
			Error = error;
			Cancelled = cancelled;
			MailMergeMessage = mailMergeMessage;
			StartTime = startTime;
			EndTime = endTime;
		}
	}

	/// <summary>
	/// Argument used by the event before sending of a message is completed.
	/// </summary>
	public class MailSenderBeforeSendEventArgs : EventArgs
	{
		public readonly bool Cancelled;
		public readonly Exception Error;
		public readonly MailMergeMessage MailMergeMessage;
		public readonly DateTime StartTime;

		internal MailSenderBeforeSendEventArgs(Exception error, bool cancelled, MailMergeMessage mailMergeMessage,
		                                       DateTime startTime)
		{
			Error = error;
			Cancelled = cancelled;
			MailMergeMessage = mailMergeMessage;
			StartTime = startTime;
		}
	}

	/// <summary>
	/// Argument used by the event before starting a mail merge.
	/// </summary>
	public class MailSenderMergeBeginEventArgs : EventArgs
	{
		public readonly MailMergeMessage MailMergeMessage;
		public readonly DateTime StartTime;

		internal MailSenderMergeBeginEventArgs(DateTime startTime, MailMergeMessage mailMergeMessage)
		{
			StartTime = startTime;
			MailMergeMessage = mailMergeMessage;
		}
	}

	/// <summary>
	/// Argument used by the event after every mail sent during a mail merge.
	/// </summary>
	public class MailSenderMergeProgressEventArgs : EventArgs
	{
		public readonly int ErrorMsg;
		public readonly MailMergeMessage MailMergeMessage;
		public readonly int SentMsg;
		public readonly DateTime StartTime;
		public readonly int TotalMsg;
		public bool Completed;

		internal MailSenderMergeProgressEventArgs(DateTime startTime, int totalMsg, int sentMsg, int errorMsg,
		                                          MailMergeMessage mailMergeMessage, bool completed)
		{
			StartTime = startTime;
			TotalMsg = totalMsg;
			SentMsg = sentMsg;
			ErrorMsg = errorMsg;
			MailMergeMessage = mailMergeMessage;
			Completed = completed;
		}
	}

	/// <summary>
	/// Argument used by the event after finishing a mail merge.
	/// </summary>
	public class MailSenderMergeCompleteEventArgs : EventArgs
	{
		public readonly bool Cancelled;
		public readonly DateTime EndTime;
		public readonly int MailsSent;
		public readonly DateTime StartTime;

		internal MailSenderMergeCompleteEventArgs(bool cancelled, DateTime startTime, DateTime endTime, int mailsSent)
		{
			Cancelled = cancelled;
			StartTime = startTime;
			EndTime = endTime;
			MailsSent = mailsSent;
		}
	}

	/// <summary>
	/// Argument used by the event after sending a message has failed.
	/// </summary>
	public class MailSenderSendFailureEventArgs : EventArgs
	{
		public readonly Exception Error;
		public readonly int FailureCounter;
		public readonly MailMergeMessage MailMergeMessage;
		public readonly int MaxFailures;
		public readonly int RetryDelayTime;

		internal MailSenderSendFailureEventArgs(Exception error, int failureCounter, int maxFailures, int retryDelayTime,
		                                        MailMergeMessage mailMergeMessage)
		{
			Error = error;
			FailureCounter = failureCounter;
			MaxFailures = maxFailures;
			RetryDelayTime = retryDelayTime;
			MailMergeMessage = mailMergeMessage;
		}
	}
}