using CommunityToolkit.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarlicGuardian.Animations
{
    public class ScaleAnimation : BaseAnimation
    {
        public string ElementNameToAnimate { get; set; } = string.Empty;

        public override async Task Animate(VisualElement view)
        {
            double scale = view.Scale;

            VisualElement element = view;
            if (!string.IsNullOrEmpty(ElementNameToAnimate) && view != null)
            {
                element = (VisualElement)view.FindByName(ElementNameToAnimate);
            }

            await element.ScaleTo(.9 * scale, length: 50, Easing.CubicIn).ConfigureAwait(true);
            await element.ScaleTo(scale, length: 250, Easing.CubicOut).ConfigureAwait(true);
        }
    }
}
