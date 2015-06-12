using natp.DataRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using natp.Models;

namespace natp.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Client")]
        public ActionResult Book()
        {
            var cRep = new ClientRepository(new npDataContext());
            var dRep = new DesignerRepository(new npDataContext());
            var c = cRep.getClientByAccount(int.Parse(User.Identity.GetUserId()));
            var d = dRep.getDesigner(c.DesignerId);
            ClosedDatesModel dresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ClosedDatesModel>(System.IO.File.ReadAllText(d.WorkSchedules.First().ClosedDatesLocation));
            ViewBag.closedDates = dresult.closedDates;
            ViewBag.client = c;
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetTimes(int dId, string date, float offset)
        {

            var dRepo = new DesignerRepository(new npDataContext());
            var aRepo = new AppointmentRepository(new npDataContext());
            var d = dRepo.getDesigner(dId);
            WorkScheduleModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkScheduleModel>(System.IO.File.ReadAllText(d.WorkSchedules.First().Location));
            ClosedDatesModel dresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ClosedDatesModel>(System.IO.File.ReadAllText(d.WorkSchedules.First().ClosedDatesLocation));
            string responseStatus = "Failure";
            var times = new List<DateTime>();
            var bookDate = DateTime.Parse(date);

            try
            {
                if (!dresult.closedDates.Contains(bookDate))
                {
                    var availableAppointments = aRepo.getAvailableDesignerAppointmentsForDate(dId, bookDate, result);
                    foreach (var a in availableAppointments)
                    {
                        //int hours = (int)offset;
                        //int mins = (int)((offset-(float)hours) * 60.0);
                        DateTime aUpdated = new DateTime(a.Year, a.Month, a.Day, a.Hour, a.Minute, 0, DateTimeKind.Utc);

                        times.Add(aUpdated);
                    }
                    responseStatus = "Success";
                }
            }
            catch(Exception e)
            {
                responseStatus = "Failure";

            }
            var response = new BookTimesResponse() { Status = responseStatus, Times = times };
            return new JsonResult() { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SetAppointment(SetAppointmentViewModel model)
        {
            var aRepo = new AppointmentRepository(new npDataContext());
            var tztime = DateTime.Parse(model.Time);
            int hours = (int)model.Offset;
            int mins = (int)((model.Offset - (float)hours) * 60.0);
            var utcTime = new DateTime(tztime.Year, tztime.Month, tztime.Day, tztime.Hour - hours, tztime.Minute - mins, 0, DateTimeKind.Utc);
            var apt = new Appointment() { ClientId = model.ClientId, Designerd = model.DesignerId, IsCanceled = false, IsConfirmed = false, DateCreatedUtc = DateTime.UtcNow, AppointmentTimeUtc = utcTime, TimeOffset = (decimal)model.Offset };
            var aId = aRepo.addAppointment(apt);
            var response = new BookTimesResponse() { Status = "Success" };
            return new JsonResult() { Data = response };
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult MakeComment(AppointmentCommentFormViewModel model)
        {
            var response = new BookTimesResponse();
            try
            {

                    var aRepo = new AppointmentCommentRepository(new npDataContext());
                    var appointmentComment = new AppointmentComment() { AccountId = model.AccountId, AppointmentId = model.AppointmentId, Text = model.Text, CreatedUtc = DateTime.UtcNow };
                    aRepo.addAppointmentComment(appointmentComment);
                    response = new BookTimesResponse() { Status = "Success" };
                    return new JsonResult() { Data = response };

            }
            catch (Exception ex)
            {
                response = new BookTimesResponse() { Status = "Failure" };
            }
            return new JsonResult() { Data = response };
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult MakeNote(AppointmentNoteFormViewModel model)
        {
            var response = new BookTimesResponse();
            try
            {

                var aRepo = new AppointmentNoteRepository(new npDataContext());
                var appointmentNote= new AppointmentNote() {  AppointmentId = model.AppointmentId, Text = model.Text, CreatedUtc = DateTime.UtcNow };
                aRepo.addAppointmentNote(appointmentNote);
                response = new BookTimesResponse() { Status = "Success" };
                return new JsonResult() { Data = response };

            }
            catch (Exception ex)
            {
                response = new BookTimesResponse() { Status = "Failure" };
            }
            return new JsonResult() { Data = response };
        }

        // GET: Appointment
        public ActionResult View(int id)
        {
            var aRepo = new AppointmentRepository(new npDataContext());
            var app = aRepo.getAppointment(id);
            ViewBag.appointment = app;
            return View();
        }

        // GET: Appointment
        public ActionResult GetAppointmentComments(int id)
        {
            var aRepo = new AppointmentCommentRepository(new npDataContext());
            var app = aRepo.getAllAppointmentAppointmentComments(id);
            return PartialView("~/Views/Shared/_AppointmentCommentPartial.cshtml", new AppointmentCommentViewModel() { Comments = app });
        }

        // GET: Appointment
        public ActionResult GetAppointmentNotes(int id)
        {
            var aRepo = new AppointmentNoteRepository(new npDataContext());
            var app = aRepo.getAllAppointmentAppointmentNotes(id);
            return PartialView("~/Views/Shared/_AppointmentNotePartial.cshtml", new AppointmentNoteViewModel() { Notes = app });
        }
    }
}