using CESI_MoviesLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CESI_MoviesLibrary.Views
{
    /// <summary>
    /// Logique d'interaction pour RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
            {
                viewModel.Password = RegisterPasswordBox.Password;
            }
        }
    }
}
