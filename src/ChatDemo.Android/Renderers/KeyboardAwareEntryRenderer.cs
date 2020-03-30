using Android.Content;
using Android.Widget;
using ChatDemo.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(KeyboardAwareEntry), typeof(ChatDemo.Droid.Renderers.KeyboardAwareEntryRenderer))]
namespace ChatDemo.Droid.Renderers
{
    public class KeyboardAwareEntryRenderer : EntryRenderer
    {
        public KeyboardAwareEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
                Control.EditorAction += Handle_EditorAction;

            if (e.OldElement != null && Control != null)
                Control.EditorAction -= Handle_EditorAction;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Control.EditorAction -= Handle_EditorAction;
        }

        void Handle_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;

            if (e.ActionId == Android.Views.InputMethods.ImeAction.Send)
            {
                Element.ReturnCommand?.Execute(null);
                e.Handled = true;
            }
        }
    }
}