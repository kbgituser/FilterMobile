namespace Mobile.ContentPages;
using Mobile.ViewModels.Client;

public partial class ClientCreatePage : ContentPage
{
    private readonly ClientDetailsViewModel _vm;
    public ClientCreatePage(ClientDetailsViewModel vm)
    {
        InitializeComponent(); // load XAML so controls appear
        _vm = vm;
        _vm.InitializeNew();
        BindingContext = _vm;
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
