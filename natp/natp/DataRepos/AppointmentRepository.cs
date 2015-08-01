using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private npDataContext db;

        public AppointmentRepository(npDataContext _db)
        {
            db = _db;
        }

        public List<Appointment> getAllAppointments()
        {
            var result = new List<Appointment>();
            try
            {
                result = db.Appointments.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public List<Appointment> getDesignerAppointments(int id)
        {
            var result = new List<Appointment>();
            try
            {
                result = db.Appointments.Where(a => a.Designerd == id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public List<Appointment> getDesignerAppointmentsForDate(int id, DateTime date)
        {
            var result = new List<Appointment>();
            try
            {
                result = db.Appointments.Where(a => a.Designerd == id && a.AppointmentTimeUtc.Date == date.Date).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public List<Appointment> getClientAppointments(int id)
        {
            var result = new List<Appointment>();
            try
            {
                result = db.Appointments.Where(a => a.ClientId == id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public List<Appointment> getClientAppointmentsForDate(int id, DateTime date)
        {
            var result = new List<Appointment>();
            try
            {
                result = db.Appointments.Where(a => a.ClientId == id && a.AppointmentTimeUtc.Date == date.Date).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public bool canScheduleAppointmentForDesigner(int designer, DateTime date)
        {
            bool result = false;
            try
            {
                var exists = db.Appointments.Where(a => a.Designerd == designer && a.AppointmentTimeUtc.Date == date.Date && a.IsCanceled == false).Any();
                result = !exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            return result;
        }

        public Appointment getAppointment(int id)
        {
            var result = new Appointment();
            try
            {
                result = db.Appointments.First(a => a.AppointmentId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Appointment() { AppointmentId = -1};
            }
            return result;
        }

        public int addAppointment(Appointment appointment)
        {
            try
            {
                this.db.Appointments.InsertOnSubmit(appointment);
                this.db.SubmitChanges();
                return appointment.AppointmentId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool updateAppointment(Appointment appointment)
        {
            bool success = false;
            try
            {
                var apt = db.Appointments.First(a => a.AppointmentId == appointment.AppointmentId);
                apt.DateConfirmed = appointment.DateConfirmed;
                apt.IsConfirmed = appointment.IsConfirmed;
                apt.IsCanceled = appointment.IsCanceled;
                db.SubmitChanges();
                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }
            return success;
        }


        public List<DateTime> getAvailableDesignerAppointmentsForDate(int id, DateTime date, Models.WorkScheduleModel schedule)
        {
            var result = new List<DateTime>();
            var dayOfWeek = (int)date.DayOfWeek;
            try
            {
                var appointments = db.Appointments.Where(a => a.Designerd == id && a.AppointmentTimeUtc.Date == date.Date && a.IsCanceled == false).ToList();

                switch (dayOfWeek)
                {
                    case 0:
                        {
                            foreach (var w in schedule.sun)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt",CultureInfo.InvariantCulture );

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours((double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if(!found)
                                {
                                    time = time.AddHours(4.0);
                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            foreach (var w in schedule.mon)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            foreach (var w in schedule.tue)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            foreach (var w in schedule.wed)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            foreach (var w in schedule.thu)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            foreach (var w in schedule.fri)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                    case 6:
                        {
                            foreach (var w in schedule.sat)
                            {
                                var found = false;
                                var time = DateTime.ParseExact(w, "hh:mm tt", CultureInfo.InvariantCulture);

                                foreach (var a in appointments)
                                {
                                    var appointmentTime = a.AppointmentTimeUtc.AddHours( (double)+a.TimeOffset);
                                    if (appointmentTime.TimeOfDay == time.TimeOfDay)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    time = time.AddHours(4.0);

                                    DateTime newDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0,DateTimeKind.Utc);
                                    result.Add(newDate);
                                }
                            }
                            break;
                        }
                     default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<DateTime>();
            }
            return result;
        }


        public List<Appointment> getDesignerPendingAppointments(int id)
        {
            var result = new List<Appointment>();
            try
            {
                var date = DateTime.UtcNow;
                result = db.Appointments.Where(a => a.Designerd == id && a.AppointmentTimeUtc > date && a.IsCanceled == false && a.IsConfirmed == false).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }

        public List<Appointment> getDesignerUpcomingAppointments(int id)
        {
            var result = new List<Appointment>();
            try
            {
                var date = DateTime.UtcNow;
                result = db.Appointments.Where(a => a.Designerd == id && a.AppointmentTimeUtc > date && a.IsCanceled == false && a.IsConfirmed == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Appointment>();
            }
            return result;
        }
    }
}