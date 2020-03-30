using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ChatDemo.Effects
{
    [Preserve(AllMembers = true)]
    public class KeyboardDismissOnDragEffect : RoutingEffect
    {
        public KeyboardDismissOnDragEffect()
            : base($"{nameof(ChatDemo)}.{nameof(Effects)}.{nameof(KeyboardDismissOnDragEffect)}")
        {
        }
    }
}