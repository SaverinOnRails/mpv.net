using CommunityToolkit.Mvvm.ComponentModel;

namespace MpvNet.Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public IGlEnabledPlayer MpvPlayer => Player;

    public void Start()
    {
        var file = "/home/noble/Videos/wff-recordings/20260817_072419output.mkv";
        Player.LoadFiles([file], false, false);
    }
}
