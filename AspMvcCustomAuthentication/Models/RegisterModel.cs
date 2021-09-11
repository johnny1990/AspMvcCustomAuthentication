using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspMvcCustomAuthentication.Models
{
    public class RegisterModel
    {
        [Display(Name = "UserId")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter FirstName")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 6 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public Nullable<bool> Is_Deleted { get; set; }

        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail address id is not valid")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Security Answer")]
        [Display(Name = "Security Answer")]
        public string SecurityAnwser { get; set; }

        public List<RegisterModel> RegisteredUsersInfo { get; set; }
    }
}