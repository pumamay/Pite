namespace Proyecto.PiteApi.Interfaces;

public interface IDeletableEntity
{
    DateTime CreatedDate { get; set; }
    bool IsActive { get; set; }
    bool IsDeletable { get; set; }
    DateTime? UpdatedDate { get; set; }
}
