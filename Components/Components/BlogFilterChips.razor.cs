using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogFilterChips {
        [Parameter] public List<BlogPost> BlogPosts { get; set; }
        [Parameter] public List<Tag> Tags { get; set; }
        MudChip[] selected;
        public List<Tag> SelectedTags { get {
                return selected == null ? new() : Tags.Where(tag => selected.Any(chip => chip.Text == tag.Name)).ToList();
            }
        }
        bool IsAnyBlogMatchingTags => BlogPosts.Any(x => x.Visible && SelectedTags.All(tag => x.Tags.Contains(tag)));
    }
}
