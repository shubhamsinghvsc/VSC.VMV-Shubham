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
    public class DoctorAppointment : IAuditTracking
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public Guid DoctorId { get; set; }

        [JsonProperty]
        public Guid PatientId { get; set; }

        [JsonProperty]
        public Guid HospitalBranchId { get; set; }

        [JsonProperty]
        public Guid HospitalBranchDepartmentId { get; set; }

        [JsonProperty]
        public DateTime AppointmentDateTime { get; set; }

        [JsonProperty]
        public int AppointmentDurationInMins { get; set; }

        [JsonProperty]
        public DoctorAppointmentStatus DoctorAppointmentStatus { get; set; }

        [JsonProperty]
        public DoctorAppointmentPayment DoctorAppointmentPayment { get; set; }

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
