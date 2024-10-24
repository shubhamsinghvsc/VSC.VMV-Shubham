using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;

namespace VSC.VMV.Common.Models.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public abstract class AvailablityBase
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public WeekdayEnum Weekday { get; set; }

        [JsonProperty]
        public TimeOnly StartTime { get; set; }

        [JsonProperty]
        public TimeOnly EndTime { get; set; }
    }
}
