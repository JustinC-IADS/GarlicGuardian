using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarlicGuardian.Animations
{
    public class TvAnimation : TriggerAction<VisualElement>
    {
        public State State { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            double from = State == State.On ? 0 : 1;
            double to = State == State.On ? 1 : 0;

            sender.Animate(nameof(TvAnimation), new Animation((d) =>
            {
                sender.IsVisible = true;
                sender.ScaleY = d;

                if (d == 0)
                {
                    sender.IsVisible = false;
                }
            }, from, to, Easing.CubicIn), length: 500);
        }
    }
}
