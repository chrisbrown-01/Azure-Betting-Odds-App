using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BettingOddsCalculator.Models
{
    public class WinningsAndPayout
    {
        public double TotalPayout { get; set; }

        public double Winnings { get; set; }
    }
}