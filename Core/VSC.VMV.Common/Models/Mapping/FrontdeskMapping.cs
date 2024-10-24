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
    public class FrontdeskMapping : MappingBase
    {
        [JsonProperty]
        public Guid FrontdeskId { get; set; }

        [JsonProperty]
        public List<FrontdeskAvailablity> FrontdeskAvailablities { get; set; } = new List<FrontdeskAvailablity>();
    }
}
