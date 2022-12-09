using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
//using Microsoft.AspNetCore.Http;

/// <summary>
/// Summary description for PaymentFormHelper
/// </summary>
namespace TGFDelivery.Services
{
    public enum HASH_METHOD
    {
        UNKNOWN,
        MD5,
        SHA1,
        HMACMD5,
        HMACSHA1
    }

    public class TransactionResult
    {
        private int m_nStatusCode;
        private string m_szMessage;
        private int? m_nPreviousStatusCode;
        private string m_szPreviousMessage;
        private string m_szCrossReference;
        private UInt64 m_nAmount;
        private int m_nCurrencyCode;
        private string m_szOrderID;
        private string m_szTransactionType;
        private string m_szTransactionDateTime;
        private string m_szOrderDescription;
        private string m_szCustomerName;
        private string m_szAddress1;
        private string m_szAddress2;
        private string m_szAddress3;
        private string m_szAddress4;
        private string m_szCity;
        private string m_szState;
        private string m_szPostCode;
        private int? m_nCountryCode;

        private string m_szAddressNumericCheckResult;
        private string m_szPostCodeCheckResult;
        private string m_szCV2CheckResult;
        private string m_szThreeDSecureAuthenticationCheckResult;
        private string m_szCardType;
        private string m_szCardClass;
        private string m_szCardIssuer;
        private int? m_nCardIssuerCountryCode;
        private string m_szEmailAddress;
        private string m_szPhoneNumber;

        private string m_szACSUrl;
        private string m_szPaREQ;
        private string m_szPaRES;
        private string m_szCallbackUrl;

        public int StatusCode
        {
            get { return (m_nStatusCode); }
            set { m_nStatusCode = value; }
        }
        public string Message
        {
            get { return (m_szMessage); }
            set { m_szMessage = value; }
        }
        public int? PreviousStatusCode
        {
            get { return (m_nPreviousStatusCode); }
            set { m_nPreviousStatusCode = value; }
        }
        public string PreviousMessage
        {
            get { return (m_szPreviousMessage); }
            set { m_szPreviousMessage = value; }
        }
        public string CrossReference
        {
            get { return (m_szCrossReference); }
            set { m_szCrossReference = value; }
        }
        public UInt64 Amount
        {
            get { return (m_nAmount); }
            set { m_nAmount = value; }
        }
        public int CurrencyCode
        {
            get { return (m_nCurrencyCode); }
            set { m_nCurrencyCode = value; }
        }
        public string OrderID
        {
            get { return (m_szOrderID); }
            set { m_szOrderID = value; }
        }
        public string TransactionType
        {
            get { return (m_szTransactionType); }
            set { m_szTransactionType = value; }
        }
        public string TransactionDateTime
        {
            get { return (m_szTransactionDateTime); }
            set { m_szTransactionDateTime = value; }
        }
        public string OrderDescription
        {
            get { return (m_szOrderDescription); }
            set { m_szOrderDescription = value; }
        }
        public string CustomerName
        {
            get { return (m_szCustomerName); }
            set { m_szCustomerName = value; }
        }
        public string Address1
        {
            get { return (m_szAddress1); }
            set { m_szAddress1 = value; }
        }
        public string Address2
        {
            get { return (m_szAddress2); }
            set { m_szAddress2 = value; }
        }
        public string Address3
        {
            get { return (m_szAddress3); }
            set { m_szAddress3 = value; }
        }
        public string Address4
        {
            get { return (m_szAddress4); }
            set { m_szAddress4 = value; }
        }
        public string City
        {
            get { return (m_szCity); }
            set { m_szCity = value; }
        }
        public string State
        {
            get { return (m_szState); }
            set { m_szState = value; }
        }
        public string PostCode
        {
            get { return (m_szPostCode); }
            set { m_szPostCode = value; }
        }
        public int? CountryCode
        {
            get { return (m_nCountryCode); }
            set { m_nCountryCode = value; }
        }


