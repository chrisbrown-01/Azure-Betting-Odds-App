using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BettingOddsCalculator.Pages
{
    public class AboutModel : PageModel
    {
        // TODO: copy paste github readme to about page
        private readonly ILogger<AboutModel> _logger;

        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("About page GET request received.");
        }
    }
}