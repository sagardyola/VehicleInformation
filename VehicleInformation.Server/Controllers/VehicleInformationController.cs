using Microsoft.AspNetCore.Mvc;
using VehicleInformation.Business.Repository.IRepository;
using VehicleInformation.Models;

namespace VehicleInformation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInformationController : ControllerBase
    {
        private readonly IVehicleInfoRepository _vehicleInfoRepository;

        public VehicleInformationController(IVehicleInfoRepository vehicleInfoRepository)
        {
            _vehicleInfoRepository = vehicleInfoRepository;
        }

        [HttpGet("{RegistrationNumber}")]
        public async Task<IActionResult> Get(string? RegistrationNumber)
        {
            if (RegistrationNumber == null)
            {
                return BadRequest(new ServiceResponseDTO<VehicleMOTDetailsDTO>()
                {
                    Message = "Invalid Registration Number",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var vehicleDetails = await _vehicleInfoRepository.Get(RegistrationNumber);
            if (vehicleDetails == null)
            {
                return NotFound(new ServiceResponseDTO<VehicleMOTDetailsDTO>()
                {
                    Message = "Registration Number Not Found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(vehicleDetails);
        }
    }
}