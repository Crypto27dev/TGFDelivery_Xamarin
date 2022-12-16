namespace TGFDelivery.Models.ViewCellModel
{
    public class CrustViewCellModel
    {
        public CrustViewCellModel(string size, string price, string backColor)
        {
            Size = size;
            Price = price;
            BackColor = backColor;
        }

        public string Size { get; set; }
        public string Price { get; set; }
        public string BackColor { get; set; }
    }
}
