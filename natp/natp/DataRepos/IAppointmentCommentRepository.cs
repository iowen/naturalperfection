using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public interface IAppointmentCommentRepository
    {
        List<AppointmentComment> getAllAppointmentComments();
        AppointmentComment getAppointmentComment(int id);
        List<AppointmentComment> getAllDesignerAppointmentComments(int id);

        List<AppointmentComment> getAllAppointmentAppointmentComments(int id);
        int addAppointmentComment(AppointmentComment appointmentComment);
        bool updateAppointmentComment(AppointmentComment appointmentComment);
    }
}