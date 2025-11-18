using Mobile.Services;
using Mobile.ViewModels.FilterSet;

namespace Mobile.ContentPages
{
    public partial class FilterSetDetailsPage : ContentPage
    {
        private readonly FilterSetDetailsViewModel _viewModel;
        private readonly int _filterSetId;

        public FilterSetDetailsPage(int filterSetId, FilterSetApiService _apiService)
        {
            InitializeComponent();
            _filterSetId = filterSetId;
            _viewModel = new(_apiService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadDetailsAsync(_filterSetId);
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
