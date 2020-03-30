using System;
using ChatDemo.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ChatDemo.iOS.Services
{
    public class KeyboardService : BaseKeyboardService
    {
        protected override void OnHideKeyboard()
        {
            base.OnHideKeyboard();
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }

        protected override Type OnConvertToNativeType(View view)
        {
            var nativeType = Platform.CreateRenderer(view).NativeView.Self;
            return nativeType?.GetType();
        }
    }
}