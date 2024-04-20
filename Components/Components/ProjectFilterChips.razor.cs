using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectFilterChips {
        [Parameter] public List<Project> Projects { get; set; }
        [Parameter] public List<Tag> Tags { get; set; }
        MudChip[] selected;
        public List<Tag> SelectedTags { get {
                return selected == null ? new() : Tags.Where(tag => selected.Any(chip => chip.Text == tag.Name)).ToList();
            }
        }
        bool IsAnyProjectMatchingTags => Projects.Any(x => x.Visible && SelectedTags.All(tag => x.Tags.Contains(tag)));
    }
}
