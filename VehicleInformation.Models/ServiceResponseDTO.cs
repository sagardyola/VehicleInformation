namespace VehicleInformation.Models
{
    public class ServiceResponseDTO<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
