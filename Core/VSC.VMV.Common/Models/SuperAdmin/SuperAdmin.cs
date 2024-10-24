using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models.Base;

namespace VSC.VMV.Common.Models.CorporateAdmin
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class SuperAdmin : PeopleMetaData
    {

    }
}
