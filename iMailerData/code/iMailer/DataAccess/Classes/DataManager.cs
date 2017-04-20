using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

public enum DataProviders
{
    Access,
    Excel,
    SQLServer,
    Oracle
}


namespace Infolancers.iMailer.DataAccess
{
    class DataManager
    {
        private static Infolancers.iMailer.DataAccess.IDataProvider _provider;
        
        public static Infolancers.iMailer.DataAccess.IDataProvider CurrentDataProvider
        { 
            get
            {
                if (_provider == null)
                {
                    _provider = CreateDataProvider(DataProviders.Access);
                }
                return _provider;
             }
        }

        private static Infolancers.iMailer.DataAccess.IDataProvider CreateDataProvider(DataProviders type)
        {
            switch (type)
            {
                case DataProviders.Access:
                    return new AccessDataProvider(System.Configuration.ConfigurationManager.ConnectionStrings["iMailerAccessConnectionString"].ConnectionString);
            }
            return null;
        }
        
    }
}
