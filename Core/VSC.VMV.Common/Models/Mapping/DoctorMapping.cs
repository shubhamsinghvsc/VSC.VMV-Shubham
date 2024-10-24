using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.Base;
using VSC.VMV.Common.Models.Mapping.Availablity;

namespace VSC.VMV.Common.Models.Mapping
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class DoctorMapping : MappingBase
    {
        [JsonProperty]
        public Guid DoctorId { get; set; }

        [JsonProperty]
        public List<DoctorAvailablity> DoctorAvailablities { get; set; } = new List<DoctorAvailablity>();

        [JsonProperty]
        public int TimeSlotInMins { get; set; }

        [JsonProperty]
        public int FeePerAppointment { get; set; }

        [JsonProperty]
        public int FeeForFollowupAppointment { get; set; }

        [JsonProperty]
        public int FollowupTimeInDays { get; set; } = 7;
    }
}
