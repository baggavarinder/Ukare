using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Users
{
    public interface IUserManager 
    {
        string CreateUser(CoreUser model);

        List<SelectListItem> GetCountries();

        List<SelectListItem> GetSpecialities();

        int LoginUser(CoreUserLoginModel model);

        List<CoreUser> GetUsers();

        CoreUser GetUserById(int id);

        int UpdateUser(CoreUser user);

        List<CoreUserRolesModel> GetRoles(int id);

        bool VerifyUser(int id, string code);

        bool ForgotPassword(CoreUserLoginModel email);

        bool DeleteUser(int id);

        List<CoreUser> GetUsersWithPrefix(string prefix);
    }
}
