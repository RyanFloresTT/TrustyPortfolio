namespace TrustyPortfolio.Models.Domain {
    public class BlogPost {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool Visible { get; set; }
        public bool Featured { get; set; }

        public Project Project { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
