using VSC.VMV.Common.Models.Mapping;

namespace VSC.VMV.Common.Interfaces
{
    public interface IMappingsRepository
    {
        bool AddCorporateAdminMapping(CorporateAdminMapping corporateAdminMapping, ref string errorMessage);

        bool DisableCorporateAdminMapping(Guid corporateAdminMappingId, ref string errorMessage);

        List<CorporateAdminMapping> GetActiveCorporateAdminMappings();


        bool AddDoctorMapping(DoctorMapping doctorMapping, ref string errorMessage);

        bool DisableDoctorMapping(Guid doctorMappingId, ref string errorMessage);

        List<DoctorMapping> GetActiveDoctorMappings();


        bool AddFrontdeskMapping(FrontdeskMapping frontdeskMapping, ref string errorMessage);

        bool DisableFrontdeskMapping(Guid frontdeskMappingId, ref string errorMessage);

        List<FrontdeskMapping> GetActiveFrontdeskMappings();


        bool AddNurseMapping(NurseMapping nurseMapping, ref string errorMessage);
        
        bool DisableNurseMapping(Guid nurseMappingId, ref string errorMessage);
        
        List<NurseMapping> GetActiveNurseMappings();
    }
}