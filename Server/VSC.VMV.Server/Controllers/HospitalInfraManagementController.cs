using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using VSC.VMV.Common.Constants;
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Common.Models.CorporateAdmin;
using VSC.VMV.Common.Models.Doctor;
using VSC.VMV.Common.Models.Frontdesk;
using VSC.VMV.Common.Models.Hospitals;
using VSC.VMV.Common.Models.Nurse;
using VSC.VMV.Repository;

namespace VSC.VMV.Server.Controllers
{
    [Route(RouteMapConstants.BaseControllerRoute)]
    public class HospitalInfraManagementController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHospitalManagementRepository _hospitalManagementRepository;

        public HospitalInfraManagementController(IHospitalManagementRepository hospitalManagementRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PeopleManagementController>();
            _hospitalManagementRepository = hospitalManagementRepository;
        }

        #region Corporate

        [HttpPost]
        [Route(nameof(AddHospitalCorporate))]
        public IActionResult AddHospitalCorporate([FromBody] HospitalCorporate hospitalCorporate)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.AddHospitalCorporate(hospitalCorporate, ref errorMessage))
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
        [Route(nameof(DeleteCorporate))]
        public IActionResult DeleteCorporate([FromQuery] Guid corporateId)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.DisableCorporate(corporateId, ref errorMessage))
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
        [Route(nameof(GetActiveCorporates))]
        public IActionResult GetActiveCorporates()
        {
            try
            {
                return Ok(_hospitalManagementRepository.GetActiveCorporates());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Corporate

        #region BranchHospital

        [HttpPost]
        [Route(nameof(AddHospitalBranch))]
        public IActionResult AddHospitalBranch([FromBody] HospitalBranch hospitalBranch)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.AddHospitalBranch(hospitalBranch, ref errorMessage))
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
        [Route(nameof(DeleteHospitalBranch))]
        public IActionResult DeleteHospitalBranch([FromQuery] Guid hospitalBranchId)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.DisableHospitalBranch(hospitalBranchId, ref errorMessage))
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
        [Route(nameof(GetActiveBranches))]
        public IActionResult GetActiveBranches([FromQuery] Guid corporateId)
        {
            try
            {
                return Ok(_hospitalManagementRepository.GetActiveBranches(corporateId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion BranchHospital


        #region HospitalBranchDepartment

        [HttpPost]
        [Route(nameof(AddHospitalBranchDepartment))]
        public IActionResult AddHospitalBranchDepartment([FromBody] HospitalBranchDepartment hospitalBranchDepartment)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.AddHospitalBranchDepartment(hospitalBranchDepartment, ref errorMessage))
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
        [Route(nameof(DeleteHospitalBranchDepartment))]
        public IActionResult DeleteHospitalBranchDepartment([FromQuery] Guid hospitalBranchDepartmentId)
        {
            try
            {
                string errorMessage = "";
                if (_hospitalManagementRepository.DisableHospitalBranchDepartment(hospitalBranchDepartmentId, ref errorMessage))
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
        [Route(nameof(GetActiveHospitalBrancheDepartments))]
        public IActionResult GetActiveHospitalBrancheDepartments([FromQuery] Guid hospitalBranchId)
        {
            try
            {
                return Ok(_hospitalManagementRepository.GetActiveHospitalBrancheDepartments(hospitalBranchId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion HospitalBranchDepartment

    }
}
