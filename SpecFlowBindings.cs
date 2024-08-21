using System.Reflection;
namespace OdinReactMemoryGameTests;

[Binding]
public class SpecFlowBindingHooks
{
    private readonly IObjectContainer _objectContainer;
    private string _tracePath = $"test.tracing{DateTime.Now.ToString("hhMMss")}";
    private int _traceCounter = 1;

    public SpecFlowBindingHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public async Task SetupWebDriver(ScenarioContext scenarioContext)
    {
        var builder = new ConfigurationBuilder();
        var env = Environment.GetEnvironmentVariable("env") ?? "qa";
        var appConfigFile = $"./Configurations/appsettings.{env}.json";

        builder
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "");

        builder
            .AddJsonFile(appConfigFile, optional: false, reloadOnChange: true);

        var defaultConfig = builder.Build();
        var appConfig = defaultConfig.Get<AppConfig>();

        this._objectContainer.RegisterInstanceAs<AppConfig>(appConfig!);

        var playwright = await Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(appConfig.BrowserTypeLaunchOptions);

        var context = await browser.NewContextAsync();
        /* await context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        this._objectContainer.RegisterInstanceAs<IBrowserContext>(context);
 */
        var page = await context.NewPageAsync();
        page.SetDefaultTimeout(3000);

        this._objectContainer.RegisterInstanceAs<IPlaywright>(playwright);
        this._objectContainer.RegisterInstanceAs<IBrowser>(browser);
        this._objectContainer.RegisterInstanceAs<IPage>(page);        
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
       /*  var context = this._objectContainer.Resolve<IBrowserContext>();
        await context.Tracing.StopAsync(new()
        {
            Path = $"{_tracePath}_{_traceCounter++.ToString().PadLeft(3, '0')}.zip"
        }); */

        var browser = this._objectContainer.Resolve<IBrowser>();
        await browser.CloseAsync();
        var playwright = this._objectContainer.Resolve<IPlaywright>();
        playwright.Dispose();
    }
}
