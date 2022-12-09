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
using TGFDelivery.CustomViews;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BlueEntry), typeof(BlueEntryRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    [Obsolete]
    public class BlueEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            if (Control != null)
            {
                SetColor(Android.Graphics.Color.Transparent);

                this.EditText.FocusChange += (sender, ee) => {
                    bool hasFocus = ee.HasFocus;
                    if (hasFocus)
                    {
                        SetColor(Android.Graphics.Color.LightSeaGreen);
                    }
                    else
                    {
                        SetColor(Android.Graphics.Color.White);
                    }
                };
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);

            IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
            IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

            // my_cursor is the xml file name which we defined above
            JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
        }
        void SetColor(Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(color);
            }
            else
            {
                Control.Background.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
            }
        }
    }
}