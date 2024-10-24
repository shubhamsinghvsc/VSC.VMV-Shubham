using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;

namespace VSC.VMV.Common.Interfaces
{
    public interface IAuditTracking
    {
        [JsonProperty]
        public DateTime CreationTime { get; set; }

        [JsonProperty]
        public string CreatedBy { get; set; }

        [JsonProperty]
        public Guid CreatedById { get; set; }

        [JsonProperty]
        public UserTypeEnum CreatedByUserTypeEnum { get; set; }
    }
}
