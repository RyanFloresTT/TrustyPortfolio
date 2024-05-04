using Markdig;
using Markdig.Prism;
using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectIndex {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle { get; set; }
        public Project? Project { get; set; }

        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            Project = PortfolioData.Projects.FirstOrDefault(x => x.UrlHandle == UrlHandle);

        }

        string GetBlogUrlHandle(string blogUrlHandle) => $"/Blogs/{blogUrlHandle}";

        private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePrism()
            .Build();

    }
}
