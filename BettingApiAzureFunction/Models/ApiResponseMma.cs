using System;
using System.Text.Json.Serialization;

namespace BettingApiAzureFunction.Models
{
    public class ApiResponseMma
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("sport_key")]
        public string? SportKey { get; set; }

        [JsonPropertyName("sport_title")]
        public string? SportTitle { get; set; }

        [JsonPropertyName("commence_time")]
        public DateTime? CommenceTime { get; set; }

        [JsonPropertyName("home_team")]
        public string? HomeTeam { get; set; }

        [JsonPropertyName("away_team")]
        public string? AwayTeam { get; set; }

        [JsonPropertyName("bookmakers")]
        public Bookmaker[]? Bookmakers { get; set; }
    }

    public class Bookmaker
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonPropertyName("markets")]
        public Market[]? Markets { get; set; }
    }

    public class Market
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonPropertyName("outcomes")]
        public Outcome[]? Outcomes { get; set; }
    }

    public class Outcome
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("price")]
        public int? Price { get; set; }
    }
}