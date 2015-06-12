using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class AppointmentCommentRepository : IAppointmentCommentRepository
    {
        private npDataContext db;

        public AppointmentCommentRepository(npDataContext dc)
        {
            db = dc;
        }

        public List<AppointmentComment> getAllAppointmentComments()
        {
            var result = new List<AppointmentComment>();
            try
            {
                result = db.AppointmentComments.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<AppointmentComment>();
            }
            return result;
        }

        public AppointmentComment getAppointmentComment(int id)
        {
            var result = new AppointmentComment();
            try
            {
                result = db.AppointmentComments.Where(a => a.AppointmentCommentId == id).First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new AppointmentComment();
            }
            return result;
        }

        public int addAppointmentComment(AppointmentComment appointmentComment)
        {
            try
            {
                this.db.AppointmentComments.InsertOnSubmit(appointmentComment);
                this.db.SubmitChanges();
                return appointmentComment.AppointmentCommentId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool updateAppointmentComment(AppointmentComment appointmentComment)
        {
            throw new NotImplementedException();
        }


        public List<AppointmentComment> getAllDesignerAppointmentComments(int id)
        {
            var result = new List<AppointmentComment>();
            try
            {
                result = db.AppointmentComments.Where(a => a.Appointment.Designerd == id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<AppointmentComment>();
            }
            return result;
        }

        public List<AppointmentComment> getAllAppointmentAppointmentComments(int id)
        {
            var result = new List<AppointmentComment>();
            try
            {
                result = db.AppointmentComments.Where(a => a.AppointmentId == id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<AppointmentComment>();
            }
            return result;
        }
    }
}
