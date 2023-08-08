namespace Proyecto.PiteApi.Interfaces;

public interface IDatedEntity
{
    DateTime CreatedDate { get; set; }
    bool IsActive { get; set; }
    DateTime? UpdatedDate { get; set; }
    DateTime? DeletedDate { get; set; }
}
