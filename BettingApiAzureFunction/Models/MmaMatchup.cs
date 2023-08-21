using System.ComponentModel;

namespace BettingOddsCalculator.Models
{
    public class MmaMatchup
    {
        [DisplayName("Fighter Name")]
        public string? FighterName { get; set; }

        [DisplayName("Fighter Odds")]
        public int? FighterOdds { get; set; }

        [DisplayName("Opponent Name")]
        public string? OpponentName { get; set; }

        [DisplayName("Opponent Odds")]
        public int? OpponentOdds { get; set; }

        [DisplayName("Bookmaker")]
        public string? Bookmaker { get; set; }
    }
}