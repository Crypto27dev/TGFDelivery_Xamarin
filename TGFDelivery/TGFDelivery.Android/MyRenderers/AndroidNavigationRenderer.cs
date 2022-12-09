using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(AndroidNavigationRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    [Obsolete]
    public class AndroidNavigationRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                textView.Typeface = Typeface.CreateFromAsset(this.Context.Assets, "Oswald[wght].ttf");

                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}