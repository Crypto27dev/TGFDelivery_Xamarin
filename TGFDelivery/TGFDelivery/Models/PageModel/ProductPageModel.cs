using FreshMvvm;
using WinPizzaData;

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
