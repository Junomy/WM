using Microsoft.AspNetCore.Http;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Common.Interfaces;

public interface IUserService
{
    Task<User?> GetUser(CancellationToken cancellationToken = default);
}
