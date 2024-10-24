using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.VMV.Common.Models.Doctor
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class DoctorQualification
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public Guid DoctorId { get; set; } = Guid.Empty;

        [JsonProperty]
        public string Degree { get; set; } = "";

        [JsonProperty]
        public string College { get; set; } = "";

        [JsonProperty]
        public string CollegeAddress { get; set; } = "";

        [JsonProperty]
        public ushort GraduationYear { get; set; } = 0;
    }
}
