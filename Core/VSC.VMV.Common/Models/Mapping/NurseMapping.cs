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
    public class NurseMapping : MappingBase
    {
        [JsonProperty]
        public Guid NurseId { get; set; }

        [JsonProperty]
        public List<NurseAvailablity> NurseAvailablities { get; set; } = new List<NurseAvailablity>();
    }
}