        public string AddressNumericCheckResult
        {
            get { return (m_szAddressNumericCheckResult); }
            set { m_szAddressNumericCheckResult = value; }
        }
        public string PostCodeCheckResult
        {
            get { return (m_szPostCodeCheckResult); }
            set { m_szPostCodeCheckResult = value; }
        }
        public string CV2CheckResult
        {
            get { return (m_szCV2CheckResult); }
            set { m_szCV2CheckResult = value; }
        }
        public string ThreeDSecureAuthenticationCheckResult
        {
            get { return (m_szThreeDSecureAuthenticationCheckResult); }
            set { m_szThreeDSecureAuthenticationCheckResult = value; }
        }
        public string CardType
        {
            get { return (m_szCardType); }
            set { m_szCardType = value; }
        }
        public string CardClass
        {
            get { return (m_szCardClass); }
            set { m_szCardClass = value; }
        }
        public string CardIssuer
        {
            get { return (m_szCardIssuer); }
            set { m_szCardIssuer = value; }
        }
        public int? CardIssuerCountryCode
        {
            get { return (m_nCardIssuerCountryCode); }
            set { m_nCardIssuerCountryCode = value; }
        }
        public string EmailAddress
        {
            get { return (m_szEmailAddress); }
            set { m_szEmailAddress = value; }
        }
        public string PhoneNumber
        {
            get { return (m_szPhoneNumber); }
            set { m_szPhoneNumber = value; }
        }

        public string ACSUrl
        {
            get { return (m_szACSUrl); }
            set { m_szACSUrl = value; }
        }
        public string PaREQ
        {
            get { return (m_szPaREQ); }
            set { m_szPaREQ = value; }
        }
        public string PaRES
        {
            get { return (m_szPaRES); }
            set { m_szPaRES = value; }
        }
        public string CallbackUrl
        {
            get { return (m_szCallbackUrl); }
            set { m_szCallbackUrl = value; }
        }
    }

    public class PaymentFormHelper
    {
        public static string GetHashMethod(HASH_METHOD hmHashMethod)
        {
            return (hmHashMethod.ToString());
        }
        public static HASH_METHOD GetHashMethod(string szHashMethod)
        {
            HASH_METHOD hmHashMethod = HASH_METHOD.UNKNOWN;

            if (String.IsNullOrEmpty(szHashMethod))
            {
                throw new Exception("Hash method must not be null or empty");
            }
            if (szHashMethod.ToUpper() == "MD5")
            {
                hmHashMethod = HASH_METHOD.MD5;
                goto Finished;
            }
            if (szHashMethod.ToUpper() == "SHA1")
            {
                hmHashMethod = HASH_METHOD.SHA1;
                goto Finished;
            }
            if (szHashMethod.ToUpper() == "HMACMD5")
            {
                hmHashMethod = HASH_METHOD.HMACMD5;
                goto Finished;
            }
            if (szHashMethod.ToUpper() == "HMACSHA1")
            {
                hmHashMethod = HASH_METHOD.HMACSHA1;
                goto Finished;
            }
        Finished:;
            if (hmHashMethod == HASH_METHOD.UNKNOWN)
            {
                throw new Exception("Invalid hash method: " + szHashMethod);
            }

            return (hmHashMethod);
        }

        /*
        public static string GetSiteSecureBaseURL(HttpRequest hrHTTPRequest)
        {
            string szReturnString = null;
            int nIndex;

            szReturnString = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(hrHTTPRequest);

            nIndex = szReturnString.LastIndexOf('/');

            szReturnString = szReturnString.Substring(0, nIndex + 1);

            return (szReturnString);
        }
        */


