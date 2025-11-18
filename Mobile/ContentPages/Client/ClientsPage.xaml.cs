namespace Mobile.ContentPages;
using Mobile.ViewModels.Client;
using Microsoft.Extensions.DependencyInjection;

public partial class ClientsPage : ContentPage
{
    private readonly ClientListViewModel _viewModel;
    private readonly IServiceProvider _services;

    public ClientsPage(ClientListViewModel viewModel, IServiceProvider services)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _services = services;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.Clients == null || _viewModel.Clients.Count == 0)
        {
            await _viewModel.LoadClientsAsync();
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        var createPage = _services.GetRequiredService<ClientCreatePage>();
        await Navigation.PushAsync(createPage);
    }
}