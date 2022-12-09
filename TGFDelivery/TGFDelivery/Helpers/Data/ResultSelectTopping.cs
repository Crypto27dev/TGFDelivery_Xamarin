using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGFDelivery.Helpers.Data
{
    public class ResultSelectTopping
    {
        public string Price;

        public string Portion;

        public string Message;

        public ResultSelectTopping()
        {
            Price = "0.00";
            Portion = "0";
            Message = "success";
        }
    }
}
