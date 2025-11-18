using Mobile.Services;
using SharedModels.FilterSetDtos;

namespace Mobile.ViewModels.FilterSet
{
    public class FilterSetDetailsViewModel
    {
        public FilterSetDto FilterSet { get; private set; }
        private readonly FilterSetApiService _apiService;

        public FilterSetDetailsViewModel(FilterSetApiService apiService)
        {
            _apiService = apiService;
        }

    public async Task LoadDetailsAsync(int id)
        {
            FilterSet = await _apiService.GetFilterSetByIdAsync(id);
        }
    }
}
