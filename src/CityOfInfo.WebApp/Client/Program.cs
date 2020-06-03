using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using TG.Blazor.IndexedDB;
using System.Collections.Generic;
using CityOfInfo.WebApp.Client.Model;

namespace CityOfInfo.WebApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = builder.HostEnvironment.BaseAddress;

            builder.Services
                .AddTransient<IDomainContext>(sp => 
                {
                    // TODO: Fix DTD parsing exception when running this locally
                    // var address = baseAddress;
                    var address = "https://cityof.info/";
                    var suffix = address.EndsWith('/') ? string.Empty : "/";
                    suffix += "odata";
                    return new DomainContext(address + suffix);
                })                
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons()                
                .AddIndexedDB(dbStore =>
                {
                    dbStore.DbName = "Database"; //example name
                    dbStore.Version = 1;

                    dbStore.Stores.Add(new StoreSchema
                    {
                        Name = "Powers",
                        PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = false },
                        Indexes = new List<IndexSpec>
                        {
                            new IndexSpec{Name="name", KeyPath = "name", Auto=false}
                        }
                    });
                })
                .AddHttpClient(WebApp.Shared.Globals.DatabaseBlobClient, client =>
                    client.BaseAddress = WebApp.Shared.Globals.DatabaseBlobBaseAddress);

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();

            Simple.OData.Client.V4Adapter.Reference();
        }
    }
}
