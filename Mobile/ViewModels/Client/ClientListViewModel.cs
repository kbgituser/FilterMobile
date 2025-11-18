using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedModels.ClientDtos;
using System.Collections.ObjectModel;
using Mobile.Services;

namespace Mobile.ViewModels.Client;

public partial class ClientListViewModel : ObservableObject
{
  private readonly ClientApiService _clientApiService;

  [ObservableProperty]
  private ObservableCollection<ClientDto> clients;

  [ObservableProperty]
  private bool isLoading;

  public ClientListViewModel(ClientApiService clientApiService)
  {
    _clientApiService = clientApiService;
  }

  [RelayCommand]
  public async Task LoadClientsAsync()
  {
    try
    {
      IsLoading = true;
      var data = await _clientApiService.GetClientsAsync();
      if (data != null)
        Clients = new ObservableCollection<ClientDto>(data);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during data loading: {ex.Message}");
    }
    finally
    {
      IsLoading = false;
    }
  }
}
