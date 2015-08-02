using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace natp.Models
{
    public class GenericResponse
    {
        public GenericResponse()
        {
            Errors = new List<string>();
        }
        public string Status;
        public List<string> Errors;
        
    }
    public class BookTimesResponse
    {
        public BookTimesResponse()
        {
            Errors = new List<string>();
            Times = new List<DateTime>();
        }
        public string Status;
        public List<string> Errors;
        public List<DateTime> Times;
        public string CanClose;
    }

    public class SetAppointmentViewModel
    {
        public int ClientId{ get; set; }
        public int DesignerId { get; set; }
        public string Time { get; set; }
        public float Offset { get; set; }
        public int IsDesigner { get; set; }
    }

    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int ClientId { get; set; }
        public int DesignerId { get; set; }
        public string Time { get; set; }
        public float Offset { get; set; }
    }

    public class SetScheduleViewModel
    {
        public string Days { get; set; }
        public string StartHour { get; set; }
        public string StartMin { get; set; }
        public string StartTod { get; set; }
        public string EndHour { get; set; }
        public string EndMin { get; set; }
        public string EndTod { get; set; }
        public string Interval { get; set; }
    }
    public class ScheduleItemModel
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int aId { get; set;}
    }
    public class ScheduleModel
    {
        public ScheduleModel()
        {
            Schedule = new List<ScheduleItemModel>();
        }
        public List<ScheduleItemModel> Schedule {get; set;}
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AppointmentCommentFormViewModel
    {
        public int AppointmentId { get; set; }
        public int AccountId { get; set; }
        public bool CanComment { get; set; }
        [Required]
        public string Text { get; set; }
    }
    public class AppointmentNoteFormViewModel
    {
        public int AppointmentId { get; set; }
        [Required]
        public string Text { get; set; }
    }

    public class AppointmentCommentViewModel
    {
        public IEnumerable<AppointmentComment> Comments { get; set; }
    }

    public class AppointmentNoteViewModel
    {
        public IEnumerable<AppointmentNote> Notes { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class AccountUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountId { get; set; }
    }

    public class AccountUpdatePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int AccountId { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Contact Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
