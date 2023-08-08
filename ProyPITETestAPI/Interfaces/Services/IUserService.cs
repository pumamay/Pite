using Proyecto.PiteApi.Interfaces.Contracts;
using Proyecto.PiteApi.Models;

namespace Proyecto.PiteApi.Interfaces;

public interface IUserService : IRepository<User>, IReadRepositoryBase<User> { }
