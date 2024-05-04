using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Pages.Admin
{
    public partial class DeleteTagConfirmation {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Tag Tag { get; set; } = new Tag();

        private void Cancel() {
            MudDialog.Cancel();
        }

        private void DeleteTag() {
            Snackbar.Add("Tag Deleted", Severity.Success);
            MudDialog.Close(DialogResult.Ok(Tag.Id));
        }

    }
}
