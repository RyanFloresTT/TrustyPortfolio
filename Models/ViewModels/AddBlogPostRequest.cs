using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Models.ViewModels {
    public class AddBlogPostRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string FeaturedImageUrl { get; set; }
        [Required]
        public string UrlHandle { get; set; }
        [Required]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        [Required]
        public bool Visible { get; set; }
        [Required]
        public bool Featured { get; set; }

        // Display Projects
        public IEnumerable<SelectListItem> Projects { get; set; }
        // Collect Project
        public string SelectedProjectId { get; set; }

        // Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tags
        public string[] SelectedTags { get; set; } = [];
    }
}
