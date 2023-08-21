using BettingOddsCalculator.Models;
using BettingOddsCalculator.Pages;

namespace BettingOddsCalculator.Services
{
    public class Calculations : ICalculations
    {
        private readonly ILogger<Calculations> _logger;

        public Calculations(ILogger<Calculations> logger)
        {
            _logger = logger;
        }

        public WinningsAndPayout? CalculateWinningsAndPayout(int americanOdds, int betAmount)
        {
            _logger.LogInformation("Calculating winnings and payouts.");

            double _americanOdds = (double)americanOdds;
            double _betAmount = (double)betAmount;
            double _winnings;
            double _totalPayout;

            if (_americanOdds >= 100)
            {
                // Winnings = (Amount Bet* Odds) / 100
                _winnings = (_betAmount * _americanOdds) / 100;
                _totalPayout = _winnings + _betAmount;
            }
            else if (_americanOdds <= -100)
            {
                // Winnings = Amount Bet / (Odds / -100)
                _winnings = _betAmount / (_americanOdds / -100);
                _totalPayout = _winnings + _betAmount;
            }
            else
            {
                _logger.LogError(
                    "americanOdds parameter did not meet conditional statement requirement. " +
                    "americanOdds: {americanOdds} " +
                    "betAmount: {betAmount} " +
                    "_americanOdds: {_americanOdds} " +
                    "_betAmount: {_betAmount} ",
                    americanOdds,
                    betAmount,
                    _americanOdds,
                    _betAmount);

                return null;
            }

            return new WinningsAndPayout
            {
                Winnings = _winnings,
                TotalPayout = _totalPayout
            };
        }
    }
}