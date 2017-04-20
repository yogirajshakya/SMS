using System.Net.Mail;
using System.Net.Mime;

namespace Infolancers.iMailer.Emailer
{
	internal class PlainBodyBuilder : BodyBuilderBase
	{
		private readonly string _plainText;

		public PlainBodyBuilder(string plainText)
		{
			_plainText = plainText;
		}


		/// <summary>
		/// Gets the ready made body part for a mail message.
		/// </summary>
		public override AlternateView Body
		{
			get
			{
				AlternateView plainBody = AlternateView.CreateAlternateViewFromString(Bugfixer.LimitLineLengthRfc2822(_plainText),
				                                                                      CharacterEncoding, MediaTypeNames.Text.Plain);
				plainBody.TransferEncoding = Tools.IsSevenBit(plainBody.ContentStream)
				                             	? TransferEncoding.SevenBit
				                             	: TextTransferEncoding != TransferEncoding.SevenBit
				                             	  	? TextTransferEncoding
				                             	  	: TransferEncoding.QuotedPrintable;
				plainBody.ContentType.CharSet = CharacterEncoding.HeaderName; // RFC 2045 Section 5.1 - http://www.ietf.org
				return plainBody;
			}
		}

		/// <summary>
		/// Get the text representation of the source document
		/// </summary>
		public override string DocText
		{
			get { return _plainText; }
		}
	}
}