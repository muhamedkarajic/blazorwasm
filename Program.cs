using System.Reactive.Linq;
using System.Reactive.Subjects;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorWebAssemblyApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<SharedStateService>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
public class SharedStateService
{
    private readonly ILocalStorageService localStorage;
    public SharedStateService(ILocalStorageService LocalStorage)
    {
        localStorage = LocalStorage;
    }

    public readonly BehaviorSubject<string> value = new("Test");


    public async Task OnInitializedAsync()
    {
        var someSharedDataMaybe = await localStorage.GetItemAsStringAsync("message");
        if (someSharedDataMaybe is not null)
        {
            value.OnNext(someSharedDataMaybe);
        }
    }
}