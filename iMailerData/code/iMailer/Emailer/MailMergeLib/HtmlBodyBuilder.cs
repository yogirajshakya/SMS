using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Builds the HTML body part for a mail message. Images references will be converted to
	/// embedded cid content. Does not make any general changes to the HTML document
	/// </summary>
	internal class HtmlBodyBuilder : BodyBuilderBase
	{
		private readonly List<string> _badInlineFiles = new List<string>(10);
		private readonly List<FileAttachment> _inlineAtt = new List<FileAttachment>(20);
		private readonly HtmlTagHelper _tagHelper;
		private string _docBaseUrl = string.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="html">text of title tag to use</param>
		public HtmlBodyBuilder(string html)
		{
			BinaryTransferEncoding = TransferEncoding.Base64;
			_tagHelper = new HtmlTagHelper("base", html);
			if (_tagHelper.StartTags.Count <= 0) return;

			string href;
			if ((href = _tagHelper.GetAttributeValue(_tagHelper.StartTags[0], "href")) != null)
				_docBaseUrl = MakeUri(href);

			// remove if base tag is local file reference
			if (href != null)
				if (href.StartsWith(Uri.UriSchemeFile))
					_tagHelper.ReplaceTag(_tagHelper.StartTags[0], string.Empty);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="html">HTML text</param>
		/// <param name="newTitle">text of title tag to use</param>
		public HtmlBodyBuilder(string html, string newTitle) : this(html)
		{
			_tagHelper.TagName = "title";
			if (_tagHelper.StartTagsTextEndTags.Count <= 0) return;

			// string oldTitle = _tagHelper.GetValueBetweenStartAndEndTag(_tagHelper.StartTagsTextEndTags[0]);
			_tagHelper.ReplaceTag(_tagHelper.StartTagsTextEndTags[0],
			                      _tagHelper.SetValueBetweenStartAndEndTag(_tagHelper.StartTagsTextEndTags[0], newTitle));
		}

		/// <summary>
		/// Gets the list of inline attachments (linked resources) referenced in the HTML text.
		/// </summary>
		public List<FileAttachment> InlineAtt
		{
			get { return _inlineAtt; }
		}


		/// <summary>
		/// Get the HTML representation of the source document
		/// </summary>
		public string DocHtml
		{
			get { return _tagHelper.HtmlText.ToString(); }
		}

		/// <summary>
		/// Get the text representation of the source document
		/// </summary>
		public override string DocText
		{
			get { return _tagHelper.GetPlainText(null).ToString(); }
		}

		/// <summary>
		/// Gets the ready made body part for a mail message.
		/// </summary>
		public override AlternateView Body
		{
			get
			{
				ReplaceImgSrcByCid();
				AlternateView htmlBody =
					AlternateView.CreateAlternateViewFromString(Bugfixer.LimitLineLengthRfc2822(_tagHelper.HtmlText.ToString()),
					                                            CharacterEncoding, MediaTypeNames.Text.Html);
				htmlBody.TransferEncoding = Tools.IsSevenBit(htmlBody.ContentStream)
				                            	? TransferEncoding.SevenBit
				                            	: TextTransferEncoding != TransferEncoding.SevenBit
				                            	  	? TextTransferEncoding
				                            	  	: TransferEncoding.QuotedPrintable;
				
				htmlBody.ContentType.CharSet = CharacterEncoding.HeaderName; // RFC 2045 Section 5.1 - http://www.ietf.org

				// process inline attachment
				foreach (FileAttachment ia in _inlineAtt)
				{
					try
					{
						// Send attachments as part of the multipart/related MIME part,
						// as described in RFC2387
						// Some older clients may need Inline Attachments instead of LinkedResources:
						// RFC2183: 2.1 The Inline Disposition Type
						// A bodypart should be marked `inline' if it is intended to be displayed automatically upon display of the message. Inline
						// bodyparts should be presented in the order in which they occur, subject to the normal semantics of multipart messages.
						var lr = new LinkedResource(ia.Filename, ia.MimeType)
						         	{
						         		ContentId = ia.DisplayName,
						         		TransferEncoding = BinaryTransferEncoding
						         	};
						htmlBody.LinkedResources.Add(lr);
					}
					catch (FileNotFoundException)
					{
						_badInlineFiles.Add(ia.Filename);
					}
					catch (IOException)
					{
						_badInlineFiles.Add(ia.Filename);
					}
				}
				return htmlBody;
			}
		}

		/// <summary>
		/// Gets or sets the Base URL of the HTML document. 
		/// This is used for building path of embedded images.
		/// </summary>
		public string DocBaseUrl
		{
			set { _docBaseUrl = MakeUri(value); }
			get { return _docBaseUrl; }
		}

		/// <summary>
		/// Gets inline files referenced in the HTML text, that were missing or not readable.
		/// </summary>
		public List<string> BadInlineFiles
		{
			get { return _badInlineFiles; }
		}

		/// <summary>
		/// Gets or sets the transfer encoding for any binary content (e.g. Base64)
		/// </summary>
		public TransferEncoding BinaryTransferEncoding { get; set; }

		/// <summary>
		/// Converts the SRC attribute of IMG tags into embedded content ids (cid).
		/// Example: &lt;img src="filename.jpg" /&lt; becomes &lt;img src="cid:unique-cid-jpg" /&lt;
		/// </summary>
		private void ReplaceImgSrcByCid()
		{
			var fileList = new List<string>();

			_tagHelper.TagName = "img";

			foreach (string element in _tagHelper.StartTags)
			{
				string srcAttr = _tagHelper.GetAttributeValue(element, "src");
				if (string.IsNullOrEmpty(srcAttr))
					continue;

				try
				{
					// this will succeed only with local files (at this time, they don't need to exist yet)
					string filename = MakeFullPath(MakeLocalPath(srcAttr));
					if (!fileList.Contains(filename))
					{
						string contentType = Mime.GetContentType(new FileInfo(filename));
						var lr = new LinkedResource(new MemoryStream(new ASCIIEncoding().GetBytes(string.Empty)), contentType);
						_inlineAtt.Add(new FileAttachment(filename, MakeCid(string.Empty, lr.ContentId, new FileInfo(filename).Extension),
						                                  contentType));
						_tagHelper.ReplaceTag(element,
						                      _tagHelper.SetAttributeValue(element, "src",
						                                                   MakeCid("cid:", lr.ContentId, new FileInfo(filename).Extension)));
						fileList.Add(filename);
						lr.Dispose();
					}
				}
				catch
				{
					continue;
				}
			}
		}

		/// <summary>
		/// Makes the content identifier (CID)
		/// </summary>
		/// <param name="prefix">i.e. normally "cid:"</param>
		/// <param name="contentId">unique indentifier</param>
		/// <param name="fileExt">file extension, so that content type can be easily identified. May be string.empty</param>
		/// <returns></returns>
		private static string MakeCid(string prefix, string contentId, string fileExt)
		{
			return prefix + contentId + fileExt.Replace('.', '-');
		}

		/// <summary>
		/// Determines the full path for the given local file
		/// </summary>
		/// <param name="filename">local file name</param>
		/// <returns>Full path for the given local file</returns>
		private string MakeFullPath(string filename)
		{
			string fullpath = Tools.MakeFullPath(MakeLocalPath(_docBaseUrl), filename);
			return fullpath;
		}

		/// <summary>
		/// Determines the local path for the given URI
		/// </summary>
		/// <param name="uri">RFC1738: "file://" [ host | "localhost" ] "/" path</param>
		/// <returns>Local path for the given URI</returns>
		private static string MakeLocalPath(string uri)
		{
			return uri.StartsWith(Uri.UriSchemeFile) ? new Uri(uri).LocalPath : uri;

			/* Note:
			 * In case the filename does not contain the Uri.UriSchemeFile prefix,
			 * it will not be decoded. Then the follwing line should be used.

			 * Pre-process for + sign space formatting since System.Uri doesn't handle it.
			 * "Plus" literals are encoded as %2b normally so this should be safe enough
			 * 
			 * return uri.StartsWith(Uri.UriSchemeFile) ? new Uri(uri).LocalPath : Uri.UnescapeDataString(uri.Replace("+", " "));
			 */
		}

		/// <summary>
		/// Makes a RFC1738 compliant URI, like: "file://" [ host | "localhost" ] "/" path
		/// </summary>
		/// <param name="localPath"></param>
		/// <returns>A RFC1738 compliant URI: "file://" [ host | "localhost" ] "/" path</returns>
		private static string MakeUri(string localPath)
		{
			return ! localPath.StartsWith(Uri.UriSchemeFile)
			       	? string.Format("{0}{1}/{2}", Uri.UriSchemeFile, Uri.SchemeDelimiter, localPath)
			       	: localPath;
		}

		/// <summary>
		/// NOT USED with Infolancers.iMailer.Emailer by default
		/// Strips out HTML tags that are bad when used in user supplied content.
		/// Leaves a|b|br|blockquote|em|h1|h2|h3|h4|h5|h6|hr|i|li|ol|p|u|ul|strong|sub|sup.
		/// </summary>
		/// <param name="input">HTML text source</param>
		/// <returns>HTML with stripped bad tags</returns>
		public static string StripBadHtmlTags(string input)
		{
			// remove comments
			var comments = new Regex("<!--[.]*->");
			string output = comments.Replace(input, string.Empty);

			// strip out comments and doctype
			var docType = new Regex("<!DOCTYPE[.]*>", RegexOptions.IgnoreCase);
			output = docType.Replace(output, string.Empty);

			// strip out most known tags except (a|b|br|blockquote|em|h1|h2|h3|h4|h5|h6|hr|i|li|ol|p|u|ul|strong|sub|sup)
			var badTags =
				new Regex(
					@"< [/]{0,1}(abbr|acronym|address|applet
				|area|base|basefont|bdo|big|body|button|caption|center|cite|code|col
				|colgroup|dd|del|dir|div|dfn|dl|dt|embed|fieldset|font|form|frame
				|frameset|head|html|iframe|img|input|ins|isindex|kbd|label|legend
				|link|map|menu|meta|noframes|noscript|object|optgroup|option
				|param|pre|q|s|samp|script|select|small|span|strike|style|table
				|tbody|td|textarea|tfoot|th|thead|title|tr|tt|var|xmp){1}[.]*>",
					RegexOptions.IgnoreCase);
			return badTags.Replace(output, string.Empty);
		}
	}
}