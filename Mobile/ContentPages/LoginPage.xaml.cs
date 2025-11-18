using Mobile.Services;

namespace Mobile.ContentPages
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public LoginPage(AuthService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;
            var email = UsernameEntry.Text; // treat as email
            var password = PasswordEntry.Text;

            try
            {
                var success = await _authService.LoginAsync(email, password);
                if (success)
                {
                    var mainPage = _serviceProvider.GetService<MainPage>();
                    await Navigation.PushAsync(mainPage);
                }
                else
                {
                    ErrorLabel.Text = "Invalid credentials.";
                    ErrorLabel.IsVisible = true;
                }
            }
            catch
            {
                ErrorLabel.Text = "Login failed. Please try again.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
