using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchedulingApp.Notifications
{
    public class NotificationInput
    {
        public Person Person { get; set; }
        public Schedule Schedule { get; set; }
    }
}
