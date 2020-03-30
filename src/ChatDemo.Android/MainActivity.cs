using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using ChatDemo.Droid.Services;
using System;
using System.Linq;

namespace ChatDemo.Droid
{
    [Activity(Label = "ChatDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App.KeyboardService = new KeyboardService(this);

            LoadApplication(new App());

            Window.SetSoftInputMode(SoftInput.AdjustResize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // ========================================================================================================================
        // BEGIN HACK: Workaround for blocking keyboard dismissal due to lost focus
        // ========================================================================================================================

        bool ViewContains(View view, int eventX, int eventY)
        {
            int[] locationOnScreen = new int[2];
            view.GetLocationOnScreen(locationOnScreen);

            int x = locationOnScreen[0];
            int y = locationOnScreen[1];
            int width = view.Width;
            int height = view.Height;

            int minX = x;
            int maxX = x + width;
            int minY = y;
            int maxY = y + height;

            return (eventX <= maxX && eventX >= minX) && (eventY <= maxY && eventY >= minY);
        }

        Type ResolveRegisteredType(Type type)
        {
            if (type != null && !App.KeyboardService.KeyboardFocusLockInputSources.Contains(type))
                return ResolveRegisteredType(type.BaseType);

            return type;
        }

        private bool _suppressFocusChange;

        // Facilitates logic centrally via a common keyboard service i.e. ShouldDismissKeyboard
        // Derived from whether the source of the touch event came from a control that should not take focus
        // Keyboard service facilitates registration of types that should not dismiss keyboard
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            int eventX = (int)ev.GetX();
            int eventY = (int)ev.GetY();

            View hitView = null;
            var root = Window.DecorView.FindViewById(Android.Resource.Id.Content);
            var viewsToInterrogate = root.Touchables.Reverse().ToList();

            foreach (var view in viewsToInterrogate)
            {
                if (ViewContains(view, eventX, eventY))
                {
                    hitView = view;
                    break;
                }
            }

            Type keyboardAwareType = ResolveRegisteredType(hitView?.GetType());
            _suppressFocusChange = keyboardAwareType != null &&
                                   App.KeyboardService.UpdateKeyboardFocusState(keyboardAwareType);

            var result = base.DispatchTouchEvent(ev);
            _suppressFocusChange = false;

            return result;
        }

        public override View CurrentFocus
        {
            get
            {
                if (_suppressFocusChange)
                    return null;

                return base.CurrentFocus;
            }
        }

        // ========================================================================================================================
        // END HACK
        // ========================================================================================================================
    }
}