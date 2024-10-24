using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models.Base;
using VSC.VMV.Common.Models.Mapping.Availablity;

namespace VSC.VMV.Common.Models.Mapping
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class CorporateAdminMapping : MappingBase
    {
        [JsonProperty]
        public Guid CorporateAdminId { get; set; }

        [JsonProperty]
        public List<CorporateAdminAvailablity> CorporateAdminAvailablities { get; set; } = new List<CorporateAdminAvailablity>();
    }
}
