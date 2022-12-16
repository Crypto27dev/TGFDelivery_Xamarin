using System;
using System.Collections.Generic;
using System.Globalization;
using TGFDelivery.Models.ServiceModel;
using WinPizzaData;
using WPUtility;

namespace TGFDelivery.Data
{
    public class OrderLineData : BindableBase
    {
        public OrderLineData()
        {
            _DeOrderLine = new OrderLine();
            SelectedSide = -1;
        }
        public OrderLineData(OrderLine TemValue)
        {
            _DeOrderLine = TemValue;
            SelectedSide = -1;
        }


        public static List<OrderLineData> GetWraper(List<OrderLine> DeItems)
        {
            var DeWraperItem = new List<OrderLineData>();
            DeItems.ForEach(p => { DeWraperItem.Add(new OrderLineData(p)); });
            return DeWraperItem;
        }

        private OrderLine _DeOrderLine;
        public OrderLine DeOrderLine
        {
            get { return _DeOrderLine; }
            set
            {
                _DeOrderLine = value;
                OnPropertyChanged();
            }
        }
        public string Name4Binding
        {
            get { return _DeOrderLine.Name4Binding; }
            set
            {
                _DeOrderLine.Name4Binding = value;
                OnPropertyChanged();
            }
        }
        public Decimal Price
        {
            get { return _DeOrderLine.Price; }
            set
            {
                _DeOrderLine.Price = value;
                OnPropertyChanged();
            }
        }

        public Decimal OfferPrice
        {
            get { return _DeOrderLine.OfferPrice; }
            set
            {
                _DeOrderLine.OfferPrice = value;
                OnPropertyChanged();
            }
        }

        public string Topping4Binding
        {
            get { return _DeOrderLine.Topping4Binding; }
            set
            {
                _DeOrderLine.Topping4Binding = value;
                OnPropertyChanged();
            }
        }
        public string Side4Binding
        {
            get { return _DeOrderLine.Side4Binding; }
            set
            {
                _DeOrderLine.Side4Binding = value;
                OnPropertyChanged();
            }
        }
        public bool IsDeal
        {
            get { return _DeOrderLine.IsDeal; }
            set
            {
                _DeOrderLine.IsDeal = value;
                OnPropertyChanged();
            }
        }
        public bool DealPart
        {
            get { return _DeOrderLine.DealPart; }
            set
            {
                _DeOrderLine.DealPart = value;
                OnPropertyChanged();
            }
        }

        public bool IsOfferItemPicked
        {
            get { return _DeOrderLine.IsOfferItemPicked; }
            set
            {
                _DeOrderLine.IsOfferItemPicked = value;
                OnPropertyChanged();
            }
        }
        public MixedOption DeMixedOption
        {
            get { return _DeOrderLine.DeMixedOption; }
            set
            {
                _DeOrderLine.DeMixedOption = value;
            }
        }
        public WinPizzaEnums.PrepStatus DePrepStatus
        {
            get { return _DeOrderLine.DePrepStatus; }
            set
            {
                _DeOrderLine.DePrepStatus = value;
                OnPropertyChanged();
            }
        }
        public WinPizzaEnums.OrderLineStatus DeStatus
        {
            get { return _DeOrderLine.DeStatus; }
            set
            {
                _DeOrderLine.DeStatus = value;
                OnPropertyChanged();
            }
        }

        public int Qty
        {
            get { return _DeOrderLine.Qty; }
            set
            {
                _DeOrderLine.Qty = value;
                OnPropertyChanged();
            }
        }
        int _SelectedSide;
        public int SelectedSide
        {
            get { return _SelectedSide; }
            set
            {
                _SelectedSide = value;
                OnPropertyChanged();
            }
        }
    }

