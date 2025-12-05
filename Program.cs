using ShoppingSiteDotNetCore.DAL;
using ShoppingSiteDotNetCore.NTier;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//DbConnection - Dependency Injection
//Only 1 instance for Whole Project/app
//Logging, Configuration Setting
builder.Services.AddSingleton<DbConnector>();

//NTier Services - Dependency Injection
builder.Services.AddScoped<ICategoryTblServices, CategoryTblServices>();
builder.Services.AddScoped<ISubCategoryTblServices, SubCategoryTblServices>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
