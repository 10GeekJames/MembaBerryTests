using OdinReactMemoryGameTests.UseCases;

namespace OdinReactMemoryGameTests.Steps
{
    [Binding]
    public class MembaGameSteps
    {
        private readonly AppConfig _appConfig;
        private readonly MembaGamePage _membaGamePage;

        public MembaGameSteps(AppConfig appConfig, MembaGamePage membaGamePage)
        {
            _appConfig = appConfig;
            _membaGamePage = membaGamePage;
        }

        [StepDefinition(@"I am on the Memba game page")]
        public async Task GivenIAmOnTheMembaGamePage()
        {
            await _membaGamePage.NavigateToAsync();
            (await _membaGamePage.IsOnPageAsync()).Should().Be(true);
        }
        [StepDefinition(@"I click the (.*) memba")]
        public void WhenIClickTheMemba(int index)
        {
            _membaGamePage.ClickMemba(index);
        }

        [StepDefinition(@"I should see a score of (.*)")]
        public void ThenIShouldAScoreOf(int score)
        {
            var currentScore = _membaGamePage.CurrentScore();
            currentScore.Should().Be(score);
        }

        [StepDefinition(@"I click each unique memba")]
        public void IClickEachUniqueMemba()
        {
            var allNames = _membaGamePage.GetAllMembaNames();
            var score = 0;
            foreach (var name in allNames)
            {
                score++;
                Console.WriteLine($"name {name} score {score} of allNames.Count {allNames.Count()}");
                _membaGamePage.ClickMembaByName(name);
                var currentScore = _membaGamePage.CurrentScore();
                currentScore.Should().Be(score);
            }
            Console.WriteLine($"final score {score}, final count: {allNames.Count()}");
        }

        [StepDefinition(@"I pause for (.*) seconds")]
        public void IPauseForSeconds(int pause)
        {
            Task.Delay(pause * 1000).GetAwaiter().GetResult();
        }
    }
}