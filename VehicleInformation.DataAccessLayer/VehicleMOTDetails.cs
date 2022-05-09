namespace VehicleInformation.DataAccessLayer
{
    public class MotTest
    {
        public string completedDate { get; set; }
        public string testResult { get; set; }
        public string expiryDate { get; set; }
        public string odometerValue { get; set; }
        public string odometerUnit { get; set; }
        public string odometerResultType { get; set; }
        public string motTestNumber { get; set; }
        public List<RfrAndComment> rfrAndComments { get; set; }
    }

    public class RfrAndComment
    {
        public string text { get; set; }
        public string type { get; set; }
        public bool dangerous { get; set; }
    }

    public class VehicleMOTDetails
    {
        public string registration { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string manufactureYear { get; set; }
        public string firstUsedDate { get; set; }
        public string fuelType { get; set; }
        public string primaryColour { get; set; }
        public string vehicleId { get; set; }
        public string registrationDate { get; set; }
        public string manufactureDate { get; set; }
        public string engineSize { get; set; }
        public string dvlaId { get; set; }
        public string motTestExpiryDate { get; set; }
        public List<MotTest> motTests { get; set; }
    }
}