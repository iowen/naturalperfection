using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.Models
{
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
    }
}
