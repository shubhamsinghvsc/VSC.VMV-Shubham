using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models.Base;

namespace VSC.VMV.Common.Models.Doctor
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Doctor : PeopleMetaData
    {
        [JsonProperty]
        public List<DoctorQualification> Qualifications { get; set; } = new List<DoctorQualification>();

        [JsonProperty]
        public List<string> Specialization { get; set; } = new List<string>();

        [JsonProperty]
        public ushort PracticeStartYear { get; set; } = 0;
    }
}
