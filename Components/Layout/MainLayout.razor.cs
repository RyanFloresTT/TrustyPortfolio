using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using TrustyPortfolio.Models.Domain;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Layout {
    public partial class MainLayout {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IBlogRepository BlogRepository { get; set; }
        [Inject] IProjectRepository ProjectRepository { get; set; }
        [Inject] ITagRepository TagRepository { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] UserManager<ApplicationUser> UserManager { get; set; }
        [CascadingParameter] HttpContext HttpContext { get; set; } = default!;
        ApplicationUser User;

        PortfolioData PortfolioData { get; set; } = new();
        public bool IsDarkMode { get; set; } = true;
        readonly string[] routes = ["/", "blogs", "projects", "about", "resume"];

        string ToggledColor {
            get {
                return IsDarkMode ? $"color:{Colors.Blue.Lighten2};" : $"color:{Colors.Orange.Darken1};";
            }
        }

        bool _drawerOpen = false;
        MudThemeProvider mudThemeProvider;
        string? currentUrl;

        readonly MudTheme MyCustomTheme = new() {
            Typography = new() {
                Default = new() {
                    FontFamily = ["Ubuntu"]
                }
            },
            
        };
        protected override async Task OnInitializedAsync() {
            try {
                await FetchDataAsync();
            }
            catch (TaskCanceledException ex) {
                Console.WriteLine(ex.ToString());
            }
            var authstate = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authstate != null) {
                User = await UserManager.GetUserAsync(authstate.User);
            }
        }
        async Task FetchDataAsync() {
            var blogResult = await BlogRepository.GetAllAsync("", "Publish Date", "Desc");
            PortfolioData.Blogs = blogResult.ToList();
            blogResult = await BlogRepository.GetFeaturedAsync();
            PortfolioData.FeaturedBlogs = blogResult.ToList();

            var projectResult = await ProjectRepository.GetAllAsync("", "Publish Date", "Desc");
            PortfolioData.Projects = projectResult.ToList();
            projectResult = await ProjectRepository.GetFeaturedAsync();
            PortfolioData.FeaturedProjects = projectResult.ToList();

            var tagResult = await TagRepository.GetAllAsync();
            PortfolioData.Tags = tagResult.ToList();
        }
        protected override void OnInitialized() {
            currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            NavigationManager.LocationChanged += OnLocationChanged;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (firstRender) {
                IsDarkMode = await mudThemeProvider.GetSystemPreference();
                await mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
                StateHasChanged();
            }
        }
        Task OnSystemPreferenceChanged(bool newValue) {
            //IsDarkMode = newValue;
            StateHasChanged();
            return Task.CompletedTask;
        }
        void DrawerToggle() {
            _drawerOpen = !_drawerOpen;
        }
        void OnLocationChanged(object? sender, LocationChangedEventArgs e) {
            currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
            StateHasChanged();
        }
        void Handle_ActivePanelIndexChanged(int index) {
            NavigationManager.NavigateTo($"{routes[index]}");
        }
    }
}

public class PortfolioData {
    public List<BlogPost> Blogs { get; set; } = new();
    public List<BlogPost> FeaturedBlogs { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    public List<Project> FeaturedProjects { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}
