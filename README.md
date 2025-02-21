# Youth Innovation System

## Description
The **Youth Innovation System** is a platform designed to bridge the gap between innovators and investors. Users with innovative ideas can create posts to seek funding, while investors can explore, engage, and support projects of interest.

## Features
- **User Authentication & Authorization**: Secure authentication using JWT and role-based authorization for Admins, Users, and Investors.
- **Multi-Role Support**: Different functionalities tailored for Users, Investors, and Admins.
- **Real-Time Messaging**: Integrated SignalR for instant communication.
- **Interactive Features**: Users can like and comment on posts to enhance engagement.
- **Optimized Database Schema**: Designed a relational database in SQL Server for efficient data storage and retrieval.
- **RESTful API**: Adheres to RESTful principles to ensure a consistent, scalable, and easy-to-use API.
- **Onion Architecture**: Ensures better separation of concerns and maintainability.
- **Repository Pattern**: Facilitates efficient data access and management.

## Technologies Used
- **Backend**: C#, ASP.NET Core API, Entity Framework Core
- **Database**: SQL Server
- **Authentication**: JWT Authentication
- **Real-Time Communication**: SignalR
- **API Testing**: Postman
- **ORM & Querying**: LINQ, Entity Framework Core

## Installation
1. **Clone the repository**
   ```sh
   git clone https://github.com/your-username/youth-innovation-system.git
   cd youth-innovation-system
   ```
2. **Set up the database**
   - Ensure you have SQL Server installed and running.
   - Update `appsettings.json` with your database connection string.
   - Run migrations:
     ```sh
     dotnet ef database update
     ```
3. **Run the application**
   ```sh
   dotnet run
   ```

## API Documentation
Use **Postman** or any API testing tool to test the available endpoints.

## Contributing
Contributions are welcome! Feel free to fork the repo and submit a pull request.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact
For any inquiries, reach out via GitHub Issues or email me at `michealghobriall@gmail.com`.
