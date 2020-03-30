using ChatDemo.iOS.Services;
using Foundation;
using UIKit;

namespace ChatDemo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUIGestureRecognizerDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            App.KeyboardService = new KeyboardService();

            LoadApplication(new App());

            var result = base.FinishedLaunching(app, options);

            if (result)
            {
                UITapGestureRecognizer tap = new UITapGestureRecognizer(Self, new ObjCRuntime.Selector("gestureRecognizer:shouldReceiveTouch:"));
                tap.Delegate = (IUIGestureRecognizerDelegate)Self;
                app.KeyWindow.AddGestureRecognizer(tap);
            }

            return result;
        }

        [Export("gestureRecognizer:shouldReceiveTouch:")]
        public bool ShouldReceiveTouch(UIGestureRecognizer gestureRecognizer, UITouch touch)
        {
            var nativeType = gestureRecognizer.View.HitTest(touch.LocationInView(gestureRecognizer.View), new UIEvent());
            App.KeyboardService.UpdateKeyboardFocusState(nativeType.GetType());

            return false;
        }
    }
}