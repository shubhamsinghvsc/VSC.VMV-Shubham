using Microsoft.AspNetCore.Mvc;
using VSC.VMV.Common.Constants;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.CorporateAdmin;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Frontdesk;
using VSC.VMV.Common.Models.Nurse;
using VSC.VMV.Server.Models;

namespace VSC.VMV.Server.Controllers
{
  [Route(RouteMapConstants.BaseControllerRoute)]
  public class PeopleManagementController : Controller
  {
    private readonly ILogger _logger;
    private readonly IPeopleManagementRepository _peopleManagementRepository;

    public PeopleManagementController(IPeopleManagementRepository peopleManagementRepository, ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<PeopleManagementController>();
      _peopleManagementRepository = peopleManagementRepository;
    }

    #region Patient

    [HttpPost]
    [Route(nameof(AddPatient))]
    public IActionResult AddPatient([FromBody] Patient patient)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.AddPatient(patient, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(DeletePatient))]
    public IActionResult DeletePatient([FromQuery] Guid patientId)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.DisablePatient(patientId, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(GetPatients))]
    public IActionResult GetPatients()
    {
      try
      {
        return Ok(_peopleManagementRepository.GetActivePatients());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    #endregion Patient

    #region Doctor

    [HttpPost]
    [Route(nameof(AddDoctor))]
    public IActionResult AddDoctor([FromBody] Doctor doctor)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.AddDoctor(doctor, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(DeleteDoctor))]
    public IActionResult DeleteDoctor([FromQuery] Guid doctorId)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.DisableDoctor(doctorId, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(GetDoctors))]
    public IActionResult GetDoctors()
    {
      try
      {
        return Ok(_peopleManagementRepository.GetActiveDoctors());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    #endregion Doctor

    #region Nurse

    [HttpPost]
    [Route(nameof(AddNurse))]
    public IActionResult AddNurse([FromBody] Nurse nurse)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.AddNurse(nurse, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(DeleteNurse))]
    public IActionResult DeleteNurse([FromQuery] Guid nurseId)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.DisableNurse(nurseId, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(GetNurses))]
    public IActionResult GetNurses()
    {
      try
      {
        return Ok(_peopleManagementRepository.GetActiveNurses());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    #endregion Nurse

    #region CorporateAdmin

    [HttpPost]
    [Route(nameof(AddCorporateAdmin))]
    public IActionResult AddCorporateAdmin([FromBody] CorporateAdmin corporateAdmin)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.AddCorporateAdmin(corporateAdmin, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route(nameof(DeleteCorporateAdmin))]
    public IActionResult DeleteCorporateAdmin([FromQuery] Guid corporateAdminId)
    {
      try
      {
        string errorMessage = "";
        if (_peopleManagementRepository.DisableCorporateAdmin(corporateAdminId, ref errorMessage))
        {
          return Ok();
        }
        else
        {
          return BadRequest(errorMessage);
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // @ SHUBHAM SINGH 

    // 1. OLD GetCorporateAdmins()

    //[HttpGet]
    //[Route(nameof(GetCorporateAdmins))]
    //public IActionResult GetCorporateAdmins()
    //{
    //    try
    //    {
    //        return Ok(_peopleManagementRepository.GetActiveCorporateAdmins());
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    // 1. validated GetCorporateAdminnns method

    [HttpGet]
    [Route(nameof(GetCorporateAdmins))]
    public IActionResult GetCorporateAdmins()
    {
      try
      {
        _logger.LogInformation("Fetching active corporate admins.");

        var corporateAdmins = _peopleManagementRepository.GetActiveCorporateAdmins();

        if (corporateAdmins == null || !corporateAdmins.Any())
        {
          _logger.LogWarning("No active corporate admins found.");
          return NotFound(new ApiResponse<string>
          {
            Success = false,
            Message = "No active corporate admins found.",
            Data = null
          });
        }

        return Ok(new ApiResponse<object>
        {
          Success = true,
          Message = "Corporate admins fetched successfully.",
          Data = corporateAdmins
        });
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "An invalid operation occurred while fetching corporate admins.");
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid operation encountered.",
          Data = null
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while fetching corporate admins.");
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }




    #endregion CorporateAdmin

    #region Frontdesk

    // 2. OLD AddFrontedDesk

    //[HttpPost]
    //[Route(nameof(AddFrontdesk))]
    //public IActionResult AddFrontdesk([FromBody] Frontdesk frontdesk)
    //{
    //    try
    //    {
    //        string errorMessage = "";
    //        if (_peopleManagementRepository.AddFrontdesk(frontdesk, ref errorMessage))
    //        {
    //            return Ok();
    //        }
    //        else
    //        {
    //            return BadRequest(errorMessage);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}


    // 2. Validated AddFrontDesk()

    [HttpPost]
    [Route(nameof(AddFrontdesk))]
    public IActionResult AddFrontdesk([FromBody] Frontdesk frontdesk)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      //if (frontdesk == null)
      //{
      //  return BadRequest(new ApiResponse<string> 
      //  {
      //    Success = false,
      //    Message = "Frontdesk object is null.",
      //    Data = null
      //  });
      //}

      //if (string.IsNullOrWhiteSpace(frontdesk.FirstName))
      //{
      //    return BadRequest(new ApiResponse<string>
      //    {
      //        Success = false,
      //        Message = "Frontdesk first name is required.",
      //        Data = null
      //    });
      //}

      //if (string.IsNullOrWhiteSpace(frontdesk.LastName))
      //{
      //    return BadRequest(new ApiResponse<string>
      //    {
      //        Success = false,
      //        Message = "Frontdesk last name is required.",
      //        Data = null
      //    });
      //}

      try
      {
        _logger.LogInformation("Attempting to add a new frontdesk: {frontdesk}", frontdesk);
        string errorMessage = "";

        if (_peopleManagementRepository.AddFrontdesk(frontdesk, ref errorMessage))
        {
          return Ok(new ApiResponse<object>
          {
            Success = true,
            Message = "Frontdesk added successfully.",
            Data = null
          });
        }
        else
        {
          _logger.LogWarning("Failed to add frontdesk: {ErrorMessage}", errorMessage);
          return BadRequest(new ApiResponse<string>
          {
            Success = false,
            Message = errorMessage,
            Data = null
          });
        }
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "An invalid operation occurred while adding a frontdesk.");
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid operation encountered.",
          Data = null
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while adding a frontdesk.");
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }




    // 3. OLD DeleteFrontDesks()

    //[HttpGet]
    //[Route(nameof(DeleteFrontdesk))]
    //public IActionResult DeleteFrontdesk([FromQuery] Guid frontdeskId)
    //{
    //    try
    //    {
    //        string errorMessage = "";
    //        if (_peopleManagementRepository.DisableFrontdesk(frontdeskId, ref errorMessage))
    //        {
    //            return Ok();
    //        }
    //        else
    //        {
    //            return BadRequest(errorMessage);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    // 3. VALIDATED DeleteFrontDesk method

    [HttpGet]
    [Route(nameof(DeleteFrontdesk))]
    public IActionResult DeleteFrontdesk([FromQuery] Guid frontdeskId)
    {
      if (frontdeskId == Guid.Empty)
      {
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid frontdesk ID provided.",
          Data = null
        });
      }

      try
      {
        _logger.LogInformation("Attempting to disable frontdesk with ID: {FrontdeskId}", frontdeskId);
        string errorMessage = "";

        if (_peopleManagementRepository.DisableFrontdesk(frontdeskId, ref errorMessage))
        {
          return Ok(new ApiResponse<object>
          {
            Success = true,
            Message = "Frontdesk disabled successfully.",
            Data = null
          });
        }
        else
        {
          Console.WriteLine("hey i'm deleteFrontdesk method try - else ");

          _logger.LogWarning("Failed to disable frontdesk: {ErrorMessage}", errorMessage);
          return BadRequest(new ApiResponse<string>
          {
            Success = false,
            Message = errorMessage,
            Data = null
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("hey i'm deleteFrontdesk method");

        _logger.LogError(ex, "An unexpected error occurred while disabling frontdesk with ID: {FrontdeskId}", frontdeskId);
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }


    // 4. old GetFrontDesk()

    //[HttpGet]
    //[Route(nameof(GetFrontdesks))]
    //public IActionResult GetFrontdesks()
    //{
    //    try
    //    {
    //        return Ok(_peopleManagementRepository.GetActiveFrontdesks());
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    // 4. updated GetFrontDeesks 

    [HttpGet]
    [Route(nameof(GetFrontdesks))]
    public IActionResult GetFrontdesks()
    {
      try
      {
        _logger.LogInformation("Fetching active frontdesk employees.");

        var frontdesks = _peopleManagementRepository.GetActiveFrontdesks();

        if (frontdesks == null || !frontdesks.Any())
        {
          _logger.LogWarning("No active frontdesk employees found.");
          return NotFound(new ApiResponse<object>
          {
            Success = false,
            Message = "No active frontdesk employees found.",
            Data = null
          });
        }

        return Ok(new ApiResponse<object>
        {
          Success = true,
          Message = "Frontdesk employees fetched successfully.",
          Data = frontdesks
        });
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "An invalid operation occurred while fetching frontdesk employees.");
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid operation encountered.",
          Data = null
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while fetching frontdesk employees.");
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }


    #endregion Frontdesk

    #region SuperAdmin

    // 5. OLD AddSuperAdmin()

    //[HttpPost]
    //[Route(nameof(AddSuperAdmin))]
    //public IActionResult AddSuperAdmin([FromBody] SuperAdmin superAdmin)
    //{
    //    try
    //    {
    //        string errorMessage = "";
    //        if (_peopleManagementRepository.AddSuperAdmin(superAdmin, ref errorMessage))
    //        {
    //            return Ok();
    //        }
    //        else
    //        {
    //            return BadRequest(errorMessage);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}


    // 5. VALIDATED AddSuperAdmin()

    [HttpPost]
    [Route(nameof(AddSuperAdmin))]
    public IActionResult AddSuperAdmin([FromBody] SuperAdmin superAdmin)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      //if (superAdmin == null)
      //{
      //  return BadRequest(new ApiResponse<string>
      //  {
      //    Success = false,
      //    Message = "SuperAdmin object is null.",
      //    Data = null
      //  });
      //}



      try
      {
        _logger.LogInformation("Attempting to add a new super admin: {SuperAdmin}", superAdmin);
        string errorMessage = "";

        if (_peopleManagementRepository.AddSuperAdmin(superAdmin, ref errorMessage))
        {
          return Ok(new ApiResponse<object>
          {
            Success = true,
            Message = "Super admin added successfully.",
            Data = null
          });
        }
        else
        {
          _logger.LogWarning("Failed to add super admin: {ErrorMessage}", errorMessage);
          return BadRequest(new ApiResponse<string>
          {
            Success = false,
            Message = errorMessage,
            Data = null
          });
        }
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "An invalid operation occurred while adding a super admin.");
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid operation encountered.",
          Data = null
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while adding a super admin.");
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }





    // 6. OLD DeleteSuperAdmin()

    //[HttpGet]
    //[Route(nameof(DeleteSuperAdmin))]
    //public IActionResult DeleteSuperAdmin([FromQuery] Guid superAdminId)
    //{
    //    try
    //    {
    //        string errorMessage = "";
    //        if (_peopleManagementRepository.DisableSuperAdmin(superAdminId, ref errorMessage))
    //        {
    //            return Ok();
    //        }
    //        else
    //        {
    //            return BadRequest(errorMessage);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}


    // 6.  VALIDATED DeleteSuperAdmin()

    [HttpGet]
    [Route(nameof(DeleteSuperAdmin))]
    public IActionResult DeleteSuperAdmin([FromQuery] Guid superAdminId)
    {
      if (superAdminId == Guid.Empty)
      {
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid super admin ID provided.",
          Data = null
        });
      }

      try
      {
        _logger.LogInformation("Attempting to disable super admin with ID: {SuperAdminId}", superAdminId);
        string errorMessage = "";

        if (_peopleManagementRepository.DisableSuperAdmin(superAdminId, ref errorMessage))
        {
          return Ok(new ApiResponse<object>
          {
            Success = true,
            Message = "Super admin disabled successfully.",
            Data = null
          });
        }
        else
        {
          _logger.LogWarning("Failed to disable super admin: {ErrorMessage}", errorMessage);
          return BadRequest(new ApiResponse<string>
          {
            Success = false,
            Message = errorMessage,
            Data = null
          });
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while disabling super admin with ID: {SuperAdminId}", superAdminId);
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }


    // 7. OLD GetSuperAdmins())

    //[HttpGet]
    //[Route(nameof(GetSuperAdmins))]
    //public IActionResult GetSuperAdmins()
    //{
    //    try
    //    {
    //        return Ok(_peopleManagementRepository.GetActiveSuperAdmins());
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}


    // 7. VALIDate GetSuperAdmins() 

    [HttpGet]
    [Route(nameof(GetSuperAdmins))]
    public IActionResult GetSuperAdmins()
    {
      try
      {
        _logger.LogInformation("Fetching active super admins.");

        var superAdmins = _peopleManagementRepository.GetActiveSuperAdmins();

        if (superAdmins == null || !superAdmins.Any())
        {
          _logger.LogWarning("No active super admins found.");
          return NotFound(new ApiResponse<string>
          {
            Success = false,
            Message = "No active super admins found.",
            Data = null
          });
        }

        return Ok(new ApiResponse<object>
        {
          Success = true,
          Message = "Super admins fetched successfully.",
          Data = superAdmins
        });
      }
      catch (InvalidOperationException ex)
      {
        _logger.LogError(ex, "An invalid operation occurred while fetching super admins.");
        return BadRequest(new ApiResponse<string>
        {
          Success = false,
          Message = "Invalid operation encountered.",
          Data = null
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An unexpected error occurred while fetching super admins.");
        return StatusCode(500, new ApiResponse<string>
        {
          Success = false,
          Message = "An internal server error occurred.",
          Data = null
        });
      }
    }


    #endregion SuperAdmin
  }
}
