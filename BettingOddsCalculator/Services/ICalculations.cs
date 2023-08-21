using BettingOddsCalculator.Models;

namespace BettingOddsCalculator.Services
{
    public interface ICalculations
    {
        WinningsAndPayout? CalculateWinningsAndPayout(int americanOdds, int betAmount);
    }
}