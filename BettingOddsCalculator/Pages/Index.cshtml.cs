using BettingOddsCalculator.Services;
using BettingOddsCalculator.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BettingOddsCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICalculations _calculations;

        public IndexModel(
            ILogger<IndexModel> logger,
            ICalculations calculations)
        {
            _logger = logger;
            _calculations = calculations;
        }

        [BindProperty(SupportsGet = true)]
        [Required]
        [Range(1, int.MaxValue)]
        [DisplayName("Bet Amount")]
        public int BetAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        [AmericanOddsRange]
        [DisplayName("American Odds")]
        public int AmericanOdds { get; set; }

        [BindProperty]
        [DisplayName("Total Payout")]
        public double TotalPayout { get; set; }

        [BindProperty]
        [DisplayName("Winnings")]
        public double Winnings { get; set; }

        public IActionResult OnGet()
        {
            _logger.LogInformation("Index page GET request received.");

            if (!ModelState.IsValid) return Page();

            var winningsAndPayout = _calculations.CalculateWinningsAndPayout(AmericanOdds, BetAmount);

            if (winningsAndPayout != null)
            {
                TotalPayout = winningsAndPayout.TotalPayout;
                Winnings = winningsAndPayout.Winnings;
            }

            return Page();
        }
    }
}