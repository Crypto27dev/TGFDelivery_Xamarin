using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util.Regex;
using TGFDelivery.CustomViews;
using TGFDelivery.Droid.MyRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Pattern = Java.Util.Regex.Pattern;

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace TGFDelivery.Droid.MyRenderers
{
    [Obsolete]
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != e.OldElement)
            {
                if (e.OldElement != null)
                    e.OldElement.PropertyChanged -= Element_PropertyChanged;

                if (e.NewElement != null)
                    e.NewElement.PropertyChanged += Element_PropertyChanged;
            }
            SetText();
        }

        private void SetText()
        {
            if (Element is HyperlinkLabel hyperlinkLabelElement && hyperlinkLabelElement != null)
            {
                string text = hyperlinkLabelElement.GetText(out List<HyperlinkLabelLink> links);
                Control.Text = text;
                //Control.SetTextColor(Android.Graphics.Color.LightSeaGreen);
                if (text != null)
                {
                    foreach (var item in links)
                    {
                        var pattern = Pattern.Compile(item.Text);
                        Linkify.AddLinks(Control, pattern, "",
                            new CustomMatchFilter(item.Start),
                            new CustomTransformFilter(item.Link));
                        Control.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
                    }
                }
            }
        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == HyperlinkLabel.RawTextProperty.PropertyName)
                SetText();
        }
    }

    public class CustomTransformFilter : Java.Lang.Object, Linkify.ITransformFilter
    {
        readonly string url;
        public CustomTransformFilter(string url)
        {
            this.url = url;
        }

        public string TransformUrl(Matcher match, string url)
            => this.url;
    }

    public class CustomMatchFilter : Java.Lang.Object, Linkify.IMatchFilter
    {
        readonly int start;
        public CustomMatchFilter(int start)
        {
            this.start = start;
        }

        public bool AcceptMatch(ICharSequence s, int start, int end)
            => start == this.start;
    }
}