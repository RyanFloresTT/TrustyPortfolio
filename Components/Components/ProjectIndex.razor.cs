using Markdig;
using Markdig.Prism;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectIndex {
        [Inject] IJSRuntime JS { get; set; }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle { get; set; }
        public Project? Project { get; set; }

        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            Project = PortfolioData.Projects.FirstOrDefault(x => x.UrlHandle == UrlHandle);

        }
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            await JS.InvokeVoidAsync("highlightAll");
        }

        string GetBlogUrlHandle(string blogUrlHandle) => $"/Blogs/{blogUrlHandle}";

        private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePrism()
            .Build();

    }
}
