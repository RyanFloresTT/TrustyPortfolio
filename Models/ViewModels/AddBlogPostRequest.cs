﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrustyPortfolio.Models.ViewModels {
    public class AddBlogPostRequest {
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tags
        public string[] SelectedTags { get; set; } = [];
    }
}
