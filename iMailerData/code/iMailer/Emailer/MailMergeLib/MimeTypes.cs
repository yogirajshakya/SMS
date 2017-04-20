using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace Infolancers.iMailer.Emailer
{
	/// <summary>
	/// Class to determine Mime types of a file.
	/// </summary>
	public static class Mime
	{
		/// <summary>
		/// Determines mime type of a file first by searching the Windows Registry,
		/// and if not sucessful, calls Win32-API FindMimeFromData in urlmon.dll, finally uses its own list.
		/// </summary>
		public static string GetContentType(FileInfo fileinfo)
		{
			const string TypeUnknown = "application/octet-stream";
			string mime = string.Empty;

			if (fileinfo == null)
				throw new NullReferenceException("fileinfo must not be null.");

			try
			{
				var dummy = new RegistryPermission(RegistryPermissionAccess.Read, @"\\HKEY_CLASSES_ROOT");
				RegistryKey rkContentTypes = Registry.ClassesRoot.OpenSubKey(fileinfo.Extension);
				if (rkContentTypes != null)
				{
					object key = rkContentTypes.GetValue("Content Type");
					// there may be entries for this extension, that do not have a content type in registry
					mime = key != null ? key.ToString() : string.Empty;
				}

				if (string.IsNullOrEmpty(mime))
				{
					mime = GetContentTypeByWinApi(fileinfo);
				}
			}
			catch
			{
				// error reading mime type from registry or using Windows API
			}

			// if no mime type found up to now, finally use our own list
			if (string.IsNullOrEmpty(mime))
			{
				mime = MimeTypes.GetMimeType(fileinfo.Name);
			}
			return string.IsNullOrEmpty(mime) ? TypeUnknown : mime;
		}


		/// <summary>
		/// External Windows API function for method GetContentType
		/// </summary>
		[DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
		private static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
		                                           [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1,
		                                           	SizeParamIndex = 3)] byte[] pBuffer, int cbSize,
		                                           [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
		                                           int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);

		/// <summary>
		/// Ensures that file exists and retrieves the content type 
		/// </summary>
		/// <remarks>
		/// Sources:
		/// Managed code:
		/// http://www.pinvoke.net/default.aspx/urlmon.FindMimeFromData
		/// MIME Type Detection in Internet Explorer:
		/// http://msdn.microsoft.com/workshop/networking/moniker/overview/appendix_a.asp
		/// </remarks>
		/// <param name="fileinfo"></param>
		/// <returns>Returns mime type, for instance "image/jpeg"</returns>
		private static string GetContentTypeByWinApi(FileInfo fileinfo)
		{
			IntPtr mimeOut;
			if (!fileinfo.Exists)
				throw new FileNotFoundException(fileinfo.FullName + " not found");

			var maxContent = (int) fileinfo.Length;
			if (maxContent > 4096) maxContent = 4096;
			FileStream fs = fileinfo.OpenRead();

			var buffer = new byte[maxContent];
			fs.Read(buffer, 0, maxContent);
			fs.Close();

			int result = FindMimeFromData(IntPtr.Zero, fileinfo.FullName, buffer, maxContent, null, 0, out mimeOut, 0);

			if (result != 0)
				throw Marshal.GetExceptionForHR(result);

			string mime = Marshal.PtrToStringUni(mimeOut);
			Marshal.FreeCoTaskMem(mimeOut);

			return mime;
		}

		#region Nested type: MimeTypes

		/// <summary>
		/// Class to determine mime types by the file extension
		/// </summary>
		/// <remarks>
		/// source: Mono 1.2.3.1 AttachmentBase.MimeTypes
		/// </remarks>
		private class MimeTypes
		{
			private static readonly Dictionary<string, string> mimeTypes;

			static MimeTypes()
			{
				mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
				            	{
				            		{"3dm", "x-world/x-3dmf"},
				            		{"3dmf", "x-world/x-3dmf"},
				            		{"aab", "application/x-authorware-bin"},
				            		{"aam", "application/x-authorware-map"},
				            		{"aas", "application/x-authorware-seg"},
				            		{"abc", "text/vnd.abc"},
				            		{"acgi", "text/html"},
				            		{"afl", "video/animaflex"},
				            		{"ai", "application/postscript"},
				            		{"aif", "audio/aiff"},
				            		{"aifc", "audio/aiff"},
				            		{"aiff", "audio/aiff"},
				            		{"aim", "application/x-aim"},
				            		{"aip", "text/x-audiosoft-intra"},
				            		{"ani", "application/x-navi-animation"},
				            		{"aos", "application/x-nokia-9000-communicator-add-on-software"},
				            		{"aps", "application/mime"},
				            		{"art", "image/x-jg"},
				            		{"asf", "video/x-ms-asf"},
				            		{"asm", "text/x-asm"},
				            		{"asp", "text/asp"},
				            		{"asx", "application/x-mplayer2"},
				            		{"au", "audio/x-au"},
				            		{"avi", "video/avi"},
				            		{"avs", "video/avs-video"},
				            		{"bcpio", "application/x-bcpio"},
				            		{"bm", "image/bmp"},
				            		{"bmp", "image/bmp"},
				            		{"boo", "application/book"},
				            		{"book", "application/book"},
				            		{"boz", "application/x-bzip2"},
				            		{"bsh", "application/x-bsh"},
				            		{"bz", "application/x-bzip"},
				            		{"bz2", "application/x-bzip2"},
				            		{"c", "text/plain"},
				            		{"c++", "text/plain"},
				            		{"cat", "application/vnd.ms-pki.seccat"},
				            		{"cc", "text/plain"},
				            		{"ccad", "application/clariscad"},
				            		{"cco", "application/x-cocoa"},
				            		{"cdf", "application/cdf"},
				            		{"cer", "application/pkix-cert"},
				            		{"cha", "application/x-chat"},
				            		{"chat", "application/x-chat"},
				            		{"class", "application/java"},
				            		{"conf", "text/plain"},
				            		{"cpio", "application/x-cpio"},
				            		{"cpp", "text/plain"},
				            		{"cpt", "application/x-cpt"},
				            		{"crl", "application/pkix-crl"},
				            		{"crt", "application/pkix-cert"},
				            		{"csh", "application/x-csh"},
				            		{"css", "text/css"},
				            		{"cxx", "text/plain"},
				            		{"dcr", "application/x-director"},
				            		{"deepv", "application/x-deepv"},
				            		{"def", "text/plain"},
				            		{"der", "application/x-x509-ca-cert"},
				            		{"dif", "video/x-dv"},
				            		{"dir", "application/x-director"},
				            		{"dl", "video/dl"},
				            		{"doc", "application/msword"},
				            		{"dot", "application/msword"},
				            		{"dp", "application/commonground"},
				            		{"drw", "application/drafting"},
				            		{"dv", "video/x-dv"},
				            		{"dvi", "application/x-dvi"},
				            		{"dwf", "drawing/x-dwf (old)"},
				            		{"dwg", "application/acad"},
				            		{"dxf", "application/dxf"},
				            		{"dxr", "application/x-director"},
				            		{"el", "text/x-script.elisp"},
				            		{"elc", "application/x-elc"},
				            		{"eps", "application/postscript"},
				            		{"es", "application/x-esrehber"},
				            		{"etx", "text/x-setext"},
				            		{"evy", "application/envoy"},
				            		{"f", "text/plain"},
				            		{"f77", "text/plain"},
				            		{"f90", "text/plain"},
				            		{"fdf", "application/vnd.fdf"},
				            		{"fif", "image/fif"},
				            		{"fli", "video/fli"},
				            		{"flo", "image/florian"},
				            		{"flx", "text/vnd.fmi.flexstor"},
				            		{"fmf", "video/x-atomic3d-feature"},
				            		{"for", "text/plain"},
				            		{"fpx", "image/vnd.fpx"},
				            		{"frl", "application/freeloader"},
				            		{"funk", "audio/make"},
				            		{"g", "text/plain"},
				            		{"g3", "image/g3fax"},
				            		{"gif", "image/gif"},
				            		{"gl", "video/gl"},
				            		{"gsd", "audio/x-gsm"},
				            		{"gsm", "audio/x-gsm"},
				            		{"gsp", "application/x-gsp"},
				            		{"gss", "application/x-gss"},
				            		{"gtar", "application/x-gtar"},
				            		{"gz", "application/x-gzip"},
				            		{"gzip", "application/x-gzip"},
				            		{"h", "text/plain"},
				            		{"hdf", "application/x-hdf"},
				            		{"help", "application/x-helpfile"},
				            		{"hgl", "application/vnd.hp-HPGL"},
				            		{"hh", "text/plain"},
				            		{"hlb", "text/x-script"},
				            		{"hlp", "application/x-helpfile"},
				            		{"hpg", "application/vnd.hp-HPGL"},
				            		{"hpgl", "application/vnd.hp-HPGL"},
				            		{"hqx", "application/binhex"},
				            		{"hta", "application/hta"},
				            		{"htc", "text/x-component"},
				            		{"htm", "text/html"},
				            		{"html", "text/html"},
				            		{"htmls", "text/html"},
				            		{"htt", "text/webviewhtml"},
				            		{"htx", "text/html"},
				            		{"ice", "x-conference/x-cooltalk"},
				            		{"ico", "image/x-icon"},
				            		{"idc", "text/plain"},
				            		{"ief", "image/ief"},
				            		{"iefs", "image/ief"},
				            		{"iges", "application/iges"},
				            		{"igs", "application/iges"},
				            		{"ima", "application/x-ima"},
				            		{"imap", "application/x-httpd-imap"},
				            		{"inf", "application/inf"},
				            		{"ins", "application/x-internett-signup"},
				            		{"ip", "application/x-ip2"},
				            		{"isu", "video/x-isvideo"},
				            		{"it", "audio/it"},
				            		{"iv", "application/x-inventor"},
				            		{"ivr", "i-world/i-vrml"},
				            		{"ivy", "application/x-livescreen"},
				            		{"jam", "audio/x-jam"},
				            		{"jav", "text/plain"},
				            		{"java", "text/plain"},
				            		{"jcm", "application/x-java-commerce"},
				            		{"jfif", "image/jpeg"},
				            		{"jfif-tbnl", "image/jpeg"},
				            		{"jpe", "image/jpeg"},
				            		{"jpeg", "image/jpeg"},
				            		{"jpg", "image/jpeg"},
				            		{"jps", "image/x-jps"},
				            		{"js", "application/x-javascript"},
				            		{"jut", "image/jutvision"},
				            		{"kar", "audio/midi"},
				            		{"ksh", "text/x-script.ksh"},
				            		{"la", "audio/nspaudio"},
				            		{"lam", "audio/x-liveaudio"},
				            		{"latex", "application/x-latex"},
				            		{"list", "text/plain"},
				            		{"lma", "audio/nspaudio"},
				            		{"log", "text/plain"},
				            		{"lsp", "application/x-lisp"},
				            		{"lst", "text/plain"},
				            		{"lsx", "text/x-la-asf"},
				            		{"ltx", "application/x-latex"},
				            		{"m", "text/plain"},
				            		{"m1v", "video/mpeg"},
				            		{"m2a", "audio/mpeg"},
				            		{"m2v", "video/mpeg"},
				            		{"m3u", "audio/x-mpequrl"},
				            		{"man", "application/x-troff-man"},
				            		{"map", "application/x-navimap"},
				            		{"mar", "text/plain"},
				            		{"mbd", "application/mbedlet"},
				            		{"mc$", "application/x-magic-cap-package-1.0"},
				            		{"mcd", "application/mcad"},
				            		{"mcf", "image/vasa"},
				            		{"mcp", "application/netmc"},
				            		{"me", "application/x-troff-me"},
				            		{"mht", "message/rfc822"},
				            		{"mhtml", "message/rfc822"},
				            		{"mid", "audio/midi"},
				            		{"midi", "audio/midi"},
				            		{"mif", "application/x-mif"},
				            		{"mime", "message/rfc822"},
				            		{"mjf", "audio/x-vnd.AudioExplosion.MjuiceMediaFile"},
				            		{"mjpg", "video/x-motion-jpeg"},
				            		{"mm", "application/base64"},
				            		{"mme", "application/base64"},
				            		{"mod", "audio/mod"},
				            		{"moov", "video/quicktime"},
				            		{"mov", "video/quicktime"},
				            		{"movie", "video/x-sgi-movie"},
				            		{"mp2", "video/mpeg"},
				            		{"mp3", "audio/mpeg3"},
				            		{"mpa", "audio/mpeg"},
				            		{"mpc", "application/x-project"},
				            		{"mpe", "video/mpeg"},
				            		{"mpeg", "video/mpeg"},
				            		{"mpg", "video/mpeg"},
				            		{"mpga", "audio/mpeg"},
				            		{"mpp", "application/vnd.ms-project"},
				            		{"mpt", "application/x-project"},
				            		{"mpv", "application/x-project"},
				            		{"mpx", "application/x-project"},
				            		{"mrc", "application/marc"},
				            		{"ms", "application/x-troff-ms"},
				            		{"mv", "video/x-sgi-movie"},
				            		{"my", "audio/make"},
				            		{"mzz", "application/x-vnd.AudioExplosion.mzz"},
				            		{"nap", "image/naplps"},
				            		{"naplps", "image/naplps"},
				            		{"nc", "application/x-netcdf"},
				            		{"ncm", "application/vnd.nokia.configuration-message"},
				            		{"nif", "image/x-niff"},
				            		{"niff", "image/x-niff"},
				            		{"nix", "application/x-mix-transfer"},
				            		{"nsc", "application/x-conference"},
				            		{"nvd", "application/x-navidoc"},
				            		{"oda", "application/oda"},
				            		{"omc", "application/x-omc"},
				            		{"omcd", "application/x-omcdatamaker"},
				            		{"omcr", "application/x-omcregerator"},
				            		{"p", "text/x-pascal"},
				            		{"p10", "application/pkcs10"},
				            		{"p12", "application/pkcs-12"},
				            		{"p7a", "application/x-pkcs7-signature"},
				            		{"p7c", "application/pkcs7-mime"},
				            		{"p7m", "application/pkcs7-mime"},
				            		{"p7r", "application/x-pkcs7-certreqresp"},
				            		{"p7s", "application/pkcs7-signature"},
				            		{"part", "application/pro_eng"},
				            		{"pas", "text/pascal"},
				            		{"pbm", "image/x-portable-bitmap"},
				            		{"pcl", "application/x-pcl"},
				            		{"pct", "image/x-pict"},
				            		{"pcx", "image/x-pcx"},
				            		{"pdb", "chemical/x-pdb"},
				            		{"pdf", "application/pdf"},
				            		{"pfunk", "audio/make"},
				            		{"pgm", "image/x-portable-graymap"},
				            		{"pic", "image/pict"},
				            		{"pict", "image/pict"},
				            		{"pkg", "application/x-newton-compatible-pkg"},
				            		{"pko", "application/vnd.ms-pki.pko"},
				            		{"pl", "text/plain"},
				            		{"plx", "application/x-PiXCLscript"},
				            		{"pm", "image/x-xpixmap"},
				            		{"pm4", "application/x-pagemaker"},
				            		{"pm5", "application/x-pagemaker"},
				            		{"png", "image/png"},
				            		{"pnm", "application/x-portable-anymap"},
				            		{"pot", "application/mspowerpoint"},
				            		{"pov", "model/x-pov"},
				            		{"ppa", "application/vnd.ms-powerpoint"},
				            		{"ppm", "image/x-portable-pixmap"},
				            		{"pps", "application/mspowerpoint"},
				            		{"ppt", "application/mspowerpoint"},
				            		{"ppz", "application/mspowerpoint"},
				            		{"pre", "application/x-freelance"},
				            		{"prt", "application/pro_eng"},
				            		{"ps", "application/postscript"},
				            		{"pvu", "paleovu/x-pv"},
				            		{"pwz", "application/vnd.ms-powerpoint"},
				            		{"py", "text/x-script.phyton"},
				            		{"pyc", "applicaiton/x-bytecode.python"},
				            		{"qcp", "audio/vnd.qcelp"},
				            		{"qd3", "x-world/x-3dmf"},
				            		{"qd3d", "x-world/x-3dmf"},
				            		{"qif", "image/x-quicktime"},
				            		{"qt", "video/quicktime"},
				            		{"qtc", "video/x-qtc"},
				            		{"qti", "image/x-quicktime"},
				            		{"qtif", "image/x-quicktime"},
				            		{"ra", "audio/x-pn-realaudio"},
				            		{"ram", "audio/x-pn-realaudio"},
				            		{"ras", "application/x-cmu-raster"},
				            		{"rast", "image/cmu-raster"},
				            		{"rexx", "text/x-script.rexx"},
				            		{"rf", "image/vnd.rn-realflash"},
				            		{"rgb", "image/x-rgb"},
				            		{"rm", "application/vnd.rn-realmedia"},
				            		{"rmi", "audio/mid"},
				            		{"rmm", "audio/x-pn-realaudio"},
				            		{"rmp", "audio/x-pn-realaudio"},
				            		{"rng", "application/ringing-tones"},
				            		{"rnx", "application/vnd.rn-realplayer"},
				            		{"roff", "application/x-troff"},
				            		{"rp", "image/vnd.rn-realpix"},
				            		{"rpm", "audio/x-pn-realaudio-plugin"},
				            		{"rss", "text/xml"},
				            		{"rt", "text/richtext"},
				            		{"rtf", "text/richtext"},
				            		{"rtx", "text/richtext"},
				            		{"rv", "video/vnd.rn-realvideo"},
				            		{"s", "text/x-asm"},
				            		{"s3m", "audio/s3m"},
				            		{"sbk", "application/x-tbook"},
				            		{"scm", "application/x-lotusscreencam"},
				            		{"sdml", "text/plain"},
				            		{"sdp", "application/sdp"},
				            		{"sdr", "application/sounder"},
				            		{"sea", "application/sea"},
				            		{"set", "application/set"},
				            		{"sgm", "text/sgml"},
				            		{"sgml", "text/sgml"},
				            		{"sh", "text/x-script.sh"},
				            		{"shar", "application/x-bsh"},
				            		{"shtml", "text/html"},
				            		{"sid", "audio/x-psid"},
				            		{"sit", "application/x-sit"},
				            		{"skd", "application/x-koan"},
				            		{"skm", "application/x-koan"},
				            		{"skp", "application/x-koan"},
				            		{"skt", "application/x-koan"},
				            		{"sl", "application/x-seelogo"},
				            		{"smi", "application/smil"},
				            		{"smil", "application/smil"},
				            		{"snd", "audio/basic"},
				            		{"sol", "application/solids"},
				            		{"spc", "application/x-pkcs7-certificates"},
				            		{"spl", "application/futuresplash"},
				            		{"spr", "application/x-sprite"},
				            		{"sprite", "application/x-sprite"},
				            		{"src", "application/x-wais-source"},
				            		{"ssi", "text/x-server-parsed-html"},
				            		{"ssm", "application/streamingmedia"},
				            		{"sst", "application/vnd.ms-pki.certstore"},
				            		{"step", "application/step"},
				            		{"stl", "application/sla"},
				            		{"stp", "application/step"},
				            		{"sv4cpio", "application/x-sv4cpio"},
				            		{"sv4crc", "application/x-sv4crc"},
				            		{"svf", "image/x-dwg"},
				            		{"svr", "application/x-world"},
				            		{"swf", "application/x-shockwave-flash"},
				            		{"t", "application/x-troff"},
				            		{"talk", "text/x-speech"},
				            		{"tar", "application/x-tar"},
				            		{"tbk", "application/toolbook"},
				            		{"tcl", "text/x-script.tcl"},
				            		{"tcsh", "text/x-script.tcsh"},
				            		{"tex", "application/x-tex"},
				            		{"texi", "application/x-texinfo"},
				            		{"texinfo", "application/x-texinfo"},
				            		{"text", "text/plain"},
				            		{"tgz", "application/x-compressed"},
				            		{"tif", "image/tiff"},
				            		{"tiff", "image/tiff"},
				            		{"tr", "application/x-troff"},
				            		{"tsi", "audio/tsp-audio"},
				            		{"tsp", "audio/tsplayer"},
				            		{"tsv", "text/tab-separated-values"},
				            		{"turbot", "image/florian"},
				            		{"txt", "text/plain"},
				            		{"uil", "text/x-uil"},
				            		{"uni", "text/uri-list"},
				            		{"unis", "text/uri-list"},
				            		{"unv", "application/i-deas"},
				            		{"uri", "text/uri-list"},
				            		{"uris", "text/uri-list"},
				            		{"ustar", "multipart/x-ustar"},
				            		{"uu", "text/x-uuencode"},
				            		{"uue", "text/x-uuencode"},
				            		{"vcd", "application/x-cdlink"},
				            		{"vcs", "text/x-vCalendar"},
				            		{"vda", "application/vda"},
				            		{"vdo", "video/vdo"},
				            		{"vew", "application/groupwise"},
				            		{"viv", "video/vivo"},
				            		{"vivo", "video/vivo"},
				            		{"vmd", "application/vocaltec-media-desc"},
				            		{"vmf", "application/vocaltec-media-file"},
				            		{"voc", "audio/voc"},
				            		{"vos", "video/vosaic"},
				            		{"vox", "audio/voxware"},
				            		{"vqe", "audio/x-twinvq-plugin"},
				            		{"vqf", "audio/x-twinvq"},
				            		{"vql", "audio/x-twinvq-plugin"},
				            		{"vrml", "application/x-vrml"},
				            		{"vrt", "x-world/x-vrt"},
				            		{"vsd", "application/x-visio"},
				            		{"vst", "application/x-visio"},
				            		{"vsw", "application/x-visio"},
				            		{"w60", "application/wordperfect6.0"},
				            		{"w61", "application/wordperfect6.1"},
				            		{"w6w", "application/msword"},
				            		{"wav", "audio/wav"},
				            		{"wb1", "application/x-qpro"},
				            		{"wbmp", "image/vnd.wap.wbmp"},
				            		{"web", "application/vnd.xara"},
				            		{"wiz", "application/msword"},
				            		{"wk1", "application/x-123"},
				            		{"wmf", "windows/metafile"},
				            		{"wml", "text/vnd.wap.wml"},
				            		{"wmlc", "application/vnd.wap.wmlc"},
				            		{"wmls", "text/vnd.wap.wmlscript"},
				            		{"wmlsc", "application/vnd.wap.wmlscriptc"},
				            		{"word", "application/msword"},
				            		{"wp", "application/wordperfect"},
				            		{"wp5", "application/wordperfect"},
				            		{"wp6", "application/wordperfect"},
				            		{"wpd", "application/wordperfect"},
				            		{"wq1", "application/x-lotus"},
				            		{"wri", "application/mswrite"},
				            		{"wrl", "application/x-world"},
				            		{"wrz", "model/vrml"},
				            		{"wsc", "text/scriplet"},
				            		{"wsrc", "application/x-wais-source"},
				            		{"wtk", "application/x-wintalk"},
				            		{"xbm", "image/x-xbitmap"},
				            		{"xdr", "video/x-amt-demorun"},
				            		{"xgz", "xgl/drawing"},
				            		{"xif", "image/vnd.xiff"},
				            		{"xl", "application/excel"},
				            		{"xla", "application/excel"},
				            		{"xlb", "application/excel"},
				            		{"xlc", "application/excel"},
				            		{"xld", "application/excel"},
				            		{"xlk", "application/excel"},
				            		{"xll", "application/excel"},
				            		{"xlm", "application/excel"},
				            		{"xls", "application/excel"},
				            		{"xlt", "application/excel"},
				            		{"xlv", "application/excel"},
				            		{"xlw", "application/excel"},
				            		{"xm", "audio/xm"},
				            		{"xml", "text/xml"},
				            		{"xmz", "xgl/movie"},
				            		{"xpix", "application/x-vnd.ls-xpix"},
				            		{"xpm", "image/xpm"},
				            		{"x-png", "image/png"},
				            		{"xsr", "video/x-amt-showrun"},
				            		{"xwd", "image/x-xwd"},
				            		{"xyz", "chemical/x-pdb"},
				            		{"z", "application/x-compressed"},
				            		{"zip", "application/zip"},
				            		{"zsh", "text/x-script.zsh"}
				            	};
			}

			/// <summary>
			/// Returns the mime type of a file by using a built-in dictionary
			/// </summary>
			/// <param name="fileName">name of an existing or non-existing file</param>
			/// <returns>Returns the mime type of a file by using a built-in dictionary</returns>
			public static string GetMimeType(string fileName)
			{
				string result = null;
				int dot = fileName.LastIndexOf('.');

				if (dot != -1 && fileName.Length > dot + 1 && mimeTypes.ContainsKey(fileName.Substring(dot + 1)))
					result = mimeTypes[fileName.Substring(dot + 1)];

				return result ?? "application/octet-stream";
			}
		}

		#endregion
	}
}