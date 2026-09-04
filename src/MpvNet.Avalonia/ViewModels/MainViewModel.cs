using CommunityToolkit.Mvvm.ComponentModel;

namespace MpvNet.Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public IGlEnabledPlayer MpvPlayer => Player;

    public void Start()
    {
        var file = "https://test-videos.co.uk/vids/bigbuckbunny/webm/vp9/360/Big_Buck_Bunny_360_10s_1MB.webm";
        Player.LoadFiles([file], false, false);
    }
}
