using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogFilterChips {
        
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        MudChip[] selected;
        public List<Tag> SelectedTags { get {
                return selected == null ? new() : PortfolioData.Tags.Where(tag => selected.Any(chip => chip.Text == tag.Name)).ToList();
            }
        }        
        bool IsAnyBlogMatchingTags => PortfolioData.Blogs.Any(x => x.Visible && SelectedTags.All(tag => x.Tags.Contains(tag)));
    }
}
