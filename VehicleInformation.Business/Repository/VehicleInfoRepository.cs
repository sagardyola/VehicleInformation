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

            VehicleMOTDetailsDTO _details = new VehicleMOTDetailsDTO() { 
                RegistrationNo = _vDetails.registration,
                Make = _vDetails.make != null ? _vDetails.make : null,
                Model = _vDetails.model != null ? _vDetails.model : null,
                PrimaryColour = _vDetails.primaryColour != null ? _vDetails.primaryColour : null,
                ExpiryDate = _vDetails.motTestExpiryDate != null ? _vDetails.motTestExpiryDate : null
            };


            if (_vDetails.motTests != null)
            {
                _details.OdometerValue = (_vDetails.motTests.FirstOrDefault().odometerValue != null) ? _vDetails.motTests.FirstOrDefault().odometerValue : null;
                _details.OdometerValue = (_vDetails.motTests.FirstOrDefault().odometerUnit != null) ? _details.OdometerValue + _vDetails.motTests.FirstOrDefault().odometerUnit : null;

                _details.ExpiryDate = (_vDetails.motTestExpiryDate == null && _vDetails.motTests.FirstOrDefault().expiryDate != null) ? _vDetails.motTests.FirstOrDefault().expiryDate : null;
            }

            return new ServiceResponseDTO<VehicleMOTDetailsDTO> { Data = _details, StatusCode = 200 };
        }
    }
}
