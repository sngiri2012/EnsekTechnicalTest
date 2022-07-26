using EnsekTechnicalTest.Context;
using EnsekTechnicalTest.DTO;
using EnsekTechnicalTest.DTO.Response;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace EnsekTechnicalTest.Steps
{
    [Binding]
    public sealed class OrderSteps : ContextProperties
    {
        ApiHelper _apiHelper;
        ScenarioContext _scenarioContext;
        string BuyOrderConfirmationRegex = @"^You have purchased (?<Quantity>[0-9]+)(.*) at a cost of (?<UnitCost>[0-9]+\.?[0-9]*|\.[0-9]+) there are (?<UnitsRemaining>.*) units remaining. (.*) is (?<OrderId>.*).$";
        List<BuyOrders> BuyOrdersList = new List<BuyOrders>();

        public OrderSteps(ScenarioContext scenariContext) : base(scenariContext)
        {
            _scenarioContext = scenariContext;
            _apiHelper = new ApiHelper(_scenarioContext);
        }

        [Given(@"the user athenticated with username as '(.*)' and password as '(.*)'")]
        public void GivenTheUserAthenticatedWithUsernameAsAndPasswordAsAsync(string username, string password)
        {
            _apiHelper.CreateToken(username, password);
        }

        [Given(@"the user reset the test data")]
        public void GivenTheUserResetTheTestData()
        {
            //I think reset method is having a bug getting unauthorised error 
            var request = _apiHelper.CreatePostRequest("reset", string.Empty);
            var response = _apiHelper.GetPostReponse(request);
        }

        [When(@"the user purchases '(.*)' units of each fuel type")]
        public void GivenTheUserBuyUnitsOfEachFuelType(int numberOfUnits)
        {
            var availableEnergyTypes = EnergyTypes(_apiHelper.GetResponseContent<EnergyTypesDTO>("energy"));
            foreach (var item in availableEnergyTypes)
            {
                var url = $"buy/{item.Value}/{numberOfUnits}";

                var request = _apiHelper.CreatePutRequest(url);
                var response = _apiHelper.GetPutReponse(request);
                var responseMessage = JsonConvert.DeserializeObject<BuyOrderResponseDTO>(response.Content).Message;
                var match = Regex.Match(responseMessage, BuyOrderConfirmationRegex);
                if (match.Success)
                {
                    var buyOrders = new BuyOrders();
                    buyOrders.EnergyType = item.Key;
                    buyOrders.OrderId = Guid.Parse(match.Groups["OrderId"].Value);
                    buyOrders.Quantity = int.Parse(match.Groups["Quantity"].Value);
                    buyOrders.UnitCost = float.Parse(match.Groups["UnitCost"].Value);
                    buyOrders.UnitsRemaining = int.Parse(match.Groups["UnitsRemaining"].Value);
                    BuyOrdersList.Add(buyOrders);
                }
            }
        }

        [Then(@"the above purchased orders should be in the order list")]
        public void ThenTheAbovePurchasedOrdersShouldBeInTheOrderList()
        {
            var previousOrderList = _apiHelper.GetResponseContent<List<PreviousOrderDetailsDTO>>("orders");

            BuyOrdersList.ForEach(x => previousOrderList.Any(y => y.Id == x.OrderId).Should().BeTrue());
        }

        [Given(@"the user counts and prints the number of orders created before today")]
        public void GivenTheUserCountsAndPrintsTheNumberOfOrdersCreatedBeforeToday()
        {
            var previousOrderList = _apiHelper.GetResponseContent<List<PreviousOrderDetailsDTO>>("orders");
            var count = previousOrderList.Where(x => x.Time < DateTime.Now.AddDays(-1)).Count();
            Console.WriteLine($"Number orders made until yesterday are {count}");
        }

        private Dictionary<string, long> EnergyTypes(EnergyTypesDTO fuelTypes) => new Dictionary<string, long>
            {
            { "electric",fuelTypes.Electric.EnergyId },
            { "gas",fuelTypes.Gas.EnergyId },
            { "Elec",fuelTypes.Nuclear.EnergyId },
            { "oil",fuelTypes.Oil.EnergyId }
            };
    }
}