    public class OrderHeaderData : BindableBase
    {
        public OrderHeaderData(OrdertHeader Value)
        {
            if (Value != null)
            {
                OrderHeader = Value;
                if (Value.DePeople != null)
                    DePeople = new PeopleData(Value.DePeople);
                // OrderHeader.DeOrderTypeData = OrderHeaderDataSource.GetServingObject(DeOrderType);
                /// check if statuc is on cooking and this sation is prep
                /// then start timer checking for status of cooking.              
            }
        }
        public OrderHeaderData()
        {
            OrderHeader = new OrdertHeader();

        }
        private OrdertHeader _OrderHeader;
        public OrdertHeader OrderHeader
        {
            get { return _OrderHeader; }
            set
            {
                _OrderHeader = value;
                OnPropertyChanged();
            }
        }
        public string From
        {
            get { return OrderHeader.From; }
            set
            {
                OrderHeader.From = value;
                OnPropertyChanged();
            }
        }
        public string PayMethod
        {
            get { return OrderHeader.PaymentTypeIntentionTxt; }
            set
            {
                OrderHeader.PaymentTypeIntentionTxt = value;
                OnPropertyChanged();
            }
        }
        //   private PeopleData _DePeople;
        public PeopleData DePeople
        {
            get { return new PeopleData(OrderHeader.DePeople); }
            set
            {
                OrderHeader.DePeople = (value == null ? null : value.People);
                OnPropertyChanged();
            }
        }
        public WinPizzaEnums.PaymentStatus DePayStatus
        {
            get { return _OrderHeader.DePayStatus; }
            set
            {
                _OrderHeader.DePayStatus = value;
            }

        }

        public string DeOrderType
        {
            get { return _OrderHeader.DeOrderType; }
            set
            {
                _OrderHeader.DeOrderType = value;//(WinPizzaEnums.OrderType)Enum.Parse(typeof(WinPizzaEnums.OrderType), value, false);
                OnPropertyChanged();
            }
        }
        public string ImgSrc
        {
            get { return _OrderHeader.DeOrderTypeData.ImgSrc; }
            set
            {
                _OrderHeader.DeOrderTypeData.ImgSrc = value;
                OnPropertyChanged();
            }
        }
        public string DeSts
        {
            get { return _OrderHeader.DeSts.ToString(); }
            set
            {
                if (DeSts.ToString() != value)
                {
                    _OrderHeader.DeSts = (WinPizzaEnums.OrderStatus)Enum.Parse(typeof(WinPizzaEnums.OrderStatus), value, false);
                    OnPropertyChanged();
                }
            }
        }
        public int TableID
        {
            get { return _OrderHeader.TableID; }
            set { _OrderHeader.TableID = value; OnPropertyChanged(); }
        }
        public string SectionName
        {
            get { return _OrderHeader.SectionName; }
            set { _OrderHeader.SectionName = value; OnPropertyChanged(); }
        }
        public int Seats
        {
            get { return (_OrderHeader.DeTable != null ? _OrderHeader.DeTable.Seats : 0); }
            set
            {
                if (_OrderHeader.DeTable != null)
                    _OrderHeader.DeTable.Seats = value;
                OnPropertyChanged();
            }
        }
        public DateTime TeGetOrd
        {
            get { return _OrderHeader.DeOrdTimeInfo.TeGetOrd; }
            set { _OrderHeader.DeOrdTimeInfo.TeGetOrd = value; OnPropertyChanged(); }
        }
        public decimal Total
        {
            get { return _OrderHeader.Total; }
            set { _OrderHeader.Total = value; OnPropertyChanged(); }
        }
        public string ID
        {
            get { return _OrderHeader.ID; }
            set { _OrderHeader.ID = value; OnPropertyChanged(); }
        }
        public long SqlID
        {
            get { return _OrderHeader.SqlID; }
            set { _OrderHeader.SqlID = value; OnPropertyChanged(); }
        }
        //public WriteableBitmap _BarCodeBtm;
        //public WriteableBitmap BarCodeBtm
        //{
        //    set
        //    {
        //        _BarCodeBtm = value; OnPropertyChanged();
        //    }
        //    get { return _BarCodeBtm; }
        //}

