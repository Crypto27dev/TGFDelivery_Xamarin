namespace TGFDelivery.Models.ViewCellModel
{
    public class SizeViewCellModel
    {
        public SizeViewCellModel(string size, string price, string backcolor)
        {
            Size = size;
            Price = price;
            BackColor = backcolor;
        }

        public string Size { get; set; }
        public string Price { get; set; }
        public string BackColor { get; set; }
    }
}
