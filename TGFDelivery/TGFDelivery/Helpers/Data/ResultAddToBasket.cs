namespace TGFDelivery.Helpers.Data
{
    public class ResultAddToBasket
    {
        // Total product count on basket
        public int TotalItemCount;

        // Total price on basket
        public string TotalPrice;

        // Current order line price
        public string CurrentOrderLinePrice;

        // Result message
        public string Message;

        public ResultAddToBasket()
        {
            TotalItemCount = 0;
            TotalPrice = "0.00";
            CurrentOrderLinePrice = "0.00";
            Message = "unsuccess";
        }
    }
}
