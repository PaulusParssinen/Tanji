using Tanji.Core.Net.Formats;
using Tanji.Core.Net.Messages;

namespace Tanji.Core.Canvas.Shockwave;

public sealed class ShockwaveGame : IGame
{
    public string? Path { get; private set; }

    public bool IsPostShuffle => false;
    public HPlatform Platform => HPlatform.Shockwave;

    public IHFormat SendPacketFormat => IHFormat.WedgieOut;
    public IHFormat ReceivePacketFormat => IHFormat.WedgieIn;

    public string? Revision { get; private set; }

    public int MinimumConnectionAttempts { get; private set; }

    public GamePatchingOptions AppliedPatchingOptions { get; private set; }

    public ShockwaveGame(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Assemble(string path)
    {
        throw new NotImplementedException();
    }

    public void Disassemble()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void GenerateMessageHashes()
    {
        throw new NotImplementedException();
    }

    public void Patch(GamePatchingOptions options)
    {
        throw new NotImplementedException();
    }

    public bool TryResolveMessage(uint hash, out HMessage message)
    {
        throw new NotImplementedException();
    }

    public bool TryResolveMessage(string name, out HMessage message)
    {
        throw new NotImplementedException();
    }
}
