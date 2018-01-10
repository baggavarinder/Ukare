using Core.Country;
using Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ukare.Service.User
{
    public interface IUserService
    {
        string CreateUser(UserModel user);

        List<SelectListItem> GetCountries();

        List<SelectListItem> GetSpecialities();

        int LoginUser(UserLoginModel user);

        List<UserModel> GetUsers();

        UserModel GetUserById(int id);

        int UpdateUser(UserModel user);

        List<UserRolesModel> GetRoles(int id);

        bool VerifyUser(int userid, string code);

        bool ForgotPassword(UserLoginModel email);

        bool DeleteUser(int id);

        List<UserModel> GetUsersWithPrefix(string prefix);
    }
}