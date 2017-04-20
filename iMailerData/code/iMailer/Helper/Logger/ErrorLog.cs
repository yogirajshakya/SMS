using System;
using System.Diagnostics; 
using System.Net;
using System.IO; 
using System.Xml;
namespace Infolancers.iMailer.Logger
{
    public enum EventLogType
    {
        Windows,
        Custom
    }

	/// <summary>
	/// Logger is used for creating a customized error log files or an error can be registered as
	/// a log entry in the Windows Event Log on the administrator's machine.
	/// </summary>
	public class ErrorLog
	{
		protected static string strLogFilePath	= string.Empty;
		private static StreamWriter sw			= null;
		/// <summary>
		/// Setting LogFile path. If the logfile path is null then it will update error info into "LOG\LogFile<date>.log" under
		/// application directory.
		/// </summary>
		public static string LogFilePath
		{
			set
			{
				strLogFilePath	= value;	
			}
			get
			{
				return strLogFilePath;
			}
		}
		/// <summary>
		/// Empty Constructor
		/// </summary>
		public ErrorLog(){}
/// <summary>
/// Write error log entry for window event if the bLogType is true. Otherwise, write the log entry to
/// customized text-based text file
/// </summary>
/// <param name="CreateWindowsEventLog"></param>
/// <param name="oException"></param>
/// <returns>false if the problem persists</returns>
public static bool ErrorRoutine(Exception oException, string strAdditionalInformation)
{
	try
	{
		//Check whether logging is enabled or not
		//Don't process more if the logging 
		if (Helper.EventLogEnabled == false)
			return true;

		//Write to Windows event log
		if (Helper.EventLogType == EventLogType.Windows)
		{
			string EventLogName	= "iMailerLog";

			if (!EventLog.SourceExists(EventLogName))
				EventLog.CreateEventSource(oException.Message, EventLogName);

			// Inserting into event log
			EventLog Log	= new EventLog();
			Log.Source		= EventLogName;
            Log.WriteEntry(string.Format("{0}{1}Additional Information : {2}", oException.Message,System.Environment.NewLine, strAdditionalInformation), EventLogEntryType.Error);
		}
		//Custom text-based event log
		else
		{
            if (true != CustomErrorRoutine(oException, strAdditionalInformation))
				return false;
		}
		return true;
	}catch(Exception)
	{
		return false;
	}
}
/// <summary>
/// If the LogFile path is empty then, it will write the log entry to LogFile<date>.log under application directory.
/// If the LogFile<date>.log is not availble it will create it
/// If the Log File path is not empty but the file is not availble it will create it.
/// </summary>
/// <param name="objException"></param>
/// <returns>false if the problem persists</returns>
private static bool CustomErrorRoutine(Exception objException, string strInformation)
{
	string strPathName	= string.Empty ;
	if (strLogFilePath.Equals(string.Empty))
	{
        //Get Default log file path "LogFile<date>.log"
		strPathName	= GetLogFilePath();
	}
	else
	{

		//If the log file path is not empty but the file is not available it will create it
		if (false == File.Exists(strLogFilePath))
		{
			if (false == CheckDirectory(strLogFilePath))
				return false;

			FileStream fs = new FileStream(strLogFilePath,FileMode.OpenOrCreate, FileAccess.ReadWrite);
			fs.Close();
		}
		strPathName	= strLogFilePath;

	}

	bool bReturn	= true;
	// write the error log to that text file
	if (true != WriteErrorLog(strPathName,objException, strInformation))
	{
		bReturn	= false;
	}
	return bReturn;
}
/// <summary>
/// Write Source,method,date,time,computer,error and stack trace information to the text file
/// </summary>
/// <param name="strPathName"></param>
/// <param name="objException"></param>
/// <returns>false if the problem persists</returns>
private static bool WriteErrorLog(string strPathName,Exception  objException, string strInformation)
{
	bool bReturn		= false;
	string strException	= string.Empty;
    if (strInformation == null) 
        strInformation = string.Empty;
	try
	{
		sw = new StreamWriter(strPathName,true);
		sw.WriteLine("Source		: " + objException.Source.ToString().Trim());  
		sw.WriteLine("Method		: " + objException.TargetSite.Name.ToString());
		sw.WriteLine("Date		: " + DateTime.Now.ToLongTimeString());
		sw.WriteLine("Time		: " + DateTime.Now.ToShortDateString());
		sw.WriteLine("Computer	: " + Dns.GetHostName().ToString()); 
		sw.WriteLine("Error		: " +  objException.Message.ToString().Trim());
		sw.WriteLine("Stack Trace	: " + objException.StackTrace.ToString().Trim());
        sw.WriteLine("Additional Information : " + strInformation);
        sw.WriteLine("^^-------------------------------------------------------------------^^"); 
		sw.Flush();
		sw.Close();
		bReturn	= true;
	}
	catch(Exception)
	{
		bReturn	= false;
	}
	return bReturn;
}
/// <summary>
/// Check the log file in applcation directory. If it is not available, creae it
/// </summary>
/// <returns>Log file path</returns>
private static string GetLogFilePath()
{
	try
	{
		// get the base directory
		string baseDir =  AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath + "\\Log";
        
        if (!System.IO.Directory.Exists(baseDir))
            System.IO.Directory.CreateDirectory(baseDir);
		// search the file below the current directory
		string retFilePath = baseDir + "\\" + string.Format("LogFile-{0}.log", DateTime.Today.ToString("yyyyMMMdd"));

		// if exists, return the path
		if (File.Exists(retFilePath) == true)
			return retFilePath;
			//create a text file
		else
		{
			if (false == CheckDirectory(retFilePath))
				return  string.Empty;

			FileStream fs = new FileStream(retFilePath,FileMode.OpenOrCreate, FileAccess.ReadWrite);
			fs.Close();
		}

		return retFilePath;
	}
	catch(Exception)
	{
		return string.Empty; 
	}
}
/// <summary>
/// Create a directory if not exists
/// </summary>
/// <param name="strLogPath"></param>
/// <returns></returns>
private static bool CheckDirectory(string strLogPath)
{
	try
	{
		int nFindSlashPos		= strLogPath.Trim().LastIndexOf("\\"); 
		string strDirectoryname	= strLogPath.Trim().Substring(0,nFindSlashPos);

		if (false == Directory.Exists(strDirectoryname))
			Directory.CreateDirectory(strDirectoryname); 

		return true;
	}
	catch(Exception)
	{
		return false;

	}
}
	
		private static string GetApplicationPath()
		{
			try
			{
				string strBaseDirectory	= AppDomain.CurrentDomain.BaseDirectory.ToString();
				int nFirstSlashPos		= strBaseDirectory.LastIndexOf("\\");
				string strTemp			= string.Empty ;

				if (0 < nFirstSlashPos)
					strTemp			= strBaseDirectory.Substring(0,nFirstSlashPos);

				int nSecondSlashPos		= strTemp.LastIndexOf("\\");
				string strTempAppPath	= string.Empty;
				if (0 < nSecondSlashPos)
					strTempAppPath	= strTemp.Substring(0,nSecondSlashPos);

				string strAppPath=strTempAppPath.Replace("bin","");
				return strAppPath;
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}
}
}
