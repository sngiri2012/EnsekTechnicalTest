using TechTalk.SpecFlow;

namespace EnsekTechnicalTest.Context
{
    public class ContextProperties
    {
        private readonly ScenarioContext _scenarioContext;
        public ContextProperties(ScenarioContext scenariContext)
        {
            _scenarioContext = scenariContext;
        }

        public string EnergyDetails
        {
            get => (string)_scenarioContext[nameof(EnergyDetails)];
            set => _scenarioContext[nameof(EnergyDetails)] = value;
        }

        public string Token
        {
            get => (string)_scenarioContext[nameof(Token)];
            set => _scenarioContext[nameof(Token)] = value;
        }
    }
}
