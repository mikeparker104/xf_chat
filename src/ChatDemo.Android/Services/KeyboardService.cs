using System;
using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using ChatDemo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ChatDemo.Droid.Services
{
    public class KeyboardService : BaseKeyboardService
    {
        Context _context;

        public KeyboardService(Context context) => _context = context;

        protected override Type OnConvertToNativeType(View view)
        {
            var nativeView = Platform.CreateRendererWithContext(view, _context).View;
            return nativeView.GetType();
        }

        protected override void OnHideKeyboard()
        {
            base.OnHideKeyboard();

            using (var inputMethodManager = _context.GetSystemService(Context.InputMethodService) as InputMethodManager)
            {
                if (inputMethodManager != null && _context is Activity)
                {
                    var activity = _context as Activity;
                    var token = activity.CurrentFocus?.WindowToken;
                    inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                    activity.Window.DecorView.ClearFocus();
                }
            }
        }
    }
}