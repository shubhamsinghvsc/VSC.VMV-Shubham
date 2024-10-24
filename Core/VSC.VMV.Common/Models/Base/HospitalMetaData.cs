using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Interfaces;

namespace VSC.VMV.Common.Models.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public abstract class HospitalMetaData : ContactInfoBase, IAuditTracking
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public bool IsActive { get; set; } = true;

        [JsonProperty]
        public string LicenseNumber { get; set; } = "";

        [JsonProperty]
        public string GSTNumber { get; set; } = "";

        [JsonProperty]
        public DateTime EstablishedDate { get; set; } = DateTime.MinValue;

        [JsonProperty]
        public string Description { get; set; } = "";

        [JsonProperty]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        [JsonProperty]
        public string CreatedBy { get; set; } = "";

        [JsonProperty]
        public Guid CreatedById { get; set; }

        [JsonProperty]
        public UserTypeEnum CreatedByUserTypeEnum { get; set; }
    }
}
