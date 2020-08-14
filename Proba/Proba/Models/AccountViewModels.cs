using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Web;

namespace Proba.Models
{
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
        [Display(Name = "Email адреса")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email адреса")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
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
    }
    public class RegisterClientViewModel
    {

        [Required(ErrorMessage = "Задолжително внесете име")]
        [Display(Name = "Име")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Задолжително внесете презиме")]
        [Display(Name = "Презиме")]
        public string ClientSurname { get; set; }
        [Required(ErrorMessage = "Задолжително внесете пол")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Задолжително внесете датум на раѓање")]
        [Display(Name = "Датум на раѓање")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Задолжително внесете град")]
        [Display(Name = "Град")]
        public string City { get; set; }


        [Required(ErrorMessage = "Задолжително внесете Еmail адреса")]
        [EmailAddress]
        [Display(Name = "Email адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Задолжително внесете лозинка")]
        [StringLength(100, ErrorMessage = "{0}та мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврдете ја лозинката")]
        [Compare("Password", ErrorMessage = "Внесените лозинки не се совпаѓаат.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Прикачете слика")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
    public class RegisterSalonViewModel
    {

        [Required(ErrorMessage = "Задолжително внесете го името на салонот")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Задолжително внесете адреса на салонот")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Задолжително внесете град")]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Required(ErrorMessage = "Задолжително внесете Еmail адреса")]
        [EmailAddress]
        [Display(Name = "Email адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Задолжително внесете лозинка")]
        [StringLength(100, ErrorMessage = "{0}та мора да содржи барем {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврдете ја лозинката")]
        [Compare("Password", ErrorMessage = "Внесените лозинки не се совпаѓаат.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Задолжително внесете телефон за контакт")]
        [Display(Name = "Телефон за контакт")]

        public string PhoneNumber { get; set; }
        [Display(Name="Услуги")]
        public IEnumerable<Type> SelectedServices { get; set; }
        public IEnumerable<Service> Services { get; set; }
        [Display(Name = "Прикачете слика")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFileS { get; set; }
        public RegisterSalonViewModel()
        {
            Services = new List<Service>()
            {
                new Service()
                {
                    TypeOfService = Type.КОСА,
                    Name = Type.КОСА.ToString(),

                },
                 new Service()
                {
                    TypeOfService = Type.ШМИНКА,
                    Name = Type.ШМИНКА.ToString(),

                },
                  new Service()
                {
                    TypeOfService = Type.МАНИКИР,
                    Name = Type.МАНИКИР.ToString(),

                },
                   new Service()
                {
                    TypeOfService = Type.ПЕДИКИР,
                    Name = Type.ПЕДИКИР.ToString(),

                },
                    new Service()
                {
                    TypeOfService = Type.ДЕПИЛАЦИЈА,
                    Name = Type.ДЕПИЛАЦИЈА.ToString()
                }

            };
            SelectedServices = new List<Type>();
        }

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
