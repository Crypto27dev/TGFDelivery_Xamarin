using System;
using System.ComponentModel;
using TGFDelivery.CustomViews;
using TGFDelivery.iOS.MyRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColoredTableView), typeof(ColoredTableViewRenderer))]
namespace TGFDelivery.iOS.MyRenderers
{
    public class ColoredTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            var tableView = Control as UITableView;
            var coloredTableView = Element as ColoredTableView;
            tableView.SeparatorColor = coloredTableView.SeparatorColor.ToUIColor();           
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "SeparatorColor")
            {
                var tableView = Control as UITableView;
                var coloredTableView = Element as ColoredTableView;

                tableView.SeparatorColor = coloredTableView.SeparatorColor.ToUIColor();
            }
        }
    }
}
