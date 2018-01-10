using System;
using System.Collections.Generic;
using System.Text;

namespace Ukare.Filter.Authorization
{
    public interface IAuthorizer
    {
        bool IsAuthorized(UserAuthorizationParam UserAuthor);
        List<UserAuthorizationParam> getUserAuthorizedList(long userId);
    }
}
