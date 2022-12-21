using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TGFDelivery.Data;
using WinPizzaData;
using WPUtility;

namespace TGFDelivery.Services
{
    public class CoreServices
    {
        public async static Task<string> GetClientBraintreeToken(string DataSourceName)
        {
            string EndPoint = AppSettings.BaseCardPaymentServiceAddress + AppSettings.Method_GetClientBrainteeToken + "?DataBaseName=" + DataSourceName.ToUpper();
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPoint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
            {


            }
            return "";
        }
        /// <summary>
        /// Get store profile from store ID
        /// </summary>
        /// <param name="StoreID">Store ID</param>
        /// <returns></returns>
        public async static Task<FoodStoreProfile> GetStoreProfile(String StoreID)
        {
            var EndPoint = AppSettings.BaseStoreServiceAddress + AppSettings.Method_GetStoreProfile + StoreID;
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPoint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
                return (FoodStoreProfile)JsonConvert.DeserializeObject((string)Msg.WinPizzaObject, typeof(FoodStoreProfile));
            return null;
        }

        public async static Task<bool> GetStoreClosed(string DataSourceName, ServersUrl DeServersUrl, Store DeStore)
        {
            //return false;
            string EndPoint = DeServersUrl.MenuSRV + AppSettings.Method_GetStoreClosed + "?DataBaseName=" + DataSourceName.ToUpper();
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPoint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
            {
                bool IsStoreClosed = !bool.Parse((string)Msg.WinPizzaObject);
                if (!IsStoreClosed)
                {
                    return (!OpeningDate.IsOpen(DeStore.OnlineRoles.FirstOrDefault(p => p.DeDaysOfWeek == DateTime.Now.DayOfWeek.ToString())));
                }
                return !IsStoreClosed;
            }
            return false;
        }

        /// <summary>
        /// Load Main Menu
        /// </summary>
        /// <param name="StoreID">StoreID</param>
        /// <returns></returns>
        public async static Task<Store> LoadMainMenu(string StoreID, string DataSourceName, ServersUrl DeServersUrl, string ImgDomain, string MenuID)
        {
            string strMenuSRV = DeServersUrl.MenuSRV;

            string LoadMenuUrl = string.Format(AppSettings.Method_LoadMenu, DataSourceName.ToUpper(), MenuID);
            Store StoreData = new Store();
            string EndPoint = strMenuSRV + LoadMenuUrl;// + DataSourceName;
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPoint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
            {
                var dd = (string)JsonConvert.DeserializeObject((string)Msg.WinPizzaObject, typeof(string));
                try
                {
                    var ss = XElement.Parse(dd);
                    StoreData = Store.ParsToStore(ss, "ONLINE", ImgDomain);
                    var gg = StoreData.ToppingGrp;


                }
                catch(Exception c)
                {

                }
              //  StoreData.MakeMenuCurrent(MenuID);
            }
            //string contents = File.ReadAllText(@"D:\Process\TGFPizza\Complete\menu.txt");
            //StoreData = Store.ConvertToStore(XElement.Parse((string)JsonConvert.DeserializeObject((string)contents, typeof(string))));
            return StoreData;
        }

        /// <summary>
        /// Get webservices endpoint from storeID
        /// </summary>
        /// <param name="StoreID">Store ID</param>
        /// <returns></returns>
        public async static Task<ServersUrl> GetWebServicesEndPoint(String StoreID)
        {
            var EndPOint = AppSettings.BaseStoreServiceAddress + AppSettings.Method_GetWebServices + StoreID;
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPOint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
                return (ServersUrl)JsonConvert.DeserializeObject((string)Msg.WinPizzaObject, typeof(ServersUrl));
            return null;
        }

        public async static Task<string> FindAddressDe(string DataSourceName, String PostCode, ServersUrl DeServersUrl)
        {


            string strMenuSRV = DeServersUrl.MenuSRV;
            string LoadMenuUrl = string.Format(@"/PostCodeToStreets?DataSource={0}&PostCode={1}&CountryCode={2}", DataSourceName, PostCode, "");
            string EndPoint = strMenuSRV + LoadMenuUrl; ;
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(EndPoint);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
                return (string)Msg.WinPizzaObject;
            return null;
        }

