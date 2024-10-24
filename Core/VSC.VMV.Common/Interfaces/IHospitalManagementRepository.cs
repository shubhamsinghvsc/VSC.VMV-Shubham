using VSC.VMV.Common.Models.Hospitals;

namespace VSC.VMV.Common.Interfaces
{
    public interface IHospitalManagementRepository
    {
        bool AddHospitalCorporate(HospitalCorporate hospitalCorporate, ref string errorMessage);

        bool DisableCorporate(Guid id, ref string errorMessage);

        List<HospitalCorporate> GetActiveCorporates();


        bool AddHospitalBranch(HospitalBranch hospitalBranch, ref string errorMessage);

        bool DisableHospitalBranch(Guid id, ref string errorMessage);

        List<HospitalBranch> GetActiveBranches(Guid corporateId);

        bool AddHospitalBranchDepartment(HospitalBranchDepartment hospitalBranchDepartment, ref string errorMessage);

        bool DisableHospitalBranchDepartment(Guid id, ref string errorMessage);

        List<HospitalBranchDepartment> GetActiveHospitalBrancheDepartments(Guid hospitalBranchId);


        bool IsConnected(ref string errorMessage);
    }
}