namespace ReviewServices.Core.Domain.Interfaces;

/// <summary>
/// Interface that defines basic entity
/// </summary>
public interface IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
}
