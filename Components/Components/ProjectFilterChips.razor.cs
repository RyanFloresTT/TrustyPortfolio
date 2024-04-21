using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectFilterChips {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        MudChip[] selected;
        public List<Tag> SelectedTags { get {
                return selected == null ? new() : PortfolioData.Tags.Where(tag => selected.Any(chip => chip.Text == tag.Name)).ToList();
            }
        }
        bool IsAnyProjectMatchingTags => PortfolioData.Projects.Any(x => x.Visible && SelectedTags.All(tag => x.Tags.Contains(tag)));
    }
}
