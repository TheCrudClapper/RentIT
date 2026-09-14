namespace EquipmentService.Core.Domain.Interfaces
{
    /// <summary>
    /// Class that defines basic entity
    /// </summary>
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