        public static bool getTransactionCompleteResultFromPostVariables(NameValueCollection nvcFormVariables, out TransactionResult trTransactionResult, out string szHashDigest, out string szOutputMessage)
        {
            bool boErrorOccurred;


            trTransactionResult = null;
            szHashDigest = "";
            szOutputMessage = "";
            boErrorOccurred = false;

            try
            {
                trTransactionResult = new TransactionResult();

                // hash digest
                if (nvcFormVariables["HashDigest"] != null)
                {
                    szHashDigest = nvcFormVariables["HashDigest"];
                }

                // transaction status code
                if (nvcFormVariables["StatusCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [StatusCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.StatusCode = Convert.ToInt32(nvcFormVariables["StatusCode"]);
                }
                // transaction message
                if (nvcFormVariables["Message"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Message] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Message = nvcFormVariables["Message"];
                }
                // status code of original transaction if this transaction was deemed a duplicate
                if (nvcFormVariables["PreviousStatusCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PreviousStatusCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["PreviousStatusCode"] == "")
                    {
                        trTransactionResult.PreviousStatusCode = null;
                    }
                    else
                    {
                        trTransactionResult.PreviousStatusCode = Convert.ToInt32(nvcFormVariables["PreviousStatusCode"]);
                    }
                }
                // status code of original transaction if this transaction was deemed a duplicate
                if (nvcFormVariables["PreviousMessage"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PreviousMessage] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.PreviousMessage = nvcFormVariables["PreviousMessage"];
                }
                // cross reference of transaction
                if (nvcFormVariables["CrossReference"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CrossReference] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.CrossReference = nvcFormVariables["CrossReference"];
                }
                // amount (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["Amount"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Amount] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Amount = Convert.ToUInt64(nvcFormVariables["Amount"]);
                }
                // currency code (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["CurrencyCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CurrencyCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.CurrencyCode = Convert.ToInt32(nvcFormVariables["CurrencyCode"]);
                }
                // order ID (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["OrderID"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [OrderID] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.OrderID = nvcFormVariables["OrderID"];
                }
                // transaction type (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["TransactionType"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [TransactionType] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.TransactionType = nvcFormVariables["TransactionType"];
                }
                // transaction date/time (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["TransactionDateTime"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [TransactionDateTime] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.TransactionDateTime = nvcFormVariables["TransactionDateTime"];
                }
                // order description (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["OrderDescription"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [OrderDescription] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.OrderDescription = nvcFormVariables["OrderDescription"];
                }
                // address1 (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["Address1"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Address1] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Address1 = nvcFormVariables["Address1"];
                }
                // address2 (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["Address2"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Address2] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Address2 = nvcFormVariables["Address2"];
                }
                // address3 (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["Address3"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Address3] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Address3 = nvcFormVariables["Address3"];
                }
                // address4 (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["Address4"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Address4] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Address4 = nvcFormVariables["Address4"];
                }
                // city (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["City"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [City] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.City = nvcFormVariables["City"];
                }
                // state (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["State"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [State] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.State = nvcFormVariables["State"];
                }
                // post code (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["PostCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PostCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.PostCode = nvcFormVariables["PostCode"];
                }
                // country code (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["CountryCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CountryCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CountryCode"] == "")
                    {
                        trTransactionResult.CountryCode = null;
                    }
                    else
                    {
                        trTransactionResult.CountryCode = Convert.ToInt32(nvcFormVariables["CountryCode"]);
                    }
                }



                // Address check result - response from bank
                if (nvcFormVariables["AddressNumericCheckResult"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [AddressNumericCheckResult] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["AddressNumericCheckResult"] == "")
                    {
                        trTransactionResult.AddressNumericCheckResult = null;
                    }
                    else
                    {
                        trTransactionResult.AddressNumericCheckResult = nvcFormVariables["AddressNumericCheckResult"];
                    }
                }
                // PostCode check result - response from bank
                if (nvcFormVariables["PostCodeCheckResult"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PostCodeCheckResult] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["PostCodeCheckResult"] == "")
                    {
                        trTransactionResult.PostCodeCheckResult = null;
                    }
                    else
                    {
                        trTransactionResult.PostCodeCheckResult = nvcFormVariables["PostCodeCheckResult"];
                    }
                }
                // CV2 check result - response from bank
                if (nvcFormVariables["CV2CheckResult"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CV2CheckResult] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CV2CheckResult"] == "")
                    {
                        trTransactionResult.CV2CheckResult = null;
                    }
                    else
                    {
                        trTransactionResult.CV2CheckResult = nvcFormVariables["CV2CheckResult"];
                    }
                }
                // 3D Secure Authentication check result - response from bank
                if (nvcFormVariables["ThreeDSecureAuthenticationCheckResult"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [ThreeDSecureAuthenticationCheckResult] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["ThreeDSecureAuthenticationCheckResult"] == "")
                    {
                        trTransactionResult.ThreeDSecureAuthenticationCheckResult = null;
                    }
                    else
                    {
                        trTransactionResult.ThreeDSecureAuthenticationCheckResult = nvcFormVariables["ThreeDSecureAuthenticationCheckResult"];
                    }
                }
                // Card Type - response from bank
                if (nvcFormVariables["CardType"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CardType] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CardType"] == "")
                    {
                        trTransactionResult.CardType = null;
                    }
                    else
                    {
                        trTransactionResult.CardType = nvcFormVariables["CardType"];
                    }
                }
                // Card Class - response from bank
                if (nvcFormVariables["CardClass"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CardClass] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CardClass"] == "")
                    {
                        trTransactionResult.CardClass = null;
                    }
                    else
                    {
                        trTransactionResult.CardClass = nvcFormVariables["CardClass"];
                    }
                }
                // Card Issuer - response from bank
                if (nvcFormVariables["CardIssuer"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CardIssuer] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CardIssuer"] == "")
                    {
                        trTransactionResult.CardIssuer = null;
                    }
                    else
                    {
                        trTransactionResult.CardIssuer = nvcFormVariables["CardIssuer"];
                    }
                }
                // Card Issuer Country Code - response from bank
                if (nvcFormVariables["CardIssuerCountryCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CardIssuerCountryCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["CardIssuerCountryCode"] == "")
                    {
                        trTransactionResult.CardIssuerCountryCode = null;
                    }
                    else
                    {
                        trTransactionResult.CardIssuerCountryCode = Convert.ToInt32(nvcFormVariables["CardIssuerCountryCode"]);
                    }
                }
                // Email Address (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["EmailAddress"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [EmailAddress] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["EmailAddress"] == "")
                    {
                        trTransactionResult.EmailAddress = null;
                    }
                    else
                    {
                        trTransactionResult.EmailAddress = nvcFormVariables["EmailAddress"];
                    }
                }
                // Phone Number (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["PhoneNumber"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PhoneNumber] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    if (nvcFormVariables["PhoneNumber"] == "")
                    {
                        trTransactionResult.PhoneNumber = null;
                    }
                    else
                    {
                        trTransactionResult.PhoneNumber = nvcFormVariables["PhoneNumber"];
                    }
                }




                if (boErrorOccurred)
                {
                    trTransactionResult = null;
                }
            }
            catch (Exception e)
            {
                boErrorOccurred = true;
                szOutputMessage = e.Message;
            }

            return (!boErrorOccurred);
        }
        public static bool Get3DSecureAuthenticationRequiredFromPostVariables(NameValueCollection nvcFormVariables, out TransactionResult trTransactionResult, out string szHashDigest, out string szOutputMessage)
        {
            bool boErrorOccurred;

            trTransactionResult = null;
            szHashDigest = null;
            szOutputMessage = "";
            boErrorOccurred = false;

            try
            {
                trTransactionResult = new TransactionResult();

                // hash digest
                if (nvcFormVariables["HashDigest"] != null)
                {
                    szHashDigest = nvcFormVariables["HashDigest"];
                }

                // transaction status code
                if (nvcFormVariables["StatusCode"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [StatusCode] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.StatusCode = Convert.ToInt32(nvcFormVariables["StatusCode"]);
                }
                // transaction message
                if (nvcFormVariables["Message"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [Message] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.Message = nvcFormVariables["Message"];
                }
                // cross reference of transaction
                if (nvcFormVariables["CrossReference"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [CrossReference] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.CrossReference = nvcFormVariables["CrossReference"];
                }
                // order ID (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["OrderID"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [OrderID] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.OrderID = nvcFormVariables["OrderID"];
                }
                // transaction date/time (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["TransactionDateTime"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [TransactionDateTime] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.TransactionDateTime = nvcFormVariables["TransactionDateTime"];
                }
                // order description (same as value passed into payment form - echoed back out by payment form)
                if (nvcFormVariables["ACSURL"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [ACSURL] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.ACSUrl = nvcFormVariables["ACSURL"];
                }
                // address1 (not necessarily the same as value passed into payment form - as the customer can change it on the form)
                if (nvcFormVariables["PaREQ"] == null)
                {
                    szOutputMessage = AddStringToStringList(szOutputMessage, "Expected variable [PaREQ] not received");
                    boErrorOccurred = true;
                }
                else
                {
                    trTransactionResult.PaREQ = nvcFormVariables["PaREQ"];
                }

                if (boErrorOccurred)
                {
                    trTransactionResult = null;
                }
            }
            catch (Exception e)
            {
                boErrorOccurred = true;
                szOutputMessage = e.Message;
            }

            return (!boErrorOccurred);
        }
        public static string AddStringToStringList(string szExistingStringList, string szStringToAdd)
        {
            string szCommaString;
            StringBuilder sbReturnString;

            sbReturnString = new StringBuilder();
            szCommaString = "";

            if (String.IsNullOrEmpty(szStringToAdd))
            {
                sbReturnString.Append(szExistingStringList);
            }
            else
            {
                if (!String.IsNullOrEmpty(szExistingStringList))
                {
                    szCommaString = ", ";
                }
                sbReturnString.AppendFormat("{0}{1}{2}", szExistingStringList, szCommaString, szStringToAdd);
            }

            return (sbReturnString.ToString());
        }
        public static byte[] StringToByteArray(string szString)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(szString);
        }
        public static string ByteArrayToHexString(byte[] aByte)
        {
            StringBuilder sbStringBuilder;
            int nCount = 0;

            sbStringBuilder = new StringBuilder();
            for (nCount = 0; nCount < aByte.Length; nCount++)
            {
                sbStringBuilder.Append(aByte[nCount].ToString("x2"));
            }

            return (sbStringBuilder.ToString());
        }
        public static string ForwardPaddedNumberString(int nNumber, int nPaddingAmount, char cPaddingChar)
        {
            string szReturnString;
            StringBuilder sbString;
            int nCount = 0;

            szReturnString = nNumber.ToString();

            if (szReturnString.Length < nPaddingAmount &&
                nPaddingAmount > 0)
            {
                sbString = new StringBuilder();

                for (nCount = 0; nCount < nPaddingAmount - szReturnString.Length; nCount++)
                {
                    sbString.Append(cPaddingChar);
                }
                sbString.Append(szReturnString);
                szReturnString = sbString.ToString();
            }
            return (szReturnString);
        }
        public static string StripAllWhitespace(string szString)
        {
            StringBuilder sbReturnString;
            int nCount = 0;

            if (szString == null)
            {
                return (null);
            }

            sbReturnString = new StringBuilder();

            for (nCount = 0; nCount < szString.Length; nCount++)
            {
                if (szString[nCount] != ' ' &&
                    szString[nCount] != '\t' &&
                    szString[nCount] != '\n' &&
                    szString[nCount] != '\r')
                {
                    sbReturnString.Append(szString[nCount]);
                }
            }
            return (sbReturnString.ToString());
        }
        public static string CalculateHashDigest(string szString, string szPreSharedKey, HASH_METHOD eHashMethod)
        {
            byte[] abKey;
            byte[] abBytes;
            byte[] abHashDigest;
            MD5 md5;
            SHA1 sha1;
            HMACMD5 hmacMD5;
            HMACSHA1 hmacSHA1;

            abKey = StringToByteArray(szPreSharedKey);
            abBytes = StringToByteArray(szString);

            switch (eHashMethod)
            {
                case HASH_METHOD.HMACMD5:
                    hmacMD5 = new HMACMD5(abKey);
                    abHashDigest = hmacMD5.ComputeHash(abBytes);
                    break;
                case HASH_METHOD.HMACSHA1:
                    hmacSHA1 = new HMACSHA1(abKey);
                    abHashDigest = hmacSHA1.ComputeHash(abBytes);
                    break;
                case HASH_METHOD.MD5:
                    md5 = new MD5CryptoServiceProvider();
                    abHashDigest = md5.ComputeHash(abBytes);
                    break;
                case HASH_METHOD.SHA1:
                    sha1 = new SHA1CryptoServiceProvider();
                    abHashDigest = sha1.ComputeHash(abBytes);
                    break;
                default:
                    throw new Exception("Invalid hash method: " + eHashMethod.ToString());
            }

            return (ByteArrayToHexString(abHashDigest));
        }

        public static string GenerateStringToHashInitial(string szMerchantID,
                                                            string szPassword,
                                                            string szAmount,
                                                            string szCurrencyCode,
                                                            string szOrderID,
                                                            string szTransactionType,
                                                            string szTransactionDateTime,
                                                            string szCallbackURL,
                                                            string szOrderDescription,
                                                            string szPreSharedKey,
                                                            HASH_METHOD hmHashMethod)
        {
            StringBuilder sbReturnString;
            bool boIncludePreSharedKeyInString = false;

            sbReturnString = new StringBuilder();

            switch (hmHashMethod)
            {
                case HASH_METHOD.MD5:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.SHA1:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.HMACMD5:
                    boIncludePreSharedKeyInString = false;
                    break;
                case HASH_METHOD.HMACSHA1:
                    boIncludePreSharedKeyInString = false;
                    break;
            }

            if (boIncludePreSharedKeyInString)
            {
                sbReturnString.AppendFormat("PreSharedKey={0}&", szPreSharedKey);
            }

            sbReturnString.AppendFormat("MerchantID={0}&Password={1}&Amount={2}&CurrencyCode={3}&OrderID={4}&TransactionType={5}" +
                                        "&TransactionDateTime={6}&CallbackURL={7}&OrderDescription={8}",
                                            szMerchantID, szPassword, szAmount, szCurrencyCode, szOrderID,
                                            szTransactionType, szTransactionDateTime, szCallbackURL, szOrderDescription);

            return (sbReturnString.ToString());
        }
        public static string GenerateStringToHash3DSecureAuthenticationRequired(string szMerchantID,
                                                                                string szPassword,
                                                                                TransactionResult trTransactionResult,
                                                                                string szPreSharedKey,
                                                                                HASH_METHOD hmHashMethod)
        {
            StringBuilder sbReturnString;
            bool boIncludePreSharedKeyInString = false;

            sbReturnString = new StringBuilder();

            switch (hmHashMethod)
            {
                case HASH_METHOD.MD5:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.SHA1:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.HMACMD5:
                    boIncludePreSharedKeyInString = false;
                    break;
                case HASH_METHOD.HMACSHA1:
                    boIncludePreSharedKeyInString = false;
                    break;
            }

            if (boIncludePreSharedKeyInString)
            {
                sbReturnString.AppendFormat("PreSharedKey={0}&", szPreSharedKey);
            }

            sbReturnString.AppendFormat("MerchantID={0}&Password={1}&StatusCode={2}&Message={3}&CrossReference={4}&OrderID={5}" +
                                        "&TransactionDateTime={6}&ACSURL={7}&PaREQ={8}",
                                            szMerchantID, szPassword, trTransactionResult.StatusCode, trTransactionResult.Message, trTransactionResult.CrossReference,
                                            trTransactionResult.OrderID, trTransactionResult.TransactionDateTime, trTransactionResult.ACSUrl, trTransactionResult.PaREQ);

            return (sbReturnString.ToString());
        }
        public static string GenerateStringToHash3DSecurePostAuthentication(string szMerchantID, string szPassword, string szCrossReference, string szTransactionDateTime, string szCallbackURL, string szPaRES, string szPreSharedKey, HASH_METHOD hmHashMethod)
        {
            StringBuilder sbReturnString;
            bool boIncludePreSharedKeyInString = false;

            sbReturnString = new StringBuilder();

            switch (hmHashMethod)
            {
                case HASH_METHOD.MD5:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.SHA1:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.HMACMD5:
                    boIncludePreSharedKeyInString = false;
                    break;
                case HASH_METHOD.HMACSHA1:
                    boIncludePreSharedKeyInString = false;
                    break;
            }

            if (boIncludePreSharedKeyInString)
            {
                sbReturnString.AppendFormat("PreSharedKey={0}&", szPreSharedKey);
            }

            sbReturnString.AppendFormat("MerchantID={0}&Password={1}&CrossReference={2}&TransactionDateTime={3}&CallbackURL={4}&PaRES={5}",
                                            szMerchantID, szPassword, szCrossReference, szTransactionDateTime, szCallbackURL, szPaRES);

            return (sbReturnString.ToString());
        }
        public static string GenerateStringToHashPaymentComplete(string szMerchantID, string szPassword, TransactionResult trTransactionResult, string szPreSharedKey, HASH_METHOD hmHashMethod)
        {
            StringBuilder sbReturnString;
            bool boIncludePreSharedKeyInString = false;

            sbReturnString = new StringBuilder();

            switch (hmHashMethod)
            {
                case HASH_METHOD.MD5:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.SHA1:
                    boIncludePreSharedKeyInString = true;
                    break;
                case HASH_METHOD.HMACMD5:
                    boIncludePreSharedKeyInString = false;
                    break;
                case HASH_METHOD.HMACSHA1:
                    boIncludePreSharedKeyInString = false;
                    break;
            }

            if (boIncludePreSharedKeyInString)
            {
                sbReturnString.AppendFormat("PreSharedKey={0}&", szPreSharedKey);
            }

            sbReturnString.AppendFormat("MerchantID={0}&Password={1}&StatusCode={2}&Message={3}&PreviousStatusCode={4}&PreviousMessage={5}&CrossReference={6}" +
                                        "&AddressNumericCheckResult={7}&PostCodeCheckResult={8}&CV2CheckResult={9}&ThreeDSecureAuthenticationCheckResult={10}" +
                                        "&CardType={11}&CardClass={12}&CardIssuer={13}&CardIssuerCountryCode={14}&Amount={15}&CurrencyCode={16}&OrderID={17}" +
                                        "&TransactionType={18}&TransactionDateTime={19}&OrderDescription={20}&Address1={21}&Address2={22}&Address3={23}" +
                                        "&Address4={24}&City={25}&State={26}&PostCode={27}&CountryCode={28}&EmailAddress={29}&PhoneNumber={30}",
                                            szMerchantID, szPassword, trTransactionResult.StatusCode, trTransactionResult.Message, trTransactionResult.PreviousStatusCode,
                                            trTransactionResult.PreviousMessage, trTransactionResult.CrossReference, trTransactionResult.AddressNumericCheckResult,
                                            trTransactionResult.PostCodeCheckResult, trTransactionResult.CV2CheckResult, trTransactionResult.ThreeDSecureAuthenticationCheckResult,
                                            trTransactionResult.CardType, trTransactionResult.CardClass, trTransactionResult.CardIssuer, trTransactionResult.CardIssuerCountryCode,
                                            trTransactionResult.Amount, trTransactionResult.CurrencyCode, trTransactionResult.OrderID, trTransactionResult.TransactionType,
                                            trTransactionResult.TransactionDateTime, trTransactionResult.OrderDescription, trTransactionResult.Address1, trTransactionResult.Address2,
                                            trTransactionResult.Address3, trTransactionResult.Address4, trTransactionResult.City, trTransactionResult.State,
                                            trTransactionResult.PostCode, trTransactionResult.CountryCode, trTransactionResult.EmailAddress, trTransactionResult.PhoneNumber);

            return (sbReturnString.ToString());
        }

        public static string GenerateStringToHash(string szAmount,
                                                    string szCurrencyCode,
                                                    string szOrderID,
                                                    string szTransactionType,
                                                    string szTransactionDateTime,
                                                    string szCallbackURL,
                                                    string szOrderDescription,
                                                    string szCustomerName,
                                                    string szAddress1,
                                                    string szAddress2,
                                                    string szAddress3,
                                                    string szAddress4,
                                                    string szCity,
                                                    string szState,
                                                    string szPostCode,
                                                    string szCountryCode,
                                                    string szSecretKey)
        {
            StringBuilder sbReturnString;
            sbReturnString = new StringBuilder();

            sbReturnString.AppendFormat("Amount={0}&CurrencyCode={1}&OrderID={2}&TransactionType={3}" +
                                        "&TransactionDateTime={4}&CallbackURL={5}&OrderDescription={6}&CustomerName={7}" +
                                        "&Address1={8}&Address2={9}&Address3={10}&Address4={11}&City={12}&State={13}&PostCode={14}" +
                                        "&CountryCode={15}&SecretKey={16}",
                                            szAmount, szCurrencyCode, szOrderID,
                                            szTransactionType, szTransactionDateTime, szCallbackURL, szOrderDescription,
                                            szCustomerName, szAddress1, szAddress2, szAddress3, szAddress4,
                                            szCity, szState, szPostCode, szCountryCode, szSecretKey);

            return (sbReturnString.ToString());
        }
        // This is a "hook" function that is run when the results of the transaction are
        // known.
        // You should put your code that does any post transaction tasks
        // (e.g. updates the order object, sends the customer an email etc) in this function
        public static bool ReportTransactionResult(TransactionResult trTransactionResult,
                                                    out string szOutputMessage)
        {
            bool boErrorOccurred = false;
            szOutputMessage = "";

            try
            {
                switch (trTransactionResult.StatusCode)
                {
                    // transaction authorised
                    case 0:
                        break;
                    // card referred (treat as decline)
                    case 4:
                        break;
                    // transaction declined
                    case 5:
                        break;
                    // duplicate transaction
                    case 20:
                        // need to look at the previous status code to see if the
                        // transaction was successful
                        if (trTransactionResult.PreviousStatusCode.Value == 0)
                        {
                            // transaction authorised
                        }
                        else
                        {
                            // transaction not authorised
                        }
                        break;
                    // error occurred
                    case 30:
                        break;
                    default:
                        break;
                }

                // put code to update/store the order with the transaction result

            }
            catch (Exception e)
            {
                boErrorOccurred = true;
                szOutputMessage = e.Message;
            }
            return (!boErrorOccurred);
        }
    }
}