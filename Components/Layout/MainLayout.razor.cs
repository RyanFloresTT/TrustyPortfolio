using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace TrustyPortfolio.Components.Layout {
    public partial class MainLayout {
        [Inject] NavigationManager NavigationManager { get; set; }

        public bool IsDarkMode { get; set; }

        bool _drawerOpen = false;
        MudThemeProvider mudThemeProvider;
        string? currentUrl;

        protected override void OnInitialized() {
            currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            NavigationManager.LocationChanged += OnLocationChanged;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (firstRender) {
                IsDarkMode = await mudThemeProvider.GetSystemPreference();
                StateHasChanged();
            }
        }
        public void Dispose() {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        void DrawerToggle() {
            _drawerOpen = !_drawerOpen;
        }
        void OnLocationChanged(object? sender, LocationChangedEventArgs e) {
            currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
            StateHasChanged();
        }
        void Login() {
            NavigationManager.NavigateTo("/Account/Login");
        }
    }
}
