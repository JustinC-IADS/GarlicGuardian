using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace GarlicGuardian;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();

        Task.Run(async () =>
        {
            await Task.Delay(2000);

            while (true)
            {
                await Task.Delay(100);

                double usage = WeakReferenceMessenger.Default.Send<GetUsageMessage>().Response;

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    MediaPlayer.Volume = usage < 2 ? 0 : .3;                   
                });
            }
        });
    }
}

