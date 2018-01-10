using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
using Models.Model;

namespace Ukare.Filter.Authorization
{
    public class BasicAuthorizer : IAuthorizer
    {
        public bool IsAuthorized(UserAuthorizationParam UserAuthor)
        {
            bool IsPermitted = false;

            using (var db = new UKareEntities())
            {
                var aUser = db.Users.Where(p => p.Id == UserAuthor.UserId).FirstOrDefault();
                if (aUser != null)
                {
                    var userRoleId = aUser.RoleId;

                    var userPermissions = db.Permissions.Where(p => p.RoleId == userRoleId).ToList();

                    

                    IsPermitted = userPermissions.Any(up => up.ActionName.Equals(UserAuthor.PermittedAction, StringComparison.InvariantCultureIgnoreCase)
                    && up.ActionName.Equals(UserAuthor.PermittedAction, StringComparison.InvariantCultureIgnoreCase));
                    return IsPermitted;
                }
            }
            return IsPermitted;
        }


        public List<UserAuthorizationParam> getUserAuthorizedList(long userId)
        {
            List<UserAuthorizationParam> ret = new List<UserAuthorizationParam>();
            try
            {
                using (var db = new UKareEntities())
                {
                    var aUser = db.Users.Find(userId);
                    if (aUser != null)
                    {
                        //foreach (Role r in aUser.Roles)
                        //{
                            //ret = ret.Concat(
                            //    (from rap in r.AssetPermissions
                            //     select new UserAuthorizationParam
                            //    {
                            //        UserId = userId,
                            //        AssetItem = rap.Asset.Code,
                            //        PermittedAction = rap.Permission.Name,
                            //    }).ToList()
                            //             ).ToList();
                        //}
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }
    }
}
