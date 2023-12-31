﻿namespace Proyecto.PiteApi.Interfaces.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
