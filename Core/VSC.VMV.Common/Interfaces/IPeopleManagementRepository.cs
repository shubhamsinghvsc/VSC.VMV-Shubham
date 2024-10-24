using VSC.VMV.Common.Models.CorporateAdmin;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Frontdesk;
using VSC.VMV.Common.Models.Nurse;

namespace VSC.VMV.Common.Interfaces
{
    public interface IPeopleManagementRepository
    {
        bool AddDoctor(Doctor doctor, ref string errorMessage);
        
        bool DisableDoctor(Guid id, ref string errorMessage);

        List<Doctor> GetActiveDoctors();


        bool AddPatient(Patient patient, ref string errorMessage);

        bool DisablePatient(Guid id, ref string errorMessage);
        
        List<Patient> GetActivePatients();

        bool AddNurse(Nurse nurse, ref string errorMessage);

        bool DisableNurse(Guid id, ref string errorMessage);

        List<Nurse> GetActiveNurses();

        bool AddFrontdesk(Frontdesk frontdesk, ref string errorMessage);

        bool DisableFrontdesk(Guid id, ref string errorMessage);

        List<Frontdesk> GetActiveFrontdesks();

        bool AddCorporateAdmin(CorporateAdmin corporateAdmin, ref string errorMessage);

        bool DisableCorporateAdmin(Guid id, ref string errorMessage);

        List<CorporateAdmin> GetActiveCorporateAdmins();

        bool AddSuperAdmin(SuperAdmin superAdmin, ref string errorMessage);

        bool DisableSuperAdmin(Guid id, ref string errorMessage);

        List<SuperAdmin> GetActiveSuperAdmins();


        bool IsConnected(ref string errorMessage);
    }
}