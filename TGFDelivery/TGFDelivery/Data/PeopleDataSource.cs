using System.Threading.Tasks;
using TGFDelivery.Models.ServiceModel;
using WinPizzaData;

namespace TGFDelivery.Data
{
    public class AddressData : BindableBase
    {
        public AddressData()
        {
            _DeAddress = new Address();
        }
        public AddressData(Address DeAddress)
        {
            if (DeAddress == null)
                DeAddress = new Address();
            _DeAddress = DeAddress;
        }
        public override string ToString()
        {
            if (DeAddress != null)
                return DeAddress.ToString();
            return string.Empty;
        }

        private Address _DeAddress;
        public Address DeAddress
        {
            get { return _DeAddress; }
            set
            {
                if (_DeAddress != value)
                { _DeAddress = value; OnPropertyChanged(); }
            }
        }

        //      private string _PrimaryStreet;
        public string DeDuration
        {
            get { return DeAddress.DeDuration.text; }
            set { if (DeAddress.DeDuration.text != value) { DeAddress.DeDuration.text = value; OnPropertyChanged(); } }
        }

        public string DeDistance
        {
            get { return DeAddress.DeDistance; }
            set { if (DeAddress.DeDistance != value) { DeAddress.DeDistance = value; OnPropertyChanged(); } }
        }
        public string Company
        {
            get { return DeAddress.Company; }
            set { if (DeAddress.Company != value) { DeAddress.Company = value; OnPropertyChanged(); } }
        }
        public string SubBuilding
        {
            get { return DeAddress.SubBuilding; }
            set { if (DeAddress.SubBuilding != value) { DeAddress.SubBuilding = value; OnPropertyChanged(); } }
        }
        public string BuildingNumber
        {
            get { return DeAddress.BuildingNumber; }
            set { if (DeAddress.BuildingNumber != value) { DeAddress.BuildingNumber = value; OnPropertyChanged(); } }
        }
        public string BuildingName
        {
            get { return DeAddress.BuildingName; }
            set { if (DeAddress.BuildingName != value) { DeAddress.BuildingName = value; OnPropertyChanged(); } }
        }
        public string PostCode
        {
            get { return DeAddress.DePostCode.PostCode; }
            set { if (DeAddress.DePostCode.PostCode != value) { DeAddress.DePostCode.PostCode = value; OnPropertyChanged(); } }
        }
        public string PrimaryStreet
        {
            get { return DeAddress.DePostCode.PrimaryStreet; }
            set { if (DeAddress.DePostCode.PrimaryStreet != value) { DeAddress.DePostCode.PrimaryStreet = value; OnPropertyChanged(); } }
        }
        public string PostTown
        {
            get { return DeAddress.DePostCode.PostTown; }
            set { if (DeAddress.DePostCode.PostTown != value) { DeAddress.DePostCode.PostTown = value; OnPropertyChanged(); } }
        }

    }
    public class PeopleData : BindableBase
    {
        public PeopleData()
        {
            People = new People();
            DeAddress = new AddressData(new Address());
        }
        public PeopleData(People DeValue)
        {
            People = DeValue;
            DeAddress = new AddressData(People.DeAddress);
        }
        private People _People;
        public People People
        {
            get { return _People; }
            set
            {
                if (_People != value)
                { _People = value; OnPropertyChanged(); }
            }
        }
        AddressData _DeAddress;
        public AddressData DeAddress
        {
            get { return _DeAddress; }
            set
            {
                { _DeAddress = value; OnPropertyChanged(); }
            }
        }


        public AddressPostCode DePostCode
        {
            get { return _People.DeAddress?.DePostCode; }
            set
            {
                if (_People.DeAddress != null)
                    _People.DeAddress.DePostCode = value;
                else
                {
                    _People.DeAddress = new Address() { DePostCode = value };
                }
                OnPropertyChanged();
            }
        }
        public string ID
        {
            get { return (People.ISValid(People) ? People.ID : string.Empty); }
            set
            {
                People.ID = value; OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return People?.Name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    People.Name = value;
                }
                else
                {
                    People.Name = "CUSTOMER";
                }
                OnPropertyChanged();
            }
        }


        public string Phone
        {
            get { return People.Phone; }
            set
            {
                People.Phone = value; OnPropertyChanged();

            }
        }
        public string Email
        {
            get { return People.Email; }
            set
            {

                People.Email = value; OnPropertyChanged();
            }
        }

        public int TotalOrder
        {
            get { return People.TotalOrder; }
            set
            {

                People.TotalOrder = value; OnPropertyChanged();
            }
        }

    }
    public sealed class PeopleDataSource
    {
        public static PeopleDataSource _PeopleDataSource = new PeopleDataSource();
        public static async Task<WPMessage> SearchGiftV(string TheCode)
        {
            WPMessage Msg = new WPMessage();
            //var WinPizzaPeopleClient = ServiceChanelSource.GetPeopleServicesChanel(ServiceUrls.PeopleSrv);
            //try
            //{
            //    Msg = await WinPizzaPeopleClient.SearchGiftVAsync(DataSourceName, TheCode);
            //}
            //catch (Exception s)
            //{

            //}
            return Msg;
        }
    }
}
