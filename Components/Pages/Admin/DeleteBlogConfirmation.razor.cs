using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Pages.Admin
{
    public partial class DeleteBlogConfirmation {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public BlogPost BlogPost { get; set; } = new BlogPost();

        private void Cancel() {
            MudDialog.Cancel();
        }

        private void DeleteBlog() {
            Snackbar.Add("Blog Deleted", Severity.Success);
            MudDialog.Close(DialogResult.Ok(BlogPost.Id));
        }

    }
}
