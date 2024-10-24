using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sperry.ToolDataManager.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Helper;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Mapping;

namespace VSC.VMV.Repository
{
    public class MappingsRepository : IMappingsRepository
    {
        private const int RETRY_COUNT = 3;
        private readonly ILogger<MappingsRepository> _logger;
        private VMVDBConnectionInfo _vmvDBConnectionInfo;

        public MappingsRepository(VMVDBConnectionInfo vmvDBConnectionInfo, ILoggerFactory logger)
        {
            _vmvDBConnectionInfo = vmvDBConnectionInfo;
            _logger = logger.CreateLogger<MappingsRepository>();
        }


        #region CorporateAdminMapping

        public bool AddCorporateAdminMapping(CorporateAdminMapping corporateAdminMapping, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        CorporateAdminMapping existingCorporateAdminMapping = mxADBContext.CorporateAdminMappings.FirstOrDefault(u => u.Id.Equals(corporateAdminMapping.Id));
                        if (existingCorporateAdminMapping == null)
                        {
                            mxADBContext.CorporateAdminMappings.Add(corporateAdminMapping);
                        }
                        else
                        {
                            corporateAdminMapping.Id = existingCorporateAdminMapping.Id;
                            mxADBContext.CorporateAdminMappings.Update(corporateAdminMapping);
                        }
                        int rowsAffected = mxADBContext.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Respository: Failed to save corporate admin mappings information";
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

        public bool DisableCorporateAdminMapping(Guid corporateAdminMappingId, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        CorporateAdminMapping existingCorporateAdminMapping = mxADBContext.CorporateAdminMappings.FirstOrDefault(u => u.Id == corporateAdminMappingId);
                        if (existingCorporateAdminMapping != null)
                        {
                            existingCorporateAdminMapping.IsActive = false;
                            mxADBContext.CorporateAdminMappings.Update(existingCorporateAdminMapping);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable corporate admin mapping with id {corporateAdminMappingId}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {corporateAdminMappingId}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<CorporateAdminMapping> GetActiveCorporateAdminMappings()
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.CorporateAdminMappings.Where(u => u.IsActive).Include(p => p.CorporateAdminAvailablities).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<CorporateAdminMapping>();
        }

        #endregion CorporateAdminMapping

        #region DoctorMapping

        public bool AddDoctorMapping(DoctorMapping doctorMapping, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        DoctorMapping existingDoctorMapping = mxADBContext.DoctorMappings.FirstOrDefault(u => u.Id.Equals(doctorMapping.Id));
                        if (existingDoctorMapping == null)
                        {
                            mxADBContext.DoctorMappings.Add(doctorMapping);
                        }
                        else
                        {
                            doctorMapping.Id = existingDoctorMapping.Id;
                            mxADBContext.DoctorMappings.Update(doctorMapping);
                        }
                        int rowsAffected = mxADBContext.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Respository: Failed to save doctor mappings information";
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

        public bool DisableDoctorMapping(Guid doctorMappingId, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        DoctorMapping existingDoctorMapping = mxADBContext.DoctorMappings.FirstOrDefault(u => u.Id == doctorMappingId);
                        if (existingDoctorMapping != null)
                        {
                            existingDoctorMapping.IsActive = false;
                            mxADBContext.DoctorMappings.Update(existingDoctorMapping);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable doctor mapping with id {doctorMappingId}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {doctorMappingId}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<DoctorMapping> GetActiveDoctorMappings()
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.DoctorMappings.Where(u => u.IsActive).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<DoctorMapping>();
        }

        #endregion DoctorMapping

        #region FrontdeskMapping

        public bool AddFrontdeskMapping(FrontdeskMapping frontdeskMapping, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        FrontdeskMapping existingFrontdeskMapping = mxADBContext.FrontdeskMappings.FirstOrDefault(u => u.Id.Equals(frontdeskMapping.Id));
                        if (existingFrontdeskMapping == null)
                        {
                            mxADBContext.FrontdeskMappings.Add(frontdeskMapping);
                        }
                        else
                        {
                            frontdeskMapping.Id = existingFrontdeskMapping.Id;
                            mxADBContext.FrontdeskMappings.Update(frontdeskMapping);
                        }
                        int rowsAffected = mxADBContext.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Respository: Failed to save frontdesk mappings information";
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

        public bool DisableFrontdeskMapping(Guid frontdeskMappingId, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        FrontdeskMapping existingFrontdeskMapping = mxADBContext.FrontdeskMappings.FirstOrDefault(u => u.Id == frontdeskMappingId);
                        if (existingFrontdeskMapping != null)
                        {
                            existingFrontdeskMapping.IsActive = false;
                            mxADBContext.FrontdeskMappings.Update(existingFrontdeskMapping);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable frontdesk mapping with id {frontdeskMappingId}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {frontdeskMappingId}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<FrontdeskMapping> GetActiveFrontdeskMappings()
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.FrontdeskMappings.Where(u => u.IsActive).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<FrontdeskMapping>();
        }

        #endregion FrontdeskMapping

        #region NurseMapping

        public bool AddNurseMapping(NurseMapping nurseMapping, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        NurseMapping existingNurseMapping = mxADBContext.NurseMappings.FirstOrDefault(u => u.Id.Equals(nurseMapping.Id));
                        if (existingNurseMapping == null)
                        {
                            mxADBContext.NurseMappings.Add(nurseMapping);
                        }
                        else
                        {
                            nurseMapping.Id = existingNurseMapping.Id;
                            mxADBContext.NurseMappings.Update(nurseMapping);
                        }
                        int rowsAffected = mxADBContext.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            errorMessage = "Respository: Failed to save nurse mappings information";
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

        public bool DisableNurseMapping(Guid nurseMappingId, ref string errorMessage)
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        NurseMapping existingNurseMapping = mxADBContext.NurseMappings.FirstOrDefault(u => u.Id == nurseMappingId);
                        if (existingNurseMapping != null)
                        {
                            existingNurseMapping.IsActive = false;
                            mxADBContext.NurseMappings.Update(existingNurseMapping);
                            mxADBContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            errorMessage = $"Respository: Failed to disable nurse mapping with id {nurseMappingId}";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Respository: {ex.Message} \n Id: {nurseMappingId}";
                    exception = ex;
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return false;
        }

        public List<NurseMapping> GetActiveNurseMappings()
        {
            Exception exception = null;
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                try
                {
                    using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
                    {
                        mxADBContext.Database.EnsureCreated();
                        return mxADBContext.NurseMappings.Where(u => u.IsActive).ToList();
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                    _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
                    _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
                }
            }
            return new List<NurseMapping>();
        }

        #endregion NurseMapping
    }
}
