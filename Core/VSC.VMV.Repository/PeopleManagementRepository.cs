using Microsoft.Extensions.Logging;
using Sperry.ToolDataManager.Core.Common.Models;
using VSC.VMV.Common.Helper;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.CorporateAdmin;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Frontdesk;
using VSC.VMV.Common.Models.Nurse;

namespace VSC.VMV.Repository
{
  public class PeopleManagementRepository : IPeopleManagementRepository
  {
    private const int RETRY_COUNT = 3;
    private readonly ILogger<PeopleManagementRepository> _logger;
    private VMVDBConnectionInfo _vmvDBConnectionInfo;

    public PeopleManagementRepository(VMVDBConnectionInfo vmvDBConnectionInfo, ILoggerFactory logger)
    {
      _vmvDBConnectionInfo = vmvDBConnectionInfo;
      _logger = logger.CreateLogger<PeopleManagementRepository>();
    }

    #region Patient

    public bool AddPatient(Patient patient, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(patient.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              Patient existingPatient = mxADBContext.Patients.FirstOrDefault(u => u.Id == patient.Id || u.UniqueIdentifier.ToLower() == patient.UniqueIdentifier.ToLower());
              if (existingPatient == null)
              {
                mxADBContext.Patients.Add(patient);
              }
              else
              {
                patient.Id = existingPatient.Id;
                mxADBContext.Patients.Update(patient);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save patient information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty";
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

    public bool DisablePatient(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            Patient existingPatient = mxADBContext.Patients.FirstOrDefault(u => u.Id == id);
            if (existingPatient != null)
            {
              existingPatient.IsActive = false;
              mxADBContext.Patients.Update(existingPatient);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the patient with id {id}";
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

    public List<Patient> GetActivePatients()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.Patients.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<Patient>();
    }

    #endregion Patient

    #region Doctor

    public bool AddDoctor(Doctor doctor, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(doctor.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              doctor.Qualifications.ForEach(q => q.DoctorId = doctor.Id);
              Doctor existingDoctor = mxADBContext.Doctors.FirstOrDefault(u => u.Id == doctor.Id || u.UniqueIdentifier.ToLower() == doctor.UniqueIdentifier.ToLower());
              if (existingDoctor == null)
              {
                mxADBContext.Doctors.Add(doctor);
              }
              else
              {
                doctor.Id = existingDoctor.Id;
                mxADBContext.Doctors.Update(doctor);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save doctor information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty for doctor";
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

    public bool DisableDoctor(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            Doctor existingDoctor = mxADBContext.Doctors.FirstOrDefault(u => u.Id == id);
            if (existingDoctor != null)
            {
              existingDoctor.IsActive = false;
              mxADBContext.Doctors.Update(existingDoctor);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the doctor with id {id}";
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

    public List<Doctor> GetActiveDoctors()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.Doctors.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<Doctor>();
    }

    #endregion Doctor

    #region Nurse

    public bool AddNurse(Nurse nurse, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(nurse.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              Nurse existingNurse = mxADBContext.Nurses.FirstOrDefault(u => u.Id == nurse.Id || u.UniqueIdentifier.ToLower() == nurse.UniqueIdentifier.ToLower());
              if (existingNurse == null)
              {
                mxADBContext.Nurses.Add(nurse);
              }
              else
              {
                nurse.Id = existingNurse.Id;
                mxADBContext.Nurses.Update(nurse);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save nurse information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty";
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

    public bool DisableNurse(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            Nurse existingNurse = mxADBContext.Nurses.FirstOrDefault(u => u.Id == id);
            if (existingNurse != null)
            {
              existingNurse.IsActive = false;
              mxADBContext.Nurses.Update(existingNurse);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the nurse with id {id}";
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

    public List<Nurse> GetActiveNurses()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.Nurses.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<Nurse>();
    }

    #endregion Nurse

    #region Frontdesk

    public bool AddFrontdesk(Frontdesk frontdesk, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(frontdesk.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              Frontdesk existingFrontdesk = mxADBContext.Frontdesks.FirstOrDefault(u => u.Id == frontdesk.Id || u.UniqueIdentifier.ToLower() == frontdesk.UniqueIdentifier.ToLower());
              if (existingFrontdesk == null)
              {
                mxADBContext.Frontdesks.Add(frontdesk);
              }
              else
              {
                frontdesk.Id = existingFrontdesk.Id;
                mxADBContext.Frontdesks.Update(frontdesk);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save frontdesk information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty";
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

    public bool DisableFrontdesk(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            Frontdesk existingFrontdesk = mxADBContext.Frontdesks.FirstOrDefault(u => u.Id == id);
            if (existingFrontdesk != null)
            {
              existingFrontdesk.IsActive = false;
              mxADBContext.Frontdesks.Update(existingFrontdesk);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the frontdesk with id {id}";
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

    public List<Frontdesk> GetActiveFrontdesks()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.Frontdesks.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<Frontdesk>();
    }

    #endregion Frontdesk

    #region CorporateAdmin

    public bool AddCorporateAdmin(CorporateAdmin corporateAdmin, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(corporateAdmin.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              CorporateAdmin existingCorporateAdmin = mxADBContext.CorporateAdmins.FirstOrDefault(u => u.Id == corporateAdmin.Id || u.UniqueIdentifier.ToLower() == corporateAdmin.UniqueIdentifier.ToLower());
              if (existingCorporateAdmin == null)
              {
                mxADBContext.CorporateAdmins.Add(corporateAdmin);
              }
              else
              {
                corporateAdmin.Id = existingCorporateAdmin.Id;
                mxADBContext.CorporateAdmins.Update(corporateAdmin);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save corporateAdmin information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty";
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

    public bool DisableCorporateAdmin(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            CorporateAdmin existingCorporateAdmin = mxADBContext.CorporateAdmins.FirstOrDefault(u => u.Id == id);
            if (existingCorporateAdmin != null)
            {
              existingCorporateAdmin.IsActive = false;
              mxADBContext.CorporateAdmins.Update(existingCorporateAdmin);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the corporateAdmin with id {id}";
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

    public List<CorporateAdmin> GetActiveCorporateAdmins()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.CorporateAdmins.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<CorporateAdmin>();
    }

    #endregion CorporateAdmin

    #region SuperAdmin

    public bool AddSuperAdmin(SuperAdmin superAdmin, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          if (!string.IsNullOrEmpty(superAdmin.UniqueIdentifier))
          {
            using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
            {
              mxADBContext.Database.EnsureCreated();
              SuperAdmin existingSuperAdmin = mxADBContext.SuperAdmins.FirstOrDefault(u => u.Id == superAdmin.Id || u.UniqueIdentifier.ToLower() == superAdmin.UniqueIdentifier.ToLower());
              if (existingSuperAdmin == null)
              {
                mxADBContext.SuperAdmins.Add(superAdmin);
              }
              else
              {
                superAdmin.Id = existingSuperAdmin.Id;
                mxADBContext.SuperAdmins.Update(superAdmin);
              }
              int rowsAffected = mxADBContext.SaveChanges();
              if (rowsAffected > 0)
              {
                return true;
              }
              else
              {
                errorMessage = "Respository: Failed to save superAdmin information";
              }
            }
          }
          else
          {
            errorMessage = "Respository: The unique identifier information cannot be empty";
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

    public bool DisableSuperAdmin(Guid id, ref string errorMessage)
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            SuperAdmin existingSuperAdmin = mxADBContext.SuperAdmins.FirstOrDefault(u => u.Id == id);
            if (existingSuperAdmin != null)
            {
              existingSuperAdmin.IsActive = false;
              mxADBContext.SuperAdmins.Update(existingSuperAdmin);
              mxADBContext.SaveChanges();
              return true;
            }
            else
            {
              errorMessage = $"Respository: Failed to disable the superAdmin with id {id}";
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

    public List<SuperAdmin> GetActiveSuperAdmins()
    {
      Exception exception = null;
      for (int i = 0; i < RETRY_COUNT; i++)
      {
        try
        {
          using (VMVDBContext mxADBContext = new VMVDBContext(_vmvDBConnectionInfo))
          {
            mxADBContext.Database.EnsureCreated();
            return mxADBContext.SuperAdmins.Where(u => u.IsActive).ToList();
          }
        }
        catch (Exception ex)
        {
          exception = ex;
          _logger.LogCritical($"Connection: {_vmvDBConnectionInfo.ConnectionString}");
          _logger.LogError(ex, MethodNameHelper.GetMethodContextName());
        }
      }
      return new List<SuperAdmin>();
    }

    #endregion SuperAdmin

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
