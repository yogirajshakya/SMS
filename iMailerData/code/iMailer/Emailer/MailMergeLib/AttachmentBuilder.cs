using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Provides methods for building FileAttachments and StringAttachmets for MailMergeMessages.
	/// </summary>
	internal class AttachmentBuilder
	{
		private readonly Attachment _attachment;

		public AttachmentBuilder(FileAttachment fileAtt, Encoding characterEncoding, TransferEncoding textTransferEncoding,
		                         TransferEncoding binaryTransferEncoding)
		{
			_attachment = new Attachment(fileAtt.Filename) {ContentType = {MediaType = fileAtt.MimeType}};
			_attachment.ContentDisposition.Inline = false;
			_attachment.ContentDisposition.FileName = fileAtt.DisplayName.Trim(new[] {'\\', '/', ':'});
			_attachment.ContentDisposition.CreationDate = File.GetCreationTime(fileAtt.Filename);
			_attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(fileAtt.Filename);
			_attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(fileAtt.Filename);
			_attachment.NameEncoding = characterEncoding;

			// Take care of correct encoding for the file name, otherwise
			// _attachment.TransferEncoding will throw FormatException 'MailHeaderFieldInvalidCharacter'
			// This also encodes spaces in the attachment name, because otherwise they would not be displayed correctly (RFC2047)
			Bugfixer.CorrectAttachmentFileNameEncoding(_attachment, characterEncoding);

			if (_attachment.ContentType.MediaType.ToLower().StartsWith("text/"))
			{
				_attachment.ContentType.CharSet = characterEncoding.HeaderName;
				_attachment.TransferEncoding = Tools.IsSevenBit(_attachment.ContentStream, characterEncoding)
				                               	? TransferEncoding.SevenBit
				                               	: textTransferEncoding;
			}
			else
			{
				_attachment.ContentType.CharSet = null;
				_attachment.TransferEncoding = binaryTransferEncoding;
			}
		}

		public AttachmentBuilder(StringAttachment stringAtt, Encoding characterEncoding, TransferEncoding textTransferEncoding,
		                         TransferEncoding binaryTransferEncoding)
		{
			string displayName = ShortNameFromFile(stringAtt.DisplayName);

			_attachment = Attachment.CreateAttachmentFromString(stringAtt.Content, displayName, characterEncoding,
			                                                    stringAtt.MimeType);
			_attachment.ContentType.MediaType = stringAtt.MimeType;
			_attachment.ContentDisposition.Inline = false;
			_attachment.ContentDisposition.FileName = displayName;
			_attachment.ContentDisposition.CreationDate = DateTime.Now;
			_attachment.ContentDisposition.ModificationDate = DateTime.Now;
			_attachment.ContentDisposition.ReadDate = DateTime.Now;
			// Use predefined att.ContentId
			_attachment.NameEncoding = characterEncoding;

			// Take care of correct encoding for the file name, otherwise
			// _attachment.TransferEncoding will throw FormatException 'MailHeaderFieldInvalidCharacter'
			Bugfixer.CorrectAttachmentFileNameEncoding(_attachment, characterEncoding);

			if (_attachment.ContentType.MediaType.ToLower().StartsWith("text/"))
			{
				_attachment.ContentType.CharSet = characterEncoding.HeaderName;
				_attachment.TransferEncoding = Tools.IsSevenBit(_attachment.ContentStream, characterEncoding)
				                               	? TransferEncoding.SevenBit
				                               	: textTransferEncoding;
			}
			else
			{
				_attachment.ContentType.CharSet = null;
				_attachment.TransferEncoding = binaryTransferEncoding;
			}
		}

		public AttachmentBuilder(StreamAttachment streamAtt, Encoding characterEncoding, TransferEncoding textTransferEncoding,
		                         TransferEncoding binaryTransferEncoding)
		{
			_attachment = new Attachment(streamAtt.Stream, streamAtt.DisplayName, streamAtt.MimeType)
			              	{ContentType = {MediaType = streamAtt.MimeType}};
			_attachment.ContentDisposition.Inline = false;
			_attachment.ContentDisposition.FileName = streamAtt.DisplayName.Trim(new[] {'\\', '/', ':'});
			_attachment.ContentDisposition.CreationDate = DateTime.Now;
			_attachment.ContentDisposition.ModificationDate = _attachment.ContentDisposition.CreationDate;
			_attachment.ContentDisposition.ReadDate = _attachment.ContentDisposition.CreationDate;
			_attachment.NameEncoding = characterEncoding;

			// Take care of correct encoding for the file name, otherwise
			// _attachment.TransferEncoding will throw FormatException 'MailHeaderFieldInvalidCharacter'
			// This also encodes spaces in the attachment name, because otherwise they would not be displayed correctly (RFC2047)
			Bugfixer.CorrectAttachmentFileNameEncoding(_attachment, characterEncoding);

			if (_attachment.ContentType.MediaType.ToLower().StartsWith("text/"))
			{
				_attachment.ContentType.CharSet = characterEncoding.HeaderName;
				_attachment.TransferEncoding = textTransferEncoding;
			}
			else
			{
				_attachment.ContentType.CharSet = null;
				_attachment.TransferEncoding = binaryTransferEncoding;
			}
		}

		public Attachment Attachment
		{
			get { return _attachment; }
		}

		private static string ShortNameFromFile(string fileName)
		{
			int num = fileName.LastIndexOfAny(new[] {'\\', ':'}, fileName.Length - 1, fileName.Length);
			return num > 0 ? fileName.Substring(num + 1, (fileName.Length - num) - 1) : fileName;
		}
	}
}