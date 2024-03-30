using NUnit.Framework;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Tests.Models {
    [TestFixture]
    public class ModelTests {
        [Test]
        public void Blog_Should_SetPropertiesCorrectly() {
            // Arrange
            BlogPost blog = new() {
                Title = "Title",
                Heading = "Heading",
                Content = "This is a test blog post.",
                Description = "This is a description of a test blog post.",
                FeaturedImageUrl = "https://www.google.com",
                UrlHandle = "title",
                PublishDate = DateTime.Today,
                Visible = true,
                Featured = false,
                Tags = { 
                    new Tag(){
                        Name = "NameTag",
                        DisplayName = "DisplayNameTag"
                    } 
                }
            };

            // Assert
            Assert.That(string.Equals("Test Blog", blog.Title));
            Assert.That(string.Equals("Heading", blog.Heading));
            Assert.That(string.Equals("This is a test blog post.", blog.Content));
            Assert.That(string.Equals("This is a description of a test blog post.", blog.Description));
            Assert.That(string.Equals("https://www.google.com", blog.FeaturedImageUrl));
            Assert.That(string.Equals("title", blog.UrlHandle));
            Assert.That(blog.PublishDate == DateTime.Today);
            Assert.That(blog.Visible);
            Assert.That(!blog.Featured);
            Assert.That(blog.Tags.Count == 1 && blog.Tags.First() == new Tag() {
                Name = "NameTag",
                DisplayName = "DisplayNameTag"
            });
        }
        [Test]
        public void Project_Should_SetPropertiesCorrectly() {
            // Arrange
            Project project = new() {
                Title = "Title",
                Heading = "Heading",
                Content = "This is a test blog post.",
                Description = "This is a description of a test blog post.",
                FeaturedImageUrl = "https://www.google.com",
                UrlHandle = "title",
                ProjectUrl = "https://www.github.com",
                PublishDate = DateTime.Today,
                Visible = true,
                Featured = false,
                Tags = {
                    new Tag(){
                        Name = "NameTag",
                        DisplayName = "DisplayNameTag"
                    }
                },
                Blogs = {
                    new BlogPost() { }
                }
            };

            // Assert
            Assert.That(string.Equals("Test Blog", project.Title));
            Assert.That(string.Equals("Heading", project.Heading));
            Assert.That(string.Equals("This is a test blog post.", project.Content));
            Assert.That(string.Equals("This is a description of a test blog post.", project.Description));
            Assert.That(string.Equals("https://www.google.com", project.FeaturedImageUrl));
            Assert.That(string.Equals("title", project.UrlHandle));
            Assert.That(string.Equals("https://www.github.com", project.ProjectUrl));
            Assert.That(project.PublishDate == DateTime.Today);
            Assert.That(project.Visible);
            Assert.That(!project.Featured);
            Assert.That(project.Tags.Count == 1 && project.Tags.First() == new Tag() {
                Name = "NameTag",
                DisplayName = "DisplayNameTag"
            });
            Assert.That(project.Tags.Count == 1);
        }
        [Test]
        public void Tag_Should_SetPropertiesCorrectly() {
            // Arrange
            Tag tag = new() {
                Name = "NameTag",
                DisplayName = "DisplayNameTag"
            };

            // Assert
            Assert.That(string.Equals("Test Blog", tag.Name));
            Assert.That(string.Equals("Heading", tag.DisplayName));
        }
    }
}
