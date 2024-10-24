using Microsoft.Extensions.Logging;
using Sperry.ToolDataManager.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Helper;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.Hospitals;

namespace VSC.VMV.Repository
{
    public class HospitalManagementRepository : IHospitalManagementRepository
    {
        private const int RETRY_COUNT = 3;
        private readonly ILogger<HospitalManagementRepository> _logger;
        private VMVDBConnectionInfo _vmvDBConnectionInfo;

        public HospitalManagementRepository(VMVDBConnectionInfo vmvDBConnectionInfo, ILoggerFactory logger)
        {
            _vmvDBConnectionInfo = vmvDBConnectionInfo;
            _logger = logger.CreateLogger<HospitalManagementRepository>();
        }

        #region Corporate

        public bool AddHospitalCorporate(HospitalCorporate hospitalCorporate, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        HospitalCorporate existingHospitalCorporate = mxADBContext.HospitalCorporates.FirstOrDefault(u => u.Id.Equals(hospitalCorporate.Id));
                        if (existingHospitalCorporate == null)
                        {
                            mxADBContext.HospitalCorporates.Add(hospitalCorporate);
                        }
                        else
                        {
                            hospitalCorporate.Id = existingHospitalCorporate.Id;
                            mxADBContext.HospitalCorporates.Update(hospitalCorporate);
                        }
                        int rowsAffected = mxADBContext.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Respository: Failed to save hospital corporate";
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public bool DisableCorporate(Guid id, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        HospitalCorporate existingHospitalCorporate = mxADBContext.HospitalCorporates.FirstOrDefault(u => u.Id == id);
                        if (existingHospitalCorporate != null)
                        {
                            existingHospitalCorporate.IsActive = false;
                            mxADBContext.HospitalCorporates.Update(existingHospitalCorporate);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable the hospital corporate with id {id}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {id}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<HospitalCorporate> GetActiveCorporates()
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.HospitalCorporates.Where(u => u.IsActive).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<HospitalCorporate>();
        }

        #endregion Corporate

        #region BranchHospital

        public bool AddHospitalBranch(HospitalBranch hospitalBranch, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        if (mxADBContext.HospitalCorporates.Any(h => h.Id == hospitalBranch.HospitalCorporateId))
                        {
                            HospitalBranch existingHospitalBranch = mxADBContext.HospitalBranches.FirstOrDefault(u => u.Id.Equals(hospitalBranch.Id));
                            if (existingHospitalBranch == null)
                            {
                                mxADBContext.HospitalBranches.Add(hospitalBranch);
                            }
                            else
                            {
                                hospitalBranch.Id = existingHospitalBranch.Id;
                                mxADBContext.HospitalBranches.Update(hospitalBranch);
                            }
                            int rowsAffected = mxADBContext.SaveChanges();
                            if (rowsAffected > 0)
                            {
                                return true;
                            }
                            else
                            {
                                errorMessage = "Respository: Failed to save hospital branch";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public bool DisableHospitalBranch(Guid id, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        HospitalBranch existingHospitalBranch = mxADBContext.HospitalBranches.FirstOrDefault(u => u.Id == id);
                        if (existingHospitalBranch != null)
                        {
                            existingHospitalBranch.IsActive = false;
                            mxADBContext.HospitalBranches.Update(existingHospitalBranch);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable the hospital branch with id {id}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {id}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<HospitalBranch> GetActiveBranches(Guid corporateId)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.HospitalBranches.Where(u => u.IsActive && u.HospitalCorporateId == corporateId).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<HospitalBranch>();
        }

        #endregion BranchHospital

        #region HospitalBranchDepartment

        public bool AddHospitalBranchDepartment(HospitalBranchDepartment hospitalBranchDepartment, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        if (mxADBContext.HospitalBranches.Any(h => h.Id == hospitalBranchDepartment.HospitalBranchId))
                        {
                            HospitalBranchDepartment existingHospitalBranchDepartment = mxADBContext.HospitalBranchDepartments.FirstOrDefault(u => u.Id.Equals(hospitalBranchDepartment.Id));
                            if (existingHospitalBranchDepartment == null)
                            {
                                mxADBContext.HospitalBranchDepartments.Add(hospitalBranchDepartment);
                            }
                            else
                            {
                                hospitalBranchDepartment.Id = existingHospitalBranchDepartment.Id;
                                mxADBContext.HospitalBranchDepartments.Update(hospitalBranchDepartment);
                            }
                            int rowsAffected = mxADBContext.SaveChanges();
                            if (rowsAffected > 0)
                            {
                                return true;
                            }
                            else
                            {
                                errorMessage = "Respository: Failed to save hospital branch department";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public bool DisableHospitalBranchDepartment(Guid id, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        HospitalBranchDepartment existingHospitalBranchDepartment = mxADBContext.HospitalBranchDepartments.FirstOrDefault(u => u.Id == id);
                        if (existingHospitalBranchDepartment != null)
                        {
                            existingHospitalBranchDepartment.IsActive = false;
                            mxADBContext.HospitalBranchDepartments.Update(existingHospitalBranchDepartment);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable the hospital branch department with id {id}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {id}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<HospitalBranchDepartment> GetActiveHospitalBrancheDepartments(Guid hospitalBranchId)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.HospitalBranchDepartments.Where(u => u.IsActive && u.HospitalBranchId == hospitalBranchId).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<HospitalBranchDepartment>();
        }

        #endregion HospitalBranchDepartment

        public bool IsConnected(ref string errorMessage)
        {
            try
            {
                using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                {
                    mxADBContext.Database.EnsureCreated();
                    return mxADBContext.Database.CanConnect();
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"{ex.Message}\n{ex.StackTrace}";
                _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
            }
            return false;
        }
    }
}
