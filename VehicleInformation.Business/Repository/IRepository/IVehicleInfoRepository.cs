using VehicleInformation.Models;

namespace VehicleInformation.Business.Repository.IRepository
{
    public interface IVehicleInfoRepository
    {
        Task<ServiceResponseDTO<VehicleMOTDetailsDTO>> Get(string Id);

    }
}
