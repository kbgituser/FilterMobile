using Mobile.Services;
using SharedModels.FilterSetDtos;
using System.Collections.ObjectModel;

namespace Mobile.ViewModels.FilterSet;

public class FilterSetListViewModel
{
  public ObservableCollection<FilterSetDto> FilterSets { get; set; } = new();
  private readonly FilterSetApiService _apiService;

  public FilterSetListViewModel(FilterSetApiService apiService)
  {
    _apiService = apiService;
  }

  public async Task LoadFilterSetsAsync()
  {
    var filterSets = await _apiService.GetFilterSetsAsync();
    FilterSets.Clear();
    FilterSets = new ObservableCollection<FilterSetDto>(filterSets);
  }
}