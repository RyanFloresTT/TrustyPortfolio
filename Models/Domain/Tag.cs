﻿namespace TrustyPortfolio.Models.Domain {
    public class Tag {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
