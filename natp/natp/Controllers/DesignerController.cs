using natp.DataRepos;
using natp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Hosting;

namespace natp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Designer")]
    public class DesignerController : Controller
    {
        // GET: Designer
         
        public ActionResult Index()
        {
           // int dId = 1;
            var offset = TimeZone.CurrentTimeZone.GetUtcOffset(Request.RequestContext.HttpContext.Timestamp);

            var dRepo = new DesignerRepository(new npDataContext());
            var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkScheduleModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().Location)));
            dynamic dresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ClosedDatesModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().ClosedDatesLocation)));
            ViewBag.upcomingAppointments = d.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == true && s.IsCanceled == false).ToList();
            ViewBag.todaysAppointments = d.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc.Date == DateTime.UtcNow.Date && s.IsConfirmed == true && s.IsCanceled == false).ToList();
            ViewBag.pendingAppointments = d.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == false && s.IsCanceled == false).ToList();
            var sm = new ScheduleModel();
            List<Appointment> ah = d.Appointments.Where<Appointment>(s => s.IsConfirmed == true && s.IsCanceled == false).ToList();
             foreach (var i in ah)
             {
                 sm.Schedule.Add(new ScheduleItemModel() { title = i.Client.Account.FirstName + " " + i.Client.Account.LastName, start = i.AppointmentTimeUtc.Add(offset).ToString("yyyy-MM-dd") + "T" + i.AppointmentTimeUtc.Add(offset).ToString("HH:mm:ss"), end = i.AppointmentTimeUtc.Add(offset).ToString("yyyy-MM-dd") + "T" + i.AppointmentTimeUtc.Add(offset).AddMinutes(20).ToString("HH:mm:ss"), aId = i.AppointmentId });
             }
             foreach (var cd in dresult.closedDates)
             {
                 sm.Schedule.Add(new ScheduleItemModel() { title = "Closed", start = cd.ToString("yyyy-MM-dd") , aId = -1});

             }
             var sl = Newtonsoft.Json.JsonConvert.SerializeObject(sm.Schedule);
             var sj = sl.Replace("\"title\"", "title").Replace("\"start\"", "start").Replace("\"end\"", "end").Replace("\"aId\"", "aId").Replace("\"", "'");
             ViewBag.appointmentHistory = sj;
             ViewBag.designer = d;
            return View();
        }

         
         public ActionResult Schedule()
         {
             // int dId = 1;
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             WorkScheduleModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkScheduleModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().Location)));
             dynamic dresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ClosedDatesModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().ClosedDatesLocation)));
             ViewBag.closedDates = dresult.closedDates;

             ViewBag.workScheduleModel = result;
             ViewBag.designer = d;
             return View();
         }


         public ActionResult Clients()
         {
             // int dId = 1;
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             ViewBag.clients = d.Clients.ToList();
             ViewBag.designer = d;
             return View();
         }

         public ActionResult Appointments()
         {
             // int dId = 1;
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             ViewBag.upcomingAppointments = d.Appointments.Where(a => a.AppointmentTimeUtc > DateTime.UtcNow && a.IsConfirmed == true && a.IsCanceled == false).ToList();
             ViewBag.pendingAppointments = d.Appointments.Where(a => a.AppointmentTimeUtc > DateTime.UtcNow && a.IsConfirmed == false && a.IsCanceled == false).ToList();
             ViewBag.pastAppointments = d.Appointments.Where(a => a.AppointmentTimeUtc < DateTime.UtcNow).ToList();
             ViewBag.designer = d;
             return View();
         }
        
         public ActionResult CloseDate()
         {
             // int dId = 1;
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             WorkScheduleModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkScheduleModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().Location)));
             ClosedDatesModel dresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ClosedDatesModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().ClosedDatesLocation)));
             var closedDates = dresult.closedDates.Where(cd => cd > DateTime.Now).ToList();
             ViewBag.closedDates = closedDates;
             string serializedClosedDates = Newtonsoft.Json.JsonConvert.SerializeObject(closedDates);
             ViewBag.closedJson = serializedClosedDates.Replace("T00:00:00", "");
             ViewBag.workScheduleModel = result;
             ViewBag.designer = d;
             return View();
         }

         [HttpPost]
         public ActionResult ConfirmAppointment(AppointmentViewModel model)
         {
             var aRepo = new AppointmentRepository(new npDataContext());
             Appointment apt = aRepo.getAppointment(model.AppointmentId);
             apt.IsConfirmed = true;
             apt.DateConfirmed = DateTime.UtcNow;
             apt.IsCanceled = false;
             var responseStatus = (aRepo.updateAppointment(apt)) ? "Success" : "Failure";
             var response = new BookTimesResponse() { Status = responseStatus};
             return new JsonResult() { Data = response };
         }

         [HttpPost]
         public ActionResult CancelAppointment(AppointmentViewModel model)
         {
             var aRepo = new AppointmentRepository(new npDataContext());
             Appointment apt = aRepo.getAppointment(model.AppointmentId);
             apt.IsCanceled = true;
             var responseStatus = (aRepo.updateAppointment(apt)) ? "Success" : "Failure";
             var response = new BookTimesResponse() { Status = responseStatus };
             return new JsonResult() { Data = response };
         }

         [HttpPost]
         public ActionResult RejectAppointment(AppointmentViewModel model)
         {
             var aRepo = new AppointmentRepository(new npDataContext());
             Appointment apt = aRepo.getAppointment(model.AppointmentId);
             apt.IsConfirmed = false;
             apt.IsCanceled = true;
             var responseStatus = (aRepo.updateAppointment(apt)) ? "Success" : "Failure";
             var response = new BookTimesResponse() { Status = responseStatus };
             return new JsonResult() { Data = response };
         }

         [HttpPost]
         public ActionResult SetSchedule(SetScheduleViewModel model)
         {
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             WorkScheduleModel result = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkScheduleModel>(System.IO.File.ReadAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().Location)));
             WorkScheduleModel schedule = new WorkScheduleModel();
             var hours = GenerateHours(model);
             var days = model.Days.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);
             foreach(var day in days)
             {
                 string sday = day.Trim();
                 if (sday == "sun")
                     schedule.sun = hours;
                 if (sday == "mon")
                     schedule.mon = hours;
                 if (sday == "tue")
                     schedule.tue = hours;
                 if (sday == "wed")
                     schedule.wed = hours;
                 if (sday == "thu")
                     schedule.thu = hours;
                 if (sday == "fri")
                     schedule.fri = hours;
                 if (sday == "sat")
                     schedule.sat = hours;
             }
             var scheduleJson = Newtonsoft.Json.JsonConvert.SerializeObject(schedule);
             System.IO.File.WriteAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().Location), scheduleJson);
             var responseStatus =  "Success" ;
             var response = new BookTimesResponse() { Status = responseStatus };
             return new JsonResult() { Data = response };
         }
         [HttpPost]
         public ActionResult CanClose (string DateToClose)
         {
             var dt = DateTime.Parse(DateToClose.Split(new []{'-'}).Last());
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
             var a = d.Appointments.Any(app => app.AppointmentTimeUtc.Date == dt.Date && app.IsCanceled == false && app.IsConfirmed == true );
             var responseStatus =  (!a) ? "Success" : "Failure";
             var response = new BookTimesResponse() { Status = responseStatus};
             return new JsonResult() { Data = response };
         }

         [HttpPost]
         public ActionResult SetCloseDates(string DatesToClose)
         {
             var responseStatus =  "Failure";
             try{
             dynamic ds = Newtonsoft.Json.JsonConvert.DeserializeObject(DatesToClose);
             var cDates = new ClosedDatesJsonModel();
             for (int i =0; i< ds.Count; i++)
             {
                 string ddd = ds[i].Value.ToString();
                 var dt = DateTime.Parse(ddd.Split(new[] { '-' }).Last());
                 cDates.closedDates.Add(dt.ToString("M-d-yyyy"));
             }
            // var dt = DateTime.Parse(DateToClose.Split(new[] { '-' }).Last());
             var dRepo = new DesignerRepository(new npDataContext());
             var d = dRepo.getDesignerByAccount(int.Parse((User.Identity.GetUserId())));
           //  var a = d.Appointments.Any(app => app.AppointmentTimeUtc.Date == dt.Date);
             var closedJson = Newtonsoft.Json.JsonConvert.SerializeObject(cDates);

             System.IO.File.WriteAllText(HostingEnvironment.MapPath(d.WorkSchedules.First().ClosedDatesLocation), closedJson);
             responseStatus =  "Success";
             }
             catch(Exception e)
             {
                 responseStatus = "Failure";
             }
             var response = new BookTimesResponse() { Status = responseStatus };
             return new JsonResult() { Data = response };
         }

         private List<string> GenerateHours(SetScheduleViewModel model)
         {
             var dt = DateTime.Now;
             var result = new List<string>();
             int shour = (model.StartTod == "am") ? int.Parse(model.StartHour) % 12 : int.Parse(model.StartHour) % 12 + 12;
             int ehour = (model.EndTod == "am") ? int.Parse(model.EndHour) % 12 : int.Parse(model.EndHour) % 12 + 12;
             int interval = int.Parse(model.Interval);
             var sdate = new DateTime(dt.Year, dt.Month, dt.Day, shour, int.Parse(model.StartMin), 0);
             var edate = new DateTime(dt.Year, dt.Month, dt.Day, ehour, int.Parse(model.EndMin), 0);
             var curDate = sdate;
             while (curDate < edate)
             {
                 result.Add(curDate.ToString("hh:mm tt"));
                 curDate = curDate.AddMinutes((double)interval);
             }
             return result;
         }
    }
}