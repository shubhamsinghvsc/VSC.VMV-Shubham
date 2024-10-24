using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using VSC.VMV.Common.Enums;
using VSC.VMV.Common.Interfaces;

namespace VSC.VMV.Common.Models.Base
{
  [JsonObject(MemberSerialization.OptIn)]
  public abstract class PeopleMetaData : ContactInfoBase, IAuditTracking
  {
    [JsonProperty]
    [CustomValidation(typeof(PeopleMetaData), nameof(ValidateGuid))]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonProperty]
    [Required(ErrorMessage = "Unique Identifier is required")]
    public string UniqueIdentifier { get; set; } = "";

    [JsonProperty]
    [Required(ErrorMessage = "First Name is required")]
    [MaxLength(100, ErrorMessage = "First Name cannot exceed 100 characters")]
    public string FirstName { get; set; } = "";

    [JsonProperty]
    [Required(ErrorMessage = "Last Name is required")]
    [MaxLength(100, ErrorMessage = "Last Name cannot exceed 100 characters")]
    public string LastName { get; set; } = "";

    [JsonProperty]
    public bool IsActive { get; set; } = true;

    [JsonProperty]
    [Required(ErrorMessage = "Date of Birth is required")]
    [CustomValidation(typeof(PeopleMetaData), nameof(ValidateBirthDate))]
    public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;

    [JsonProperty]
    [Required(ErrorMessage = "Gender is required")]
    public GenderEnum Gender { get; set; } = GenderEnum.Unknown;

    [JsonProperty]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = "";

    [JsonProperty]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    [JsonProperty]
    [Required(ErrorMessage = "CreatedBy is required")]
    public string CreatedBy { get; set; } = "";

    [JsonProperty]
    [CustomValidation(typeof(PeopleMetaData), nameof(ValidateGuid))]
    public Guid CreatedById { get; set; }

    [JsonProperty]
    [Required(ErrorMessage = "User Type is required")]
    public UserTypeEnum CreatedByUserTypeEnum { get; set; }

    public static ValidationResult? ValidateGuid(Guid id, ValidationContext context)
    {
      if (id == Guid.Empty)
      {
        return new ValidationResult("Guid cannot be empty");
      }
      return ValidationResult.Success;
    }

    public static ValidationResult? ValidateBirthDate(DateOnly date, ValidationContext context)
    {

      if (date == DateOnly.MinValue)
      {
        return new ValidationResult("Invalid Date of Birth: Date cannot be uninitialized.");
      }

      if (date > DateOnly.FromDateTime(DateTime.UtcNow))
      {
        return new ValidationResult("Invalid Date of Birth: Date cannot be in the future.");
      }

      if (date < DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-150)))
      {
        return new ValidationResult("Invalid Date of Birth: Date cannot be more than 150 years in the past.");
      }

      return ValidationResult.Success;
    }
  }
}
