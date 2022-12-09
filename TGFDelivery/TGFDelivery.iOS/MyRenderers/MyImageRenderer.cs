//using System;
//using System.ComponentModel;
//using Foundation;
//using TGFDelivery.iOS.MyRenderers;
//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(Image), typeof(MyImageRenderer))]
//namespace TGFDelivery.iOS.MyRenderers
//{
//    public class MyImageRenderer : ImageRenderer
//	{
//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);

//            if (e.PropertyName == "IsLoading")
//            {
//                var handler = Xamarin.Forms.Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(Element.Source);
//                if (!Element.IsLoading && Control.Image == null && handler is ImageLoaderSourceHandler)
//                {
//                    var imageLoader = Element.Source as UriImageSource;
//                    var imgPath = imageLoader.Uri.OriginalString;
//                    NSUrlSession session = NSUrlSession.SharedSession;
//                    var task = session.CreateDataTask(new NSUrl(imgPath), (data, response, error) =>
//                    {
//                        InvokeOnMainThread(() =>
//                        {
//                            if (data != null)
//                            {
//                                Control.Image = UIImage.LoadFromData(data);
//                            }
//                        });
//                    });
//                    task.Resume();
//                }
//            }
//        }
//    }
//}

