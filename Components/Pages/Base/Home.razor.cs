using Microsoft.AspNetCore.Components;

namespace TrustyPortfolio.Components.Pages.Base {
    public partial class Home {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        string GetYouTubeURL() => $"https://www.youtube.com/@trustytea3136";

    }
}