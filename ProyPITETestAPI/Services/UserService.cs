using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto.PiteApi.Data;
using Proyecto.PiteApi.Helpers;
using Proyecto.PiteApi.Interfaces;
using Proyecto.PiteApi.Interfaces.Contracts;
using Proyecto.PiteApi.Models;
using System.Linq;

namespace Proyecto.PiteApi.Services;

public class UserService : BaseRepository<User>, IUserService
{
    private readonly PiteDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IApplicationReadDbConnection _readDbConnection;
    private readonly IApplicationWriteDbConnection _writeDbConnection;

    public UserService(PiteDbContext dbContext, IApplicationReadDbConnection readDbConnection,
                       IApplicationWriteDbConnection writeDbConnection, IMapper mapper) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _readDbConnection = readDbConnection;
        _writeDbConnection = writeDbConnection;
        _mapper = mapper;
    }
}
