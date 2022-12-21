namespace TGFDelivery.Data
{
    public static class AppSettings
    {
        #region "CoreService"
        public static string BaseUserServiceAddress = @"http://atoosa.co.uk/thirdpartyuseservices/storeservices.svc/";
        public static string BaseStoreServiceAddress = @"https://services.tgfpizza.com/ThirdPartyServices/storeservices.svc/";
        public static string BaseGermanyStoreServiceAddress = @"http://{0}/WinPizzaStoreServices/WinPizzaStoreServices.WebEnableServices.svc/";
        public static string BaseCardPaymentServiceAddress = @"https://www.korush.eu/BraintreeReporting/CardPayment.svc?wsdl";
        // string BaseGermanyStoreServiceAddress = "http://192.168.2.120/WinPizzaStoreServices/WinPizzaStoreServices.WebEnableServices.svc/";

        public static string Method_FindAddress = "FindAddress?PostCode=";
        public static string Method_FindAddressDe = "PostCodeToStreets?PostCode=";
        public static string Method_GetStoreProfile = "GetStoreProfile?StoreID=";
        public static string Method_GetWebServices = "GetWebServicesEndPoint?StoreID=";
        public static string Method_VerifyPin = "/SendVarficationPIN";
        public static string Method_IsUserVerify = "/IsUserVerified";
        public static string Method_VarifyByEmail = "/VarifyByEmail";
        public static string Method_GetStoreClosed = "/IsStoreClosed";
        public static string Method_GetClientBrainteeToken = "/GetClientBrainteeToken";
        public static string Method_LoadMenu = "/LoadMenu?DataName={0}&MenueID={1}";
        public static string HostURLSubmitOrder = "/SubmitOrderOnline";
        #endregion

        #region "MessageCenter"
        public static string STATUS_LOADING = "Loading";
        public static string STATUS_DONE = "Done";
        public static string STATUS_PRICE = "Price";
        #endregion

        public static string AllowedHosts = "*";
        public static string ShareSiteUrl = @"https://";
        public static string UserVarifyMsg = "Welcome to TGFPIZZA, for your first login you'll need the activation ";
        public static string CountryPhoneCode = "+49";
        public static string CountryCode = "DE";
        public static string MerchantID = "tgfpiz-7139429";
        public static string Password = "WinPizza2020";
        public static string PaymentProcessorDomain = "payzoneonlinepayments.com";
        public static string PaymentProcessorPort = "4430";
        public static string SecretKey = "GdwVdPTm/+HmSC0q7zbG0Utd9D4=";
        public static string TGFMailAddressFrom = "tgfpizza@live.co.uk";
        public static string TGFMailAddressTo = "pedar.pesar.nave@gmail.com";
        public static string MailServerAddress = "smtp.gmail.com";
        public static string FirstPageBanner1 = "slide1.jpg";
        public static string FirstPageBanner2 = "1.jpg";
        public static string FirstPageBanner3 = "2.jpg";
        public static string MenuPageBanner = "Banner.png";
        public static string MenuID = "ONLINE";
        public static string StoreID = "TGFGerrmanyMain";
        public static string ExternalIP = "93.241.85.45";
        public static string InternalIP = "93.241.85.45";
        public static string SiteApplicationName = "TGFStore";
        public static string Culture = "de-DE";
        public static string MaxToppingCount = "3";
        public static string MaxCanModifyToppings = "3";
        public static string BraintreeEnvironment = "sandbox";
        public static string BraintreeMerchantId = "jg8kwkwby734ysyg";
        public static string BraintreePublicKey = "rsvv4mpysxbn2gnn";
        public static string BraintreePrivateKey = "9f5c14dcead778716badac89311ad18a";


    }
}
