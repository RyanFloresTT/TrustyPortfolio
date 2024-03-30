using System.ComponentModel.DataAnnotations;

namespace TrustyPortfolio.Models.ViewModels {
    public class AddTagRequest {
        [Required]
        public string Name { get; set; }
    }
}
