
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Models.ViewModels {
    public class HomeViewModel {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}
