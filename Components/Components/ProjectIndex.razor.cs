using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectIndex {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle { get; set; }
        public Project? Project { get; set; }

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            Project = PortfolioData.Projects.FirstOrDefault(x => x.UrlHandle == UrlHandle);
        }

    }
}
