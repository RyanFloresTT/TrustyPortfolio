# TrustyPortfolio

## Project Overview

**TrustyPortfolio** is a full-stack, database-driven portfolio and blog platform built with Blazor and MudBlazor. Designed and engineered entirely by me, it features a robust admin panel, dynamic content management, and a modern, responsive UI. This project demonstrates my ability to architect, implement, and optimize a real-world web application using modern .NET technologies.

---

## Tech Stack

- **Blazor Server** (ASP.NET Core 8)
- **MudBlazor** (UI component library)
- **Entity Framework Core** (PostgreSQL & SQL Server support)
- **Microsoft Identity** (authentication, roles, claims, password hashing)
- **Cloudinary** (image uploads)
- **Markdig** (Markdown rendering with syntax highlighting)
- **NUnit** (unit testing)

---

## Key Features

- **Admin Dashboard**: Secure, role-based admin area for managing blog posts, projects, and tags. Only users with the "Admin" role can access content management features.
    - _[Consider adding a screenshot of the admin dashboard overview, if not already present.]_
- **Content Management**: Create, edit, and delete blog posts, projects, and tags with rich Markdown support and image uploads.
    - _[Screenshots of the blog/project creation/editing forms would be great here.]_
- **Modern UI & Theming**: Responsive design with MudBlazor, including custom themes, dark/light mode toggle (auto-detects system preference), and accessible navigation.
    - _[Showcase dark/light mode toggle and responsive layout in screenshots.]_
- **Advanced Filtering**: Users can filter blogs and projects by tags using interactive chip components. Featured content is highlighted on the homepage.
    - _[Add a screenshot of the tag filtering in action for both blogs and projects.]_
- **Performance Optimization**: All content is loaded and cached on startup, minimizing database queries and providing instant navigation between pages.
- **Security**: Built-in authentication, authorization, and anti-forgery protection. Admin role is strictly enforced for all content management routes.
- **Extensible Architecture**: Clean separation of concerns with repositories, view models, and domain models. Easily extendable for new features.

---

## Screenshots

![Admin Panel](https://res.cloudinary.com/djdtmbpce/image/upload/v1715878280/Screen_Shot_2024-05-16_at_9.51.10_AM_t6ngyp.png)

![Edit Blogs Page](https://res.cloudinary.com/djdtmbpce/image/upload/v1715878575/Screen_Shot_2024-05-16_at_9.56.05_AM_acy0c0.png)

---

## Architecture & Design Decisions

- **Blazor + MudBlazor**: Chosen for rapid development of a modern, interactive UI with strong .NET integration.
- **Caching**: On initial load, all content is fetched and stored in a `PortfolioData` class, reducing database round-trips and improving user experience.
- **Repository Pattern**: All data access is abstracted via repositories, making the codebase maintainable and testable.
- **Role-based Security**: Microsoft Identity is used for robust authentication and authorization, with strict separation between admin and regular users.
- **Markdown & Syntax Highlighting**: Blog content supports Markdown with code highlighting for technical posts.
- **Cloudinary Integration**: Images are uploaded and served via Cloudinary for performance and reliability.

---

## What I Learned / Challenges Overcome

- Implemented secure, role-based admin features from scratch
- Built a fully custom content management system (CMS) with Blazor
- Designed a performant caching strategy for a dynamic site
- Integrated third-party services (Cloudinary, Markdig, MudBlazor)
- Developed a responsive, accessible UI with advanced filtering and theming
- Gained deep experience with .NET, Blazor, and modern web best practices

---

## Future Improvements

- User accounts with favorites, notifications, comments, and profile customization
- Contact form with email integration
- More advanced analytics and admin insights
- Enhanced UI/UX polish and animations
- "What I'm Working On" and in-place editing for admins

---

## Legacy Note

> **Note:** This is no longer my active portfolio site. It has been replaced by a Next.js template, but I keep this project here to showcase my .NET and Blazor skills. I'm proud of the engineering and design work that went into TrustyPortfolio!
