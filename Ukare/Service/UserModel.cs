using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ukare.Service
{
    public class UserModel
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Title is required.")]

        public int RoleId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Confirm Email is required.")]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        public string ConfirmEmail { get; set; }


        //[Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Telephone is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid telephone number")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid mobile number")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Country of residence is required.")]
        public int CountryIdResd { get; set; }
        [Required(ErrorMessage = "Country of qualification is required.")]
        public int CountryIdQual { get; set; }
        [Required(ErrorMessage = "Qualfied date is required.")]
        public DateTime QualfiedDate { get; set; }
        [Required(ErrorMessage = "Job location is required.")]
        public int JobLocation { get; set; }
        [Required(ErrorMessage = "Experience is required.")]
        public string ExperienceIn1 { get; set; }
        [Required(ErrorMessage = "Experience is required.")]
        public string ExperienceIn2 { get; set; }
        [Required(ErrorMessage = "Speciality is required.")]
        public int Speciality1 { get; set; }
        [Required(ErrorMessage = "Speciality is required.")]
        public int Speciality2 { get; set; }
        [Required(ErrorMessage = "Speciality is required.")]
        public int Speciality3 { get; set; }

        public HttpPostedFileBase file { get; set; }
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }

        //public bool Admin { get; set; }
        //public bool Employee { get; set; }

        //public List<UserRolesModel> ListUserRoleModel { get; set; }


        //public UserLoginModel() {

        //    ListUserRoleModel = new List<UserRolesModel>
        //    {
        //        new UserRolesModel {Id= 1, Name= "Admin/Office staff" },
        //        new UserRolesModel {Id= 2, Name= "Employee" }
        //    };

        //}

    }


    public class UserRolesModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public bool Selected { get; set; }
    }

}
