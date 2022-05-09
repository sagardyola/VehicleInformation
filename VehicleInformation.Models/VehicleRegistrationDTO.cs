using System.ComponentModel.DataAnnotations;

namespace VehicleInformation.Models
{
    public class VehicleRegistrationDTO
    {
        [Required(ErrorMessage = "Please enter the Registration number")]
        public string RegistrationNo { get; set; }
    }
}
