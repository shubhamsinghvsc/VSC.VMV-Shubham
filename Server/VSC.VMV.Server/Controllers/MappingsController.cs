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
using VSC.VMV.Common.Models.Mapping;
using VSC.VMV.Common.Models.Nurse;
using VSC.VMV.Repository;

namespace VSC.VMV.Server.Controllers
{
    public class MappingsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMappingsRepository _mappingsRepository;

        public MappingsController(IMappingsRepository mappingsRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MappingsController>();
            _mappingsRepository = mappingsRepository;
        }

        #region CorporateAdminMapping

        [HttpPost]
        [Route(nameof(AddCorporateAdminMapping))]
        public IActionResult AddCorporateAdminMapping([FromBody] CorporateAdminMapping corporateAdminMapping)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.AddCorporateAdminMapping(corporateAdminMapping, ref errorMessage))
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
        [Route(nameof(DeleteCorporateAdminMapping))]
        public IActionResult DeleteCorporateAdminMapping([FromQuery] Guid corporateAdminMappingId)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.DisableCorporateAdminMapping(corporateAdminMappingId, ref errorMessage))
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
        [Route(nameof(GetActiveCorporateAdminMappings))]
        public IActionResult GetActiveCorporateAdminMappings()
        {
            try
            {
                return Ok(_mappingsRepository.GetActiveCorporateAdminMappings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion CorporateAdminMapping

        #region DoctorMapping

        [HttpPost]
        [Route(nameof(AddDoctorMapping))]
        public IActionResult AddDoctorMapping([FromBody] DoctorMapping doctorMapping)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.AddDoctorMapping(doctorMapping, ref errorMessage))
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
        [Route(nameof(DeleteDoctorMapping))]
        public IActionResult DeleteDoctorMapping([FromQuery] Guid doctorMappingId)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.DisableDoctorMapping(doctorMappingId, ref errorMessage))
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
        [Route(nameof(GetActiveDoctorMappings))]
        public IActionResult GetActiveDoctorMappings()
        {
            try
            {
                return Ok(_mappingsRepository.GetActiveDoctorMappings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion DoctorMapping

        #region FrontdeskMapping

        [HttpPost]
        [Route(nameof(AddFrontdeskMapping))]
        public IActionResult AddFrontdeskMapping([FromBody] FrontdeskMapping frontdeskMapping)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.AddFrontdeskMapping(frontdeskMapping, ref errorMessage))
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
        [Route(nameof(DeleteFrontdeskMapping))]
        public IActionResult DeleteFrontdeskMapping([FromQuery] Guid frontdeskMappingId)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.DisableFrontdeskMapping(frontdeskMappingId, ref errorMessage))
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
        [Route(nameof(GetActiveFrontdeskMappings))]
        public IActionResult GetActiveFrontdeskMappings()
        {
            try
            {
                return Ok(_mappingsRepository.GetActiveFrontdeskMappings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion FrontdeskMapping

        #region NurseMapping

        [HttpPost]
        [Route(nameof(AddNurseMapping))]
        public IActionResult AddNurseMapping([FromBody] NurseMapping nurseMapping)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.AddNurseMapping(nurseMapping, ref errorMessage))
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
        [Route(nameof(DeleteNurseMapping))]
        public IActionResult DeleteNurseMapping([FromQuery] Guid nurseMappingId)
        {
            try
            {
                string errorMessage = "";
                if (_mappingsRepository.DisableNurseMapping(nurseMappingId, ref errorMessage))
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
        [Route(nameof(GetActiveNurseMappings))]
        public IActionResult GetActiveNurseMappings()
        {
            try
            {
                return Ok(_mappingsRepository.GetActiveNurseMappings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion NurseMapping
    }
}
