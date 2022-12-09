using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TGFDelivery.Data;
using TGFDelivery.Models.ServiceModel;
using WinPizzaData;
using Xamarin.Forms;

namespace TGFDelivery.Views.Tab
{
    public class ProductPageModel : FreshBasePageModel
    {

        public override void Init(object initData)
        {
            ((ProductPage)this.CurrentPage).SetCategory((Category)initData);
        }
    }
}
