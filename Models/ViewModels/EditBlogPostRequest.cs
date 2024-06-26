﻿
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrustyPortfolio.Models.ViewModels {
    public class EditBlogPostRequest {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool Visible { get; set; }
        public bool Featured { get; set; }

        // Display Projects
        public IEnumerable<SelectListItem> Projects {  get; set; }
        // Collect Project
        public string? SelectedProjectId {  get; set; }

        // Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tags
        public string[] SelectedTags { get; set; } = [];
    }
}
