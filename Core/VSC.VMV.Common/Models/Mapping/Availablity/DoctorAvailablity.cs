using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Models.Base;

namespace VSC.VMV.Common.Models.Mapping.Availablity
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class DoctorAvailablity : AvailablityBase
    {
        [JsonProperty]
        public Guid DoctorMappingId { get; set; }
    }
}
