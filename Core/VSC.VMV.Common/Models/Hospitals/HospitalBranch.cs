using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models.Base;

namespace VSC.VMV.Common.Models.Hospitals
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class HospitalBranch : HospitalMetaData
    {
        [JsonProperty]
        public Guid HospitalCorporateId { get; set; }
    }
}
