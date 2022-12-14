using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TGFDelivery.CustomViews;
using TGFDelivery.iOS.MyRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WhiteEntry), typeof(WhiteEntryRenderer))]
namespace TGFDelivery.iOS.MyRenderers
{
    [System.Obsolete]
    public class WhiteEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.TextColor = UIColor.White;

            }
        }
    }
}