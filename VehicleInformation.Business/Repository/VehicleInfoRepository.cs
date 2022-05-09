using Newtonsoft.Json;
using VehicleInformation.Business.Repository.IRepository;
using VehicleInformation.DataAccessLayer;
using VehicleInformation.Models;

namespace VehicleInformation.Business.Repository
{
    public class VehicleInfoRepository : IVehicleInfoRepository
    {
        public async Task<ServiceResponseDTO<VehicleMOTDetailsDTO>> Get(string RegistrationNumber)
        {
            var spHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };

            var url = $"https://beta.check-mot.service.gov.uk";
            HttpClient client = new HttpClient(spHandler);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");

            Task<HttpResponseMessage> task = client.GetAsync($"/trade/vehicles/mot-tests?registration={RegistrationNumber}");
            var content = await task.Result.Content.ReadAsStringAsync();

            if (content == null) return new ServiceResponseDTO<VehicleMOTDetailsDTO> { StatusCode = 400, Message = "NOT Found" };

            if (content.Contains("errorMessage")) 
            {
                var _errorDetails = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                return new ServiceResponseDTO<VehicleMOTDetailsDTO> { Success = false, StatusCode = 404, Message = _errorDetails.errorMessage}; 
            }

            var _vDetails = JsonConvert.DeserializeObject<IEnumerable<VehicleMOTDetails>>(content).FirstOrDefault();
            VehicleMOTDetailsDTO _details = new VehicleMOTDetailsDTO();

            if (_vDetails.registration != null) _details.RegistrationNo = _vDetails.registration;
            if (_vDetails.make != null) _details.Make = _vDetails.make;
            if (_vDetails.model != null) _details.Model = _vDetails.model;
            if (_vDetails.primaryColour != null) _details.PrimaryColour = _vDetails.primaryColour;

            if (_vDetails.motTests != null)
            {
                if (_vDetails.motTests.FirstOrDefault().expiryDate != null) _details.ExpiryDate = _vDetails.motTests.FirstOrDefault().expiryDate;
                if (_vDetails.motTests.FirstOrDefault().odometerValue != null) _details.OdometerValue = _vDetails.motTests.FirstOrDefault().odometerValue;
            }

            return new ServiceResponseDTO<VehicleMOTDetailsDTO> { Data = _details, StatusCode = 200 };
        }
    }
}
