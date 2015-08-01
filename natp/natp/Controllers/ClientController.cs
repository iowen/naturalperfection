using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using natp.DataRepos;

namespace natp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Client")]
    public class ClientController : Controller
    {
        // GET: Client
    
        public ActionResult Index()
        {
            var cRep = new ClientRepository(new npDataContext());
            var c = cRep.getClientByAccount(int.Parse(User.Identity.GetUserId()));
            var dRepo = new DesignerRepository(new npDataContext());
            ViewBag.upcomingAppointment = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == true && s.IsCanceled == false).ToList().FirstOrDefault();
            ViewBag.todaysAppointment = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc.Date == DateTime.UtcNow.Date && s.IsConfirmed == true && s.IsCanceled == false).ToList().FirstOrDefault();
            ViewBag.pendingAppointments = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == false && s.IsCanceled == false).ToList();
            ViewBag.AppointmentHistory = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc < DateTime.UtcNow && s.IsConfirmed == true && s.IsCanceled == false).ToList();
            ViewBag.client = c;
            return View();
        }

        public ActionResult History()
        {
            var cRep = new ClientRepository(new npDataContext());
            var c = cRep.getClientByAccount(int.Parse(User.Identity.GetUserId()));
            ViewBag.upcomingAppointments = c.Appointments.Where(a => a.AppointmentTimeUtc > DateTime.UtcNow).ToList();
            ViewBag.pastAppointments = c.Appointments.Where(a => a.AppointmentTimeUtc < DateTime.UtcNow).ToList();
            ViewBag.client = c;
            return View();
        }

        // GET: Client
        public ActionResult View(int id)
        {
            var cRepo = new ClientRepository(new npDataContext());
            var c = cRepo.getClient(id);
            ViewBag.upcomingAppointment = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == true && s.IsCanceled == false).ToList().FirstOrDefault();
            ViewBag.todaysAppointment = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc.Date == DateTime.UtcNow.Date && s.IsConfirmed == true && s.IsCanceled == false).ToList().FirstOrDefault();
            ViewBag.pendingAppointments = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc > DateTime.UtcNow && s.IsConfirmed == false && s.IsCanceled == false).ToList();
            ViewBag.AppointmentHistory = c.Appointments.Where<Appointment>(s => s.AppointmentTimeUtc < DateTime.UtcNow && s.IsConfirmed == true && s.IsCanceled == false).ToList();
            ViewBag.client = c;
            return View();
        }
    }
}