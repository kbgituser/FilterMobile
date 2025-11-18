using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Services;
using SharedModels.ClientDtos;

namespace Mobile.ViewModels.Client;

public partial class ClientDetailsViewModel : ObservableObject
{
    private readonly ClientApiService _clientApiService;

    [ObservableProperty]
    private ClientDto clientDto;

    public ClientDetailsViewModel(ClientApiService clientApiService)
    {
        _clientApiService = clientApiService;
    }

    // Prepare a fresh DTO for the create page
    public void InitializeNew()
    {
        ClientDto = new ClientDto
        {
            Name = string.Empty,
            System = string.Empty,
            Address = string.Empty,
            Phone = string.Empty,
            Notes = string.Empty,
            ApplicationUserId = string.Empty // set by user or app
        };
    }

    [RelayCommand]
    public async Task LoadClientDetailsAsync(int id)
    {
        ClientDto = await _clientApiService.GetClientByIdAsync(id);
    }

    [RelayCommand]
    public async Task CreateClientAsync()
    {
        if (ClientDto != null)
        {
            var ok = await _clientApiService.CreateClientAsync(ClientDto);
            if (!ok)
            {
                // Optionally add error handling/logging here
            }
        }
    }

    [RelayCommand]
    public async Task EditClientAsync()
    {
        if (ClientDto != null)
        {
            await _clientApiService.UpdateClientAsync(ClientDto.Id, ClientDto);
        }
    }

    [RelayCommand]
    public async Task DeleteClientAsync()
    {
        if (ClientDto != null)
        {
            await _clientApiService.DeleteClientAsync(ClientDto.Id);
        }
    }
}
