namespace TGFDelivery.Helpers.Data
{
    public class ResultAddToOfferOrder
    {
        public string ProductName;

        public string TotalPrice;

        public string Message;

        public bool IsAllPicked;

        public ResultAddToOfferOrder(string strProductName, string dAllPrice, string strMessage, bool bIsAllPicked = false)
        {
            ProductName = strProductName;
            TotalPrice = dAllPrice;
            Message = strMessage;
            IsAllPicked = bIsAllPicked;
        }
    }
}
