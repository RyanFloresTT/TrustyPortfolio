using Microsoft.AspNetCore.Components;
using MudBlazor;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Pages.Admin
{
    public partial class DeleteProjectConfirmation {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Project Project { get; set; } = new Project();

        private void Cancel() {
            MudDialog.Cancel();
        }

        private void DeleteProject() {
            Snackbar.Add("Project Deleted", Severity.Success);
            MudDialog.Close(DialogResult.Ok(Project.Id));
        }

    }
}
