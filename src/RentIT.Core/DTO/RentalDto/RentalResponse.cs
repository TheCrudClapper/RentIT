namespace RentIT.Core.DTO.RentalDto
{
    public class RentalResponse
    {
        public Guid Id{ get; set; }
        public string EquipmentName { get; set; } = null!;
        public DateTime? ReturnedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentalPrice { get; set; }
        public string CreatorEmail { get; set; } = null!;
    }
}
