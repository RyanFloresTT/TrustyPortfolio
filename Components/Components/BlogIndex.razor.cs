using Markdig;
using Markdig.Prism;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogIndex {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
        [Parameter] public string UrlHandle { get; set; }
        public BlogPost? BlogPost { get; set; }

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            BlogPost = PortfolioData.Blogs.FirstOrDefault(x => x.UrlHandle == UrlHandle);
        }

        private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePrism()
            .Build();

        
    }


}

