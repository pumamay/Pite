using Proyecto.PiteApi.Interfaces.Contracts;
public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class { }