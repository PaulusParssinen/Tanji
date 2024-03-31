using System.Net;

using Tanji.Core.Net;

namespace Tanji.Infrastructure.Services;

public interface IRemoteEndPointResolverService<TEndPoint> where TEndPoint : IPEndPoint
{
    Task<TEndPoint> ResolveAsync(string ticket, CancellationToken cancellationToken = default);
    Task<TEndPoint> ResolveAsync(HNode local, HConnectionContext context, CancellationToken cancellationToken = default);
}