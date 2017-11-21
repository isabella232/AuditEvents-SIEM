using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprenda.AuditEventForwarder.Syslog
{
    class ReportCard
    {
        private List<string> infoMessages;
        private List<string> warningMessages;
        private List<string> errorMessages;


        public List<string> ErrorMessages
        {
            get { return errorMessages ?? (errorMessages = new List<string>()); }
            set { errorMessages = value; }
        }
        public List<string> WarningMessages
        {
            get { return warningMessages ?? (warningMessages = new List<string>()); }
            set { warningMessages = value; }
        }

        public List<string> InfoMessages
        {
            get
            {
                return infoMessages ?? (infoMessages = new List<string>());
            }
            set { infoMessages = value; }
        }
    }
}
