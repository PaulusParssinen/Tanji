using System.Collections.ObjectModel;

using Tanji.Core.Net;

namespace Tanji.Infrastructure.Services;

public interface IConnectionHandlerService
{
    ObservableCollection<IHConnection> Connections { get; }

    Task<IHConnection> InterceptConnectionAsync(HConnectionContext context, CancellationToken cancellationToken = default);
}