using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using Connect.Conference.Mobile.Common;
using Connect.Conference.Mobile.iOS;

[assembly: ExportRenderer(typeof(NoHelperEntry), typeof(NoHelperEntryRenderer))]
namespace Connect.Conference.Mobile.iOS
{
    public class NoHelperEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
                Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
                Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
            }
        }
    }
}
