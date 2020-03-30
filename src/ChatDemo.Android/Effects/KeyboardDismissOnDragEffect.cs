using System;
using Android.Content;
using Android.Views.InputMethods;
using ChatDemo.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("ChatDemo.Effects")]
[assembly: ExportEffect(typeof(ChatDemo.Droid.Effects.KeyboardDismissOnDragEffect), nameof(KeyboardDismissOnDragEffect))]
namespace ChatDemo.Droid.Effects
{
    public class KeyboardDismissOnDragEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // TODO: Need to hook this up only once the keyboard has become visible (optimization)
            Control.ScrollChange += ScrollChanged;
        }

        protected override void OnDetached()
        {
            // TODO: Need to unhook this once the keyboard has been hidden (optimization)
            Control.ScrollChange -= ScrollChanged;
        }

        void ScrollChanged(object sender, Android.Views.View.ScrollChangeEventArgs e)
        {
            var scrollDelta = Math.Abs(e.OldScrollY - e.ScrollY);

            // For demo purposes, 300 has been used as an arbitrary number to infer that the keyboard is showing/hiding
            if (scrollDelta >= 300)
                return;

            var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (!inputMethodManager.IsAcceptingText)
                return;

            App.KeyboardService.HideKeyboard();
        }
    }
}