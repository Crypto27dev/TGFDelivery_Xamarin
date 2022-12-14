using System;
using TGFDelivery.iOS.MyRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]
namespace TGFDelivery.iOS.MyRenderers
{
    public class NavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("Oswald", 24);
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }
    }
}
