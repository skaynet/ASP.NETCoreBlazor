using ASP.NETCoreBlazor.Data;
using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FinanceContext>(opt => opt.UseInMemoryDatabase("FinanceDatabaseInMemory"));
builder.Services.AddScoped<IRepository<OperationType>, OperationTypeRepository>();
builder.Services.AddScoped<IRepository<FinancialTransaction>, FinancialTransactionRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

//Заполняем базу тестовыми данными
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<FinanceContext>();
    DataSeeder.SeedData(context);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
