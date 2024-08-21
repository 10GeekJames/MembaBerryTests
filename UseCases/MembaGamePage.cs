namespace OdinReactMemoryGameTests.UseCases;
public class MembaGamePage
{
    private readonly IPage _page;
    private readonly AppConfig _appConfig;

    private const string _allMembaSelector = "id=memCard";
    private const string _currentScoreSelector = "div[id=score]";

    private ILocator AllMembasLocator => _page.Locator(_allMembaSelector);
    private ILocator CurrentScoreLocator => _page.Locator(_currentScoreSelector);
    private IList<string> _membaNames = new List<string>();    
    public MembaGamePage(IPage page, AppConfig appConfig)
    {
        _page = page;
        _appConfig = appConfig;
    }

    public async Task<bool> IsOnPageAsync () => await _page.TitleAsync() == "Memba Chewbacca?";

    public async Task NavigateToAsync() => await _page.GotoAsync(_appConfig.TestUrl);    

    public void ClickMemba(int index)
    {
        var allMembas = AllMembasLocator.AllAsync().Result;
        var memba = allMembas.ToArray()[index];
        var membaClickable = memba.Locator("div[id=cardTitle]");
        membaClickable.ClickAsync().GetAwaiter().GetResult();        
    }

    public IEnumerable<string> GetAllMembaNames() {
        if(_membaNames.Count() == 0) {
            var allMembas = AllMembasLocator.AllAsync().Result;
            _membaNames = allMembas.Select(memba => memba.Locator("div[id=cardTitle]").InnerTextAsync().Result).ToList();
        }      
        return _membaNames;   
    }
    
    public int CurrentScore() 
    {
        var currentScoreString = CurrentScoreLocator.InnerTextAsync().Result;
        return int.Parse(currentScoreString.Split(":")[1]);
    }

    public void ClickMembaByName(string name) {
        var allMembas = AllMembasLocator.AllAsync().Result;
        foreach(var member in allMembas) {
            Console.WriteLine($"memba-text: {member.Locator("div[id=cardTitle]").InnerTextAsync().Result.Trim()} == {name.Trim()}");
        }
        var namedMemba = allMembas.Single(memba => memba.Locator("div[id=cardTitle]").InnerTextAsync().Result.Trim() == name.Trim());
        var clickableMemba = namedMemba.Locator("div[id=cardTitle]");        
        clickableMemba.ClickAsync().GetAwaiter().GetResult();
    }
}
