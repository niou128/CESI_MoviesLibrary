using CommunityToolkit.Mvvm.ComponentModel;

namespace CESI_MoviesLibrary.Core.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private string? successMessage;

        [ObservableProperty]
        private bool isEmpty;

        protected void SetBusy(string? message = null)
        {
            IsBusy = true;
            ErrorMessage = null;
            SuccessMessage = null;
            if (message != null)
                ErrorMessage = message;
        }

        protected void ClearStatus()
        {
            IsBusy = false;
            ErrorMessage = null;
            SuccessMessage = null;
        }
    }
}
