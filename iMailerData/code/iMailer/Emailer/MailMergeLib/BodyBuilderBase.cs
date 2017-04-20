using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Abstract base class for HtmlBodyBuilder and PlainBodyBuilder.
	/// </summary>
	internal abstract class BodyBuilderBase
	{
		protected BodyBuilderBase()
		{
			TextTransferEncoding = TransferEncoding.SevenBit;
			CharacterEncoding = Encoding.Default;
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
		/// Gets the ready made body part for a mail message.
		/// </summary>
		public abstract AlternateView Body { get; }

		/// <summary>
		/// Get the text representation of the source document
		/// </summary>
		public abstract string DocText { get; }
	}
}