using ChatDemo.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardAwareEntry), typeof(ChatDemo.iOS.Renderers.KeyboardAwareEntryRenderer))]
namespace ChatDemo.iOS.Renderers
{
    public class KeyboardAwareEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                Control.ShouldEndEditing += Handle_ShouldEndEditing;
                Control.ShouldReturn += Handle_ShouldReturn;
            }
                
            if (e.OldElement != null && Control != null)
            {
                Control.ShouldEndEditing -= Handle_ShouldEndEditing;
                Control.ShouldReturn -= Handle_ShouldReturn;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Control.ShouldEndEditing -= Handle_ShouldEndEditing;
            Control.ShouldReturn -= Handle_ShouldReturn;
        }

        bool Handle_ShouldReturn(UITextField textField)
            => false;

        bool Handle_ShouldEndEditing(UITextField textField)
            => !App.KeyboardService.ShouldLockKeyboardFocus; 
    }
}