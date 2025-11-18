using Mobile.ContentPages;
using Mobile.Services;

namespace Mobile
{
  public partial class MainPage : ContentPage
  {
    int count = 0;
    private readonly FilterSetApiService _filterSetApiService;
    private readonly IServiceProvider _services;

    public MainPage(FilterSetApiService filterSetApiService, IServiceProvider services)
    {
      InitializeComponent();
      _filterSetApiService = filterSetApiService;
      _services = services;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
      count++;
      CounterBtn.Text = count == 1 ? $"Clicked {count} time" : $"Clicked {count} times";
      SemanticScreenReader.Announce(CounterBtn.Text);      
    }

    private async void OnGoToClientsPageClicked(object sender, EventArgs e)
    {
      var page = _services.GetService<ClientsPage>();
      if (page != null)
        await Navigation.PushAsync(page);
    }

    private async void OnGoToFilterSetListPageClicked(object sender, EventArgs e)
    {
      var page = _services.GetService<FilterSetListPage>();
      if (page != null)
        await Navigation.PushAsync(page);
    }
  }
}