        /// <summary>
        /// Find address from postcode
        /// </summary>
        /// <param name="PostCode">Postcode</param>
        /// <returns></returns>
        public async static Task<string> FindAddress(String PostCode)
        {
            var Msg = await LoadStoreHelperFunctoin.DoWebJsonServices(AppSettings.BaseUserServiceAddress + AppSettings.Method_FindAddress + PostCode);
            if (Msg.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
                return (string)Msg.WinPizzaObject;
            return null;
        }

        public static async Task<List<WinPizzaData.Address>> GetAddressFromPostcode(string PostCode, FoodStoreProfile StoreProfile, string CountryCode, ServersUrl DeServersUrl)
        {
            // lets setup a country Code where?
            // from system culture, i think each store site has count code, we can set in config
            //CultureInfo.CurrentCulture.Name
            // i think to set in config is better
            //ok  add coutrokycodeic
            List<Address> lstAddress = new List<Address>();
            if (CountryCode == WinPizzaEnums.AddresFormat.GERMANY.ToString())
            {
                // i need datbasename, and serverUrl ???
                string strResponse = await FindAddressDe(StoreProfile.DeDataSourceName, PostCode, DeServersUrl);
                strResponse = strResponse.Replace("\"", "");
                string[] AddressList = strResponse.Split(';');
                for (int i = 0; i < AddressList.Length - 1; i++)
                {
                    Address DeAddress = new Address();
                    string[] data = AddressList[i].Split('@');
                    DeAddress.DePostCode.PrimaryStreet = data[0];
                    DeAddress.DePostCode.SecondaryStreet = data[1];
                    DeAddress.DePostCode.PostCode = PostCode;
                    lstAddress.Add(DeAddress);
                }
            }
            else
            {
                // Get address list from postcode with backend api
                string strResponse = await FindAddress(PostCode);

                int nStartIndex = strResponse.IndexOf("<lat>") + 5;
                int nEndIndex = strResponse.IndexOf("<\\/lat>", nStartIndex);
                string strLat = strResponse.Substring(nStartIndex, nEndIndex - nStartIndex);

                nStartIndex = strResponse.IndexOf("<lng>") + 5;
                nEndIndex = strResponse.IndexOf("<\\/lng>", nStartIndex);
                string strLng = strResponse.Substring(nStartIndex, nEndIndex - nStartIndex);

                strResponse = strResponse.Replace("<Data>", "*");
                strResponse = strResponse.Replace("<\\/Data>", "^");
                string pattern = @"\*(?<val>[^\^]*)";
                MatchCollection data_match = Regex.Matches(strResponse, pattern);


                foreach (Match match in data_match)
                {
                    Address DeAddress = new Address();

                    // Pattern <BuildingName> - <\/BuildingName>
                    string strData = match.Groups["val"].Value;
                    pattern = @"<BuildingName>(?<val>[^\<]*)";
                    Match temp_match = Regex.Match(strData, pattern);
                    DeAddress.BuildingName = temp_match.Groups["val"].Value;

                    // Pattern <BuildingNumber> - <\/BuildingNumber>
                    pattern = @"<BuildingNumber>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.BuildingNumber = temp_match.Groups["val"].Value;

                    // Pattern <Company> - <\/Company>
                    pattern = @"<Company>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.Company = temp_match.Groups["val"].Value;
                    DeAddress.Company.Replace("&amp;", "&");

                    // Pattern <SubBuilding> - <\/SubBuilding>
                    pattern = @"<SubBuilding>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.SubBuilding = temp_match.Groups["val"].Value;

                    // Pattern <CountryName> - <\/CountryName>
                    pattern = @"<CountryName>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.Country = temp_match.Groups["val"].Value;

                    // Pattern <County> - <\/County>
                    pattern = @"<County>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.County = temp_match.Groups["val"].Value;

                    // Pattern <PostTown> - <\/PostTown>
                    pattern = @"<PostTown>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.PostTown = temp_match.Groups["val"].Value;

                    // Pattern <PrimaryStreet> - <\/PrimaryStreet>
                    pattern = @"<PrimaryStreet>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.PrimaryStreet = temp_match.Groups["val"].Value;

                    // Pattern <SecondaryStreet> - <\/SecondaryStreet>
                    pattern = @"<SecondaryStreet>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.SecondaryStreet = temp_match.Groups["val"].Value;

                    // Pattern <Postcode> - <\/Postcode>
                    pattern = @"<Postcode>(?<val>[^\<]*)";
                    temp_match = Regex.Match(strData, pattern);
                    DeAddress.DePostCode.PostCode = temp_match.Groups["val"].Value;

                    DeAddress.DePostCode.Lat = strLat;
                    DeAddress.DePostCode.lng = strLng;

                    lstAddress.Add(DeAddress);
                }
            }
            return lstAddress;
        }

        /// <summary>
        /// Submit order to store
        /// </summary>
        /// <param name="StoreID">Store ID</param>
        /// <param name="DeServersUrl">Server Url</param>
        /// <param name="DeBasket">Basket</param>
        /// <param name="DeDataSourceName">Data source name</param>
        /// <param name="DeOrderType">Order type 1 : Delivery 2 : Collect</param>
        /// <param name="PostCode">Postcode</param>
        /// <param name="DeAddress">User Address</param>
        /// <returns></returns>
        public static async Task<WPMessage> SubmitOrder(string StoreID, ServersUrl DeServersUrl, Basket DeBasket, string DeDataSourceName, WinPizzaEnums.OrderType DeOrderType, string PostCode, Address DeAddress, string CountryCode)
        {
            DateTime CurTime = DateTime.Now;
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var newDate = TimeZoneInfo.ConvertTime(CurTime, TimeZoneInfo.Local, britishZone);
            WPMessage Msg = new WPMessage();
            DeBasket.DeOrderHeader.StoreID = StoreID;
            DeBasket.DeOrderHeader.DeSalesAgent.OrderTaken_StaffID = WinPizzaEnums.OrderFrom.WEB.ToString();
            DeBasket.DeOrderHeader.DeOrderType = DeOrderType.ToString();
            DeBasket.DeOrderHeader.OrderFrom = WinPizzaEnums.OrderFrom.WEB;
            //DeBasket.DeOrderHeader.DeOrdTimeInfo =
            // new OrdTimeInfo()
            //{
            //    TsDlryOrd = newDate,
            //    TeDlryOrd = newDate,
            //    TsGetOrd = newDate,
            //    TeGetOrd = newDate,
            //    TDispachOrd = newDate,
            //    TsPrepOrd = newDate,
            //    TePrepOrd = newDate,
            //    TExpectedOrdReady = newDate,
            //    TimedOrd = newDate
            //};

            // we let server to set the time still we have to test this i do not know if works from 
            // your pc location
            DeBasket.DeOrderHeader.DeOrdTimeInfo = null;
            //    Msg.WinPizzaObject = Newtonsoft.Json.JsonConvert.SerializeObject(DeOrderData.DeOrder);
            if (DeAddress != null)
            {
                DeBasket.DeOrderHeader.DePeople.DeAddress = DeAddress;
            }
            else
            {
                DeBasket.DeOrderHeader.DePeople.DeAddress = new Address();
            }
            DeBasket.DeOrderHeader.DePeople.DeAddress.DePostCode.StoreID = StoreID;
            DeBasket.DeOrderHeader.DePeople.DeAddress.DeDuration = new Duration();
            DeBasket.DeOrderHeader.DePeople.DeAddress.DeDuration.text = "";
            DeBasket.DeOrderHeader.DePeople.DeAddress.DeDuration.value = 0;
            Msg.WinPizzaObject = Newtonsoft.Json.JsonConvert.SerializeObject(DeBasket);
            // okay?storeid?yesok country code?DE?yes , but do not want to hard code it , one sec
            // okay?ok

            Msg.DeMsgBody = DeDataSourceName + ";" + CountryCode;
            Msg.DeMsgType = WinPizzaEnums.MessageType.NEWORDER;
            string EndPoint = DeServersUrl.MenuSRV + AppSettings.HostURLSubmitOrder;
            return await LoadStoreHelperFunctoin.PostWebJson(EndPoint, Msg);
        }

        public static async Task<bool> IsUserVerified(string PhoneNumber, string StoreID, ServersUrl DeServersUrl)
        {
            WPMessage Msg = new WPMessage();
            Msg.DeMsgBody = PhoneNumber;// this can be email or mobile            
            Msg.WinPizzaObject = StoreID;// this has the format  "StoreID;
            string EndPoint = DeServersUrl.MenuSRV + AppSettings.Method_IsUserVerify;
            WPMessage resultMessage = await LoadStoreHelperFunctoin.PostWebJson(EndPoint, Msg);
            if (resultMessage.DeMsgType == WinPizzaEnums.MessageType.ACTIONSUCCESS)
            {
                return resultMessage.DeMsgBody == WinPizzaEnums.Varification.Varified.ToString() ? false : false;
            }
            return false;
        }

        public static async Task<WPMessage> SendSMS(PhoneNumber PhoneN, FoodStoreProfile Fs, string DePhone, string DeMessage, ServersUrl DeServersUrl)
        {
            WPMessage Msg = new WPMessage();
            Msg.DeMsgBody = DeMessage;
            Msg.WinPizzaObject = DePhone + ";";
            PhoneN.DataSourceName = Fs.DeDataSourceName;
            PhoneN.StoreID = Fs.StoreID;
            Msg.DataObject = JsonConvert.SerializeObject(PhoneN);
            Msg = await LoadStoreHelperFunctoin.PostWebJson(DeServersUrl.MenuSRV + AppSettings.Method_VerifyPin, Msg);
            return Msg;
        }

        public static async Task<WPMessage> SendEmail(string DeName, string DeEmail, String DeStoreID, ServersUrl DeServersUrl)
        {
            WPMessage Msg = new WPMessage();
            Msg.DeMsgBody = DeName + ";" + DeEmail;
            Msg.WinPizzaObject = DeStoreID;
            //BaseStoreServiceAddress = string.Format(BaseStoreServiceAddress, InternallIP);
            return await LoadStoreHelperFunctoin.PostWebJson(DeServersUrl.MenuSRV + AppSettings.Method_VarifyByEmail, Msg);
        }

        public static async Task<WPMessage> SubmitVerifyCode(string DeVerifyCode, ServersUrl DeServersUrl)
        {
            WPMessage Msg = new WPMessage();
            Msg.DeMsgBody = DeVerifyCode;
            return await LoadStoreHelperFunctoin.PostWebJson(DeServersUrl + AppSettings.Method_VerifyPin, Msg);
        }

        public static WinPizzaEnums.OrderStatus SendMail(string XmlOrd, string SMTPHost, string EmailFrom, string EmailTo, string Subject)
        {
            WinPizzaEnums.OrderStatus CurrentStatus = WinPizzaEnums.OrderStatus.FAILED;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(EmailFrom);
                    mail.To.Add(EmailTo);
                    mail.Subject = Subject;
                    mail.Body = XmlOrd;
                    mail.Priority = MailPriority.Normal;
                    using (SmtpClient client = new SmtpClient(SMTPHost, 587))
                    {
                        client.EnableSsl = true;
                        client.Credentials = CredentialCache.DefaultNetworkCredentials;
                        client.Send(mail);
                        CurrentStatus = WinPizzaEnums.OrderStatus.EMAILED;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return CurrentStatus;
        }

        public static string CardPay(string MerchantID, string Password, string SecretKey, string SiteUrl, string Domain, Basket DeBasket, string NameOnCard, string CardNumber, string CardExpiry, string CardCVC, string Postcode, string OrderNumber)
        {
            string m_szFormAction = SiteUrl + "PaymentForm.aspx";

            string Amount = (DeBasket.DeOrderHeader.Total * 100).ToString().Replace(".00", "");
            string CurrencyCode = "826";
            string OrderID = OrderNumber;
            string TransactionType = "PREAUTH";
            string OrderDescription = OrderNumber;

            // the GMT/UTC relative date/time for the transaction (MUST either be in GMT/UTC 
            // or MUST include the correct timezone offset)
            DateTime CurTime = DateTime.Now;
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var newDate = TimeZoneInfo.ConvertTime(CurTime, TimeZoneInfo.Local, britishZone);

            string m_szTransactionDateTime = newDate.ToString("yyyy-MM-dd HH:mm:ss zzz");

            // the country code - ISO 3166-1  3-digit numeric (e.g. UK = 826)
            // PLEASE NOTE: Currently, do not change this from the value of 826.
            string m_szCountryCode = Convert.ToString(826);

            string szStringToHash = PaymentFormHelper.GenerateStringToHashInitial(MerchantID, Password, Amount, CurrencyCode, OrderID, TransactionType, m_szTransactionDateTime, SiteUrl, OrderDescription, SecretKey, HASH_METHOD.SHA1);

            string m_szHashDigest = PaymentFormHelper.CalculateHashDigest(szStringToHash, SecretKey, HASH_METHOD.SHA1);

            m_szFormAction = "https://mms." + Domain + "/Pages/PublicPages/TransparentRedirect.aspx";

            var param = new Dictionary<string, string>();
            param["HashDigest"] = m_szHashDigest;
            param["MerchantID"] = MerchantID;
            param["Amount"] = Amount;
            param["CurrencyCode"] = CurrencyCode;
            param["OrderID"] = OrderID;
            param["TransactionType"] = TransactionType;
            param["TransactionDateTime"] = m_szTransactionDateTime;
            param["CallbackURL"] = SiteUrl;
            param["OrderDescription"] = OrderDescription;
            param["CardName"] = NameOnCard;
            param["CardNumber"] = CardNumber;
            param["ExpiryDateMonth"] = CardExpiry.Split('/')[0];
            string strYear = CardExpiry.Split('/')[1];
            if (strYear.Length == 4)
            {
                strYear = strYear.Substring(2, 2);
            }
            param["ExpiryDateYear"] = strYear;
            param["StartDateMonth"] = "";
            param["StartDateYear"] = "";
            param["IssueNumber"] = "";
            param["CV2"] = CardCVC;
            param["Address1"] = "";
            param["Address2"] = "";
            param["Address3"] = "";
            param["Address4"] = "";
            param["City"] = "";
            param["State"] = "";
            param["PostCode"] = Postcode;
            //param["Address1"] = DeBasket.DeOrderHeader.DePeople.DeAddress.BuildingNumber + " " + DeBasket.DeOrderHeader.DePeople.DeAddress.DePostCode.PrimaryStreet;
            //param["Address2"] = "";
            //param["Address3"] = "";
            //param["Address4"] = "";
            //param["City"] = DeBasket.DeOrderHeader.DePeople.DeAddress.DePostCode.County;
            //param["State"] = DeBasket.DeOrderHeader.DePeople.DeAddress.DePostCode.PostTown;
            //param["PostCode"] = DeBasket.DeOrderHeader.DePeople.DeAddress.DePostCode.PostCode;
            param["CountryCode"] = CurrencyCode;

            string paramData = BuildQueryData(param);
            string postData = paramData;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(m_szFormAction);
            webRequest.Method = "POST";

            try
            {
                if (postData != "")
                {
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    var data = Encoding.UTF8.GetBytes(postData);
                    using (var stream = webRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream str = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(str))
                {
                    string strResult = sr.ReadToEnd();
                    int nStartIndex = strResult.IndexOf("<span id=\"lbErrorMessageLabel\"><ul><li>");
                    int nEndIndex = strResult.IndexOf("</li></ul></span>", nStartIndex + 39);
                    if (nStartIndex > -1 && nEndIndex > -1)
                    {
                        strResult = "Error: " + strResult.Substring(nStartIndex + 39, nEndIndex - nStartIndex - 39).Replace("&quot;", "");
                    }
                    else
                    {
                        nStartIndex = strResult.IndexOf("<input id=\"Message\" name=\"Message\" type=\"hidden\" value=\"");
                        int nnEndIndex = strResult.IndexOf("\" />", nStartIndex);
                        if (nStartIndex > 0 && nnEndIndex > 0)
                        {
                            strResult = strResult.Substring(nStartIndex + 56, nnEndIndex - nStartIndex - 56);
                            if (strResult.IndexOf("AuthCode:") == -1)
                            {
                                strResult = "Error: " + strResult;
                            }
                        }
                    }
                    return strResult;
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (response == null)
                        throw;

                    using (Stream str = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static string BuildQueryData(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            StringBuilder b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value)));

            try { return b.ToString().Substring(1); }
            catch (Exception) { return ""; }
        }

        private static string BuildJSON(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            var entries = new List<string>();
            foreach (var item in param)
                entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));

            return "{" + string.Join(",", entries) + "}";
        }
    }
}
