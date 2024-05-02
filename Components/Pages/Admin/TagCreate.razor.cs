using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Pages.Admin {
    public partial class TagCreate {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ITagRepository TagRepository { get; set; }
        public Tag? Tag { get; set; }

        private async Task HandleSubmit() {
            try {
                await TagRepository.AddAsync(Tag);
                NavigationManager.NavigateTo("/Admin/Tags/Edit");
            }
            catch (Exception ex) {
                // Handle any errors during update (show error message)
            }
        }

    }
}
