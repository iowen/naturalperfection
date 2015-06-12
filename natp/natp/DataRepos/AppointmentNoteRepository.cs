using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class AppointmentNoteRepository : IAppointmentNoteRepository
    {
               private npDataContext db;

               public AppointmentNoteRepository(npDataContext dc)
        {
            db = dc;
        }

               public List<AppointmentNote> getAllAppointmentNotes()
               {
                   var result = new List<AppointmentNote>();
                   try
                   {
                       result = db.AppointmentNotes.ToList();
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       result = new List<AppointmentNote>();
                   }
                   return result;
               }

               public AppointmentNote getAppointmentNote(int id)
               {
                   var result = new AppointmentNote();
                   try
                   {
                       result = db.AppointmentNotes.Where(a => a.AppointmentNoteId == id).First();
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       result = new AppointmentNote();
                   }
                   return result;
               }

               public int addAppointmentNote(AppointmentNote appointmentNote)
               {
                   try
                   {
                       this.db.AppointmentNotes.InsertOnSubmit(appointmentNote);
                       this.db.SubmitChanges();
                       return appointmentNote.AppointmentNoteId;
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       return -1;
                   }
               }

               public bool updateAppointmentNote(AppointmentNote appointmentNote)
               {
                   throw new NotImplementedException();
               }


               public List<AppointmentNote> getAllDesignerAppointmentNotes(int id)
               {
                   var result = new List<AppointmentNote>();
                   try
                   {
                       result = db.AppointmentNotes.Where(a => a.Appointment.Designerd == id).ToList();
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       result = new List<AppointmentNote>();
                   }
                   return result;
               }

               public List<AppointmentNote> getAllAppointmentAppointmentNotes(int id)
               {
                   var result = new List<AppointmentNote>();
                   try
                   {
                       result = db.AppointmentNotes.Where(a => a.AppointmentId == id).ToList();
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.ToString());
                       result = new List<AppointmentNote>();
                   }
                   return result;
               }
    }
}
