using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarlicGuardian;

internal partial class GuardianModel : ObservableObject, IRecipient<GetPowerMessage>
{
    /// <summary>
    /// Monitors the electricity usage.
    /// </summary>
    /// <remarks>
    /// Usage property is automatically updated every .1 seconds.    
    /// Status property is used to update status displayed on Guardian LCD.
    /// SupplyingPower property is used to control whether electricity is allowed to flow to the appliance.
    /// </remarks>
    private async void MonitorUsage()
    {
        // wait for the appliance to be turned on
        while (Usage < 2);

        // track how many cycles the appliance has been in standby
        int count = 0; 
     
        // wait for in stand by for 5 seconds
        while (count < 5)
        {
            // only checks status once every second
            await Task.Delay(1000);
            
            if (Usage < 2)
            {             
                // record appliance in idle state 
                count++;
                int secondsRemaining = 5 - count;
                Status = $"OFF in {secondsRemaining}";
            }
            else
            {
                // appliance is use; reset count and status
                count = 0;                
                Status = "ON";
            }                
        }

        // cut power
        SupplyingPower = false;
        Status = "OFF";
    }

    [RelayCommand]
    private void Reset()
    {
        SupplyingPower = true;
        Task.Run(MonitorUsage);
    }

    #region hide
    public GuardianModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        Reset();

        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(100);
                Usage = WeakReferenceMessenger.Default.Send<GetUsageMessage>().Response;
            }
        });
    }

    public void Receive(GetPowerMessage message)
    {
        message.Reply(SupplyingPower ? 120 : 0);
    }

    [ObservableProperty]
    private double _usage = 98;

    [ObservableProperty]
    private string _status;

    [ObservableProperty]
    private bool _supplyingPower = true;
    #endregion
}
