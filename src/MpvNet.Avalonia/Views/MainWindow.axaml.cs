using System.Drawing;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MpvNet.Avalonia.Controls;

namespace MpvNet.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Player.InitGl();
        Player.FileLoaded += Player_FileLoaded;
        Player.Pause += Player_Pause;
        Player.PlaylistPosChanged += Player_PlaylistPosChanged;
        Player.Seek += UpdateProgressBar;
        Player.Shutdown += Player_Shutdown;
        Player.VideoSizeChanged += Player_VideoSizeChanged;
        Player.ClientMessage += Player_ClientMessage;
    }

    private void Player_VideoSizeChanged(System.Drawing.Size size)
    {
        throw new NotImplementedException();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
    }

    protected override async void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
    }

    private void Player_ClientMessage(string[] obj)
    {
        throw new NotImplementedException();
    }


    private void Player_Shutdown()
    {
        throw new NotImplementedException();
    }

    private void UpdateProgressBar()
    {
        throw new NotImplementedException();
    }

    private void Player_PlaylistPosChanged(int obj)
    {
        // throw new NotImplementedException();
    }

    private void Player_Pause()
    {
        // throw new NotImplementedException();
    }

    private void Player_FileLoaded()
    {
        throw new NotImplementedException();
    }
}
