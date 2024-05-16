# TrustyPortfolio

## Features

### Admin Account
This website uses a minimal API to direct me to a login page and [Microsoft Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio) to manage my roles, claims, password hashing, authenticaion and authorization.
Technically anyone can go and create an account with this website, but there isn't any functionality you can do with it yet. Any new accounts are created with the default role. I added the "Admin" role to my account directly so it's the only one that has admin access.
Through this, I'm given access to extra page options which allow me to add/edit my content.

![Admin Panel](https://res.cloudinary.com/djdtmbpce/image/upload/v1715878280/Screen_Shot_2024-05-16_at_9.51.10_AM_t6ngyp.png)

### Blazor

My creation and edit pages are not stylish in the slightest, but they aren't for users and as long as I know what to do with them then it's fine :P . I will be making adjustments to it in the future though as I start to clean this project up.
Because I'm using [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor), and more specifically [MudBlazor](https://mudblazor.com/docs/), I was able to easily drop in a `DataGrid` component which, after a little adjusting to make it work with my models, works perfectly! I had made some methods in my repositories to allow for filtering and sortng if I wanted to, but this component does it all for me client side which is really nice. I even had a method for filtering by name if I wanted to search as I typed into a searchbox, but again, this component does it all for my client side and it's super awesome!

![Edit Blogs Page](https://res.cloudinary.com/djdtmbpce/image/upload/v1715878575/Screen_Shot_2024-05-16_at_9.56.05_AM_acy0c0.png)

MudBlazor has themes, which makes it super easy to customize what colors I want my site to be. Within these themes are DarkMode and LightMode themes, this made is incredibly easy to make a button to toggle between the two and using Blazor's `OnInitializedAsync()` method, I'm able to retrieve the user's system preference, and automatically set the site to it!

### Blog/Project/Tags

Another really amazing thing from MudBlazor, is that I was able to build another fitlering system for my users with the `MudChips` component. Users can click on these chips and it will automatically filter out the page based on their selection.
I built out Project and Blog cards differently so that I can add things like a project repo button if that project has one. I also have a different card for the featured version of these so that I could modify those in the way that I wanted. I'm not very good at front end development so things might not look the prettiest, but they are functional.

Once the application is loaded, I cache all my content for the site and package it into a class that holds data "PortfolioData" which has fields for each of my database queries. This is to prevent re-querying the database each time a user wants to look at something different which saves on performance for the site and me some money for my databases running.
At first I wasn't doing this and I had some loading bars indicating that the page content was still loading, but I much prefer caching it all because now each page loads instantly. The only downfall to this, which shouldn't be too bad, is that if I publish any content while a user is on my website, they won't see it unless they refresh the page. It isn't that much of a problem, but it's at least something I'm aware of, but I don't think I'm planning on fixing it any time soon.


## Future

This project is one that I will be updating as I learn new things. There are a few bugs and minor styling issue that might need to be fixed but besdies that, in terms of what I want to add for features, I'm thinking of adding accounts for everyone so that they can
- Add posts and projects to a "favorite" list that they can go back to
- If they opt in, receive email notifications when I publish a new blog post or project
- Comment on posts/projects
- Customize their profile picture by choosing a default or uploading one for themselves
- Share posts easily

Besides adding more functionality to accounts, I'd also like to add:
- A Contact Me page where people can submit a message and I'd receive it through email
- Though not a feature, I'd like to give my site some more flavor, it's very minimalistic
- A "What I'm Working On" section
- The ability to open a page to Edit the blog post/project reading it as an admin

If you have suggestions on what I should add let me know! :D
