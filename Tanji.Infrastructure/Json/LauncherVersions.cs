﻿using System.Text.Json.Serialization;

using Tanji.Core.Json;
using Tanji.Core.Canvas;

namespace Tanji.Infrastructure.Json;

public readonly record struct PlatformCheck
{
    public string? Version { get; init; }
    public string? Path { get; init; }
}
public readonly record struct Installation
{
    public required string Version { get; init; }
    public required string Path { get; init; }

    [JsonPropertyName("client")]
    [JsonConverter(typeof(PlatformConverter))]
    public required HPlatform Platform { get; init; }

    [JsonConverter(typeof(EpochDateTimeConverter))]
    public required DateTime LastModified { get; init; }
}
public readonly record struct UpdateCheck
{
    public PlatformCheck Unity { get; init; }
    public PlatformCheck Air { get; init; }
    public PlatformCheck Shockwave { get; init; }

    [JsonConverter(typeof(EpochDateTimeConverter))]
    public required DateTime Time { get; init; }
}
public readonly record struct LauncherVersions
{
    public required Installation[] Installations { get; init; }
    public required UpdateCheck LastCheck { get; init; }
}