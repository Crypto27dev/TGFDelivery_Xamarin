using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGFDelivery.Data;
using TGFDelivery.Helpers;
using TGFDelivery.Models.ServiceModel;
using WinPizzaData;

namespace TGFDelivery.Models
{
    public class IndexPageModel : ViewModelBase
    {
        #region "Language"
        public string PostCodePlaceHolder
        {
            get { return Languages.PostCode; }
        }
        public string Collection
        {
            get { return Languages.Collection.ToUpper(); }
        }
        public string Delivery
        {
            get { return Languages.Delivery.ToUpper(); }
        }
        public string Login
        {
            get { return Languages.Login.ToUpper(); }
        }       
       
        #endregion
    }
}
