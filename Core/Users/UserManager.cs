using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Web.Mvc;
using System.IO;
using System.Web;
using Core.Notifications;
using System.Data;

namespace Core.Users
{
    public class UserManager : IUserManager
    {
        UKareEntities db = new UKareEntities();
        public string CreateUser(CoreUser model)
        {
            try
            {
                var isEmailExist = db.Users.Where(p => p.Email == model.Email).FirstOrDefault();
                var result = "";
                if (isEmailExist != null)
                {
                    result = @"{ 'Status': 'exist', 'UserId':'','RoleId':''}";
                    return result;
                }
                User user = new User();

                user.Title = model.Title;
                user.Name = model.Name;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.Telephone = model.Telephone;
                user.MobileNumber = model.MobileNumber;
                user.Country = db.Countries.Where(c => c.Id == model.CountryIdQual).FirstOrDefault();
                user.Country1 = db.Countries.Where(c => c.Id == model.CountryIdResd).FirstOrDefault();
                user.QualfiedDate = model.QualfiedDate;
                user.JobLocation = db.Countries.Where(c => c.Id == model.JobLocation).Select(c => c.Id).FirstOrDefault();
                user.ExperienceIn1 = model.ExperienceIn1;
                user.ExperienceIn2 = model.ExperienceIn2;
                user.CreatedByUserId = null;
                user.CreatedOnDatetime = DateTime.Now;
                user.UpdatedByUserId = null;
                 user.IsActive = true;
                user.UpdatedOnDatetime = DateTime.Now;
                //user.IsActive = false;

                user.RoleId = 4;

                List<Speciality> _listSpeciality = new List<Speciality>();
                _listSpeciality.Add(db.Specialities.Where(c => c.Id == model.Speciality1).FirstOrDefault());
                _listSpeciality.Add(db.Specialities.Where(c => c.Id == model.Speciality2).FirstOrDefault());
                _listSpeciality.Add(db.Specialities.Where(c => c.Id == model.Speciality3).FirstOrDefault());

                user.Specialities = _listSpeciality;

                var path = "";

                if (model.File != null && model.File.ContentLength > 0)
                {
                    var filename = Guid.NewGuid() + Path.GetFileName(model.File.FileName);
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UserDocuments\\", filename);
                    var filetype = Path.GetExtension(model.File.FileName);

                    List<UserDocument> listUserDoc = new List<UserDocument>();

                    UserDocument userDoc = new UserDocument();
                    userDoc.Name = filename;
                    userDoc.Path = path;
                    userDoc.DocType = filetype;
                    userDoc.CreatedDate = DateTime.Now;
                    userDoc.UpdatedDate = DateTime.Now;

                    listUserDoc.Add(userDoc);

                    user.UserDocuments = listUserDoc;

                }

                db.Users.Add(user);
                db.SaveChanges();
                if (model.File != null)
                {
                    model.File.SaveAs(path);
                }
                //generate token and save in database

                string possibles = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                char[] code = new char[4];
                Random rd = new Random();

                for (int i = 0; i < 4; i++)
                {
                    code[i] = possibles[rd.Next(0, possibles.Length)];
                }

                string codeString = new string(code);
                
                VerificationToken token = new VerificationToken();
                token.Token = codeString;
                token.UserId = user.Id;
                token.GeneratedDateTime = DateTime.Now;

                //db.VerificationTokens.Add(token);
                //db.SaveChanges();
                HttpContext.Current.Session["UserEmailId"] = user.Email;
                 result= @"{ 'Status': '200', 'UserId':'" + user.Id+ "','RoleId':'"+ user .RoleId+ "'}";
                return result;
                //send mail for verification
                //SendNotification notify = new SendNotification();

                //string mailSubject = "Account verification";

                //bool mailStatus = notify.SendMailAccountVerification(model.Email, model.Name, user.Id, codeString, mailSubject);

                //if (mailStatus)
                //{
                //    return "success";
                //}
                //else
                //{
                //    DeleteUser(user.Id);
                //    return "mail failed";
                //}
            }
            catch (Exception ex)
            {
                return ex.Message.ToString(); ;
            }

        }
        public List<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            db = new UKareEntities();

            var countryList = db.Countries.Select(c => c).ToList();

