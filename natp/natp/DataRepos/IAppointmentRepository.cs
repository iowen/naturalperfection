using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using natp.Models;

namespace natp.DataRepos
{
    public interface IAppointmentRepository
    {
        List<Appointment> getAllAppointments();
        List<Appointment> getDesignerAppointments(int id);
        List<Appointment> getDesignerAppointmentsForDate(int id, DateTime date);
        List<DateTime> getAvailableDesignerAppointmentsForDate(int id, DateTime date, WorkScheduleModel schedule);
        List<Appointment> getDesignerPendingAppointments(int id);
        List<Appointment> getDesignerUpcomingAppointments(int id);
        List<Appointment> getClientAppointments(int id);
        List<Appointment> getClientAppointmentsForDate(int id, DateTime date);
        Appointment getAppointment(int id);
        int addAppointment(Appointment appointment);
        bool updateAppointment(Appointment appointment);
    }
}
