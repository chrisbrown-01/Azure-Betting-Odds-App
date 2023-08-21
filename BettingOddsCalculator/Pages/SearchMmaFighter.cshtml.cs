using BettingOddsCalculator.Models;
using BettingOddsCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;

namespace BettingOddsCalculator.Pages
{
    public class SearchMmaFighterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SearchMmaFighterModel> _logger;

        public SearchMmaFighterModel(
                    ILogger<SearchMmaFighterModel> logger,
                    IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty(SupportsGet = true)]
        [Required]
        [DisplayName("Fighter Name")]
        [StringLength(
            maximumLength: 50,
            MinimumLength = 2,
            ErrorMessage = "Input must be between 2 and 50 characters long.")]
        public string FighterName { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        [Range(1, int.MaxValue)]
        [DisplayName("Bet Amount")]
        public int? BetAmount { get; set; }

        public List<MmaMatchup> MmaMatchups { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!ModelState.IsValid) return Page();

            var httpClient = _httpClientFactory.CreateClient("SearchMmaFighter");
            using var response = await httpClient.GetAsync($"MmaBettingOdds?fighterName={FighterName}");

            if (!response.IsSuccessStatusCode) return Page();

            var mmaMatchups = await JsonSerializer.DeserializeAsync<List<MmaMatchup>>(await response.Content.ReadAsStreamAsync());

            if (mmaMatchups != null && mmaMatchups.Count > 0)
            {
                MmaMatchups = mmaMatchups;
            }

            return Page();
        }
    }
}