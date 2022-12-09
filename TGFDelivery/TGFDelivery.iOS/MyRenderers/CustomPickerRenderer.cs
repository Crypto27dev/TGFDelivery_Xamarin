using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TGFDelivery.iOS.MyRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace TGFDelivery.iOS.MyRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
                return;
            Control.Layer.BorderWidth = 1;
            Control.Layer.BorderColor = Color.White.ToCGColor();
        }
    }
}