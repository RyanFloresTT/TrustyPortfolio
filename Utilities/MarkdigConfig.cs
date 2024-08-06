using Markdig;
using Markdig.Prism;

namespace TrustyPortfolio.Utilities {
    public static class MarkdigConfig {
        public static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
            .UsePrism()
            .UseBootstrap()
            .UseMediaLinks()
            .UseTaskLists()
            .Build();
    }
}
