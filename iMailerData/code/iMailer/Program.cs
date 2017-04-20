using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Infolancers.iMailer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
         
            catch (Exception e)
            {
                Helper.ShowMessage(e.Message, "Error", MessageType.Error);
                Logger.ErrorLog.ErrorRoutine(e, "");
            }
        }
    }
}
