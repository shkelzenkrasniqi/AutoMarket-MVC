AutoMarket is a web application built with ASP.NET MVC for managing car and motorcycle listings. It provides an intuitive interface for users to browse, filter, and manage vehicles.

Table of Contents
-Features
-Technologies
-Setup
-Usage
-Screenshots

*Features
-User authentication and authorization
-Admin dashboard for managing car and motorcycle brands and models
-Filtering and sorting vehicles based on various criteria
-Detailed view for each vehicle
-Responsive design using Bootstrap

*Technologies
-ASP.NET MVC
-Entity Framework Core
-FluentValidation
-Bootstrap 4
-jQuery

*Setup
Clone the repository:
git clone https://github.com/shkelzenkrasniqi/AutoMarket-MVC.git
Navigate to the project directory:

cd AutoMarket
Restore the NuGet packages:

dotnet restore
Update the database connection string in appsettings.json

*Screenshots
![Screenshot 2024-07-19 170945](https://github.com/user-attachments/assets/8f765aa5-bb3d-4e0f-985b-374ca9125bf7)
![Screenshot 2024-07-19 171044](https://github.com/user-attachments/assets/a8dfa256-e574-46c7-90a0-7a06df480e9e)
![Screenshot 2024-07-19 171143](https://github.com/user-attachments/assets/da98628b-bd57-454a-89a7-d0743c50c7ea)
![Screenshot 2024-07-19 171226](https://github.com/user-attachments/assets/26eb7aeb-bd25-42bd-b8cf-ed613466527e)


*Usage
  User Roles
Admin: Can manage car and motorcycle brands and models.
User: Can post, browse and filter vehicle listings.

Filtering Vehicles
Users can filter vehicles based on various criteria such as brand, model, fuel type, color, mileage, seats, transmission type, and price range.

Admin Dashboard
Admins can access the dashboard to manage car and motorcycle brands and models, including adding new entries and editing or deleting existing ones.
