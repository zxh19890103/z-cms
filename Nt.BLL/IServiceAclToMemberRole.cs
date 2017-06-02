using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    interface IServiceAclToMemberRole
    {
        string GetAclMemberRoles(string entity, int id);
    }
}
