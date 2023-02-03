using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HRLeaveManagement.BlazorUI;
using HRLeaveManagement.BlazorUI.Contracts.Abstract;
using HRLeaveManagement.BlazorUI.Contracts.Concrete;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5262") });

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeManager>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();