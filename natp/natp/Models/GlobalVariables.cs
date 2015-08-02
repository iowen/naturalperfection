using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.Models
{
    public class AppResponse
    {
        public AppResponse()
        {
            Errors = new List<string>();
        }
        public string Status;
        public string Data;
        public List<string> Errors;
    }

    public static class GlobalVariables
    {
        private static object _bookAppointmentLock = new object();
        private static object _accountRegisterLock = new object();
        private static object _designerRegisterLock = new object();
        private static object _ClientRegisterLock = new object();

        public static object BookAppointmentLock { get { return _bookAppointmentLock; } }
        public static object AccountRegisterLock { get { return _accountRegisterLock; } }
        public static object DesignerRegisterLock { get { return _designerRegisterLock; } }
        public static object ClientRegisterLock { get { return _ClientRegisterLock; } }


        public static string AppUrl
        {

            get
            {
                string url = "";
                #if DEBUG
                url = "http://teamsavagemma.com/";
                #else
                    url = "http://teamsavagemma.com/";
                #endif
                return url;
            }
        }


    }
}
