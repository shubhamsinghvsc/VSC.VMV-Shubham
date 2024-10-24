using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.Mapping;

namespace VSC.VMV.Common.Models.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public abstract class MappingBase : IAuditTracking
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public Guid HospitalBranchId { get; set; }

        [JsonProperty]
        public Guid HospitalBranchDepartmentId { get; set; }

        [JsonProperty]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        [JsonProperty]
        public bool IsActive { get; set; } = true;

        [JsonProperty]
        public string CreatedBy { get; set; } = "";

        [JsonProperty]
        public Guid CreatedById { get; set; }

        [JsonProperty]
        public UserTypeEnum CreatedByUserTypeEnum { get; set; }
    }
}
