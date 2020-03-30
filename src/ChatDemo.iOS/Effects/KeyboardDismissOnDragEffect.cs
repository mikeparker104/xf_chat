using ChatDemo.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("ChatDemo.Effects")]
[assembly: ExportEffect(typeof(ChatDemo.iOS.Effects.KeyboardDismissOnDragEffect), nameof(KeyboardDismissOnDragEffect))]
namespace ChatDemo.iOS.Effects
{
    public class KeyboardDismissOnDragEffect : PlatformEffect
    {
        UIScrollViewKeyboardDismissMode _initialDismissMode;

        protected override void OnAttached()
        {
            if (Control is UIScrollView)
            {
                var scrollView = Control as UIScrollView;
                _initialDismissMode = scrollView.KeyboardDismissMode;
                scrollView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag; // TODO: Review whether we should use Interactive instead. This will involve intercepting the height of the keyboard as it changes!
            }
        }

        protected override void OnDetached()
        {
            if (Control is UIScrollView)
                (Control as UIScrollView).KeyboardDismissMode = _initialDismissMode;
        }
    }
}