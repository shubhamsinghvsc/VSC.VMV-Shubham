using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VSC.VMV.Common.Models.Base
{
  [JsonObject(MemberSerialization.OptIn)]
  [Serializable]
  public abstract class ContactInfoBase
  {
    [JsonProperty]
    [Required(ErrorMessage = "Address is Required field.")]
    public string AddressLine1 { get; set; } = "";

    [JsonProperty]
    public string AddressLine2 { get; set; } = "";

    [JsonProperty]
    [Required(ErrorMessage = "City is Required.")]
    public string City { get; set; } = "";

    [JsonProperty]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid Zip Code. (6 digits Required)")]
    public string ZipCode { get; set; } = "";

    [JsonProperty]
    public string ContactName { get; set; } = "";

    [JsonProperty]
    [Phone(ErrorMessage = "Invalid Phone Number.")]
    [Required(ErrorMessage = "Phone Number is Required")]
    public string PhoneNumber { get; set; } = "";

    [JsonProperty]
    public string EmergencyPhoneNumber { get; set; } = "";

    [JsonProperty]
    public string EmergencyContactName { get; set; } = "";

    [JsonProperty]
    public string WhatsAppNumber { get; set; } = "";

    [JsonProperty]
    public string SecondaryPhoneNumber { get; set; } = "";

    [JsonProperty]
    [EmailAddress(ErrorMessage = "Invalid Email address format.")]
    public string MailId { get; set; } = "";

    [JsonProperty]
    [EmailAddress(ErrorMessage = "Invalid Recovery Email address format.")]
    public string RecoveryMailId { get; set; } = "";

  }
}
