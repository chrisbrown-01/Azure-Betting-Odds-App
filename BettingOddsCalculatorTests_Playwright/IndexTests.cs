using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingOddsCalculatorTests_Playwright
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class IndexTests : PageTest
    {
        private const string URL = "https://betting-odds-app.azurewebsites.net/";

        [Test]
        public async Task TestTitle()
        {
            await Page.GotoAsync(URL);
            await Expect(Page).ToHaveTitleAsync("Home - Betting Odds Calculator");
        }

        [Test]
        public async Task TestHeading()
        {
            await Page.GotoAsync(URL);
            var heading = await Page.TextContentAsync("h1");
            Assert.That(heading, Is.EqualTo("Betting Odds Calculator"));
        }

        [Test]
        public async Task TestFormSubmission()
        {
            await Page.GotoAsync(URL);
            await Page.FillAsync("#AmericanOdds", "100");
            await Page.FillAsync("#BetAmount", "100");
            await Page.ClickAsync("input[type=submit]");
            var winnings = await Page.TextContentAsync("p:has-text(\"Winnings:\")");
            var totalPayout = await Page.TextContentAsync("p:has-text(\"Total Payout:\")");
            Assert.That(winnings, Is.EqualTo("Winnings: $100.00"));
            Assert.That(totalPayout, Is.EqualTo("Total Payout: $200.00"));
        }

        [Test]
        public async Task TestFormValidation()
        {
            await Page.GotoAsync(URL);
            await Page.FillAsync("#AmericanOdds", "");
            await Page.FillAsync("#BetAmount", "");
            await Page.ClickAsync("input[type=submit]");
            var americanOddsValidation = await Page.TextContentAsync("span[data-valmsg-for=\"AmericanOdds\"]");
            var betAmountValidation = await Page.TextContentAsync("span[data-valmsg-for=\"BetAmount\"]");
            Assert.That(americanOddsValidation, Is.EqualTo("The value '' is invalid."));
            Assert.That(betAmountValidation, Is.EqualTo("The value '' is invalid."));
        }

        [Test]
        public async Task TestNegativeOdds()
        {
            await Page.GotoAsync(URL);
            await Page.FillAsync("#AmericanOdds", "-200");
            await Page.FillAsync("#BetAmount", "100");
            await Page.ClickAsync("input[type=submit]");
            var winnings = await Page.TextContentAsync("p:has-text(\"Winnings:\")");
            var totalPayout = await Page.TextContentAsync("p:has-text(\"Total Payout:\")");
            Assert.That(winnings, Is.EqualTo("Winnings: $50.00"));
            Assert.That(totalPayout, Is.EqualTo("Total Payout: $150.00"));
        }
    }
}