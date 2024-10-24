using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sperry.ToolDataManager.Core.Common.Models;
using VSC.VMV.Common.Models.CorporateAdmin;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Frontdesk;
using VSC.VMV.Common.Models.Hospitals;
using VSC.VMV.Common.Models.Mapping;
using VSC.VMV.Common.Models.Mapping.Availablity;
using VSC.VMV.Common.Models.Nurse;

namespace VSC.VMV.Repository
{
  public partial class VMVDBContext : DbContext
  {
    private VMVDBConnectionInfo _vmvDBConnectionInfo;

    public VMVDBContext(VMVDBConnectionInfo vmvDBConnectionInfo)
    {
      _vmvDBConnectionInfo = vmvDBConnectionInfo;
      base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      Database.EnsureCreated();
    }

    public DbSet<HospitalCorporate> HospitalCorporates { get; set; }

    public DbSet<HospitalBranch> HospitalBranches { get; set; }

    public DbSet<HospitalBranchDepartment> HospitalBranchDepartments { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Nurse> Nurses { get; set; }

    public DbSet<Frontdesk> Frontdesks { get; set; }

    public DbSet<CorporateAdmin> CorporateAdmins { get; set; }

    public DbSet<SuperAdmin> SuperAdmins { get; set; }

    public DbSet<CorporateAdminMapping> CorporateAdminMappings { get; set; }

    public DbSet<DoctorMapping> DoctorMappings { get; set; }

    public DbSet<FrontdeskMapping> FrontdeskMappings { get; set; }

    public DbSet<NurseMapping> NurseMappings { get; set; }

    public DbSet<CorporateAdminAvailablity> CorporateAdminAvailablities { get; set; }

    public DbSet<DoctorAvailablity> DoctorAvailablities { get; set; }

    public DbSet<FrontdeskAvailablity> FrontdeskAvailablities { get; set; }

    public DbSet<NurseAvailablity> NurseAvailablities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(
            _vmvDBConnectionInfo.ConnectionString,
            sqlOptions =>
            {
              sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
              sqlOptions.CommandTimeout(300);
              // sqlOptions.UseDateOnlyTimeOnly();
            });
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Doctor>().Property(p => p.Specialization)
                                      .HasConversion(v => JsonConvert.SerializeObject(v),
                                                      v => JsonConvert.DeserializeObject<List<string>>(v));
      base.OnModelCreating(modelBuilder);
    }
  }
}
