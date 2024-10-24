using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Interfaces;

namespace VSC.VMV.Common.Models.Scheduler
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class DoctorAppointmentPayment : IAuditTracking
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public Guid DoctorAppointmentId { get; set; }

        [JsonProperty]
        public DoctorAppointmentPaymentStatusEnum Status { get; set; } = DoctorAppointmentPaymentStatusEnum.NotPaid;

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
