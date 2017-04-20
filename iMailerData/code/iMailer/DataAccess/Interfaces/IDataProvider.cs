using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Infolancers.iMailer.DataAccess
{
    interface  IDataProvider
    {
        DataTable GetMapping();
        DataTable GetMapping(bool Refresh, bool IncludeSystemFields);
        int SaveMapping(DataTable dt);
        ArrayList GetPreferredFields();
        Hashtable GetMappingFields();
        Hashtable GetMappingFieldsVisibleIndex();
        ArrayList GetMappingFieldsArray();
        Hashtable GetAlternateValue();

        DataTable GetTemplate(string Condition);
        int SaveTemplate(DataTable dt);

        DataTable GetCustomers(string Condition);
        int SaveCustomers(DataTable dt);

        DataTable GetContactList(string Condition);
        int SaveContactList(DataTable dt);

        DataTable GetSentEmail(string Condition);
        int SaveSentEmail(DataTable dt);

        DataTable GetScheduleData(DateTime ScheduleDate);
        DataTable GetScheduleData(string Condition);
        int SaveScheduleData(DataTable dt);

        DataTable GetApplicationCodes(string ApplicationType);
    }
}
