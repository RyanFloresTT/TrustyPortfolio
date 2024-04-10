using MudBlazor;

namespace TrustyPortfolio.Components.Layout {
    public partial class MainLayout {
        public bool IsDarkMode { get; set; }
        MudThemeProvider mudThemeProvider;

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (firstRender) {
                IsDarkMode = await mudThemeProvider.GetSystemPreference();
                StateHasChanged();
            }
        }

    }
}
