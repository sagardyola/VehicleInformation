using VehicleInformation.Models;

namespace VehicleInformation.Client.Service.IService
{
    public interface IVehicleInfoService
    {
        Task<ServiceResponseDTO<VehicleMOTDetailsDTO>> Get(string RegistrationNumber);
    }
}