        string _TableName = string.Empty;
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; OnPropertyChanged(); }
        }
        bool _BarCodeOn = false;
        public bool BarCodeOn
        {
            get { return _BarCodeOn; }
            set { _BarCodeOn = value; OnPropertyChanged(); }
        }
        public string AddressStr
        {
            get
            {
                if (_OrderHeader != null && _OrderHeader.DePeople != null)
                    return DePeople.DeAddress.ToString();
                return "";
            }
            set { _OrderHeader.AddressStr = value; OnPropertyChanged(); }
        }
        public string PeopleStr
        {
            get
            {
                if (_OrderHeader != null && _OrderHeader.DePeople != null)
                    return DePeople.ToString();
                return "";
            }
            set { _OrderHeader.DePeople.PeopleString = value; OnPropertyChanged(); }
        }
    }

    public class Orderdata : BindableBase
    {
        public Orderdata()
        {
            DeOrder = new Basket();
        }
        public Orderdata(Basket aOrder)
        {
            DeOrder = aOrder;
            if (aOrder != null)
                aOrder.DeOrderLines.ForEach(p =>
                {
                    DeOrderLines.Clear();
                    DeOrderLines.Add(new OrderLineData(p));

                });
            else
                DeOrder = new Basket();
        }
        private Basket _DeOrder;
        public Basket DeOrder
        {
            get { return _DeOrder; }
            set
            {
                _DeOrder = value;
                OnPropertyChanged("DeOrder");
            }
        }

        public void CopyTo(List<OrderLine> DeOrdLines)
        {
            DeOrdLines.ForEach(p =>
            {
                DeOrderLines.Add(new OrderLineData(p));

            });
        }
        private List<OrderLineData> _DeOrderLines = new List<OrderLineData>();
        public List<OrderLineData> DeOrderLines
        {
            get { return OrderLineData.GetWraper(DeOrder.DeOrderLines); }
            set
            {
                _DeOrderLines = value;
                OnPropertyChanged();
            }
        }

        // OrderHeaderData _DeOrderHeader =  new OrderHeaderData();
        public OrderHeaderData DeOrderHeader
        {
            get { return new OrderHeaderData(DeOrder.DeOrderHeader); }
            set
            {
                DeOrder.DeOrderHeader = value.OrderHeader;
                OnPropertyChanged();
            }
        }

    }
    public sealed class BasketDataSource
    {

        public static BasketDataSource _BasketDataSource = new BasketDataSource();
        static private Orderdata _BasketData = new Orderdata();

        static public Orderdata BasketData
        {
            get { return _BasketData; }
            set { _BasketData = value; }
        }

        public static void ResetBasket()
        {
            Orderdata _BasketData = new Orderdata();
        }
        static string GetPrintingGrp(string DePrintingGrp)
        {
            if (StoreDataSource.EposMode)
                return SelectedFoodGrp;// DeClientSettingData.IsPrintingGrp == true ? SelectedFoodGrp : DePrintingGrp;
            else
                return "";
        }
        public string PriceFormatting(decimal Price)
        {
            return Price.ToString((CultureInfo)CultureInfo.CurrentCulture) + CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol;
        }
        public static void DoNoCustomize(WPBaseProduct DeProduct)
        {
            if (IsOfferInProfress)
            //  if(DeOrderLineData.IsDeal)
            {
                // DoHandelMealDealPart(DeProduct, PrGrp, VatRate);
            }
            else
            {
                //var OrderLineObj = new OrderLine(DeProduct, GetPrintingGrp(DeProduct.DePrintingGrp), VatRate, SelectedGuest, StoreDataSource.GetCurrentStore().CurrentMenuID);
                //   TheOrder.DeOrder.AddLine(OrderLineObj);
                //  ProductImg = OrderLineObj.SelectedProduct.ImgUrl;              
            }
        }
        public static void CreateOrder(string PostCode, string OrderType)
        {
            BasketData = new Orderdata();
            BasketData.DeOrderHeader.DeOrderType = OrderType;
            BasketData.DeOrderHeader.DePeople = new PeopleData();
            BasketData.DeOrderHeader.DePeople.DeAddress = new AddressData();
            BasketData.DeOrderHeader.DePeople.DeAddress.PostCode = PostCode;
        }

        public static bool IsOfferInProfress
        {
            set; get;
        }

        public static string SelectedFoodGrp
        {
            set; get;
        }
    }
}