            foreach (var item in countryList)
            {
                SelectListItem model = new SelectListItem();
                model.Text = item.Name;
                model.Value = item.Id.ToString();
                countries.Add(model);
            }
            return countries;
        }
        public List<SelectListItem> GetSpecialities()
        {
            List<SelectListItem> specialities = new List<SelectListItem>();

            var specialityList = db.Specialities.ToList();

            foreach (var item in specialityList)
            {
                SelectListItem model = new SelectListItem();
                model.Text = item.Name;
                model.Value = item.Id.ToString();
                specialities.Add(model);
            }
            return specialities;
        }
        public int LoginUser(CoreUserLoginModel model)
        {
            try
            {
                var user = db.Users.Where(p => p.Email == model.Email && p.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var isactive = user.IsActive;
                    if (isactive)
                    {
                        
                        if (user.RoleId == model.RoleId)
                        {
                            HttpContext.Current.Session["UserEmailId"] = user.Email;
                            return user.Id;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<CoreUser> GetUsers()
        {

            var user = db.Users.Select(p => new CoreUser
            {
                id = p.Id,
                Email = p.Email,
                UserName = p.UserName,
                Name = p.Name
            }).ToList();
            return user;
        }

        public CoreUser GetUserById(int id)
        {
            var userData = db.Users.Where(p => p.Id == id).FirstOrDefault();

            CoreUser coreUserMap = new CoreUser();
            coreUserMap.Email = userData.Email;
            coreUserMap.Name = userData.Name;
            coreUserMap.UserName = userData.UserName;
            coreUserMap.RoleId = userData.RoleId;

            return coreUserMap;
        }

        public int UpdateUser(CoreUser user)
        {
            User userRecord = db.Users.Where(p => p.Id == user.id).FirstOrDefault();
            if (userRecord != null)
            {
                userRecord.Name = user.Name;
                userRecord.Email = user.Email;
                if (user.Password != null) { userRecord.Password = user.Password; }
                if (user.UserName != null) { userRecord.UserName = user.UserName; }
                userRecord.RoleId = user.RoleId;
                //userRecord.Roles = db.Roles.Where(p => p.Id == user.RoleId).ToList();   
                db.SaveChanges();
                return user.id;
            }
            else
            {
                return 0;
            }
        }

        public List<CoreUserRolesModel> GetRoles(int id)
        {
            List<CoreUserRolesModel> roleList = new List<CoreUserRolesModel>();

            var roles = db.Roles.ToList();
            var currentRole = db.Users.Where(p => p.Id == id).FirstOrDefault().Role;

            foreach (var item in roles)
            {
                CoreUserRolesModel model = new CoreUserRolesModel();
                model.Name = item.Name;
                model.RoleId = item.Id;
                if (item.Id == currentRole.Id)
                {
                    model.Selected = true;
                }
                roleList.Add(model);
            }
            return roleList;
        }

        public bool VerifyUser(int id, string code)
        {
            var checkcode = db.VerificationTokens.Where(p => p.UserId == id && p.Token == code).FirstOrDefault();

            if (checkcode != null)
            {
                User user = db.Users.Where(p => p.Id == id).FirstOrDefault();
                user.IsActive = true;

                db.VerificationTokens.Remove(checkcode);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        //delete user code
        public bool DeleteUser(int id)
        {

            User user = db.Users.Include("Messages").Include("Specialities").Include("VerificationTokens").Include("UserDocuments").Where(p => p.Id == id).FirstOrDefault();

            //delete user and all its references

            foreach (var item in user.UserDocuments.ToList())
            {
                db.UserDocuments.Remove(item);
            }

            foreach (var item in user.Specialities.ToList())
            {
                db.Users.Where(p => p.Id == id).FirstOrDefault().Specialities.Remove(item);
            }

            foreach (var item in user.VerificationTokens.ToList())
            {
                db.VerificationTokens.Remove(item);
            }

            foreach (var item in user.Messages.ToList())
            {
                db.Users.Where(p => p.Id == id).FirstOrDefault().Messages.Remove(item);
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return true;
        }

        public bool ForgotPassword(CoreUserLoginModel email)
        {
            User userExist = db.Users.Where(p => p.Email == email.Email).FirstOrDefault();
            if (userExist != null)
            {
                SendNotification notify = new SendNotification();
                notify.SendMailForgotPassword(userExist.Email, userExist.Name, userExist.Password, "Forgot Password");
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CoreUser> GetUsersWithPrefix(string prefix)
        {
            List<CoreUser> users = GetUsers();
            List<CoreUser> prefixedUser = users.Where(p => p.Name.ToLower().Contains(prefix.ToLower())).ToList();

            return prefixedUser;
        }

    }
}
