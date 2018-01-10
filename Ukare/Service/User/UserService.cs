using Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using System.Web.Mvc;

namespace Ukare.Service.User
{
    public class UserService : IUserService
    {
        private IUserManager _userManager;
        public UserService(IUserManager userManager)
        {
            this._userManager = userManager;
        }

        public UserService()
        {

        }

        public string CreateUser(UserModel user)
        {
            CoreUser modelCoreUser = new CoreUser();

            modelCoreUser.Title = user.Title;
            modelCoreUser.Name = user.Name;
            modelCoreUser.Email = user.Email;
            modelCoreUser.Password = user.Password;
            modelCoreUser.Telephone = user.Telephone;
            modelCoreUser.MobileNumber = user.MobileNumber;
            modelCoreUser.ExperienceIn1 = user.ExperienceIn1;
            modelCoreUser.ExperienceIn2 = user.ExperienceIn2;
            modelCoreUser.JobLocation = user.JobLocation;
            modelCoreUser.QualfiedDate = user.QualfiedDate;
            modelCoreUser.UserName = "";
            modelCoreUser.CountryIdQual = user.CountryIdQual;
            modelCoreUser.CountryIdResd = user.CountryIdResd;
            modelCoreUser.Speciality1 = user.Speciality1;
            modelCoreUser.Speciality2 = user.Speciality2;
            modelCoreUser.Speciality3 = user.Speciality3;
            modelCoreUser.File = user.file;

            return _userManager.CreateUser(modelCoreUser);
        }

        public List<SelectListItem> GetCountries()
        {
            return _userManager.GetCountries();
        }

        public List<SelectListItem> GetSpecialities()
        {
            return _userManager.GetSpecialities();
        }

        public int LoginUser(UserLoginModel user)
        {

            CoreUserLoginModel coreModel = new CoreUserLoginModel();
            coreModel.Email = user.Email;
            coreModel.Password = user.Password;
            coreModel.RoleId = user.RoleId;

            return _userManager.LoginUser(coreModel);
        }

        public List<UserModel> GetUsers()
        {

            var userlist = _userManager.GetUsers();
            var user = userlist.Select(p => new UserModel
            {
                id = p.id,
                Email = p.Email,
                UserName = p.UserName
            }).ToList();

            return user;
        }

        public UserModel GetUserById(int id)
        {
            var userData = _userManager.GetUserById(id);

            UserModel UserMap = new UserModel();
            UserMap.Email = userData.Email;
            UserMap.Name = userData.Name;
            UserMap.UserName = userData.UserName;
            UserMap.RoleId = userData.RoleId;

            return UserMap;
        }

        public int UpdateUser(UserModel user)
        {
            CoreUser userCore = new CoreUser();
            userCore.Name = user.Name;
            userCore.Email = user.Email;
            userCore.Password = user.Password;
            userCore.UserName = user.UserName;
            userCore.RoleId = user.RoleId;
            userCore.id = user.id;

            int userId = _userManager.UpdateUser(userCore);
            return userId;
        }

        public List<UserRolesModel> GetRoles(int id)
        {
            var userRoles = _userManager.GetRoles(id);

            List<UserRolesModel> UserRoleList = new List<UserRolesModel>();

            foreach (var item in userRoles)
            {
                UserRolesModel roleModel = new UserRolesModel();
                roleModel.RoleId = item.RoleId;
                roleModel.Name = item.Name;
                roleModel.Selected = item.Selected;

                UserRoleList.Add(roleModel);
            }
            return UserRoleList;
        }

        public bool VerifyUser(int userid, string code)
        {
            return _userManager.VerifyUser(userid, code);
        }

        public bool ForgotPassword(UserLoginModel email)
        {
            CoreUserLoginModel userEmail = new CoreUserLoginModel();
            userEmail.Email = email.Email;
            bool status= _userManager.ForgotPassword(userEmail);
            return status;
        }

        public bool DeleteUser(int id)
        {
            _userManager.DeleteUser(id);
            return true;
        }

        public List<UserModel> GetUsersWithPrefix(string prefix) {

            var users = _userManager.GetUsersWithPrefix(prefix).Select(p => new UserModel {
                Name = p.Name,
                id = p.id
            }).ToList();

            return users;
        }

    }
}