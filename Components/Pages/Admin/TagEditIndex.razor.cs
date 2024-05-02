using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class TagEditIndex {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ITagRepository TagRepository { get; set; }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public Guid Id {  get; set; }
        public Tag? Tag { get; set; }

        protected override async Task OnInitializedAsync() {
            Tag = PortfolioData.Tags.FirstOrDefault(x => x.Id == Id);
            await base.OnInitializedAsync();
        }
        private async Task HandleSubmit() {
            try {
                await TagRepository.UpdateAsync(Tag);
                NavigationManager.NavigateTo("/Admin/Projects/Edit");
            }
            catch (Exception ex) {
                // Handle any errors during update (show error message)
            }
        }

    }
}
