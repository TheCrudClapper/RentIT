namespace EquipmentService.Core.Domain.Interfaces
{
    /// <summary>
    /// Interface that marks item as "soft-deleteable"
    /// </summary>
    public interface ISoftDelete
    {
        public bool IsActive { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
