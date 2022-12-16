using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using TGFDelivery.Services.AlertDialogService;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AlertDialogService))]
namespace TGFDelivery.Services.AlertDialogService
{
    public class AlertDialogService : IAlertDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialogAsync(string title, string message, string close)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            AlertDialogPopup alertDialog = new AlertDialogPopup(title, message, null, close, Callback);
            await Application.Current.MainPage.Navigation.PushPopupAsync(alertDialog);
            await task;
        }

        public async Task<bool> ShowDialogConfirmationAsync(string title, string message, string cancel, string ok)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            AlertDialogPopup alertDialog = new AlertDialogPopup(title, message, cancel, ok, Callback);
            await Application.Current.MainPage.Navigation.PushPopupAsync(alertDialog);

            return await task;
        }

        private async Task Callback(bool result)
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            if (!taskCompletionSource.Task.IsCanceled &&
                !taskCompletionSource.Task.IsCompleted &&
                !taskCompletionSource.Task.IsFaulted)
            {
                taskCompletionSource.SetResult(result);
            }
        }
    }
}
