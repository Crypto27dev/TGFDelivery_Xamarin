namespace TGFDelivery.Helpers.Data
{
    public class SelectedSide
    {
        public string ToppingID { get; set; }

        public int ToppingPortion { get; set; }

        public string ToppingName { get; set; }

        public SelectedSide(string ID, int Portion, string Name)
        {
            ToppingID = ID;
            ToppingPortion = Portion;
            ToppingName = Name;
        }
    }
}
