using Newtonsoft.Json;
using VehicleInformation.Client.Service.IService;
using VehicleInformation.Models;

namespace VehicleInformation.Client.Service
{
    public class VehicleInfoService : IVehicleInfoService
    {
        private readonly HttpClient _httpClient;

        public VehicleInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponseDTO<VehicleMOTDetailsDTO>> Get(string RegistrationNumber)
        {
            var response = await _httpClient.GetAsync($"api/VehicleInformation/{RegistrationNumber}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var vInfo = JsonConvert.DeserializeObject<ServiceResponseDTO<VehicleMOTDetailsDTO>>(content);
                return vInfo;
            }
            else
            {
                return new ServiceResponseDTO<VehicleMOTDetailsDTO>()
                {
                    Success = false,
                    Message = "Bad Request",
                    StatusCode = 404
                };
            }
        }
    }
}
