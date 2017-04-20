using System;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Fixes for bugs in System.Net.Mail.
	/// </summary>
	public static class Bugfixer
	{
		static Bugfixer()
		{
			IsSwitchedOn = true;
		}

		/// <summary>
		/// Gets or sets, whether Bugfixer is active (true), or not.
		/// </summary>
		public static bool IsSwitchedOn { get; set; }


		/// <summary>
		/// Corrects 2 bugs in System.Net.Mail of .NET 2.0 including .NET 4.0:
		/// 1. Attachment.ContentDisposition.FileName must be encoded, if it contains other
		/// characters than 7bit or spaces. System.Net.Mail does not encode filenames, 
		/// but throws a FormatException 'MailHeaderFieldInvalidCharacter', so that such filenames 
		/// cannot be used at all.
		/// 2. System.Net.Mail tries to wrap long names of the Content-Type Name across several lines, 
		/// but fails by inserting illegal CrLf sequences and characters (5c 0d 5c 0d 0a).
		/// </summary>
		/// <param name="att">Attachment</param>
		/// <param name="encoding">Encoding</param>
		public static void CorrectAttachmentFileNameEncoding(Attachment att, Encoding encoding)
		{
			if (!IsSwitchedOn) return;

			if (Environment.Version.Major < 2 || Environment.Version.Major > 4)
				return;

			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}

			if (att.NameEncoding == null)
			{
				att.NameEncoding = encoding;
			}

			// Correct filename encoding only if not 7bit or containing blank character
			if (Tools.IsSevenBit(att.ContentDisposition.FileName) && !att.ContentDisposition.FileName.Contains(" "))
				return;

			if (string.IsNullOrEmpty(att.Name))
				return;

			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

			object mimePart = GetMimePart(Attachment.CreateAttachmentFromString("dummy", att.ContentType));

			// GetMethod("EncodeHeaderValue", flags) is ambiguous starting with .NET 4.0, so we supply argument types now
			var encodedName =
				mimePart.GetType().BaseType.GetMethod("EncodeHeaderValue", flags, null,
				                                      new[] {typeof (string), typeof (Encoding), typeof (Boolean)}, null).Invoke(
				                                      	mimePart, new object[] {att.Name, encoding, false}) as string;

			// Correct the file name in content-disposition header
			if (string.IsNullOrEmpty(encodedName)) return;

			/* Note for "Content-Type/name" and "Content-Disposition/filename": 
			* 
			* Both MUST to be encoded (e.g. UTF8) and MUST be broken into several lines according to RFC.
			* Content-Disposition/filename: works perfect with the following workaround.
			* content name: Long filenames will contain 1 CrLf too much after each line break, which leads to unreadable mails.
			* 
			* So here we remove the CrLf completely, which will lead to proper filenames but at the same time
			* the content name may become too long (RFC violation). Assuming that emails will rarely contain extremly
			* long filenames we go for the second option. But still: messy...
			*/

			// encode spaces with underscore (RFC compliant) and remove CrLf
			string encodedNameWithCorrections = encodedName.Replace(" ", "_").Replace("\r\n", string.Empty);

			att.ContentDisposition.FileName = encodedNameWithCorrections;
			att.GetType().GetField("name", flags).SetValue(att, encodedNameWithCorrections);
		}

		/// <summary>
		/// Corrects a bug in System.Net.Mail of .NET 2.0 including .NET 4.0:
		/// RFC2822 compliant: Each line of characters MUST be no more than 998 characters, 
		/// and SHOULD be no more than 78 characters, excluding the CRLF.
		/// </summary>
		/// <param name="input">Text to be processed</param>
		/// <returns>Returns a string with a maximum line length of 998 excluding CRLF.</returns>
		public static string LimitLineLengthRfc2822(string input)
		{
			return !IsSwitchedOn ? input : Tools.WrapLines(input, 998);
		}

		#region *** Helper Methods ***

		private static object GetMimePart(AttachmentBase att)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
			return typeof (AttachmentBase).GetField("part", flags).GetValue(att);
		}

		private static object GetHeaderCollection(AttachmentBase att)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

			// Get System.Net.Mime.MimePart:
			object mimePart = GetMimePart(att);

			// Get System.Net.Mime.MimeBasePart.HeaderCollection:
			return mimePart.GetType().BaseType.GetField("headers", flags).GetValue(mimePart);
		}
# endregion

		#region *** Methods currently not in use ***

		private static string GetHeaderCollectionValue(AttachmentBase att, string key)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
			object headerCollection = GetHeaderCollection(att);
			return
				headerCollection.GetType().GetMethod("Get", flags, null, new[] {typeof (string)}, null).Invoke(headerCollection,
				                                                                                               new object[] {key})
				as string;
		}

		private static void SetHeaderCollectionItem(AttachmentBase att, string key, string value)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
			object headerCollection = GetHeaderCollection(att);
			headerCollection.GetType().GetMethod("Set", flags).Invoke(headerCollection, new object[] {key, value});
		}


		/// <summary>
		/// Make header info name settable by user.
		/// This only works with .NET 4.0
		/// </summary>
		private static void MakeHeaderNameSettable(string headerName)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

			Assembly asm = Assembly.GetAssembly(typeof (MailAddress));
			Type mailHeaderInfoType = asm.GetType("System.Net.Mail.MailHeaderInfo", false);

			// Type headerInfoType = asm.GetType("System.Net.Mail.MailHeaderInfo+HeaderInfo", false);
			// Type mailHeaderIDtype = asm.GetType("System.Net.Mail.MailHeaderID", false);
			/*
             *  .NET 4.0 System.Net.MailHeaderInfo:
             * 
                internal static class MailHeaderInfo
                {
                    private static readonly Dictionary<string, int> m_HeaderDictionary;
                    private static readonly HeaderInfo[] m_HeaderInfo;

                    static MailHeaderInfo();
                    internal static MailHeaderID GetID(string name);
                    internal static string GetString(MailHeaderID id);
                    internal static bool IsMatch(string name, MailHeaderID header);
                    internal static bool IsSingleton(string name);
                    internal static bool IsUserSettable(string name);
                    internal static bool IsWellKnown(string name);
                    internal static string NormalizeCase(string name);

                    [StructLayout(LayoutKind.Sequential)]
                    private struct HeaderInfo
                    {
                        public readonly string NormalizedName;
                        public readonly bool IsSingleton;
                        public readonly MailHeaderID ID;
                        public readonly bool IsUserSettable;
                        public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable);
                    }
                }
            */

			// Invoke System.Net.Mail.MailHeaderInfo.GetString(MailHeaderID id)
			// in order to make sure that privat fields will be set by its constructor
			mailHeaderInfoType.GetMethod("GetString", flags).Invoke(null, new object[] {2});

			// get array HeaderInfo[] m_HeaderInfo = new HeaderInfo[] { new HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false), ... }
			// HeaderInfo is a structure with: string NormalizedName, bool IsSingleton, (enum) MailHeaderID ID. bool IsUserSettable;
			var headerInfoArray = (Array) mailHeaderInfoType.GetField("m_HeaderInfo", flags).GetValue(null);
			int index = headerInfoArray.Cast<object>().TakeWhile(headerInfo => headerInfo.GetType().GetField("NormalizedName").GetValue(headerInfo) as string != headerName).Count();

			if (index > headerInfoArray.Length - 1)
				return;

			object subjectHeaderInfo = headerInfoArray.GetValue(index);
			subjectHeaderInfo.GetType().GetField("IsUserSettable").SetValue(subjectHeaderInfo, true);
			headerInfoArray.SetValue(subjectHeaderInfo, index);
		}

		#endregion
	}
}