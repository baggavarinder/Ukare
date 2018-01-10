using System;
using System.Collections.Generic;
using System.Text;

namespace Ukare.Filter.Authorization
{
    public class UserAuthorizationParam
    {
        public long UserId { get; set; }
        public string AssetItem { get; set; }
        public string PermittedAction { get; set; }
    }
}
