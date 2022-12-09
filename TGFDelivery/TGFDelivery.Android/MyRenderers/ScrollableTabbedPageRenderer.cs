﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using TGFDelivery.CustomViews;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(ScrollableTabbedPage), typeof(ScrollableTabbedPageRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    public class ScrollableTabbedPageRenderer : TabbedPageRenderer
    {
        public ScrollableTabbedPageRenderer(Context context) : base(context)
        {

        }
        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            var tabLayout = child as TabLayout;
            if (tabLayout != null)
            {
                tabLayout.TabMode = TabLayout.ModeScrollable;
            }
        }
    }
}