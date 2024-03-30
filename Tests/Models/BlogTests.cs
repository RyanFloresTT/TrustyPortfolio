using NUnit.Framework;
using TrustyPortfolio.Models.Domain;

namespace YourNamespace.Tests.Models {
    [TestFixture]
    public class BlogTests {
        [Test]
        public void Blog_Should_SetPropertiesCorrectly() {
            // Arrange
            BlogPost blog = new (){ 
                Title = "Title",
                Heading = "Heading",
                Content = "This is a test blog post.",
                Description = "This is a description of a test blog post.",
                FeaturedImageUrl = "https://www.google.com",
                UrlHandle = "title",
                PublishDate = DateTime.Today,
                Visible = true,
                Featured = false
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
        }
    }
}
