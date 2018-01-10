using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Core.Users
{
    public class CoreUser
    {
        public int id { get; set; }
       
        public int RoleId { get; set; }

        public string Title { get; set; }
       
        public string Name { get; set; }
       
        public string Email { get; set; }
       
        public string ConfirmEmail { get; set; }
       
        public string UserName { get; set; }
       
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
       
        public string Telephone { get; set; }
       
        public string MobileNumber { get; set; }
       
        public int CountryIdResd { get; set; }
       
        public int CountryIdQual { get; set; }
       
        public DateTime QualfiedDate { get; set; }
       
        public int JobLocation { get; set; }
       
        public string ExperienceIn1 { get; set; }
       
        public string ExperienceIn2 { get; set; }
       
        public int Speciality1 { get; set; }
       
        public int Speciality2 { get; set; }
       
        public int Speciality3 { get; set; }
       
        public HttpPostedFileBase File { get; set; }

    }


    public class CoreUserLoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public int RoleId { get; set; }
    }

    public class CoreUserRolesModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public bool Selected { get; set; }
    }

}
