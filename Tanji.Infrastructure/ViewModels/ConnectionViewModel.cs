using Tanji.Infrastructure.Services;

using CommunityToolkit.Mvvm.ComponentModel;

using Sulakore.Network;

namespace Tanji.Infrastructure.ViewModels;

public partial class ConnectionViewModel : ObservableObject
{
    private readonly IWebInterceptionService _interceptions;

    [ObservableProperty]
    private string _status;

    [ObservableProperty]
    private HotelEndPoint _hotelServer;

    [ObservableProperty]
    private string _customClientPath;

    public ConnectionViewModel(IWebInterceptionService interception)
    {
        _interceptions = interception;
    }
}