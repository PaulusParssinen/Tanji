using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections.ObjectModel;

using Tanji.Core;
using Tanji.Core.Net;
using Tanji.Core.Canvas;
using Sulakore.Network.Buffers;
using Sulakore.Network.Formats;
using Tanji.Infrastructure.Factories;

using Microsoft.Extensions.Logging;

using CommunityToolkit.HighPerformance.Buffers;

using Sulakore.Network;

namespace Tanji.Infrastructure.Services.Implementations;

public sealed class ConnectionHandlerService : IConnectionHandlerService
{
    private readonly IClientHandlerService _clientHandler;
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<ConnectionHandlerService> _logger;

    public ObservableCollection<IHConnection> Connections { get; } = [];

    public ConnectionHandlerService(
        ILogger<ConnectionHandlerService> logger,
        IConnectionFactory connectionFactory,
        IClientHandlerService clientHandler)
    {
        _logger = logger;
        _clientHandler = clientHandler;
        _connectionFactory = connectionFactory;
    }

    public async Task<IHConnection> InterceptConnectionAsync(HConnectionContext context, CancellationToken cancellationToken = default)
    {
        IHConnection connection = _connectionFactory.Create(context);

        // Begin intercepting for connection attempts from the client before launching the client.
        Task interceptLocalConnectionTask = connection.InterceptLocalConnectionAsync(cancellationToken);

        // Wait for the intercepted connection
        await interceptLocalConnectionTask.ConfigureAwait(false);
        if (connection.Local == null || !connection.Local.IsConnected)
        {
            throw new Exception("Local connection to the client has not been established.");
        }

        if (context.AppliedPatchingOptions.Patches.HasFlag(HPatches.InjectAddressShouter))
        {
            using var writer = new ArrayPoolBufferWriter<byte>(32);
            int written = await connection.Local.ReceivePacketAsync(writer, cancellationToken).ConfigureAwait(false);

            IPEndPoint? remoteEndPoint = ParseRemoteEndPoint(context.SendPacketFormat, writer.WrittenSpan);
            if (remoteEndPoint == null)
            {
                throw new Exception("Failed to parse the remote endpoint from the intercepted packet.");
            }

            await connection.EstablishRemoteConnectionAsync(remoteEndPoint, cancellationToken).ConfigureAwait(false);
            
            _ = connection.AttachNodesAsync(cancellationToken);
        }

        Connections.Add(connection);
        return connection;
    }

    private static IPEndPoint? ParseRemoteEndPoint(IHFormat packetFormat, ReadOnlySpan<byte> packetSpan)
    {
        var pktReader = new HPacketReader(packetFormat, packetSpan);
        string hostNameOrAddress = pktReader.ReadUTF8().Split('\0')[0];
        int port = pktReader.Read<int>();

        if (!IPAddress.TryParse(hostNameOrAddress, out IPAddress? address))
        {
            IPAddress[] addresses = Dns.GetHostAddresses(hostNameOrAddress);
            if (addresses.Length > 0) address = addresses[0];
        }

        return address != null ? new IPEndPoint(address, port) : null;
    }
}