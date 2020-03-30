using ChatDemo.Controls;
using ChatDemo.iOS.Renderers;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardAwareGrid), typeof(KeyboardAwareGridRenderer))]
namespace ChatDemo.iOS.Renderers
{
    public class KeyboardAwareGridRenderer : ViewRenderer
    {
        NSObject _keyboardWillShowObserver;
        NSObject _keyboardWillHideObserver;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                RegisterKeyboardNotifications();

            if (e.OldElement != null)
                UnregisterKeyboardNotifications();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            UnregisterKeyboardNotifications();
        }

        void UnregisterKeyboardNotifications()
        {
            if (_keyboardWillShowObserver != null)
            {
                _keyboardWillShowObserver.Dispose();
                _keyboardWillShowObserver = null;
            }

            if (_keyboardWillHideObserver != null)
            {
                _keyboardWillHideObserver.Dispose();
                _keyboardWillHideObserver = null;
            }
        }

        void RegisterKeyboardNotifications()
        {
            if (_keyboardWillShowObserver == null)
                _keyboardWillShowObserver = UIKeyboard.Notifications.ObserveWillShow(KeyboardWillShow);

            if (_keyboardWillHideObserver == null)
                _keyboardWillHideObserver = UIKeyboard.Notifications.ObserveWillHide(KeyboardWillHide);
        }

        void KeyboardWillShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;

            if (Element != null)
                Element.Margin = new Thickness(0, 0, 0, keyboardSize.Height);
        }

        void KeyboardWillHide(object sender, UIKeyboardEventArgs args)
        {
            if (Element != null)
                Element.Margin = new Thickness(0);
        }
    }
}