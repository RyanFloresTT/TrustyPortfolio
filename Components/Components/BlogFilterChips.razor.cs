using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogFilterChips {
        enum ColorEnum {
            Secondary,
            Info,
            Success,
            Warning,
            Error
        }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        MudChip[] selected;
        public List<Tag> SelectedTags { get {
                return selected == null ? new() : PortfolioData.Tags.Where(tag => selected.Any(chip => chip.Text == tag.Name)).ToList();
            }
        }
        Color GetChipColor(Guid tagId) {
            byte[] bytes = tagId.ToByteArray();
            int sum = bytes.Sum(b => (int)b);
            int index = sum % Enum.GetNames(typeof(ColorEnum)).Length;
            ColorEnum colorEnum = (ColorEnum)index;

            switch (colorEnum) {
                case ColorEnum.Secondary:
                    return Color.Secondary;
                case ColorEnum.Info:
                    return Color.Info;
                case ColorEnum.Success:
                    return Color.Success;
                case ColorEnum.Warning:
                    return Color.Warning;
                case ColorEnum.Error:
                    return Color.Error;
                default:
                    return Color.Default;
            }
        }
        bool IsAnyBlogMatchingTags => PortfolioData.Blogs.Any(x => x.Visible && SelectedTags.All(tag => x.Tags.Contains(tag)));
    }
}
