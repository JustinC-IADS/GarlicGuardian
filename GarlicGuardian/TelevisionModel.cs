using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace GarlicGuardian
{
    internal partial class TelevisionModel : ObservableObject, IRecipient<GetUsageMessage>
    {
        public TelevisionModel()
        {
            CommunityToolkit.Mvvm.Messaging.WeakReferenceMessenger.Default.Register(this);

            Task.Run(async () =>
            {
                while (true)
                {
                    GetPowerMessage msg = WeakReferenceMessenger.Default.Send<GetPowerMessage>();
                    if (msg != null)
                    {
                        if (msg.Response > 0)
                        {
                            if (State == State.Off)
                            {
                                State = State.Standby;
                            }                           
                        }
                        else
                        {
                            State = State.Off;
                        }
                    }

                    await Task.Delay(100);
                }
            });
        }

        [ObservableProperty]
        private State _state = State.On;

        [RelayCommand]
        private void PressButton()
        { 
            if (State == State.On)
            {
                State = State.Standby;
            }
            else if (State == State.Standby)
            {
                this.State = State.On;
            }
        }

        public void Receive(GetUsageMessage message)
        {
            if (this.State == State.On)
            {
                message.Reply(98);
            }
            else if (this.State == State.Standby)
            {
                message.Reply(.5);
            }
            else
            {
                message.Reply(0);
            }
        }
    }
}
