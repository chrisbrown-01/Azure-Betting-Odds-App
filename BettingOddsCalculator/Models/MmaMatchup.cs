using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BettingOddsCalculator.Models
{
    public class MmaMatchup
    {
        [JsonPropertyName("fighterName")]
        [DisplayName("Fighter Name")]
        public string? FighterName { get; set; }

        [JsonPropertyName("fighterOdds")]
        [DisplayName("Fighter Odds")]
        public int? FighterOdds { get; set; }

        [JsonPropertyName("opponentName")]
        [DisplayName("Opponent Name")]
        public string? OpponentName { get; set; }

        [JsonPropertyName("opponentOdds")]
        [DisplayName("Opponent Odds")]
        public int? OpponentOdds { get; set; }

        [JsonPropertyName("bookmaker")]
        [DisplayName("Bookmaker")]
        public string? Bookmaker { get; set; }
    }
}