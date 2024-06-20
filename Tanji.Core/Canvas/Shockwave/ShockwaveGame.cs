using Shockky;

using Sulakore.Network.Formats;
using Tanji.Core.Net.Messages;

namespace Tanji.Core.Canvas.Shockwave;

public sealed class ShockwaveGame : IGame
{
    private readonly ShockwaveFile _shockwave;

    public string? Path { get; private set; }

    public bool IsPostShuffle => false;
    public HPlatform Platform => HPlatform.Shockwave;

    public IHFormat SendPacketFormat => IHFormat.WedgieOut;
    public IHFormat ReceivePacketFormat => IHFormat.WedgieIn;

    public string? Revision { get; private set; }

    public int MinimumConnectionAttempts { get; private set; }

    public GamePatchingOptions AppliedPatchingOptions { get; private set; }

    public ShockwaveGame(Stream stream)
    { }

    public void Assemble(string path)
    { }

    public void Disassemble()
    { }

    public void Dispose()
    { }

    public void GenerateMessageHashes()
    {
    }

    public void Patch(GamePatchingOptions options)
    { }

    public bool TryResolveMessage(uint hash, out HMessage message)
    {
        message = default;
        return false;
    }

    public bool TryResolveMessage(string name, out HMessage message)
    {
        message = default;
        return false;
    }
}
