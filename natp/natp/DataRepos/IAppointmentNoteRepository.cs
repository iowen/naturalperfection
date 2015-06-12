using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public interface IAppointmentNoteRepository
    {
        List<AppointmentNote> getAllAppointmentNotes();
        AppointmentNote getAppointmentNote(int id);
        List<AppointmentNote> getAllDesignerAppointmentNotes(int id);

        List<AppointmentNote> getAllAppointmentAppointmentNotes(int id);

        int addAppointmentNote(AppointmentNote appointmentNote);
        bool updateAppointmentNote(AppointmentNote appointmentNote);
    }
}