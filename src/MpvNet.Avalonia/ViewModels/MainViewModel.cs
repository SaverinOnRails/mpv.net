using CommunityToolkit.Mvvm.ComponentModel;

namespace MpvNet.Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public IGlEnabledPlayer MpvPlayer => Player;

    public void Start()
    {
        var file = "/home/noble/Videos/wff-recordings/output.mp4";
        Player.LoadFiles([file], false, false);
    }
}
