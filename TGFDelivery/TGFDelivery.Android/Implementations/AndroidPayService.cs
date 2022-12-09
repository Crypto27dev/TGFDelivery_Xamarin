using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Braintreepayments.Api;
using Com.Braintreepayments.Api.Exceptions;
using Com.Braintreepayments.Api.Interfaces;
using Com.Braintreepayments.Api.Models;
using Plugin.CurrentActivity;
using TGFDelivery.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(TGFDelivery.Droid.Implementations.AndroidPayService))]
namespace TGFDelivery.Droid.Implementations
{
    public class AndroidPayService : Java.Lang.Object, IPayService, IBraintreeResponseListener, IBraintreeErrorListener, IPaymentMethodNonceCreatedListener, IBraintreeCancelListener
    {
        TaskCompletionSource<bool> initializeTcs;
        TaskCompletionSource<string> payTcs;

        BraintreeFragment mBraintreeFragment;
        bool isReady = false;
        public bool CanPay { get { return isReady; } }

        public event EventHandler<string> OnTokenizationSuccessful;
        public event EventHandler<string> OnTokenizationError;

        public void OnCancel(int requestCode)
        {
            // Use this to handle a canceled activity, if the given requestCode is important.
            // You may want to use this callback to hide loading indicators, and prepare your UI for input
            payTcs.SetCanceled();
            mBraintreeFragment.RemoveListener(this);
        }

        public void OnResponse(Java.Lang.Object parameter)
        {
            if (parameter is Java.Lang.Boolean)
            {
                var res = parameter.JavaCast<Java.Lang.Boolean>();
                isReady = res.BooleanValue();
                initializeTcs?.TrySetResult(res.BooleanValue());

            }
        }

        public async Task<string> TokenizeCard(string panNumber = "4111111111111111", string expirationMonth = "12", string expirationYear = "2018", string cvv = null)
        {
            if (isReady)
            {
                payTcs = new TaskCompletionSource<string>();
                CardBuilder cardBuilder = new CardBuilder()
                    .CardNumber(panNumber).JavaCast<CardBuilder>()
                    .ExpirationMonth(expirationMonth).JavaCast<CardBuilder>()
                    .ExpirationYear(expirationYear).JavaCast<CardBuilder>()
                    .Cvv(cvv).JavaCast<CardBuilder>();

                mBraintreeFragment.AddListener(this);

                Card.Tokenize(mBraintreeFragment, cardBuilder);
            }
            else
            {
                OnTokenizationError?.Invoke(this, "Platform is not ready to accept payments");
                payTcs.TrySetException(new System.Exception("Platform is not ready to accept payments"));

            }
            return await payTcs.Task;
        }


        public void OnPaymentMethodNonceCreated(PaymentMethodNonce paymentMethodNonce)
        {
            // Send this nonce to your server
            string nonce = paymentMethodNonce.Nonce;
            mBraintreeFragment.RemoveListener(this);
            OnTokenizationSuccessful?.Invoke(this, nonce);
            payTcs?.TrySetResult(nonce);

        }

        public void OnError(Java.Lang.Exception error)
        {
            if (error is ErrorWithResponse)
            {
                ErrorWithResponse errorWithResponse = (ErrorWithResponse)error;
                BraintreeError cardErrors = errorWithResponse.ErrorFor("creditCard");
                if (cardErrors != null)
                {
                    // There is an issue with the credit card.
                    BraintreeError expirationMonthError = cardErrors.ErrorFor("expirationMonth");
                    if (expirationMonthError != null)
                    {
                        // There is an issue with the expiration month.
                        //SetErrorMessage(expirationMonthError.GetMessage());
                        OnTokenizationError?.Invoke(this, expirationMonthError.Message);
                        payTcs?.TrySetException(new System.Exception(expirationMonthError.Message));

                    }
                    else
                    {
                        OnTokenizationError?.Invoke(this, cardErrors.Message);
                        payTcs?.TrySetException(new System.Exception(cardErrors.Message));

                    }
                }
            }

            mBraintreeFragment.RemoveListener(this);
        }


        public async Task<bool> InitializeAsync(string clientToken)
        {
            try
            {
                initializeTcs = new TaskCompletionSource<bool>();
                mBraintreeFragment = BraintreeFragment.NewInstance(CrossCurrentActivity.Current.Activity, clientToken);

                GooglePayment.IsReadyToPay(mBraintreeFragment, this);
            }
            catch (InvalidArgumentException e)
            {
                initializeTcs.TrySetException(e);
            }
            return await initializeTcs.Task;
        }
    }
}