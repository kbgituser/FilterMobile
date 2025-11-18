using Mobile.Services;
using Mobile.ViewModels.FilterSet;

namespace Mobile.ContentPages
{
  public partial class FilterSetListPage : ContentPage
  {
    private FilterSetListViewModel _viewModel;

    public FilterSetListPage(FilterSetApiService apiService)
    {
      InitializeComponent();
      _viewModel = new FilterSetListViewModel(apiService);
      BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
      base.OnAppearing();
      await _viewModel.LoadFilterSetsAsync();
      FilterSetCollectionView.ItemsSource = _viewModel.FilterSets;
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
      var id = (int)((Button)sender).CommandParameter;
      // Navigate to details page
      // await Navigation.PushAsync(new FilterSetDetailsPage(id));
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
      var id = (int)((Button)sender).CommandParameter;
      // Navigate to edit page
      // await Navigation.PushAsync(new FilterSetEditPage(id));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
      var id = (int)((Button)sender).CommandParameter;
      // Navigate to delete page
      // await Navigation.PushAsync(new FilterSetDeletePage(id));
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
      // Navigate to create page
      // await Navigation.PushAsync(new FilterSetCreatePage());
    }

    private async void OnBackToMainPageClicked(object sender, EventArgs e)
    {
      await Navigation.PopToRootAsync();
    }
  }
}