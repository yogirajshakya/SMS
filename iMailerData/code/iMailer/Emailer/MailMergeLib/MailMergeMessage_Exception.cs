using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.Serialization;

namespace Infolancers.iMailer.Emailer
{
	public partial class MailMergeMessage
	{
		#region Nested type: AddressException

		/// <summary>
		///  Mail merge bad address exception.
		/// </summary>
		public class AddressException : Exception
		{
			private readonly List<string> _badAddress = new List<string>();

			public AddressException(string message, List<string> badAddress, Exception innerException)
				: base(message, innerException)
			{
				_badAddress = badAddress;
			}

			// necessary to ensure serialization is possible
			protected AddressException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public List<string> BadAddress
			{
				get { return _badAddress; }
			}
		}

		#endregion

		#region Nested type: AttachmentException

		/// <summary>
		/// Mail merge attachment exception.
		/// </summary>
		public class AttachmentException : Exception
		{
			private readonly List<string> _badAttachment = new List<string>();

			public AttachmentException(string message, List<string> badAttachment, Exception innerException)
				: base(message, innerException)
			{
				_badAttachment = badAttachment;
			}

			// necessary to ensure serialization is possible
			protected AttachmentException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public List<string> BadAttachment
			{
				get { return _badAttachment; }
			}
		}

		#endregion

		#region Nested type: EmtpyContentException

		/// <summary>
		/// Mail merge empty content exception.
		/// </summary>
		public class EmtpyContentException : Exception
		{
			public EmtpyContentException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// necessary to ensure serialization is possible
			protected EmtpyContentException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		#endregion

		#region Nested type: MailMergeMessageException

		/// <summary>
		/// Mail merge message exception.
		/// </summary>
		public class MailMergeMessageException : Exception
		{
			private readonly List<Exception> _exceptions;
			private readonly MailMessage _mailMessage;

			public MailMergeMessageException(string message, List<Exception> exceptions, MailMessage mailMessage,
			                                 Exception innerException)
				: base(message, innerException)
			{
				_exceptions = exceptions;
				_mailMessage = mailMessage;
			}

			// necessary to ensure serialization is possible
			protected MailMergeMessageException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public List<Exception> Exceptions
			{
				get { return _exceptions; }
			}

			public MailMessage MailMessage
			{
				get { return _mailMessage; }
			}
		}

		#endregion

		#region Nested type: VariableException

		/// <summary>
		/// Mail merge exception for placeholders that are missing in the datasource.
		/// </summary>
		public class VariableException : Exception
		{
			private readonly List<string> _missingVariable = new List<string>();

			public VariableException(string message, List<string> missingVariable, Exception innerException)
				: base(message, innerException)
			{
				_missingVariable = missingVariable;
			}

			// necessary to ensure serialization is possible
			protected VariableException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public List<string> MissingVariable
			{
				get { return _missingVariable; }
			}
		}

		#endregion
	}
}