using Microsoft.Extensions.Logging;
using Mobile.ContentPages;
using Mobile.Services;
using Mobile.ViewModels.Client;
using Mobile.ViewModels.FilterSet;

namespace Mobile
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      var endpoint = "https://localhost:7148/api/";
      builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })

        .Services.AddSingleton(sp=> endpoint)
        // WebApi services
        .AddTransient<AuthService>(sp => new AuthService(endpoint))
        .AddTransient<ClientApiService>(sp => new ClientApiService(endpoint, sp.GetRequiredService<AuthService>()))
        .AddTransient<FilterSetApiService>(sp => new FilterSetApiService(endpoint, "")) // Token should be set at runtime
        // ViewModels
        .AddTransient<ClientListViewModel>()
        .AddTransient<ClientDetailsViewModel>()
        .AddTransient<FilterSetListViewModel>()
        .AddTransient<FilterSetDetailsViewModel>()
        // ContentPages for DI
        .AddTransient<MainPage>()
        .AddTransient<LoginPage>()
        .AddTransient<ClientsPage>()
        .AddTransient<ClientCreatePage>()
        .AddTransient<FilterSetListPage>()
        .AddTransient<FilterSetDetailsPage>()
        ;

#if DEBUG
  		builder.Logging.AddDebug();
#endif

      return builder.Build();
    }
  }
}
