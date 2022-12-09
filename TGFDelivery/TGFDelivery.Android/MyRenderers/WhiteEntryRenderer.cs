using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Widget;
using System;
using TGFDelivery.CustomViews;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WhiteEntry), typeof(WhiteEntryRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    [System.Obsolete]
    public class WhiteEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                /*Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));*/
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);

        }
    }
}