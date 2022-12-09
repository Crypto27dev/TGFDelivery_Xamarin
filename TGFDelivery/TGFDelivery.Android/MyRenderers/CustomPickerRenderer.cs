using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        private Context context;
        public CustomPickerRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null) return;
            //for example ,change the line to red:
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Color.White);
            else
                Control.Background.SetColorFilter(Color.White, PorterDuff.Mode.Screen);
        }
    }
}